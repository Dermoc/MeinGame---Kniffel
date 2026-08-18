
using MeinGame.View;

namespace MeinGame

{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(StartseitePage), typeof(StartseitePage));
            Routing.RegisterRoute(nameof(GameplayPage), typeof(GameplayPage));
        }
    }
}
