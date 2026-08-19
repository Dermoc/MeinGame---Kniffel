using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace MeinGame.Model
{
    public partial class Ergebnisse : ObservableObject
    {
        [ObservableProperty]
        private string _playerName = string.Empty;

        // ==========================================
        // OBERER TEIL (1er bis 6er)
        // ==========================================
        [ObservableProperty] private int? _einser;
        [ObservableProperty] private int? _zweier;
        [ObservableProperty] private int? _dreier;
        [ObservableProperty] private int? _vierer;
        [ObservableProperty] private int? _fuenfer;
        [ObservableProperty] private int? _sechser;

        // ==========================================
        // UNTERER TEIL (Kombinationen)
        // ==========================================
        [ObservableProperty] private int? _dreierPasch;
        [ObservableProperty] private int? _viererPasch;
        [ObservableProperty] private int? _fullHouse;
        [ObservableProperty] private int? _kleineStrasse;
        [ObservableProperty] private int? _grosseStrasse;
        [ObservableProperty] private int? _jepJep;
        [ObservableProperty] private int? _chance;

        // ==========================================
        // BERECHNETE SUMMEN & BONUS
        // ==========================================

        // Summe 1er bis 6er
        public int GesamtOben => (Einser ?? 0) + (Zweier ?? 0) + (Dreier ?? 0) +
                                 (Vierer ?? 0) + (Fuenfer ?? 0) + (Sechser ?? 0);

        // 35 Zusatzpunkte ab genau 63 Punkten im oberen Teil
        public int Bonus => GesamtOben >= 63 ? 35 : 0;

        // Summe oberer Teil inklusive Bonus
        public int GesamtObenMitBonus => GesamtOben + Bonus;

        // Summe unterer Teil
        public int GesamtUnten => (DreierPasch ?? 0) + (ViererPasch ?? 0) + (FullHouse ?? 0) +
                                  (KleineStrasse ?? 0) + (GrosseStrasse ?? 0) + (JepJep ?? 0) + (Chance ?? 0);

        // Gesamtergebnis des Spielers
        public int Endsumme => GesamtObenMitBonus + GesamtUnten;

        // Prüft, ob alle 13 Felder ausgefüllt sind
        public bool IstVollstaendig =>
            Einser != null && Zweier != null && Dreier != null &&
            Vierer != null && Fuenfer != null && Sechser != null &&
            DreierPasch != null && ViererPasch != null && FullHouse != null &&
            KleineStrasse != null && GrosseStrasse != null && JepJep != null &&
            Chance != null;


        // ==========================================
        // AUTOMATISCHE UI-AKTUALISIERUNG
        // ==========================================
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Wenn irgendein Wertungsfeld geändert wurde, aktualisieren wir sofort alle Summen in der GUI
            if (e.PropertyName != nameof(GesamtOben) &&
                e.PropertyName != nameof(Bonus) &&
                e.PropertyName != nameof(GesamtObenMitBonus) &&
                e.PropertyName != nameof(GesamtUnten) &&
                e.PropertyName != nameof(Endsumme) &&
                e.PropertyName != nameof(IstVollstaendig))
            {
                OnPropertyChanged(nameof(GesamtOben));
                OnPropertyChanged(nameof(Bonus));
                OnPropertyChanged(nameof(GesamtObenMitBonus));
                OnPropertyChanged(nameof(GesamtUnten));
                OnPropertyChanged(nameof(Endsumme));
                OnPropertyChanged(nameof(IstVollstaendig));
            }
        }
    }
}