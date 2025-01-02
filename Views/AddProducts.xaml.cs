using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace ShopListOP.Views;


public partial class AddProducts : ContentPage
{
    public ObservableCollection<Category> Categories { get; set; }
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "shopping_list.xml");

    public AddProducts()
    {
        InitializeComponent();
        Categories = Data.Categories;
        BindingContext = this;
    }


    private void OnAddProductClicked(object sender, EventArgs e)
    {
        var newProductName = NewProductEntry.Text;
        var newProductUnit = NewUnitEntry.Text;
        var newProductAmount = 0;
        bool success = int.TryParse(NewAmountEntry.Text, out newProductAmount);
        var selectedCategory = CategoryPicker.SelectedItem as Category;

        if (!success)
        {
            newProductAmount = 0;
        }

        if (string.IsNullOrWhiteSpace(newProductUnit))
        {
            newProductUnit = "szt.";
        }

        if (selectedCategory != null && selectedCategory.Products.Any(p => p.Name == newProductName))
        {
            DisplayAlert("Blad", "Produkt o tej nazwie jest juz na twojej liscie zakupow", "OK");
            return;
        }


        if (!string.IsNullOrWhiteSpace(newProductName) && selectedCategory != null)
        {
            selectedCategory.Products.Add(new Product(
                newProductName,
                newProductAmount,
                newProductUnit,
                false
            ));
            Data.SaveData();
            NewProductEntry.Text = string.Empty;
        }
        else
        {
            DisplayAlert("Blad", "Upewnij sie, ze wpisano nazwe produktu i wybrano kategorie", "OK");
        }
    }

    private void OnAddCategoryClicked(object sender, EventArgs e)
    {
        var newCategoryName = NewCategoryEntry.Text;

        if (Data.Categories.Any(Categories => Categories.Name == newCategoryName))
        {
            DisplayAlert("Blad", "Kategoria o tej nazwie jest juz na twojej liscie zakupow", "OK");
            return;
        }

        if (!string.IsNullOrWhiteSpace(NewCategoryEntry.Text))
        {
            Data.Categories.Add(new Category { Name = NewCategoryEntry.Text });
            Data.SaveData();
            NewCategoryEntry.Text = string.Empty;
        }
    }
}
