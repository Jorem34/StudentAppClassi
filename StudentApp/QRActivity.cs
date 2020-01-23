using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StudentApp
{
    [Activity(Label = "QRActivity")]
    public class QRActivity : Activity
    {
        Button btnscan;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.qrScannerForAttendance);
            // Create your application here
            btnscan = FindViewById<Button>(Resource.Id.btnscan);

        }
       
    }
}