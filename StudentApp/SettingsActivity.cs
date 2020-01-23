using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace StudentApp
{
    [Activity(Label = "SettingsActivity")]
   
    public class SettingsActivity : Activity
    {
        EditText txtname, txtcontact, txtemail, txtaddress, txtusername, txtpassword;
        Button btnsave;
        ImageView imageView1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settings);
            txtname = FindViewById<EditText>(Resource.Id.txtname);
            txtcontact = FindViewById<EditText>(Resource.Id.txtcontact);
            txtemail = FindViewById<EditText>(Resource.Id.txtemail);
            txtaddress = FindViewById<EditText>(Resource.Id.txtaddress);
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            txtpassword = FindViewById<EditText>(Resource.Id.txtpassword);
            txtpassword = FindViewById<EditText>(Resource.Id.txtpassword);
            btnsave = FindViewById<Button>(Resource.Id.btnsave);
            infosettings();
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };
            btnsave.Click += delegate
             {
                 update();
             };
        }
        public override void OnBackPressed()
        {
            return;
        }
        public void infosettings()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/userinfo.php?userID=" + userID);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    txtname.Text = jsonData[0].fullName;
                    txtaddress.Text = jsonData[0].address;
                    txtcontact.Text = jsonData[0].number;
                    txtemail.Text = jsonData[0].email;
                    txtusername.Text = jsonData[0].username;
                }
            }
            catch (Exception i)
            {
                Toast.MakeText(this, i.Message, ToastLength.Long).Show();
            }
        }

        public void update()
        {
            if(txtpassword.Text != "")
            {
                try
                {
                    string password = Library.EasyMD5.Hash(txtpassword.Text);
                 
                    using (var client = new WebClient())
                    {

                        var values = new NameValueCollection();
                        values["fullName"] = txtname.Text;
                        values["email"] = txtemail.Text;
                        values["number"] = txtcontact.Text;
                        values["address"] = txtaddress.Text;
                        values["username"] = txtusername.Text;
                        values["password"] = password;
                        values["userID"] = Library.ControlID.userID;

                        var response = client.UploadValues("https://joremtongwebsite.000webhostapp.com/updateuserinfo.php", values);
                        var responseString = Encoding.Default.GetString(response);
                    }
                    Toast.MakeText(this, "UPDATED", ToastLength.Long).Show();
                }
                catch (Exception i)
                {
                    Toast.MakeText(this, i.Message, ToastLength.Long).Show();
                }
            }
            else
            {
                try
                {
                   

                    using (var client = new WebClient())
                    {

                        var values = new NameValueCollection();
                        values["fullName"] = txtname.Text;
                        values["email"] = txtemail.Text;
                        values["number"] = txtcontact.Text;
                        values["address"] = txtaddress.Text;
                        values["username"] = txtusername.Text;
                        values["userID"] = Library.ControlID.userID;

                        var response = client.UploadValues("https://joremtongwebsite.000webhostapp.com/updateinfopassword.php", values);
                        var responseString = Encoding.Default.GetString(response);
                    }
                    Toast.MakeText(this, "UPDATED", ToastLength.Long).Show();
                }
                catch (Exception i)
                {
                    Toast.MakeText(this, i.Message, ToastLength.Long).Show();
                }
            }
        }
    }
}