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
    /// Logika interakcji dla klasy EditWarehouseProducts.xaml
    /// </summary>
    public partial class EditWarehouseProduct : Page
    {
        private int _productId;
        private int _warehouseId;
        private ProductsViewModel _productsViewModel;
        private WarehousesViewModel _warehousesViewModel;
        private WarehouseProductsViewModel _warehouseProductsViewModel;
        private InventoryContext _context;
        private ProductRepository _productRepository;
        private WarehouseRepository _warehouseRepository;
        private WarehouseProductRepository _warehouseProductRepository;
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text

        public EditWarehouseProduct(int productId, int warehouseId)
        {
            InitializeComponent();
            _productId = productId;
            _warehouseId = warehouseId;

            _context = new InventoryContext();
            _productRepository = new ProductRepository(_context);
            _warehouseRepository = new WarehouseRepository(_context);
            _warehouseProductRepository = new WarehouseProductRepository(_context);
            _productsViewModel = new ProductsViewModel(_productRepository);
            _warehousesViewModel = new WarehousesViewModel(_warehouseRepository);
            _warehouseProductsViewModel = new WarehouseProductsViewModel(_warehouseProductRepository);

            WarehouseProduct warehouseProduct = _warehouseProductRepository.GetById(productId, warehouseId);

            List<KeyValuePair<int, string>> products = _productsViewModel.GetAllProducts().Select(x => new KeyValuePair<int, string>(x.ProductId, $"{x.Name}; {x.Sku}; {x.Price:C}")).ToList();
            KeyValuePair<int, string> selectedProduct = products.FirstOrDefault(x => x.Key == productId);
            ProductCb.DisplayMemberPath = "Value";
            ProductCb.SelectedValuePath = "Key";
            ProductCb.DataContext = products;
            ProductCb.SelectedIndex = products.IndexOf(selectedProduct);

            List<KeyValuePair<int, string>> warehouses = _warehousesViewModel.GetAllWarehouses().Select(x => new KeyValuePair<int, string>(x.WarehouseId, $"{x.Name}, {x.City}")).ToList();
            KeyValuePair<int, string> selectedWarehouse = warehouses.First(x => x.Key == warehouseId);
            WarehouseCb.DisplayMemberPath = "Value";
            WarehouseCb.SelectedValuePath = "Key";
            WarehouseCb.DataContext = warehouses;
            WarehouseCb.SelectedIndex = warehouses.IndexOf(selectedWarehouse);

            QuantityTb.Text = warehouseProduct.Quantity.ToString();
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

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WarehouseProduct product = _warehouseProductRepository.GetById(_productId, _warehouseId);

                product.WarehouseId = (int)WarehouseCb.SelectedValue;
                product.Quantity = Convert.ToInt32(QuantityTb.Text);
                product.ProductId = (int)ProductCb.SelectedValue;

                _context.SaveChanges();
            }
            catch
            {

            }
            finally
            {
                QuantityTb.Clear();
            }

            NavigationService.Navigate(new Warehouses(_warehouseId));
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Warehouses(_warehouseId));
        }
    }
}
