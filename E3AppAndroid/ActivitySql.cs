using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3AppAndroid
{
    [Activity(Label = "@string/app_name")]
    public class ActivitySql : Activity
    {
        EditText? txtNombre, txtDomicilio, txtCorreo, txtEdad, txtSaldo, txtID;
        Button? btnAlmacenar, btnConsultar;
        string? Nombre, Domicilio, Correo;
        int Edad, ID;
        double Saldo;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistasql);

            txtID = FindViewById<EditText>(Resource.Id.txtid);
            txtNombre = FindViewById<EditText>(Resource.Id.txtnombre);
            txtDomicilio = FindViewById<EditText>(Resource.Id.txtdomicilio);
            txtCorreo = FindViewById<EditText>(Resource.Id.txtcorreo);
            txtEdad = FindViewById<EditText>(Resource.Id.txtedad);
            txtSaldo = FindViewById<EditText>(Resource.Id.txtsaldo);
            btnAlmacenar = FindViewById<Button>(Resource.Id.btnguardarSQLServer);
            btnConsultar = FindViewById<Button>(Resource.Id.btnBuscarSQLServer);


            txtNombre.Text = Intent.GetStringExtra("Usuario");

            btnAlmacenar.Click += delegate
            {
                try
                {
                    var csql = new ClaseSQLite();
                    csql.nombre = txtNombre.Text;
                    csql.domicilio = txtDomicilio.Text;
                    csql.correo = txtCorreo.Text;
                    csql.edad = int.Parse(txtEdad.Text);
                    csql.saldo = double.Parse(txtSaldo.Text);
                    csql.ConexionBase();
                    if ((csql.IngresarDatos
                    (csql.nombre, csql.domicilio, 
                    csql.correo, csql.edad, csql.saldo)) == true)
                    {
                        Toast.MakeText(this, 
                            "Guardado Correctamente en SQLite", 
                            ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, 
                            "No guardado", ToastLength.Long).Show();
                    }
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };
            btnConsultar.Click += delegate
            {
                try
                {
                    var csql = new ClaseSQLite();
                    csql.ID = int.Parse(txtID.Text);
                    csql.Buscar(csql.ID);
                    txtNombre.Text = csql.nombre;
                    txtDomicilio.Text = csql.domicilio;
                    txtCorreo.Text = csql.correo;
                    txtEdad.Text = csql.edad.ToString();
                    txtSaldo.Text = csql.saldo.ToString();
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

        }
    }
}
