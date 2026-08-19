using System.IO;
using System.Text.Json;
using MeinGame.Model;

namespace MeinGame.Services
{
    public static class StatistikService
    {
        // Sicherer Speicherort für plattformunabhängige App-Daten
        private static readonly string Dateipfad = Path.Combine(FileSystem.AppDataDirectory, "spiel_historie.json");

        public static async Task SpeichereErgebnisAsync(SpielErgebnis ergebnis)
        {
            var liste = await LadeHistorieAsync();
            liste.Add(ergebnis);

            string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(Dateipfad, json);
        }

        public static async Task<List<SpielErgebnis>> LadeHistorieAsync()
        {
            if (!File.Exists(Dateipfad))
            {
                return new List<SpielErgebnis>();
            }

            string json = await File.ReadAllTextAsync(Dateipfad);
            return JsonSerializer.Deserialize<List<SpielErgebnis>>(json) ?? new List<SpielErgebnis>();
        }
    }
}