using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MeinGame.Model
{
    public partial class Ergebnisse : ObservableObject
    {
        [ObservableProperty]
        private string _playerName = string.Empty;

        //Die Spielkarte des Spielers

        [ObservableProperty] private int? _einser;
        [ObservableProperty] private int? _zweier;
        [ObservableProperty] private int? _dreier;
        [ObservableProperty] private int? _vierer;
        [ObservableProperty] private int? _fuenfer;
        [ObservableProperty] private int? _sechser;

        // Unterer Teil
        [ObservableProperty] private int? _dreierPasch;
        [ObservableProperty] private int? _viererPasch;
        [ObservableProperty] private int? _fullHouse;
        [ObservableProperty] private int? _kleineStrasse;
        [ObservableProperty] private int? _grosseStrasse;
        [ObservableProperty] private int? _jepJep;
        [ObservableProperty] private int? _chance;

        // Berechnete Eigenschaften
        public int GesamtOben => (Einser ?? 0) + (Zweier ?? 0) + (Dreier ?? 0) +
                                 (Vierer ?? 0) + (Fuenfer ?? 0) + (Sechser ?? 0);

        public int Bonus => GesamtOben >= 63 ? 35 : 0;

        public int GesamtUnten => (DreierPasch ?? 0) + (ViererPasch ?? 0) + (FullHouse ?? 0) +
                                  (KleineStrasse ?? 0) + (GrosseStrasse ?? 0) + (JepJep ?? 0) + (Chance ?? 0);

        public int Endsumme => GesamtOben + Bonus + GesamtUnten;

        // Fängt JEDE Änderung an irgendeinem Feld zentral ab:
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Wenn sich ein Feld geändert hat, die Summen einmal für die UI aktualisieren
            if (e.PropertyName != nameof(GesamtOben) &&
                e.PropertyName != nameof(Bonus) &&
                e.PropertyName != nameof(GesamtUnten) &&
                e.PropertyName != nameof(Endsumme))
            {
                OnPropertyChanged(nameof(GesamtOben));
                OnPropertyChanged(nameof(Bonus));
                OnPropertyChanged(nameof(GesamtUnten));
                OnPropertyChanged(nameof(Endsumme));
            }
        }

    }
}
