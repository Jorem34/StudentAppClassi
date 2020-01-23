using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Acr.UserDialogs;
using System.Net;
using Android.Content;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;

namespace StudentApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.Design.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtusername, txtpassword;
        Button btnlogin;
        ImageView imageView1;
        TextView lblregister;
        string fileName;
        public object CrossFileData { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            UserDialogs.Init(this);
            lblregister = FindViewById<TextView>(Resource.Id.lblregister);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            txtpassword = FindViewById<EditText>(Resource.Id.txtpassword);
            btnlogin = FindViewById<Button>(Resource.Id.btnlogin);

            btnlogin.Click += async delegate
            {
                UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                await Task.Delay(1000);
                login();
                UserDialogs.Instance.HideLoading();
            };

            lblregister.Click += async delegate
            {
                await Task.Delay(1000);
                Intent activity = new Intent(this, typeof(registrationActivity));
                StartActivity(activity);
            };

        }

        public void savefile()
        {
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UploadFile(@"http://joremtongwebsite.000webhostapp.com/upload.php", "POST", fileName);
                client.Dispose();
            }
            catch (Exception err)
            {
                Toast.MakeText(this, err.Message, ToastLength.Long).Show();
            }
        }
        public async Task OpenFolderDialogAsync()
        {
            try
            {
                FileData filedata = await CrossFilePicker.Current.PickFile();
                if (filedata == null)
                    return; // user canceled file picking

                fileName = filedata.FilePath;
                savefile();
                Toast.MakeText(this, fileName, ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        public void login()
        {
            string password = Library.EasyMD5.Hash(txtpassword.Text);
            try
            {
                using (var client = new WebClient())
                {
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/login.php?username=" + txtusername.Text + "&&password=" + password);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    string username = jsonData[0].username;
                    string passwords = jsonData[0].password;
                    string userID = jsonData[0].userID;
                    string fullname = jsonData[0].fullName;
                    if (password == passwords && txtusername.Text == username)
                    {
                        Library.ControlID.userID = userID;
                        Library.ControlID.userFullName = fullname;
                        Intent activity = new Intent(this, typeof(PageActivity));
                        StartActivity(activity);
                    }
                }
            }
            catch (Exception i)
            {
                Toast.MakeText(this, i.Message, ToastLength.Long).Show();
            }
        }
    }
}