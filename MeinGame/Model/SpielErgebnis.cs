using System;

namespace MeinGame.Model
{
    public class SpielErgebnis
    {
        public DateTime Datum { get; set; } = DateTime.Now;
        public string Spieler1Name { get; set; } = string.Empty;
        public int Spieler1Punkte { get; set; }
        public string Spieler2Name { get; set; } = string.Empty;
        public int Spieler2Punkte { get; set; }
        public string Gewinner { get; set; } = string.Empty;

        // Hilfseigenschaft für die formatierte Anzeige
        public string DatumFormatiert => Datum.ToString("dd.MM.yyyy HH:mm");
        public string ErgebnisText => $"{Spieler1Name} ({Spieler1Punkte}) vs. {Spieler2Name} ({Spieler2Punkte})";
    }
}