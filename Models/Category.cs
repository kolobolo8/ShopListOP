using System.Collections.ObjectModel;
using System.Xml.Serialization;

[XmlRoot("Category")]
public class Category : BindableObject
{
    private bool _isExpanded;

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlArray("Products")]
    [XmlArrayItem("Product")]
    public ObservableCollection<Product> Products { get; set; }

    [XmlElement("IsExpanded")]
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (_isExpanded != value)
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }
    }

    public Category()
    {
        Products = new ObservableCollection<Product>();
    }

    public Category(string name, ObservableCollection<Product> products, bool isExpanded = false)
    {
        Name = name;
        Products = products ?? new ObservableCollection<Product>();
        IsExpanded = isExpanded;
    }
}
