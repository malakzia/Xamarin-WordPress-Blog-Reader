using System;
using Android.Views;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Xamarin_WordPress_Blog_Reader.Models;
using Xamarin_WordPress_Blog_Reader;
namespace Xamarin_WordPress_Blog_Reader.Adapters
{
    public class PostsAdapter : BaseAdapter<Post>
    {
        Activity context;
        List<Post> list;

        public PostsAdapter(Activity activity, List<Post> listOfPosts)
        {
            list = listOfPosts;
            context = activity;
        }
        public override Post this[int position]
        {
            get
            {
                return list[position];
            }
        }

        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return list[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.PostItem, parent, false);

            var TitleTextView = view.FindViewById<TextView>(Resource.Id.PostsItemTitleTextView);
            var DateTextView = view.FindViewById<TextView>(Resource.Id.PostsItemDateTextView);

            var post = this[position];

            TitleTextView.Text = post.Title;
            DateTextView.Text = post.PostDate.ToLongDateString();

            return view;

        }
    }
}