using MeinGame.Services;

namespace MeinGame.View
{
    public partial class StatistikPage : ContentPage
    {
        public StatistikPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            cvHistorie.ItemsSource = await StatistikService.LadeHistorieAsync();
        }
    }
}