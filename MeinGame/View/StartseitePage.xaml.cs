namespace MeinGame.View
{
    public partial class StartseitePage : ContentPage
    {
        public StartseitePage()
        {
            InitializeComponent();
        }

        private  void btnStartGame_Clicked(object sender, EventArgs e)
        {
            // Navigate to the GamePage when the button is clicked
            Shell.Current.GoToAsync(nameof(GameplayPage));

        }
    }
}
