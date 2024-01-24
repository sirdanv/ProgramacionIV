using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
using ProductoMVVMSQLite.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProductoViewModel
    {
        public ObservableCollection<Producto> ListaProductos { get; set; }

        public Producto ProductoSeleccionado {get;set;}

        public ProductoViewModel() {
            Util.ListaProductos = new ObservableCollection<Producto>(App.productoRepository.GetAll());

            ListaProductos = Util.ListaProductos;
        
        }

        public ICommand CrearProducto =>
            new Command(async () =>
            {
               await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage());
            });

        public ICommand EditarProducto =>
            new Command(async () =>
            {
                if (ProductoSeleccionado != null)
                {
                    int IdProducto = ProductoSeleccionado.IdProducto;
                    //ProductoSeleccionado = null;
                    await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage(IdProducto));
                    ProductoSeleccionado = null;
                }
              

            });
        public ICommand ModificarDesdeBotonCommand =>
            new Command<Producto>(async (producto) =>
            {
                if (producto != null)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage(producto.IdProducto));
                }
            });

        public ICommand EliminarDesdeBotonCommand =>
            new Command<Producto>(async (producto) =>
            {
                if (producto != null)
                {
                    bool confirmarEliminar = await App.Current.MainPage.DisplayAlert("Eliminar", $"¿Eliminar {producto.Nombre}?", "Sí", "No");

                    if (confirmarEliminar)
                    {
                        App.productoRepository.Delete(producto);
                        Util.ListaProductos.Remove(producto);
                    }
                }
            });

        public ICommand VerProductoDesdeBotonCommand =>
            new Command<Producto>(async (producto) =>
            {
                if (producto != null)
                {
                    var detalleProductoViewModel = new DetalleProductoViewModel(producto);
                    var detalleProductoPage = new DetalleProductoPage { BindingContext = detalleProductoViewModel };
                    await App.Current.MainPage.Navigation.PushAsync(detalleProductoPage);
                }
            });





    }
}
