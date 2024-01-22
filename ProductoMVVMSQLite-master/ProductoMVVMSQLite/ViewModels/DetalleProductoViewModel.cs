using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
using PropertyChanged;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DetalleProductoViewModel
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public ImageSource ImagenProducto { get; set; }

        public DetalleProductoViewModel(Producto producto)
        {
            Nombre = producto.Nombre;
            Cantidad = producto.Cantidad;
            Descripcion = producto.Descripcion;
            ImagenProducto = ImageSource.FromFile(producto.Imagen);
        }
    }
}
