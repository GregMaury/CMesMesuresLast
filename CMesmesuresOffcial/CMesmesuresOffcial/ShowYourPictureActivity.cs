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

namespace CMesmesuresOffcial
{
    [Activity(Label = "ShowYourPictureActivity")]
    public class ShowYourPictureActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ShowYourPicture);

            ImageView imageViewUserPicture = FindViewById<ImageView>(Resource.Id.imageViewUserPicture);
            imageViewUserPicture = UserData.imageViewUserPicture;
        }
    }
}