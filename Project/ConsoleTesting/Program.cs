using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ConsoleTesting;
using PatientTest.Controllers.Dtos;
using PatientTest.Models;

using var httpClient = new HttpClient();

var rand = new Random();

for (int i = 0; i < 100; i++)
{
    var patient = new AddPatientTestDto(
        true,
        Gender.Male,
        DateTime.UtcNow.Date - TimeSpan.FromDays(i/4) - TimeSpan.FromMinutes(rand.NextInt64(1, 1439)),
        new (
            "official",
            $"Ivanov{i}",
            new List<string> { $"Ivan{i}", $"Ivanovich{i}" }));
    
    var response = await httpClient.PostAsJsonAsync("http://localhost:5192/Patient/Add", patient);
    if (response.IsSuccessStatusCode)
        Console.WriteLine($"[{i}] Success");
    else
        Console.WriteLine($"[{i}] Error");
}

Console.WriteLine("Done");