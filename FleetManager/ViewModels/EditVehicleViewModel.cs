using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;

public class EditVehicleViewModel : ReactiveObject
{
    private Vehicle _vehicle;

    public string Marka { get; set; }
    public string Model { get; set; }
    public int RokProdukcji { get; set; }
    public int IloscPaliwa { get; set; }
    public string StatusPojazdu { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public List<string> Status { get; } = new()
    {
        "W garażu",
        "W trasie",
        "W serwisie"
    };

    public EditVehicleViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;

        Marka = vehicle.Marka;
        Model = vehicle.Model;
        RokProdukcji = vehicle.RokProdukcji;
        IloscPaliwa = vehicle.IloscPaliwa;
        StatusPojazdu = vehicle.StatusPojazdu;

        SaveCommand = ReactiveCommand.Create(Save);
    }

    private void Save()
    {
        _vehicle.Marka = Marka;
        _vehicle.Model = Model;
        _vehicle.RokProdukcji = RokProdukcji;
        _vehicle.IloscPaliwa = IloscPaliwa;
        _vehicle.StatusPojazdu = StatusPojazdu;
    }
}