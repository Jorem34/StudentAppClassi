using System;
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
    [Activity(Label = "handoutsActivity")]
    public class handoutsActivity : Activity
    {

        LinearLayout linearLayout1;
        SwipeRefreshLayout refresher;
        ImageView imageView1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.handouts);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);

          
            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };
            listOfStudent();
            refresher.Refresh += delegate (object sender, System.EventArgs e)
            {
                listOfStudent();
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
        string id = Library.ControlID.userID;
        public void listOfStudent()
        {
            linearLayout1.RemoveAllViews();

            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    string id = Library.ControlID.userID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/studentHandoutList.php?userID=" + userID);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    foreach (dynamic jsonDatas in jsonData)
                    {
                        var layout2 = new LinearLayout(this);
                        layout2.Orientation = Orientation.Horizontal;
                        var layoutParams2 = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams2.SetMargins(0, 0, 0, 20);
                        layout2.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout2.SetPadding(20, 20, 60, 20);
                        layout2.LayoutParameters = layoutParams2;

                        var Image = new ImageView(this);
                        var param = new LayoutParams(150, 150);
                        Image.LayoutParameters = param;
                        Image.SetPadding(30, 40, 0, 0);
                        Image.SetImageResource(Resource.Drawable.pdffile);

                        var layout = new LinearLayout(this);
                        layout.Orientation = Orientation.Vertical;
                        var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.SetMargins(10, 0, 0, 20);
                        layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout.SetPadding(0, 0, 0, 0);
                        layout.LayoutParameters = layoutParams;
                        layout.Click += delegate
                        {
                            string url = "http://joremtongwebsite.000webhostapp.com/upload/" + jsonDatas["filename"].ToString();
                            if (!url.Contains("http://"))
                            {
                                string address = url;
                                url = String.Format("http://(0)", address);
                            }
                            var uri = Android.Net.Uri.Parse(url);
                            Intent intent = new Intent(Intent.ActionView, uri);
                            StartActivity(intent);
                        };

                        var username = new TextView(this);
                        username.SetTextColor(Color.White);
                        username.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                        username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 24);
                        username.Text = jsonDatas["title"].ToString();
                        username.SetPadding(0, 0, 0, 0);


                        var filename = new TextView(this);
                        filename.SetTextColor(Color.White);
                        filename.SetTypeface(Typeface.Default, TypefaceStyle.Normal);
                        filename.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        filename.Text = jsonDatas["filename"].ToString();
                        filename.SetPadding(0, 0, 0, 0);

                        var Title = new TextView(this);
                        Title.SetTextColor(Color.DimGray);
                        Title.SetTypeface(Typeface.Default, TypefaceStyle.Normal);
                        Title.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        Title.Text = jsonDatas["subject"].ToString();
                        Title.SetPadding(0, 0, 0, 0);

                        layout.AddView(username);
                        layout.AddView(filename);
                        layout.AddView(Title);
                        layout2.AddView(Image);
                        layout2.AddView(layout);
                        linearLayout1.AddView(layout2);
                    }

                }
            }
            catch (Exception i)
            {
                var layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;
                LayoutParams layoutParams = new LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                layoutParams.SetMargins(5, 5, 5, 5);
                layout.SetBackgroundColor(Color.White);
                layout.SetPadding(0, 0, 0, 0);

                var username = new TextView(this);
                username.SetTextColor(Color.Red);
                username.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                username.Text = "NO DATA YET";
                username.SetPadding(20, 20, 20, 0);

                layout.AddView(username);
                linearLayout1.AddView(layout);
            }
        }
    }
}