using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace tswiftLyricGen {

  public class MainProgram {
    public static async Task Main(string[] args) {

      var apiSetup = new APISetup();
      await apiSetup.CollectLyrics();

    }
  }

  public class APISetup {

    Uri baseAddress = new Uri("https://api.lyrics.ovh/v1/");

    public async Task CollectLyrics(){

      using (var APIClient = new HttpClient{ BaseAddress = baseAddress })
      {

        using(var response = await APIClient.GetAsync("{Taylor Swift}/{Love Story}"))
        {
          string responseData = await response.Content.ReadAsStringAsync();
          Console.WriteLine(responseData);
        }
      }
    }
  }
}
