using MeinGame.ViewModel;

namespace MeinGame.View
{
    public partial class GameplayPage : ContentPage
    {
        public GameplayPage()
        {
            InitializeComponent();



            BindingContext = new GameplayViewModel();

        }


        private void btn_wuerfeln_Clicked(object sender, EventArgs e)
        {

        }
    }
}
