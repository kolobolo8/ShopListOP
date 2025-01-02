namespace ShopListOP.Views;

public partial class CategoryView : ContentView
{
    public CategoryView()
    {
        InitializeComponent();
    }

    private void OnToggleCategoryClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Category category)
        {
            category.IsExpanded = !category.IsExpanded;

            button.Text = category.IsExpanded ? "Zwin" : "Rozwin";
            Data.SaveData();
        }
    }
}
