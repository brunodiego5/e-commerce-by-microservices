using System.Net.Http.Headers;
using System.Text.Json;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Infrastructure.Services
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly HttpClient _httpClient;
        private static string productCatalogBaseUrl = @"https://git.io/JeHiE";
        private static string getProductPathTemplate = "?productIds=[{0}]";
        private ILogger<ProductCatalogClient> _logger;

        public ProductCatalogClient(HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductCatalogClient>();

            httpClient.BaseAddress = new Uri(productCatalogBaseUrl);
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productIds)
        {
            using var response = await RequestProductFromProductCatalog(productIds);

            return await ConvertToShoppingCartItems(response);
        }

        private async Task<HttpResponseMessage> RequestProductFromProductCatalog(
            int[] productCatalogIds)
        {
            var productsResource = string.Format(getProductPathTemplate,
                string.Join(",", productCatalogIds));

            var response = await _httpClient.GetAsync(productsResource);

            _logger.LogInformation($"Content response: '{await response.Content.ReadAsStringAsync()}'.");
            
            return response;
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(
            HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var products = await JsonSerializer.DeserializeAsync<List<ProductCatalogProduct>>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
            
            return products
                .Select(
                    product => new ShoppingCartItem(
                        product.ProductId,
                        product.ProductName,
                        product.ProductDescription,
                        product.Price));
        }

        private record ProductCatalogProduct
        (
            int ProductId,
            string ProductName,
            string ProductDescription,
            Money Price

        );

    }
}