using ProductoMVVMSQLite.ViewModels;

namespace ProductoMVVMSQLite.Views;

public partial class ProductoPage : ContentPage
{
	public ProductoPage()
	{
		InitializeComponent();
		BindingContext = new ProductoViewModel();
	}

    private void listaProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}