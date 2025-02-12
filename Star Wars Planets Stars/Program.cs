using System;
using System.Text.Json;
using System.Text.Json.Serialization;

var baseAddress = "https://swapi.dev/api/";
var requistUri = "planets";
Console.WriteLine("\tStat Wars Planets Stats");
Console.WriteLine("Waiting for loading...\n");
IApiDataReader apiDataReader = new ApiDataReader();
var json = await apiDataReader.Read(baseAddress, requistUri);

var root = JsonSerializer.Deserialize<Root>(json);
var listName = new List<string>();

foreach (var item in root.results)
{
    listName.Add(item.name);
}
var listDiameter = new List<string>();

foreach (var item in root.results)
{
    listDiameter.Add(item.diameter);
}
var listPopulation = new List<string>();
foreach (var item in root.results)
{
    listPopulation.Add(item.population);
}
var listSurfaceWater = new List<string>();
foreach (var item in root.results)
{
    listSurfaceWater.Add(item.surface_water);
}
string name = "Name";
string diameter = "Diameter";
string surface = "Surface Level";
string population = "Populations";

Console.WriteLine($"{name,-20}{diameter,-20}{surface,-20}{population,-20}");
Console.WriteLine(new string('-',80));

for(int i = 0; i < listName.Count; i++)
{
    Console.WriteLine($"{listName[i],-20}{listDiameter[i],-20}{listSurfaceWater[i],-20}{listPopulation[i],-20}");
}




Console.ReadLine();

public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri);
}

public class ApiDataReader : IApiDataReader
{
    public async Task<string> Read(string baseAddress, string requestUri)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);
        HttpResponseMessage responce = await client.GetAsync(requestUri);
        responce.EnsureSuccessStatusCode();
        var json = await responce.Content.ReadAsStringAsync();
        return json;
    }
}
public record Root(
        [property: JsonPropertyName("people")] string people,
        [property: JsonPropertyName("planets")] string planets,
        [property: JsonPropertyName("films")] string films,
        [property: JsonPropertyName("species")] string species,
        [property: JsonPropertyName("vehicles")] string vehicles,
        [property: JsonPropertyName("starships")] string starships,
        [property: JsonPropertyName("count")] int count,
        [property: JsonPropertyName("next")] string next,
        [property: JsonPropertyName("previous")] object previous,
        [property: JsonPropertyName("results")] IReadOnlyList<Result> results
    );
public record Result(
        [property: JsonPropertyName("name")] string name,
        [property: JsonPropertyName("rotation_period")] string rotation_period,
        [property: JsonPropertyName("orbital_period")] string orbital_period,
        [property: JsonPropertyName("diameter")] string diameter,
        [property: JsonPropertyName("climate")] string climate,
        [property: JsonPropertyName("gravity")] string gravity,
        [property: JsonPropertyName("terrain")] string terrain,
        [property: JsonPropertyName("surface_water")] string surface_water,
        [property: JsonPropertyName("population")] string population,
        [property: JsonPropertyName("residents")] IReadOnlyList<string> residents,
        [property: JsonPropertyName("films")] IReadOnlyList<string> films,
        [property: JsonPropertyName("created")] DateTime created,
        [property: JsonPropertyName("edited")] DateTime edited,
        [property: JsonPropertyName("url")] string url
    );

