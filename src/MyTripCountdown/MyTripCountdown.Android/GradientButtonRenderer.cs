using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using MyTripCountdown.Controls;
using Skor.Controls.Droid.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyTripCountdown.Controls.GradientButton), typeof(global::MyTripCountdown.Droid.GradientButtonRenderer))]
namespace MyTripCountdown.Droid
{
    public class GradientButtonRenderer : 
        Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<global::MyTripCountdown.Controls.GradientButton, FrameLayout>
    {
        private const int DEFAULT_HEIGHT_BUTTON = 56;
        private Android.Support.V7.Widget.AppCompatButton nButton;
        private GradientButton button;
        private FrameLayout frame;

        public GradientButtonRenderer() : base()
        {

        }
        public GradientButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<global::MyTripCountdown.Controls.GradientButton> e)
        {
            base.OnElementChanged(e);
            this.button = e.NewElement as global::MyTripCountdown.Controls.GradientButton;
            this.button.HeightRequest = this.button.HeightRequest >= DEFAULT_HEIGHT_BUTTON ? this.button.HeightRequest : DEFAULT_HEIGHT_BUTTON;
            InitControls();
            RenderText();
            InitStyleButton();
            nButton.Click += (s, ev) =>
            {
                button.SendClicked();
            };
            nButton.LongClick += (s, ev) =>
            {
                button.SendLongClick();
            };
            frame.AddView(nButton);
            SetNativeControl(this.frame);
        }

        private void InitControls()
        {
            //Layout
            frame = new FrameLayout(Context);
            frame.LayoutParameters = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            nButton = new Android.Support.V7.Widget.AppCompatButton(Context);
            //Button
            var nBtnLayout = new FrameLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            nBtnLayout.SetMargins(8, 8, 8, 24);
            nButton.SetPadding(0, 0, 0, 0);
            nButton.LayoutParameters = nBtnLayout;
        }

        private void RenderText()
        {
            nButton.Text = button.Text;
            nButton.SetTextColor(button.TextColor.ToAndroid());
            nButton.Typeface = button.Font.ToTypeface();
        }
        private void InitStyleButton()
        {
            nButton.Background = CreateBackgroundForButton();
            nButton.AddRipple(button.RippleColor.ToAndroid());
            nButton.Enabled = button.IsEnabled;
            if (!button.HasShadow)
            {
                nButton.StateListAnimator = null;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "IsEnabled")
            {
                if (e.PropertyName == "IsEnabled")
                {
                    nButton.Enabled = button.IsEnabled;
                }
            }
            else if(e.PropertyName == "StartColor" ||
                e.PropertyName == "EndColor" ||
                e.PropertyName == "CenterColor" ||
                e.PropertyName == "Angle" ||
                e.PropertyName == "Image" ||
                e.PropertyName == "CornerRadius")
            {
                InitStyleButton();
            }
        }

        private Drawable CreateBackgroundForButton()
        {
            List<Drawable> drawables = new List<Drawable>();
            List<Drawable> drawablesDisabled = new List<Drawable>();
            drawables.Add(BackgroundExtension.CreateBackgroundGradient(button.StartColor.ToAndroid(),
                button.EndColor.ToAndroid(),
                button.CenterColor.ToAndroid(),
                button.CornerRadius,
                button.Angle.ToAndroid()));
            drawablesDisabled.Add(BackgroundExtension.CreateBackgroundGradient(button.StartColor.ToAndroid(),
                button.EndColor.ToAndroid(),
                button.CenterColor.ToAndroid(),
                button.CornerRadius,
                button.Angle.ToAndroid()));
            if (button.Image != null && !string.IsNullOrEmpty(button.Image.File))
            {
                var bitmapDrawable = BackgroundExtension.CreateBackgroundBitmap(button.Image.File, (int)button.HeightRequest,
                    (int)button.WidthRequest, button.CornerRadius);
                if (bitmapDrawable != null)
                {
                    drawables.Add(bitmapDrawable);
                    drawablesDisabled.Add(bitmapDrawable);
                }
            }
            Drawable layer = new LayerDrawable(drawables.ToArray());
            Drawable layerDisabled = new LayerDrawable(drawablesDisabled.ToArray());
            layerDisabled.Mutate().Alpha = 80;
            var statesListDrawable = new StateListDrawable();
            statesListDrawable.AddState(new int[] { -Android.Resource.Attribute.StateEnabled }, layerDisabled);
            statesListDrawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, layer);
            statesListDrawable.AddState(new int[] { Android.Resource.Attribute.StateEnabled }, layer);
            return statesListDrawable;
        }
    }
}