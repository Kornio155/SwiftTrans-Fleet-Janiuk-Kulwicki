namespace FleetManager.Models;

public class Vehicle
{
    public string Marka { get; set; } = string.Empty;
    public string Model { get; set; } =  string.Empty;
    public int RokProdukcji { get; set; } = 1990;
    public int IloscPaliwa { get; set; } = 5;
    public string StatusPojazdu { get; set; } = "W garażu";
    
    public string KolorTla
    {
        get
        {
            if (IloscPaliwa < 15)
                return "#8a2b34";        

            if (IloscPaliwa <= 30)
                return "#c49618";          

            return "#347527";       
        }
    }
}