using System.Collections.ObjectModel;

namespace ShopListOP.Views;

public partial class ShowProducts : ContentPage
{
	public ObservableCollection<Product> Products { get; set; }
	public ShowProducts()
	{
		InitializeComponent();
		RefreshProducts();
		BindingContext = this;
	}

	private void RefreshProducts()
	{
		Products = new ObservableCollection<Product>(
			Data.Categories
				.SelectMany(c => c.Products)
				.Where(product => !product.IsBought)
			);
	}
	private void OnProductBought(object sender, CheckedChangedEventArgs e)
	{
		if (sender is CheckBox checkBox && checkBox.BindingContext is Product product)
		{
			product.IsBought = e.Value;
			if (product.IsBought)
			{
				Products.Remove(product);
			}
			Data.SaveData();
		}
	}
}