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
using Java.Interop;
using Newtonsoft.Json;
using static Android.App.ActionBar;

namespace StudentApp
{
    [Activity(Label = "QuizFreeViewActivity")]
    public class QuizFreeViewActivity : Activity
    {
        LinearLayout linearLayout1;
        ImageView imageView1;
        Button bt1;
        int total = 0;
        public string quizID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizFreeView);
            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView1.Click += delegate
            {
                Intent activity = new Intent(this, typeof(profileActivity));
                StartActivity(activity);
            };

            quizID = Library.ControlID.quizTitleID;

            bt1 = FindViewById<Button>(Resource.Id.button1);

            validation();

            bt1.Click += async delegate
            {
                //  Toast.MakeText(this, total.ToString(), ToastLength.Short).Show();

                UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                await Task.Delay(1000);

                try
                {
                    quizID = Library.ControlID.quizTitleID;
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["quizID"] = quizID;
                        values["userID"] = Library.ControlID.userID;
                        values["score"] = total.ToString();
                        var response = client.UploadValues("http://joremtongwebsite.000webhostapp.com/quiz.php", values);
                        var responseString = Encoding.Default.GetString(response);
                    }
                    Intent activity = new Intent(this, typeof(PageActivity));
                    StartActivity(activity);
                    Toast.MakeText(this, "SCORE SEND", ToastLength.Long).Show();
                }
                catch (Exception i)
                {
                    Toast.MakeText(this, i.Message, ToastLength.Long).Show();
                }
                UserDialogs.Instance.HideLoading();

            };
        }
        
        public void list()
        {

            try
            {
              
                linearLayout1.RemoveAllViews();

                using (var client = new WebClient())
                {
                    
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/quizall.php?quizID=" + quizID);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    foreach (dynamic jsonDatas in jsonData)
                    {
                     
                        var choiceanswer = new TextView(this);
                        choiceanswer.SetTextColor(Color.Black);
                        choiceanswer.Text = "Sample";
                        choiceanswer.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                        choiceanswer.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 0);
                        choiceanswer.SetPadding(20, 0, 20, 8);

                        var qanswer = new TextView(this);
                        qanswer.SetTextColor(Color.White);
                        qanswer.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                        qanswer.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 0);
                        qanswer.Text = jsonDatas["correctanswer"].ToString();
                        qanswer.SetPadding(20, 0, 20, 8);

                        var layout = new LinearLayout(this);
                        layout.Orientation = Orientation.Vertical;
                        var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.SetMargins(0, 0, 0, 20);
                        layout.SetBackgroundResource(Resource.Drawable.layout_bg);
                        layout.SetPadding(0, 0, 0, 0);
                        layout.LayoutParameters = layoutParams;

                        var question = new TextView(this);
                        question.SetTextColor(Color.White);
                        question.SetTypeface(Typeface.Default, TypefaceStyle.Bold);
                        question.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                        question.Text = jsonDatas["question"].ToString();
                        question.SetPadding(20, 0, 20, 8);

                        var group = new RadioGroup(this);
                        group.Orientation = Orientation.Vertical;
                        LayoutParams rgroup = new LayoutParams(RadioGroup.LayoutParams.MatchParent, RadioGroup.LayoutParams.WrapContent);
                        rgroup.SetMargins(5, 5, 5, 5);
                        group.SetBackgroundColor(Color.Rgb(37, 36, 36));

                        var choice1 = new RadioButton(this);
                        choice1.SetTextColor(Color.White);
                        choice1.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        choice1.Text = jsonDatas["choiceone"].ToString();
                        choice1.Click += delegate
                        {
                            choiceanswer.Text = "";
                            choiceanswer.Text = choice1.Text;

                            if (choiceanswer.Text == qanswer.Text)
                            {
                                total++;

                            }
                        };
                        choice1.SetPadding(20, 0, 20, 0);

                        var choice2 = new RadioButton(this);
                        choice2.SetTextColor(Color.White);
                        choice2.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        choice2.Text = jsonDatas["choicetwo"].ToString();
                        choice2.Click += delegate
                        {
                            choiceanswer.Text = "";
                            choiceanswer.Text = choice2.Text;
                            if (choiceanswer.Text == qanswer.Text)
                            {
                                total++;

                            }
                        };
                        choice2.SetPadding(20, 0, 20, 0);

                        var choice3 = new RadioButton(this);
                        choice3.SetTextColor(Color.White);
                        choice3.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        choice3.Text = jsonDatas["choicethree"].ToString();
                        choice3.Click += delegate
                        {
                            choiceanswer.Text = "";
                            choiceanswer.Text = choice3.Text;
                            if (choiceanswer.Text == qanswer.Text)
                            {
                                total++;

                            }
                        };
                        choice3.SetPadding(20, 0, 20, 0);

                        var choice4 = new RadioButton(this);
                        choice4.SetTextColor(Color.White);
                        choice4.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 15);
                        choice4.Text = jsonDatas["choicefour"].ToString();
                        choice4.Click += delegate
                        {
                            choiceanswer.Text = "";
                            choiceanswer.Text = choice4.Text;
                            if (choiceanswer.Text == qanswer.Text)
                            {
                                total++;

                            }
                        };
                        choice4.SetPadding(20, 0, 20, 0);

                        layout.AddView(question);
                        group.AddView(choice1);
                        group.AddView(choice2);
                        group.AddView(choice3);
                        group.AddView(choice4);
                        layout.AddView(group);
                        layout.AddView(qanswer);
                        layout.AddView(choiceanswer);

                        linearLayout1.AddView(layout);
                    }
                }

            }

            catch(Exception i)
            {

                Toast.MakeText(this, i.Message, ToastLength.Long).Show();
            }
        
        }
        public void validation()
        {
            linearLayout1.RemoveAllViews();

            try
            {
                using (var client = new WebClient())
                {
                    string userID = Library.ControlID.userID;
                    quizID = Library.ControlID.quizTitleID;
                    var json = client.DownloadString("http://joremtongwebsite.000webhostapp.com/quizvalidation.php?userID=" + userID + "&&quizID="+quizID);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);
                    foreach (dynamic jsonDatas in jsonData)
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
                        username.Text = "Already Take This Exam";
                        username.SetPadding(20, 20, 20, 0);

                        layout.AddView(username);
                        linearLayout1.AddView(layout);
                    }

                }
            }
            catch (Exception i)
            {
                list();
            }

        }
    }
}