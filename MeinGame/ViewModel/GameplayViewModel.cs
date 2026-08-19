using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinGame.Model;
using MeinGame.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MeinGame.ViewModel
{


    // Empfängt die Parameter aus der Navigation automatisch
    [QueryProperty(nameof(Player1Name), "Player1Name")]
    [QueryProperty(nameof(Player2Name), "Player2Name")]

    public partial class GameplayViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _player1Name = "Spieler 1";

        [ObservableProperty]
        private string _player2Name = "Spieler 2";

        partial void OnPlayer1NameChanged(string value)
        {
            Spieler1.PlayerName = value;
            OnPropertyChanged(nameof(AktuellerSpielerName));
        }

        partial void OnPlayer2NameChanged(string value)
        {
            Spieler2.PlayerName = value;
            OnPropertyChanged(nameof(AktuellerSpielerName));
        }


        [ObservableProperty]
        private Ergebnisse _spieler1 = new() { PlayerName = "Spieler 1" };

        [ObservableProperty]
        private Ergebnisse _spieler2 = new() { PlayerName = "Spieler 2" };

        // Wer ist gerade dran? (true = Spieler 1, false = Spieler 2)
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AktuellerSpielerName))]
        private bool _isPlayerOneTurn = true;

        // WÜRFELLISTE
        public ObservableCollection<Wuerfel> WuerfelListe { get; } = new ObservableCollection<Wuerfel>
        {
            new Wuerfel(),
            new Wuerfel(),
            new Wuerfel(),
            new Wuerfel(),
            new Wuerfel()
        };

        // Wurfzähler mit automatischer Benachrichtigung für die UI-Texte
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(WurfStatusText))]
        [NotifyPropertyChangedFor(nameof(VerbleibendeWuerfeText))]
        private int _wurfZaehler = 0;


        // =========================================================================
        // DYNAMISCHE HILFSEIGENSCHAFTEN FÜR DIE ANZEIGE
        // =========================================================================

        public string AktuellerSpielerName => IsPlayerOneTurn ? Spieler1.PlayerName : Spieler2.PlayerName;

        public string WurfStatusText => WurfZaehler == 0
            ? "Bereit für Wurf 1"
            : $"Wurf {WurfZaehler} von 3";

        public string VerbleibendeWuerfeText => $"Verbleibend: {3 - WurfZaehler}";


        // =========================================================================
        // COMMANDS (Aktionen aus der UI)
        // =========================================================================

        // Würfeln-Logik
        [RelayCommand]
        public void Wuerfeln()
        {
            if (WurfZaehler >= 3)
            {
                return;
            }

            foreach (var wuerfel in WuerfelListe)
            {
                if (!wuerfel.IstGesperrt)
                {
                    wuerfel.Wert = Random.Shared.Next(1, 7);
                }
            }

            WurfZaehler++;
        }

        // Logik zum Sperren/Entsperren eines einzelnen Würfels
        [RelayCommand]
        public void WuerfelSperren(Wuerfel angeklickterWuerfel)
        {
            if (WurfZaehler == 0) return;
            angeklickterWuerfel.IstGesperrt = !angeklickterWuerfel.IstGesperrt;
        }

        [RelayCommand]
        public async Task ErgebnisEintragen(string kategorie)
        {
            if (WurfZaehler == 0) return;

            var aktuellerSpieler = IsPlayerOneTurn ? Spieler1 : Spieler2;

            switch (kategorie)
            {
                case "Einser":
                    if (aktuellerSpieler.Einser == null) { aktuellerSpieler.Einser = BerechneObereReihe(1); await ZugBeendenAsync(); }
                    break;
                case "Zweier":
                    if (aktuellerSpieler.Zweier == null) { aktuellerSpieler.Zweier = BerechneObereReihe(2); await ZugBeendenAsync(); }
                    break;
                case "Dreier":
                    if (aktuellerSpieler.Dreier == null) { aktuellerSpieler.Dreier = BerechneObereReihe(3); await ZugBeendenAsync(); }
                    break;
                case "Vierer":
                    if (aktuellerSpieler.Vierer == null) { aktuellerSpieler.Vierer = BerechneObereReihe(4); await ZugBeendenAsync(); }
                    break;
                case "Fuenfer":
                    if (aktuellerSpieler.Fuenfer == null) { aktuellerSpieler.Fuenfer = BerechneObereReihe(5); await ZugBeendenAsync(); }
                    break;
                case "Sechser":
                    if (aktuellerSpieler.Sechser == null) { aktuellerSpieler.Sechser = BerechneObereReihe(6); await ZugBeendenAsync(); }
                    break;
                case "DreierPasch":
                    if (aktuellerSpieler.DreierPasch == null) { aktuellerSpieler.DreierPasch = BerechneDreierPasch(); await ZugBeendenAsync(); }
                    break;
                case "ViererPasch":
                    if (aktuellerSpieler.ViererPasch == null) { aktuellerSpieler.ViererPasch = BerechneViererPasch(); await ZugBeendenAsync(); }
                    break;
                case "FullHouse":
                    if (aktuellerSpieler.FullHouse == null) { aktuellerSpieler.FullHouse = BerechneFullHouse(); await ZugBeendenAsync(); }
                    break;
                case "KleineStrasse":
                    if (aktuellerSpieler.KleineStrasse == null) { aktuellerSpieler.KleineStrasse = BerechneKleineStrasse(); await ZugBeendenAsync(); }
                    break;
                case "GrosseStrasse":
                    if (aktuellerSpieler.GrosseStrasse == null) { aktuellerSpieler.GrosseStrasse = BerechneGrosseStrasse(); await ZugBeendenAsync(); }
                    break;
                case "JepJep":
                    if (aktuellerSpieler.JepJep == null) { aktuellerSpieler.JepJep = BerechneJepJep(); await ZugBeendenAsync(); }
                    break;
                case "Chance":
                    if (aktuellerSpieler.Chance == null) { aktuellerSpieler.Chance = BerechneChance(); await ZugBeendenAsync(); }
                    break;
            }
        }

        // Komplettes Spiel zurücksetzen
        [RelayCommand]
        public void SpielZuruecksetzen()
        {
            Spieler1 = new Ergebnisse { PlayerName = Spieler1.PlayerName };
            Spieler2 = new Ergebnisse { PlayerName = Spieler2.PlayerName };
            IsPlayerOneTurn = true;
            WurfZaehler = 0;

            foreach (var wuerfel in WuerfelListe)
            {
                wuerfel.IstGesperrt = false;
                wuerfel.Wert = 1;
            }
        }


        // =========================================================================
        // SPIELABLAUF & SPIELENDE
        // =========================================================================

        private async Task ZugBeendenAsync()
        {
            // 1. Würfel und Zähler zurücksetzen
            WurfZaehler = 0;
            foreach (var wuerfel in WuerfelListe)
            {
                wuerfel.IstGesperrt = false;
                wuerfel.Wert = 1;
            }

            // 2. Prüfen, ob beide Spieler alle 13 Felder ausgefüllt haben
            if (Spieler1.IstVollstaendig && Spieler2.IstVollstaendig)
            {
                await SpielendeAuswertenAsync();
                return;
            }

            // 3. Spieler wechseln
            IsPlayerOneTurn = !IsPlayerOneTurn;
        }

        private async Task SpielendeAuswertenAsync()
        {
            string gewinnerName;
            string ergebnisText;

            if (Spieler1.Endsumme > Spieler2.Endsumme)
            {
                gewinnerName = Spieler1.PlayerName;
                ergebnisText = $"{Spieler1.PlayerName} gewinnt mit {Spieler1.Endsumme} zu {Spieler2.Endsumme} Punkten! 🏆";
            }
            else if (Spieler2.Endsumme > Spieler1.Endsumme)
            {
                gewinnerName = Spieler2.PlayerName;
                ergebnisText = $"{Spieler2.PlayerName} gewinnt mit {Spieler2.Endsumme} zu {Spieler1.Endsumme} Punkten! 🏆";
            }
            else
            {
                gewinnerName = "Unentschieden";
                ergebnisText = $"Unentschieden! Beide Spieler haben {Spieler1.Endsumme} Punkte erreicht. 🤝";
            }

            // 1. In JSON-Datei speichern
            var ergebnis = new SpielErgebnis
            {
                Datum = DateTime.Now,
                Spieler1Name = Spieler1.PlayerName,
                Spieler1Punkte = Spieler1.Endsumme,
                Spieler2Name = Spieler2.PlayerName,
                Spieler2Punkte = Spieler2.Endsumme,
                Gewinner = gewinnerName
            };
            await StatistikService.SpeichereErgebnisAsync(ergebnis);

            // 2. Dialog anzeigen
            if (Shell.Current != null)
            {
                bool neuesSpiel = await Shell.Current.DisplayAlert(
                    "Spiel beendet!",
                    $"{ergebnisText}\n\nMöchtest du eine neue Runde starten?",
                    "Neues Spiel",
                    "Abbrechen");

                if (neuesSpiel)
                {
                    SpielZuruecksetzen();
                }
            }
        }


        // =========================================================================
        // HILFSMETHODEN FÜR DIE PUNKTEBERECHNUNG
        // =========================================================================

        private int BerechneObereReihe(int augenzahl)
        {
            return WuerfelListe.Where(w => w.Wert == augenzahl).Sum(w => augenzahl);
        }

        private int SummeAllerWuerfel()
        {
            return WuerfelListe.Sum(w => w.Wert);
        }

        private int BerechneDreierPasch()
        {
            bool hatDreierPasch = WuerfelListe.GroupBy(w => w.Wert).Any(gruppe => gruppe.Count() >= 3);
            return hatDreierPasch ? SummeAllerWuerfel() : 0;
        }

        private int BerechneViererPasch()
        {
            bool hatViererPasch = WuerfelListe.GroupBy(w => w.Wert).Any(gruppe => gruppe.Count() >= 4);
            return hatViererPasch ? SummeAllerWuerfel() : 0;
        }

        private int BerechneFullHouse()
        {
            var gruppen = WuerfelListe.GroupBy(w => w.Wert).ToList();
            bool istFullHouse = (gruppen.Count == 2 && (gruppen[0].Count() == 3 || gruppen[0].Count() == 2))
                                || gruppen.Count == 1;

            return istFullHouse ? 25 : 0;
        }

        private int BerechneKleineStrasse()
        {
            var sortierteWerte = WuerfelListe.Select(w => w.Wert).Distinct().OrderBy(x => x).ToList();
            string zahlenfolge = string.Concat(sortierteWerte);

            if (zahlenfolge.Contains("1234") || zahlenfolge.Contains("2345") || zahlenfolge.Contains("3456"))
            {
                return 30;
            }

            return 0;
        }

        private int BerechneGrosseStrasse()
        {
            var sortierteWerte = WuerfelListe.Select(w => w.Wert).Distinct().OrderBy(x => x).ToList();
            string zahlenfolge = string.Concat(sortierteWerte);

            if (zahlenfolge == "12345" || zahlenfolge == "23456")
            {
                return 40;
            }

            return 0;
        }

        private int BerechneJepJep()
        {
            bool istKniffel = WuerfelListe.GroupBy(w => w.Wert).Any(gruppe => gruppe.Count() == 5);
            return istKniffel ? 50 : 0;
        }

        private int BerechneChance()
        {
            return SummeAllerWuerfel();
        }
    }
}