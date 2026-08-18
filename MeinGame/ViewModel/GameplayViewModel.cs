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


        [RelayCommand]
        public void ErgebnisEintragen(string kategorie)
        {
            // 1. Darf überhaupt eingetragen werden? (Es muss mindestens 1x gewürfelt worden sein)
            if (WurfZaehler == 0) return;

            // 2. Wer ist gerade am Zug?
            var aktuellerSpieler = IsPlayerOneTurn ? Spieler1 : Spieler2;

            // 3. Eintragen je nach ausgewählter Kategorie
            switch (kategorie)
            {
                case "Einser":
                    // Nur eintragen, wenn das Feld noch leer (null) ist
                    if (aktuellerSpieler.Einser == null)
                    {
                        aktuellerSpieler.Einser = BerechneObereReihe(1);
                        ZugBeenden();
                    }
                    break;

                case "Zweier":
                    if (aktuellerSpieler.Zweier == null)
                    {
                        aktuellerSpieler.Zweier = BerechneObereReihe(2);
                        ZugBeenden();
                    }
                    break;

                case "Dreier":
                    if (aktuellerSpieler.Dreier == null)
                    {
                        aktuellerSpieler.Dreier = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;

                case "Vierer":
                    if (aktuellerSpieler.Vierer == null)
                    {
                        aktuellerSpieler.Vierer = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;

                case "Fuenfer":
                    if (aktuellerSpieler.Fuenfer == null)
                    {
                        aktuellerSpieler.Fuenfer = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;

                case "Sechser":
                    if (aktuellerSpieler.Sechser == null)
                    {
                        aktuellerSpieler.Sechser = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;




            }
        }




        // Hilfsmethode, um die Punkte für Einser bis Sechser zu berechnen
        private int BerechneObereReihe(int augenzahl)
        {
            int summe = 0;

            // Wir gehen alle 5 Würfel durch
            foreach (var wuerfel in WuerfelListe)
            {
                // Wenn der Würfel die gesuchte Augenzahl zeigt, addieren wir sie zur Summe
                if (wuerfel.Wert == augenzahl)
                {
                    summe += augenzahl;
                }
            }

            return summe;

            // Profi-Tipp: Mit LINQ ließe sich das sogar in einer einzigen Zeile schreiben:
            // return WuerfelListe.Where(w => w.Wert == augenzahl).Sum(w => w.Wert);
        }


        // Beendet den Zug, resettet die Würfel und wechselt den Spieler
        private void ZugBeenden()
        {
            // 1. Zähler zurücksetzen
            WurfZaehler = 0;

            // 2. Alle Würfel wieder freigeben und optisch auf 1 stellen
            foreach (var wuerfel in WuerfelListe)
            {
                wuerfel.IstGesperrt = false;
                wuerfel.Wert = 1;
            }

            // 3. Spieler wechseln (True wird False, False wird True)
            IsPlayerOneTurn = !IsPlayerOneTurn;
        }

        [RelayCommand]
        public void ErgebnisEintragen(string kategorie)
        {
            // 1. Darf überhaupt eingetragen werden? (Es muss mindestens 1x gewürfelt worden sein)
            if (WurfZaehler == 0) return;

            // 2. Wer ist gerade am Zug?
            var aktuellerSpieler = IsPlayerOneTurn ? Spieler1 : Spieler2;

            // 3. Eintragen je nach ausgewählter Kategorie
            switch (kategorie)
            {
                case "Einser":
                    // Nur eintragen, wenn das Feld noch leer (null) ist
                    if (aktuellerSpieler.Einser == null)
                    {
                        aktuellerSpieler.Einser = BerechneObereReihe(1);
                        ZugBeenden();
                    }
                    break;

                case "Zweier":
                    if (aktuellerSpieler.Zweier == null)
                    {
                        aktuellerSpieler.Zweier = BerechneObereReihe(2);
                        ZugBeenden();
                    }
                    break;

                case "Dreier":
                    if (aktuellerSpieler.Dreier == null)
                    {
                        aktuellerSpieler.Dreier = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;


                case "Vierer":
                    if (aktuellerSpieler.Vierer == null)
                    {
                        aktuellerSpieler.Vierer = BerechneObereReihe(3);
                        ZugBeenden();
                    }
                    break;
                    // TODO: Vierer, Fuenfer, Sechser exakt nach dem gleichen Muster...
            }
        }

    }
}

