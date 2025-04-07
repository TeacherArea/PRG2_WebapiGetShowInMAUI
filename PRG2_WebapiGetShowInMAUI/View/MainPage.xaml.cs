using System.Diagnostics;
using System.Text.Json;
using PRG2_WebapiGetShowInMAUI.Model;

namespace PRG2_WebapiGetShowInMAUI
{
    public partial class MainPage : ContentPage
    {
        int productAmount = 3;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var products = await GetProductsAsync(productAmount);
            ProductsView.ItemsSource = products;
        }

        private async void ButtonProductAmount(object sender, EventArgs e)
        {
            if (int.TryParse(EntryProductAmount.Text, out int productAmount))
            {
                var products = await GetProductsAsync(productAmount);
                ProductsView.ItemsSource = products;
                EntryProductAmount.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Warning", "Please ad an integer, max 20", "Ok");
            }
        }
        public async Task<List<Product>> GetProductsAsync(int limit)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://fakestoreapi.com/products?limit={limit}");
            return JsonSerializer.Deserialize<List<Product>>(response);
        }
    }
}
