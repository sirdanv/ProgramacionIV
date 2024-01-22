﻿using ProductoMVVMSQLite.Models;
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
        public Producto _producto;
        public ProductoViewModel() {

            Util.ListaProductos = new ObservableCollection<Producto>(App.productoRepository.GetAll()); ;

            ListaProductos = Util.ListaProductos;
        
        }

        public ICommand CrearProducto =>
            new Command(async () =>
            {
               await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage());
            });
       
        public ICommand EliminarProducto =>
        new Command<Producto>((producto) =>
        {
           
            App.productoRepository.Delete(producto);
            ListaProductos.Remove(producto);
            Util.ListaProductos.Clear();
            App.productoRepository.GetAll().ForEach(Util.ListaProductos.Add);
        });
        public ICommand EditarProducto =>
        new Command<Producto>(async (producto) =>
        {
           
            await App.Current.MainPage.Navigation.PushAsync(new DetailsPage(producto));
        });


        
    }
}
