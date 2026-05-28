using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.NFC;

namespace MainNfcDemo
{
    [Activity(Theme = "@style/Maui.SplashTheme",
              MainLauncher = true,
              LaunchMode = LaunchMode.SingleTop,
              ConfigurationChanges = ConfigChanges.ScreenSize | 
                                    ConfigChanges.Orientation |
                                         ConfigChanges.UiMode |
                                   ConfigChanges.ScreenLayout | 
                             ConfigChanges.SmallestScreenSize |
                                        ConfigChanges.Density)]
    [IntentFilter([Android.Nfc.NfcAdapter.ActionTechDiscovered])]
    [IntentFilter([Android.Nfc.NfcAdapter.ActionTagDiscovered])]
    [IntentFilter([Android.Nfc.NfcAdapter.ActionNdefDiscovered], Categories = new[] { Intent.CategoryDefault }, DataMimeType = "*/*")]

    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CrossNFC.Init(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            CrossNFC.OnResume();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            System.Diagnostics.Debug.WriteLine($"[NFC_LOG] Интент пойман: {intent?.Action}");
            CrossNFC.OnNewIntent(intent);
        }
    }
}