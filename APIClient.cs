using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace tswiftLyricGen {

  public class MainProgram {
    public static async Task Main(string[] args) {

      var api = new APISetup();
      await api.CollectLyrics("{Billy Joel}/{Piano man}");

    }

    public static string whichArtist() {

      return "";
    }
  }

  public class APISetup {

    Uri baseAddress = new Uri("https://api.lyrics.ovh/v1/");

    public async Task CollectLyrics(string artistSong){

      using (var APIClient = new HttpClient{ BaseAddress = baseAddress })
      {

        using(var response = await APIClient.GetAsync(artistSong))
        {
          string jsonResponse = await response.Content.ReadAsStringAsync();

          var jsonDocument = JsonDocument.Parse(jsonResponse);
          var root = jsonDocument.RootElement;

          if (root.TryGetProperty("lyrics", out JsonElement lyricsElement))
            {
              string? lyrics = lyricsElement.GetString();
              Console.WriteLine(lyrics);
            }
            else
            {
              Console.WriteLine("No lyrics found in the response.");
            }
        }
      }
    }
  }
}
