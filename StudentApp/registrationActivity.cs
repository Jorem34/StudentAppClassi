using Android.App;
using Android.OS;
using Android.Widget;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Text;
using System.Security.Policy;
using Android.Content;
using Acr.UserDialogs;

namespace StudentApp
{
    [Activity(Label = "registrationActivity")]
    public class registrationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            EditText txtFnameReg, txtEmailReg, txtCntctNmbrReg, txtAddressReg, txtUnameReg, txtPwrdReg;
            Button btnSaveReg;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registration);
            // Create your application here
            txtFnameReg = FindViewById<EditText>(Resource.Id.txtFnameReg);
            txtEmailReg = FindViewById<EditText>(Resource.Id.txtEmailReg);
            txtCntctNmbrReg = FindViewById<EditText>(Resource.Id.txtCntctNmbrReg);
            txtAddressReg = FindViewById<EditText>(Resource.Id.txtAddressReg);
            txtUnameReg = FindViewById<EditText>(Resource.Id.txtUnameReg);
            txtPwrdReg = FindViewById<EditText>(Resource.Id.txtPwrdReg);
            btnSaveReg = FindViewById<Button>(Resource.Id.btnSaveReg);
            btnSaveReg.Click += async delegate
             {
                 UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                 await Task.Delay(1000);

                 string password = Library.EasyMD5.Hash(txtPwrdReg.Text);
                 try
                 {
                     if (txtUnameReg.Text != "" && txtPwrdReg.Text != "")
                     {
                         using (var client = new WebClient())
                         {
                             var values = new NameValueCollection();
                             values["fullName"] = txtFnameReg.Text;
                             values["email"] = txtEmailReg.Text;
                             values["number"] = txtCntctNmbrReg.Text;
                             values["address"] = txtAddressReg.Text;
                             values["username"] = txtUnameReg.Text;
                             values["password"] = password;
                             var response = client.UploadValues("https://joremtongwebsite.000webhostapp.com/studentRegister.php", values);
                             var responseString = Encoding.Default.GetString(response);
                         }
                         Intent activity = new Intent(this, typeof(MainActivity));
                         StartActivity(activity);
                         Toast.MakeText(this, "Registered", ToastLength.Long).Show();
                     }
                 }
                 catch (Exception i)
                 {
                     Toast.MakeText(this, i.Message, ToastLength.Long).Show();
                 }

                 UserDialogs.Instance.HideLoading();
             };
                
        }
    }
}