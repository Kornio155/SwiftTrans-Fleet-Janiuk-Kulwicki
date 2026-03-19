using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using FleetManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private const string Sciezka = "/home/kornel/RiderProjects/SwiftTrans-Fleet-Janiuk-Kulwicki/FleetManager/Assets/vehicles.json";

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public ObservableCollection<Vehicle> Vehicles { get; } = [];
    
    [Reactive] public string NowaMarka {get; set;} = string.Empty;
    [Reactive] public string NowyModel {get; set;} = string.Empty;
    [Reactive] public int NowyRokProdukcji { get; set; } = 0000;
    [Reactive] public string NowaIloscPaliwa {get; set;} = "5%";
    [Reactive] public string NowyStatus {get; set;} = "W garażu";
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    public MainWindowViewModel()
    {
        LoadVehicles();
        
        AddCommand = ReactiveCommand.Create(AddVehicles);
        SaveCommand = ReactiveCommand.Create(SaveToJson);
    }

    private void AddVehicles()
    {
        Vehicles.Add(new Vehicle
        {
            Marka = NowaMarka,
            Model = NowyModel,
            RokProdukcji = NowyRokProdukcji,
            StatusPojazdu = NowyStatus,
            IloscPaliwa = NowaIloscPaliwa
        });
    }

    private void SaveToJson()
    {
        try
        {
            File.WriteAllText(Sciezka, JsonSerializer.Serialize(Vehicles, JsonOptions));
            Console.WriteLine("Zapisane");
        }
        catch
            (Exception exception) when (exception is
                                            IOException or
                                            UnauthorizedAccessException or
                                            JsonException)
        {
            Console.WriteLine($"Save File Error {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unexpected error (report this!): {exception.Message}");
        }
    }

    private void LoadVehicles()
    {
        if (!File.Exists(Sciezka)) return;
        try
        {
            var jsonData = File.ReadAllText(Sciezka);
            var list = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
            Vehicles.Clear();
            if (list == null) return;
            foreach (var vehicle in list)
            {
                Vehicles.Add(vehicle);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Wczytanie pliku nie powiodło się {exception}");
        }
    }
    
}