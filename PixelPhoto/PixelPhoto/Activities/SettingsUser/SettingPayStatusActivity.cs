using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using PixelPhoto.Activities.Default;
using PixelPhoto.Helpers.Ads;
using PixelPhoto.Helpers.Model;
using PixelPhoto.Helpers.Utils;
using PixelPhotoClient.RestCalls;
using Exception = System.Exception;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace PixelPhoto.Activities.SettingsUser
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Locale | ConfigChanges.UiMode | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SettingPasswordActivity : AppCompatActivity
    {
        #region Variables Basic

        private Toolbar Toolbar;
        private TextView SaveTextView, LinkTextView;
        private EditText CurrentPasswordEditText, NewPsswordEditText, RepeatPasswordEditText;
       
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetTheme(AppSettings.SetTabDarkTheme ? Resource.Style.MyTheme_Dark_Base : Resource.Style.MyTheme_Base);

                // Create your application here
                SetContentView(Resource.Layout.SettingPayStatusLayout);

                //Get Value And Set Toolbar
                InitComponent();
                InitToolbar();
				LoadDataUser();

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 

        protected override void OnResume()
        {
            try
            {
                base.OnResume();
                AddOrRemoveEvent(true);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void OnPause()
        {
            try
            {
               
                base.OnPause();
                AddOrRemoveEvent(false); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void OnTrimMemory(TrimMemory level)
        {
            try
            { 
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                base.OnTrimMemory(level);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public override void OnLowMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
                base.OnLowMemory();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #region Menu

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        #endregion

        #region Functions

        private void InitComponent()
        {
            try
            {
                PayFollower = FindViewById<TextView>(Resource.Id.pay_follower);
                PayFollowers = FindViewById<TextView>(Resource.Id.pay_followers);
                PayFollowerPrice = FindViewById<TextView>(Resource.Id.pay_follower_price);
                PayFollowersPrice = FindViewById<TextView>(Resource.Id.pay_followers_price);                
       
                AdsGoogle.Ad_AdmobNative(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SetColorEditText(EditText edit)
        {
            try
            {
                edit.SetBackgroundResource(AppSettings.SetTabDarkTheme ? Resource.Drawable.EditTextStyleOne_Dark : Resource.Drawable.pixEditTextStyleOne);
                edit.SetTextColor(AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                edit.SetHintTextColor(AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitToolbar()
        {
            try
            {
                Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                if (Toolbar != null)
                {
                    Toolbar.Title = GetText(Resource.String.Lbl_Change_Password);
                                        Toolbar.SetTitleTextColor(AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                    Toolbar.SetBackgroundResource(AppSettings.SetTabDarkTheme ? Resource.Drawable.linear_gradient_drawable_Dark : Resource.Drawable.linear_gradient_drawable);

                    SetSupportActionBar(Toolbar);
                    SupportActionBar.SetDisplayShowCustomEnabled(true);
                    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                    SupportActionBar.SetHomeButtonEnabled(true);
                    SupportActionBar.SetDisplayShowHomeEnabled(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //private void AddOrRemoveEvent(bool addEvent)
        //{
            //try
            //{
                // true +=  // false -=
                //if (addEvent)
                //{
                    //SaveTextView.Click += SaveTextViewOnClick;
                    //LinkTextView.Click += LinkTextViewOnClick;
                //}
                //else
                //{
                    //SaveTextView.Click -= SaveTextViewOnClick;
                    //LinkTextView.Click -= LinkTextViewOnClick;
                //}
            //}
            //catch (Exception e)
            //{
                //Console.WriteLine(e);
            //}
        //}

       
        #endregion
        
        #region Load Data User
 
        private async void LoadDataUser()
        {
            try
            {
                new SqLiteDatabase().GetMyProfile();

                if (ListUtils.MyProfileList.Count == 0)
                    await ApiRequest.GetProfile_Api(this);

                var dataUser = ListUtils.MyProfileList.FirstOrDefault();
                if (dataUser != null)
                {
                    if (dataUser.Followers != null && int.TryParse(dataUser.Followers, out var numberFollowers))
                        PayFollowers.Text = Methods.FunString.FormatPriceValue(numberFollowers);
                    PayFollowersPrice.Text="¥" + dataUser.Following_Price
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
         
    }
}