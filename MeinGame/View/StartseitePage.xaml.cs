namespace MeinGame.View
{
    public partial class StartseitePage : ContentPage
    {
        public StartseitePage()
        {
            InitializeComponent();
        }

        private async void btnStartGame_Clicked(object sender, EventArgs e)
        {
            // 1. Namen auslesen oder Standardwerte setzen
            string name1 = string.IsNullOrWhiteSpace(txtPlayer1.Text) ? "Spieler 1" : txtPlayer1.Text.Trim();
            string name2 = string.IsNullOrWhiteSpace(txtPlayer2.Text) ? "Spieler 2" : txtPlayer2.Text.Trim();

            // 2. Parameter-Dictionary erstellen
            var navigationParameter = new Dictionary<string, object>
            {
                { "Player1Name", name1 },
                { "Player2Name", name2 }
            };

            // 3. Zur GameplayPage navigieren und Parameter übergeben
            await Shell.Current.GoToAsync(nameof(GameplayPage), navigationParameter);
        }

        private async void btnStatistik_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StatistikPage));
        }
    }
}