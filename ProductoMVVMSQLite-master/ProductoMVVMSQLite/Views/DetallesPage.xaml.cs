using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.ViewModels;

namespace ProductoMVVMSQLite.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(Producto producto)
	{
		InitializeComponent();
        BindingContext = new DetallesViewModel(producto);
    }
}