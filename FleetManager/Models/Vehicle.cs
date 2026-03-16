namespace FleetManager.Models;

public class Vehicle
{
    public string Marka { get; set; } = string.Empty;
    public string Model { get; set; } =  string.Empty;
    public int RokProdukcji { get; set; } = 1990;
    public string IloscPaliwa { get; set; } = "5%";
    public string StatusPojazdu { get; set; } = "W garażu";
}