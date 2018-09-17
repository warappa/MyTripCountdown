using MyTripCountdown.ViewModels;
using MyTripCountdown.ViewModels.Base;
using Skor.Controls;
using Xamarin.Forms;

namespace MyTripCountdown.Views
{
    public partial class MyTripCountdownView : ContentPage
	{
		public MyTripCountdownView ()
		{
			InitializeComponent ();

            BindingContext = new MyTripCountdownViewModel();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }

        private void GradientButton_Clicked(object sender, System.EventArgs e)
        {
            (sender as GradientButton).EndColor = Color.BlueViolet;
        }
    }
}