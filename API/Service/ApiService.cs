using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Data;
using Microsoft.Extensions.Configuration;

namespace API.Service
{
  public class ApiService
  {
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public ApiService(IConfiguration configuration, HttpClient httpClient)
    {
      _configuration = configuration;
      _httpClient = httpClient;
    }

    public async Task<string> GetSongsFromExternalApi()
    {
      string apiUrl = _configuration.GetSection("Api").GetSection("Url").Value;
      string apiKey = _configuration.GetSection("Api").GetSection("Key").Value;

      var response = await _httpClient.GetAsync($"{apiUrl}?method=chart.gettoptracks&country=sweden&api_key={apiKey}&format=json&limit=10");

      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine(response);
        return await response.Content.ReadAsStringAsync();
      }
      else
      {
        Console.WriteLine(response);
        return response.Content.ReadAsStringAsync().Result;
      }
    }

    public async Task<string> GetGenreOfASong(string song, string artist)
    {
      string apiUrl = _configuration.GetSection("Api").GetSection("Url").Value;
      string apiKey = _configuration.GetSection("Api").GetSection("Key").Value;

      var response = await _httpClient.GetAsync($"{apiUrl}?method=track.getInfo&api_key={apiKey}&artist={artist}&track={song}&format=json&autocorrect=1");

      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine(response);
        return await response.Content.ReadAsStringAsync();
      }
      else
      {
        Console.WriteLine(response);
        return response.Content.ReadAsStringAsync().Result;
      }
    }
  }

}