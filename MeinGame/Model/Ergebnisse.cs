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
        // BERECHNETE EIGENSCHAFTEN & SUMMEN
        // ==========================================

        // Gibt true zurück, wenn alle 13 Felder einen Wert (ungleich null) haben
        public bool IstVollstaendig =>
            Einser != null && Zweier != null && Dreier != null &&
            Vierer != null && Fuenfer != null && Sechser != null &&
            DreierPasch != null && ViererPasch != null && FullHouse != null &&
            KleineStrasse != null && GrosseStrasse != null && JepJep != null &&
            Chance != null;

        public int GesamtOben => (Einser ?? 0) + (Zweier ?? 0) + (Dreier ?? 0) +
                                 (Vierer ?? 0) + (Fuenfer ?? 0) + (Sechser ?? 0);

        public int Bonus => GesamtOben >= 63 ? 35 : 0;

        public int GesamtUnten => (DreierPasch ?? 0) + (ViererPasch ?? 0) + (FullHouse ?? 0) +
                                  (KleineStrasse ?? 0) + (GrosseStrasse ?? 0) + (JepJep ?? 0) + (Chance ?? 0);

        public int Endsumme => GesamtOben + Bonus + GesamtUnten;

        // Alias für Endsumme (falls im ViewModel 'Gesamtpunkte' aufgerufen wird)
        public int Gesamtpunkte => Endsumme;


        // ==========================================
        // AUTOMATISCHE UI-AKTUALISIERUNG
        // ==========================================
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Verhindert rekursive Endlosschleifen beim Feuern der berechneten Werte
            if (e.PropertyName != nameof(GesamtOben) &&
                e.PropertyName != nameof(Bonus) &&
                e.PropertyName != nameof(GesamtUnten) &&
                e.PropertyName != nameof(Endsumme) &&
                e.PropertyName != nameof(Gesamtpunkte) &&
                e.PropertyName != nameof(IstVollstaendig))
            {
                OnPropertyChanged(nameof(GesamtOben));
                OnPropertyChanged(nameof(Bonus));
                OnPropertyChanged(nameof(GesamtUnten));
                OnPropertyChanged(nameof(Endsumme));
                OnPropertyChanged(nameof(Gesamtpunkte));
                OnPropertyChanged(nameof(IstVollstaendig));
            }
        }
    }
}