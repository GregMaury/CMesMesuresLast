using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using System.Collections.Generic;
using Android.Provider;
using Java.IO;
using Android.Content.PM;
using CMesmesuresOffcial.Helpers;
using Android.Support.V7.App;
using Android.Support.Design.Widget;



namespace CMesmesuresOffcial
{
    [Activity(Label = "CMesmesuresOffcial", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnSignin = FindViewById<Button>(Resource.Id.button1);
            TextView btnRegister = FindViewById<TextView>(Resource.Id.editText3);
            Button btnContinue = FindViewById<Button>(Resource.Id.button2);

            btnSignin.Click += OpenSignin;
            btnRegister.Click += OpenRegister;
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                btnContinue.Click += OnContinue;
            }
        }

        private void OpenSignin(object sender, EventArgs eventArgs)
        {
        }

        private void OpenRegister(object sender, EventArgs eventArgs)
        {
        }

        private void OnContinue(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._File = new File(App._Dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._File));
            StartActivityForResult(intent, 0);
        }

        private void CreateDirectoryForPictures()
        {
            App._Dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._Dir.Exists())
            {
                App._Dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var root = Android.OS.Environment.RootDirectory;
            var oern = Android.OS.Environment.DirectoryPictures;

            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._File);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.


            //int height = Resources.DisplayMetrics.HeightPixels;
            //int width = _imageView.Height;
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = Resources.DisplayMetrics.WidthPixels;

            if (!string.IsNullOrEmpty(App._File.Path))
            {
                App._Bitmap = App._File.Path.LoadAndResizeBitmap(width, height);
            }

            if (App._Bitmap != null)
            {
                UserData.imageViewUserPicture = new ImageView(this);
                UserData.imageViewUserPicture.SetImageBitmap(App._Bitmap);
                App._Bitmap = null;
            }

            Intent showYourPictureIntent = new Intent(this, typeof(ShowYourPictureActivity));
            StartActivity(showYourPictureIntent);

            //// Dispose of the Java side bitmap.
            //GC.Collect();
        }
    }
}

