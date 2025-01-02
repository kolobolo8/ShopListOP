using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;

namespace ShopListOP.Views;

public partial class ShowLists : ContentPage
{
    public ObservableCollection<Category> Categories { get; set; }
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "shopping_list.xml");


    public ShowLists()
    {
        InitializeComponent();
        Categories = Data.Categories;
        BindingContext = this;
    }

    private  void OnExportClicked(object sender, EventArgs e)
    {
            try
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Category>));
                var filePath = Path.Combine(FileSystem.CacheDirectory, "shopping_list_export.xml");
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, Data.Categories);
                     DisplayAlert("Sukces", $"Plik zapisany w: {filePath}", "OK");
                }
            }
            catch (Exception ex)
            {
                 DisplayAlert("Blad", $"Nie udalo sie wyeksportowac listy: {ex.Message}", "OK");
            }
        }

    private async void OnImportClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Wybierz plik XML listy zakupowej",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".xml" } }
        })
            });

            if (result != null)
            {
                using (var stream = await result.OpenReadAsync())
                {
                    var serializer = new XmlSerializer(typeof(ObservableCollection<Category>));
                    var importedCategories = (ObservableCollection<Category>)serializer.Deserialize(stream);

                    if (importedCategories != null)
                    {
                        foreach (var importedCategory in importedCategories)
                        {
                            var existingCategory = Data.Categories.FirstOrDefault(c => c.Name == importedCategory.Name);
                            if (existingCategory == null)
                            {
                                Data.Categories.Add(importedCategory);
                            }
                            else
                            {
                                foreach (var importedProduct in importedCategory.Products)
                                {
                                    var existingProduct = existingCategory.Products.FirstOrDefault(p => p.Name == importedProduct.Name);
                                    if (existingProduct == null)
                                    {
                                        existingCategory.Products.Add(importedProduct);
                                    }
                                }

                            }
                        }

                        await DisplayAlert("Sukces", "Pomyslnie zaimportowano liste zakupowa", "OK");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Blad", $"Nie udalo sie zaimportowac listy: {ex.Message}", "OK");
        }
    }



}