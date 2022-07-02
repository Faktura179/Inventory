using Inventory.Data;
using Inventory.Models;
using Inventory.Repositories;
using Inventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logika interakcji dla klasy Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        private ProductsViewModel _productsViewModel;
        private ProductPropertiesViewModel _productPropertiesViewModel;
        private InventoryContext _context;
        private ProductRepository _productRepository;
        private ProductPropertyRepository _productProppertyRepository;
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        public Products()
        {
            InitializeComponent();

            _context = new InventoryContext();
            _productRepository = new ProductRepository(_context);
            _productProppertyRepository = new ProductPropertyRepository(_context);
            _productsViewModel = new ProductsViewModel(_productRepository);
            _productPropertiesViewModel = new ProductPropertiesViewModel(_productProppertyRepository);

            ProductsGrid.DataContext = _productsViewModel.GetAllProducts();
            ProductPropertyCb.DisplayMemberPath = "Value";
            ProductPropertyCb.SelectedValuePath = "Key";
            ProductPropertyCb.DataContext = _productPropertiesViewModel.GetAllProperties().Select(x => new KeyValuePair<int, string>(x.ProductPropertyId, $"{x.Name} - {x.Value}"));
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

        private void ProductPriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    Name = ProductNameTb.Text,
                    Price = Convert.ToDecimal(ProductPriceTb.Text, new CultureInfo("en-US")),
                    Sku = ProductSkuTb.Text,
                    ProductPropertyId = (int)ProductPropertyCb.SelectedValue
                };

                _productRepository.Add(product);

                _context.SaveChanges();
            }
            catch
            {

            }
            finally
            {
                ProductNameTb.Clear();
                ProductPriceTb.Clear();
                ProductSkuTb.Clear();
            }

            ProductsGrid.ItemsSource = null;
            ProductsGrid.ItemsSource = _productsViewModel.GetAllProducts();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;

            if (product is not null)
            {
                _productRepository.Remove(_productRepository.GetById(product.ProductId));

                _context.SaveChanges();
            }

            ProductsGrid.ItemsSource = null;
            ProductsGrid.ItemsSource = _productsViewModel.GetAllProducts();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;

            if (product is not null)
            {
                this.NavigationService.Navigate(new EditProduct(product.ProductId));
            }
        }
    }
}
