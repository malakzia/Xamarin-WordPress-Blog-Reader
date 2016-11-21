using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Xamarin_WordPress_Blog_Reader.Models;
using Android.Content;
using Xamarin_WordPress_Blog_Reader.Adapters;
using Android.Views;

namespace Xamarin_WordPress_Blog_Reader
{
    [Activity(Label = "Xamarin_WordPress_Blog_Reader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Post> listOfPosts;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Title = "WordPress in Xamarin!";

            var postsListView = FindViewById<ListView>(Resource.Id.PostsListView);
            var progressBar = FindViewById<ProgressBar>(Resource.Id.ProgressBar);

            progressBar.Visibility = Android.Views.ViewStates.Visible;
            var downloadedPosts = await new WordPressDotNet.WordPressConnector("http://zianasir.com/").Posts.GetPostsAsync();

            listOfPosts = new List<Post>();
            foreach (var post in downloadedPosts)
            {
                listOfPosts.Add(new Post() {
                    Id = post.id,
                    Title = post.title.rendered,
                    Content = post.content.rendered,
                    PostDate = System.Convert.ToDateTime(post.date)
                });
            }

            progressBar.Visibility = Android.Views.ViewStates.Gone;
            postsListView.Adapter = new PostsAdapter(this, listOfPosts);
            SetListViewHeightBasedOnItems(postsListView);
            postsListView.ItemClick += PostsListView_ItemClick;

        }

        private void PostsListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var htmlContent = listOfPosts[e.Position].Content;
            var title = listOfPosts[e.Position].Title;

            var intent = new Intent(this, typeof(PostActivity));
            intent.PutExtra("HTMLContent", htmlContent);
            intent.PutExtra("Title", title);

            StartActivity(intent);
        }
        public static bool SetListViewHeightBasedOnItems(ListView listView)
        {

            var listAdapter = listView.Adapter;
            if (listAdapter != null)
            {

                int numberOfItems = listAdapter.Count;

                int totalItemsHeight = 0;
                for (int itemPos = 0; itemPos < numberOfItems; itemPos++)
                {
                    View item = listAdapter.GetView(itemPos, null, listView);
                    item.Measure(0, 0);
                    totalItemsHeight += item.MeasuredHeight;
                }


                int totalDividersHeight = listView.DividerHeight *
                        (numberOfItems - 1);


                var param = listView.LayoutParameters;
                param.Height = totalItemsHeight + totalDividersHeight;
                listView.LayoutParameters = (param);
                listView.RequestLayout();

                return true;

            }
            else
            {
                return false;
            }

        }
    }
}

