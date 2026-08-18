using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MeinGame.ViewModel
{

        public partial class GameplayViewModel : ObservableObject
        {
            [ObservableProperty]
            private Ergebnisse _spieler1 = new() { PlayerName = "Spieler 1" };

            [ObservableProperty]
            private Ergebnisse _spieler2 = new() { PlayerName = "Spieler 2" };

            // Optional: Wer ist gerade dran?
            [ObservableProperty]
            private bool _isPlayerOneTurn = true;


            //WÜRFELLISTE
            public ObservableCollection<Wuerfel> WuerfelListe { get; } = new ObservableCollection<Wuerfel>
            {
                new Wuerfel(),
                new Wuerfel(),
                new Wuerfel(),
                new Wuerfel(),
                new Wuerfel()
            };

            //Wieviele Würfe
            [ObservableProperty] 
            private int _wurfZaehler = 0;


            // Würfeln-Logik mit rnd und max 3 würfeln überprüfung ob würfel gehalten wird

            [RelayCommand] // RelayCommand für den Würfeln-Button hángt automatisch Command an WuerfelnCommand
            public void Wuerfeln()
            {
                if (WurfZaehler >= 3)

                {
                    return;
                }

                foreach (var wuerfel in WuerfelListe)
                {
                    if (wuerfel.IstGesperrt == false)
                    {
                        wuerfel.Wert = new Random().Next(1, 7);
                    }
                }

                WurfZaehler++;
            }


            // Logik zum Sperren/Entsperren eines einzelnen Würfels
            [RelayCommand]
            public void WuerfelSperren(Wuerfel angeklickterWuerfel)
            {
                // Vor dem allerersten Wurf darf noch nichts gesperrt werden
                if (WurfZaehler == 0) return;

                // Kehrt den Wert um (True wird False, False wird True)
                angeklickterWuerfel.IstGesperrt = !angeklickterWuerfel.IstGesperrt;
            }
    }
}

