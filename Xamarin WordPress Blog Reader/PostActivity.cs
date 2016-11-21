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
using Android.Webkit;

namespace Xamarin_WordPress_Blog_Reader
{
    [Activity(Label = "PostActivity")]
    public class PostActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Post);

            var htmlContent = Intent.GetStringExtra("HTMLContent");
            var title = Intent.GetStringExtra("Title");

            Title = title;

            var contentWebView = FindViewById<WebView>(Resource.Id.contentWebView);
            contentWebView.LoadData(htmlContent, "text/HTML", null);

            // Create your application here
        }
    }
}