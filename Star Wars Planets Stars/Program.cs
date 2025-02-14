using System;
using System.ComponentModel;
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
var listDiameter = new List<int?>();

foreach (var item in root.results)
{

    string result = item.diameter;
    int convert = int.Parse(result);

    listDiameter.Add(convert);
}
var listPopulation = new List<int?>();
foreach (var item in root.results)
{
    string result = item.population;
    
    if (int.TryParse(result,out int value))
    {
        listPopulation.Add(value);
    }
    else
    {
        listPopulation.Add(null);
    }
}
var listSurfaceWater = new List<int?>();
foreach (var item in root.results)
{
    string result = item.surface_water;
    if(int.TryParse(result,out int value))
    {
        listSurfaceWater.Add(value);
    }
    else
    {
        listSurfaceWater.Add(null);
    }

}
string name = "Name";
string diameter = "Diameter";
string surface = "Surface Level";
string population = "Populations";

Console.WriteLine($"{name,-20}{diameter,-20}{surface,-20}{population,-20}");
Console.WriteLine(new string('-',80));

for(int i = 0; i < listName.Count; i++)
{
    Console.WriteLine($"|{listName[i],-20}|{listDiameter[i],-20}|{listSurfaceWater[i],-20}|{listPopulation[i],-20}");
}
bool IsCorrect = true;

while (IsCorrect)
{

    Console.WriteLine($"Select the statistic you are interested in :\n" +
        $"{nameof(diameter)}\n{nameof(surface)}\n{nameof(population)}");
    var Selection = Console.ReadLine();

    switch (Selection)
    {
        case "diameter":
            int? dmax = listDiameter.Max();
            int? dmin = listDiameter.Min();
            Console.WriteLine($"Maximum diameters is :{dmax} \nMinimum diameters is :{dmin}");
            IsCorrect = false;
            break;
        case "surface":
            int? smax = listSurfaceWater.Max();
            int? smin = listSurfaceWater.Min();
            Console.WriteLine($"Maximum Surface Level is: {smax} \nMinimum Surface Level is :{smin}");
            IsCorrect = false;
            break;
        case "population":
            int? pmax = listPopulation.Max();
            int? pmin = listPopulation.Min();
            Console.WriteLine($"Maximum Populations is : {pmax} \nMinimum diameters is : {pmin}");
            IsCorrect = false;
            break;
        default:
            Console.WriteLine("Please again check and Re-enter\n");
            IsCorrect = true;
            break;
    }

}






Console.WriteLine("Press Any Key To Exit");
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

