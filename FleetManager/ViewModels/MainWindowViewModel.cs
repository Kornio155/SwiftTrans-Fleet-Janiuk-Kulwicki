using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using Avalonia.Controls;
using FleetManager.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private const string Sciezka = "C:\\Users\\korne\\RiderProjects\\SwiftTrans-Fleet-Janiuk-Kulwicki\\FleetManager\\Assets\\vehicles.json";

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public ObservableCollection<Vehicle> Vehicles { get; } = [];
    public int LiczbaPojazdow => Vehicles.Count;
    public int LiczbaWGarażu => Vehicles.Count(v => v.StatusPojazdu == "W garażu");

    public int LiczbaWTrasie => Vehicles.Count(v => v.StatusPojazdu == "W trasie");

    public int LiczbaWSerwisie => Vehicles.Count(v => v.StatusPojazdu == "W serwisie");
    
    [Reactive] public Vehicle? SelectedVehicle { get; set; }
    
    [Reactive] public string NowaMarka {get; set;} = string.Empty;
    [Reactive] public string NowyModel {get; set;} = string.Empty;
    [Reactive] public int NowyRokProdukcji { get; set; } = 1990;
    [Reactive] public int NowaIloscPaliwa {get; set;} = 5;
    [Reactive] public string NowyStatus {get; set;} = "W garażu";
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public ReactiveCommand<Vehicle, Unit> DeleteCommand { get; }
    public ReactiveCommand<Vehicle, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        LoadVehicles();
        
        Vehicles.CollectionChanged += (_, _) =>
        {
            this.RaisePropertyChanged(nameof(LiczbaPojazdow));
            this.RaisePropertyChanged(nameof(LiczbaWGarażu));
            this.RaisePropertyChanged(nameof(LiczbaWTrasie));
            this.RaisePropertyChanged(nameof(LiczbaWSerwisie));
        };

        
        AddCommand = ReactiveCommand.Create(AddVehicles);
        SaveCommand = ReactiveCommand.Create(SaveToJson);
        DeleteCommand = ReactiveCommand.Create<Vehicle>(DeleteVehicle);
        EditCommand = ReactiveCommand.Create<Vehicle>(OpenEditWindow);
    }

    private void OpenEditWindow(Vehicle vehicle)
    {
        var window = new EditVehicleWindow
        {
            DataContext = new EditVehicleViewModel(vehicle)
        };

        window.Show();
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
        string json = JsonSerializer.Serialize(Vehicles, JsonOptions);

        using (StreamWriter sw = new StreamWriter(Sciezka))
        {
            sw.Write(json);
            Console.WriteLine("Saved changes to JSON");
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
    
    private void DeleteVehicle(Vehicle vehicle)
    {
        if (vehicle != null)
        {
            Vehicles.Remove(vehicle);
        }
    }
}