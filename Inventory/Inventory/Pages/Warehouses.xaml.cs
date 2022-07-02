using Inventory.Data;
using Inventory.Models;
using Inventory.Repositories;
using Inventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventory.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Warehouses.xaml
    /// </summary>
    public partial class Warehouses : Page
    {
        private ProductsViewModel _productsViewModel;
        private WarehousesViewModel _warehousesViewModel;
        private WarehouseProductsViewModel _warehouseProductsViewModel;
        private InventoryContext _context;
        private ProductRepository _productRepository;
        private WarehouseRepository _warehouseRepository;
        private WarehouseProductRepository _warehouseProductRepository;
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text

        private int _warehouseId;

        public Warehouses()
        {
            InitializeComponent();

            _context = new InventoryContext();
            _productRepository = new ProductRepository(_context);
            _warehouseRepository = new WarehouseRepository(_context);
            _warehouseProductRepository = new WarehouseProductRepository(_context);
            _productsViewModel = new ProductsViewModel(_productRepository);
            _warehousesViewModel = new WarehousesViewModel(_warehouseRepository);
            _warehouseProductsViewModel = new WarehouseProductsViewModel(_warehouseProductRepository);

            ProductCb.DisplayMemberPath = "Value";
            ProductCb.SelectedValuePath = "Key";
            ProductCb.DataContext = _productsViewModel.GetAllProducts().Select(x => new KeyValuePair<int, string>(x.ProductId, $"{x.Name}; {x.Sku}; {x.Price.ToString("C")}"));

            WarehouseCb.DisplayMemberPath = "Value";
            WarehouseCb.SelectedValuePath = "Key";
            WarehouseCb.DataContext = _warehousesViewModel.GetAllWarehouses().Select(x => new KeyValuePair<int, string>(x.WarehouseId, $"{x.Name}, {x.City}"));
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void QuantityTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WarehouseProduct product = new WarehouseProduct()
                {
                    WarehouseId = (int)WarehouseCb.SelectedValue,
                    Quantity = Convert.ToInt32(QuantityTb.Text),
                    ProductId = (int)ProductCb.SelectedValue
                };

                _warehouseProductRepository.Add(product);

                _context.SaveChanges();
            }
            catch
            {

            }
            finally
            {
                QuantityTb.Clear();
            }

            WarehouseGrid.ItemsSource = null;
            WarehouseGrid.ItemsSource = _warehouseProductsViewModel.GetByWarehouseId(_warehouseId);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            WarehouseProduct product = ((FrameworkElement)sender).DataContext as WarehouseProduct;

            if (product is not null)
            {
                _warehouseProductRepository.Remove(_warehouseProductRepository.GetById(product.ProductId, product.WarehouseId));

                _context.SaveChanges();
            }

            WarehouseGrid.ItemsSource = null;
            WarehouseGrid.ItemsSource = _warehouseProductsViewModel.GetByWarehouseId(_warehouseId);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //WarehouseProduct product = ((FrameworkElement)sender).DataContext as WarehouseProduct;

            //if (product is not null)
            //{
            //    this.NavigationService.Navigate(new EditWarehouseProduct(product.ProductId, product.WarehouseId));
            //}
        }

        private void WarehouseCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int warehouseId = ((sender as ComboBox).SelectedItem as KeyValuePair<int, string>?).Value.Key;
                _warehouseId = warehouseId;

                WarehouseGrid.ItemsSource = null;
                WarehouseGrid.ItemsSource = _warehouseProductsViewModel.GetByWarehouseId(warehouseId);
            }
            catch
            {

            }
        }
    }
}
