using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
    [Activity(Label = "QuizTitleActivity")]
    public class QuizTitleActivity : Activity
    {
        LinearLayout linearLayout1;
        SwipeRefreshLayout refresher;
        ImageView imageView1;
        string quizID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizTitle);
            quizID = Intent.GetStringExtra("quizID");
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            listOfStudent();
            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };
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
        public void listOfStudent()
        {
            linearLayout1.RemoveAllViews();

            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    string id = Library.ControlID.userID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/titleQuizList.php?userID=" + userID);
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
                        Image.SetImageResource(Resource.Drawable.answer);

                        var layout = new LinearLayout(this);
                        layout.Orientation = Orientation.Vertical;
                        var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.SetMargins(0, 0, 0, 20);
                        layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout.SetPadding(0, 0, 0, 0);
                        layout.LayoutParameters = layoutParams;
                        layout.Click += delegate
                        {


                            Library.ControlID.quizTitleID = jsonDatas["quizID"].ToString();
                            Intent activity = new Intent(this, typeof(QuizFreeViewActivity));
                            StartActivity(activity);

                        };
                        var username = new TextView(this);
                        username.SetTextColor(Color.White);
                        username.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                        username.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                        username.Text = jsonDatas["title"].ToString();
                        username.SetPadding(20, 20, 20, 0);

                        layout.AddView(username);
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