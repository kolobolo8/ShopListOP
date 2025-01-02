namespace ShopListOP.Views;

public partial class ProductView : ContentView
{
    public ProductView()
    {
        InitializeComponent();
    }

    private void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product product)
        {
            product.Amount++;
            Data.SaveData();
        }
    }

    private void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product product && product.Amount > 0)
        {
            product.Amount--;
            Data.SaveData();
        }
    }

    private void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (BindingContext is Product product)
        {
            var category = Data.Categories.FirstOrDefault(c => c.Products.Contains(product));
            category?.Products.Remove(product);
            Data.SaveData();
        }
    }

    private void OnToggleBoughtClicked(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Product product)
        {
            product.IsBought = e.Value;
            if (checkBox.Parent is HorizontalStackLayout hSL)
            {
                var nameClr = hSL.Children.OfType<Label>().FirstOrDefault();
                nameClr.TextColor = product.IsBought ? Colors.Gray : Colors.White;
            }
            Data.SaveData();
        }
    }
}
