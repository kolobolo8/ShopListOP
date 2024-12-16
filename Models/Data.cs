using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public static class Data
{
    public static ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
    private static readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "shopping_list.xml");

    static Data()
    {
        LoadData();
    }

    public static void SaveData()
    {
        using (var writer = new StreamWriter(_filePath))
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Category>));
            serializer.Serialize(writer, Categories);
        }
    }

    public static void LoadData()
    {

        if (File.Exists(_filePath))
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Category>));
            using (var reader = new StreamReader(_filePath))
            {
                Categories = (ObservableCollection<Category>)serializer.Deserialize(reader);
            }
        }
        else
        {
            var exampleCategory1 = new Category
            {
                Name = "Produkty spożywcze",
                Products = new ObservableCollection<Product>
                    {
                        new Product { Name = "Chleb", Amount = 1, Unit = "szt", IsBought = false },
                        new Product { Name = "Masło", Amount = 1, Unit = "kostka", IsBought = false },
                        new Product { Name = "Mleko", Amount = 2, Unit = "l", IsBought = false }
                    }
            };

            var exampleCategory2 = new Category
            {
                Name = "Owoce",
                Products = new ObservableCollection<Product>
                    {
                        new Product { Name = "Jabłka", Amount = 6, Unit = "szt", IsBought = false },
                        new Product { Name = "Banany", Amount = 5, Unit = "szt", IsBought = false },
                        new Product { Name = "Pomarańcze", Amount = 4, Unit = "szt", IsBought = false }
                    }
            };

            Categories.Add(exampleCategory1);
            Categories.Add(exampleCategory2);

            SaveData();
        }
    }

}



