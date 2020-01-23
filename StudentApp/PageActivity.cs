using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text;

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
    [Activity(Label = "PageActivity")]
    public class PageActivity : Activity
    {
        LinearLayout linearLayout1;
        SwipeRefreshLayout refresher;
        ImageView imageView1;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.page);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            list();
            refresher.Refresh += delegate (object sender, System.EventArgs e)
            {
                list();
                refresher.Refreshing = false;
            };

            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };

        }

        public override void OnBackPressed()
        {
            return;
        }

        public void list()
        {
            linearLayout1.RemoveAllViews();

            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/news.php?userID=" + userID);
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

                        var layout = new LinearLayout(this);
                        layout.Orientation = Orientation.Vertical;
                        var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.SetMargins(0, 0, 0, 20);
                        layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout.SetPadding(0, 0, 0, 0);
                        layout.LayoutParameters = layoutParams;

                        var username = new TextView(this);
                        username.SetTextColor(Color.White);
                        username.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                        username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                        username.Text = jsonDatas["title"].ToString();
                        username.SetPadding(20, 20, 20, 0);

                        var timedate = new TextView(this);
                        timedate.SetTextColor(Color.LightGray);
                        timedate.SetTypeface(Typeface.Default, TypefaceStyle.Normal);
                        timedate.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 8);
                        timedate.Text = jsonDatas["createdOn"].ToString();
                        timedate.SetPadding(20, 0, 20, 20);

                        var Title = new TextView(this);
                        Title.SetTextColor(Color.DimGray);
                        Title.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                        Title.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        Title.Text = jsonDatas["content"].ToString();
                        Title.SetPadding(20, 0, 20, 8);

                        layout.AddView(username);
                        layout.AddView(timedate);
                        layout.AddView(Title);
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
                username.Text = "NO POST YET";
                username.SetPadding(20, 20, 20, 0);

                layout.AddView(username);
                layout2.AddView(layout);
                linearLayout1.AddView(layout2);
            }
        }
    }
}