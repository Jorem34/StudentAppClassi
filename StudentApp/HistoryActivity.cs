﻿using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using static Android.App.ActionBar;
namespace StudentApp
{
    [Activity(Label = "HistoryActivity")]
    public class HistoryActivity : Activity
    {
        LinearLayout linearLayout1;
        SwipeRefreshLayout refresher;
        ImageView imageView1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.History);
            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            list();
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };
            refresher.Refresh += delegate (object sender, System.EventArgs e)
            {
                list();
                refresher.Refreshing = false;
            };
        }
        public override void OnBackPressed()
        {
            return;
        }
        public void list()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/historystudent.php?userID=" + userID);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    foreach (dynamic jsonDatas in jsonData)
                    {
                        var layout2 = new LinearLayout(this);
                        layout2.Orientation = Orientation.Horizontal;
                        var layoutParams2 = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams2.SetMargins(0, 0, 0, 20);
                        layout2.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout2.SetPadding(0, 0, 0, 0);
                        layout2.LayoutParameters = layoutParams2;

                        var Image = new ImageView(this);
                        var param = new LayoutParams(150, 150);
                        Image.LayoutParameters = param;
                        Image.SetPadding(30, 10, 0, 0);
                        Image.SetImageResource(Resource.Drawable.reading);

                        var layout = new LinearLayout(this);
                        layout.Orientation = Orientation.Vertical;
                        var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.SetMargins(0, 0, 0, 20);
                        layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout.SetPadding(0, 0, 0, 0);
                        layout.LayoutParameters = layoutParams;

                        var username = new TextView(this);
                        username.SetTextColor(Color.White);
                        username.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                        username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                        username.Text = jsonDatas["activity"].ToString();
                        username.SetPadding(20, 20, 20, 0);

                        var score = new TextView(this);
                        score.SetTextColor(Color.White);
                        score.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                        score.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        score.Text = "Score : " + jsonDatas["score"].ToString();
                        score.SetPadding(20, 20, 20, 0);

                        layout.AddView(username);
                        layout.AddView(score);
                        layout2.AddView(Image);
                        layout2.AddView(layout);
                        linearLayout1.AddView(layout2);
                    }

                }
            }
            catch (Exception i)
            {
                var layout2 = new LinearLayout(this);
                layout2.Orientation = Orientation.Horizontal;
                var layoutParams2 = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                layoutParams2.SetMargins(0, 0, 0, 20);
                layout2.SetBackgroundResource(Resource.Drawable.layout_bg);
                layout2.SetPadding(0, 0, 0, 0);
                layout2.LayoutParameters = layoutParams2;

                var Image = new ImageView(this);
                var param = new LayoutParams(120, 120);
                Image.LayoutParameters = param;
                Image.SetPadding(20, 30, 0, 0);
                Image.SetImageResource(Resource.Drawable.reading);

                var layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;
                var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent); ;
                layoutParams.SetMargins(0, 0, 0, 20);
                layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                layout.SetPadding(10, 10, 10, 50);
                layout.LayoutParameters = layoutParams;

                var username = new TextView(this);
                username.SetTextColor(Color.White);
                username.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                username.Text = "NO DATA YET";
                username.SetPadding(20, 20, 20, 0);

                layout.AddView(username);
                layout2.AddView(Image);
                layout2.AddView(layout);
                linearLayout1.AddView(layout2);
            }
        }
   
    }
}