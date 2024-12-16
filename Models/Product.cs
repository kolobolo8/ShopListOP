using Microsoft.Maui.Controls;
using System.Xml.Serialization;

public class Product : BindableObject
{
    private int _amount;
    private bool _isBought;

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Unit")]
    public string Unit { get; set; }

    [XmlElement("Amount")]
    public int Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
    }

    [XmlElement("IsBought")]
    public bool IsBought
    {
        get => _isBought;
        set
        {
            if (_isBought != value)
            {
                _isBought = value;
                OnPropertyChanged();
            }
        }
    }

    public Product() { }

    public Product(string name, int amount, string unit, bool isBought)
    {
        Name = name;
        Amount = amount;
        Unit = unit;
        IsBought = isBought;
    }
}
