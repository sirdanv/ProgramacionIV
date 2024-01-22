using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
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
   
    public class DetailsViewModel
    {
        public Producto Producto { get; set; }

        public DetailsViewModel(Producto producto)
        {
            Producto = producto;
            
        }

        public ICommand GuardarProducto =>
            new Command(async () =>
            {
                App.productoRepository.Update(Producto);
                Util.ListaProductos.Clear();
                App.productoRepository.GetAll().ForEach(Util.ListaProductos.Add);
                await App.Current.MainPage.Navigation.PopAsync();
            });


    }
}
