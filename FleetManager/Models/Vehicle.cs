using ReactiveUI;

public class Vehicle : ReactiveObject
{
    private string _marka;
    private string _model;
    private int _rokProdukcji;
    private int _iloscPaliwa;
    private string _statusPojazdu;

    public string Marka
    {
        get => _marka;
        set => this.RaiseAndSetIfChanged(ref _marka, value);
    }

    public string Model
    {
        get => _model;
        set => this.RaiseAndSetIfChanged(ref _model, value);
    }

    public int RokProdukcji
    {
        get => _rokProdukcji;
        set => this.RaiseAndSetIfChanged(ref _rokProdukcji, value);
    }

    public int IloscPaliwa
    {
        get => _iloscPaliwa;
        set
        {
            this.RaiseAndSetIfChanged(ref _iloscPaliwa, value);
            this.RaisePropertyChanged(nameof(KolorTla)); 
        }
    }

    public string StatusPojazdu
    {
        get => _statusPojazdu;
        set => this.RaiseAndSetIfChanged(ref _statusPojazdu, value);
    }

    public string KolorTla
    {
        get
        {
            if (IloscPaliwa < 15) return "#8a2b34";
            if (IloscPaliwa <= 30) return "#c49618";
            return "#347527";
        }
    }
}