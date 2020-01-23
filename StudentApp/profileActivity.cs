using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing;

namespace StudentApp
{
    [Activity(Label = "profileActivity")]
    public class profileActivity : Activity
    {
        ImageView imageView1;
        TextView txtname;
        Button btnupdate, btnrewards, btnabout, btnhistory, btnchallenge;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            txtname = FindViewById<TextView>(Resource.Id.txtname);
            btnupdate = FindViewById<Button>(Resource.Id.btnupdate);
            btnrewards = FindViewById<Button>(Resource.Id.btnrewards);
            btnabout = FindViewById<Button>(Resource.Id.btnabout);
            btnhistory = FindViewById<Button>(Resource.Id.btnhistory);
            btnchallenge = FindViewById<Button>(Resource.Id.btnchallenge);
            qrcode();

            btnchallenge.Click += delegate
            {
                Intent activity = new Intent(this, typeof(PageActivity));
                StartActivity(activity);
            };
            btnhistory.Click += delegate
            {
                Intent activity = new Intent(this, typeof(HistoryActivity));
                StartActivity(activity);
            };
            btnabout.Click += delegate
            {
                Intent activity = new Intent(this, typeof(SettingsActivity));
                StartActivity(activity);
            };
            btnupdate.Click += delegate
            {
                Intent activity = new Intent(this, typeof(handoutsActivity));
                StartActivity(activity);
            };
            btnrewards.Click += delegate
            {
                Intent activity = new Intent(this, typeof(QuizTitleActivity));
                StartActivity(activity);
            };
            // Create your application here
        }

        public void qrcode()
        {
            txtname.Text = Library.ControlID.userFullName;


            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Renderer = new ZXing.Rendering.BitmapRenderer();
            {
                // Background = 
                // Foreground = 
            };
            writer.Options.Height = 300;
            writer.Options.Width = 300;
            writer.Options.Margin = 1;

            var bitmap = writer.Write(Library.ControlID.userID);
            imageView1.SetImageBitmap(bitmap);
        }


    }
}