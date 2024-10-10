using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace KoloskovTrade
{
    /// <summary>
    /// Логика взаимодействия для TradePage.xaml
    /// </summary>
    public partial class TradePage : Page
    {
        int CountInPage = 10;
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Product> CurrentPageList = new List<Product>();
        List<Product> TableList;

        public TradePage()
        {
            InitializeComponent();
            var currentProduct = Koloskov_TradeEntities.GetContext().Product.ToList();
            TradeListView.ItemsSource = currentProduct;

            TBAllRecords.Text = Koloskov_TradeEntities.GetContext().Product.ToList().Count().ToString();
            SortBox.SelectedIndex = 0;
            FiltrBox.SelectedIndex = 0;
            Update();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void FiltrBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            var currentProduct = Koloskov_TradeEntities.GetContext().Product.ToList();

            if (SortBox.SelectedIndex == 1)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            else if (SortBox.SelectedIndex == 2)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }

            if (FiltrBox.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10)).ToList();
            }
            else if (FiltrBox.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15)).ToList();
            }
            else if (FiltrBox.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 15)).ToList();
            }

            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            TBAllRecords.Text = Koloskov_TradeEntities.GetContext().Product.ToList().Count().ToString();
            TBCount.Text = currentProduct.Count().ToString();
            TradeListView.ItemsSource = currentProduct;
        }
    }
}
