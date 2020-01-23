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

namespace StudentApp
{
    [Activity(Label = "Prof_DashboardActivity")]
    public class Prof_DashboardActivity : Activity
    {
        //ImageView btnqrcode;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Prof_Dashboard);
            

            
            // Create your application here
        }
    }
}