using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;

namespace CodigoBarra
{
    [Activity(Label = "Barcode Scanner", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnLerCodigo = FindViewById<Button>(Resource.Id.btnCodigo);
            btnLerCodigo.Click += BtnLerCodigo_Click;
        }

        private async void BtnLerCodigo_Click(object sender, EventArgs e)
        {
            MobileBarcodeScanner scanner = new MobileBarcodeScanner();

            //Tell our scanner to use the default overlay
            scanner.UseCustomOverlay = false;

            //We can customize the top and bottom text of the default overlay
            scanner.TopText = "Hold the camera up to the barcode about 6 inches away.";
            scanner.BottomText = "Please, wait for the barcode to automatically scan!";

            //Start scanning
            var result = await scanner.Scan();

            HandleScanResult(result);
        }

        private void HandleScanResult(ZXing.Result result)
        {
            try
            {
                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    FindViewById<EditText>(Resource.Id.txtCodigo).Text = result.Text.ToString();
                    FindViewById<LinearLayout>(Resource.Id.pnlCodigo).Visibility = ViewStates.Visible;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Erro: " + ex.Message, Android.Widget.ToastLength.Long).Show();
            }
        }
    }
}

