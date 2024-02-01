using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
using ProductoMVVMSQLite.Views;
using PropertyChanged;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NuevoProductoViewModel
    {

        public string Nombre { get; set; }
        public string Cantidad { get; set; }
        public string Descripcion { get; set; }

        private int _idProducto = 0;

        private Producto ProductoEncontrado { get; set; }



        public NuevoProductoViewModel()
        {

        }

        public NuevoProductoViewModel(int IdProducto)
        {
            _idProducto = IdProducto;
            ProductoEncontrado = App.productoRepository.Get(_idProducto);
            Nombre = ProductoEncontrado.Nombre;
            Descripcion = ProductoEncontrado.Descripcion;
            Cantidad = ProductoEncontrado.Cantidad.ToString();

 



        }

        public ICommand GuardarProducto =>
            new Command(async () =>
            {
                if (_idProducto == 0)
                {
                    Producto producto = new Producto
                    {
                        Nombre = Nombre,
                        Descripcion = Descripcion,
                        Cantidad = Int32.Parse(Cantidad),
                    };
                    App.productoRepository.Add(producto);
                }
                else
                {
                    ProductoEncontrado.Nombre = Nombre;
                    ProductoEncontrado.Cantidad = Int32.Parse(Cantidad);
                    ProductoEncontrado.Descripcion = Descripcion;

 

                    App.productoRepository.Update(ProductoEncontrado);
                }

                Util.ListaProductos.Clear();
                App.productoRepository.GetAll().ForEach(Util.ListaProductos.Add);
                await App.Current.MainPage.Navigation.PopAsync();
            });

        public ICommand TomarFoto =>
            new Command(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                    if (photo != null)
                    {
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using Stream source = await photo.OpenReadAsync();
                        using FileStream localFile = File.OpenWrite(localFilePath);
                        Console.WriteLine(localFilePath);
             
                        await source.CopyToAsync(localFile);
                    }
                }
            });

    }
}
