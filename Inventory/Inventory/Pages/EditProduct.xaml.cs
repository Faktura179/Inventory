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
    /// Logika interakcji dla klasy EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        private ProductRepository _productRepository;
        private InventoryContext _context;
        private ProductPropertyRepository _productProppertyRepository;
        private ProductPropertiesViewModel _productPropertiesViewModel;
        private int _productId;
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        public EditProduct(int productId)
        {
            InitializeComponent();
            _productId = productId;

            _context = new InventoryContext();
            _productRepository = new ProductRepository(_context);
            _productProppertyRepository = new ProductPropertyRepository(_context);
            _productPropertiesViewModel = new ProductPropertiesViewModel(_productProppertyRepository);

            Product product = _productRepository.GetById(productId);

            ProductNameTb.Text = product.Name;
            ProductSkuTb.Text = product.Sku;
            ProductPriceTb.Text = product.Price.ToString(new CultureInfo("en-US"));

            List<KeyValuePair<int, string>> propertyListItems = _productPropertiesViewModel.GetAllProperties().Select(x => new KeyValuePair<int, string>(x.ProductPropertyId, $"{x.Name} - {x.Value}")).ToList();
            KeyValuePair<int, string> property = propertyListItems.FirstOrDefault(x => x.Key == product.ProductPropertyId);
            ProductPropertyCb.DisplayMemberPath = "Value";
            ProductPropertyCb.SelectedValuePath = "Key";
            ProductPropertyCb.DataContext = propertyListItems;
            ProductPropertyCb.SelectedIndex = propertyListItems.IndexOf(property);
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
                Product product = _productRepository.GetById(_productId);

                product.Name = ProductNameTb.Text;
                product.Price = Convert.ToDecimal(ProductPriceTb.Text, new CultureInfo("en-US"));
                product.Sku = ProductSkuTb.Text;
                product.ProductPropertyId = (int)ProductPropertyCb.SelectedValue;

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

            NavigationService.Navigate(new Products());
        }
    }
}
