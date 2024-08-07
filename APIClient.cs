using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace tswiftLyricGen {

  public class MainProgram {
    public static async Task Main(string[] args) {

      var api = new APISetup();
      string userPick = userMusic();
      await api.CollectLyrics(userPick);

    }

    public static string userMusic() {

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine("Which artist would you like to choose?");
      string? userArtist = Console.ReadLine();
      Console.WriteLine($"Nice! I love {userArtist} too! What is your favorite song?");
      string? userSong = Console.ReadLine();
      Console.WriteLine($"I know that one by heart!");
      Console.ForegroundColor = ConsoleColor.White;


      return $"{{{userArtist}}}/{{{userSong}}}";
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
