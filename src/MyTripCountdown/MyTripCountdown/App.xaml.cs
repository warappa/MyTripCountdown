using MyTripCountdown.Views;
using Skor.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyTripCountdown
{
    public partial class App : Application
    {
        public App()
        {
            XamlCSS.XamarinForms.Css.Initialize(this);

            InitializeComponent();

            MainPage = new CustomNavigationPage(new MyTripCountdownView());
        }
    }
}