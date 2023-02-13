
namespace ECommerce.Pages
{
    public partial class CartPage : EShopPage
    {
        private const int ECO_TAX = 2;
        private const int VAT_TAX = 20;

        public CartPage(Driver driver)
            : base(driver)
        {
        }

        public double TotalPriceProducts()
        {
            double total = 0;

            foreach (var quantity in ProductsQuantity())
            {
                foreach (var price in ProductsPrice())
                {
                    total += quantity * price;
                }
            }

            return total;
        }

        public void UpdateProductQuantityInCart(int newQuantity, string product)
        {
            QuentityField(product).Clear();
            QuentityField(product).SendKeys(newQuantity.ToString());
            _driver.WaitUntilPageLoadsCompletely();
            UpdateButton(product).Click();
        }

        public void FillEstimateShippingTaxes(Country country, string postCode)
        {
            CountryShippingTaxes.Click();
            CountryOption(country).Click();
            RegionStateShippingTaxes.Click();
            _driver.WaitForAjax();

            try
            {
                RegionOption("1").Click();
            }
            catch (WebDriverTimeoutException)
            {
                RegionOption().Click();
            }

            PostCodeSatateShippingTaxes.SendKeys(postCode);
        }

        public List<double> GetPrices()
        {
            List<double> prices = new List<double>();
            prices.Add(double.Parse(SelectCartPriceInformation(TablePrice.SubTotal).Text.Remove(0, 1)));
            prices.Add(double.Parse(SelectCartPriceInformation(TablePrice.FlatShippingRate).Text.Remove(0, 1)));
            prices.Add(double.Parse(SelectCartPriceInformation(TablePrice.Total).Text.Remove(0, 1)));

            return prices;
        }

        public void EnterGiftCertificate(GiftSertificateTheme giftTheme)
        {
            VoucherInput.SendKeys(giftTheme.ToString());
        }

        public List<string> ShoppingTableContent()
        {
            var list = new List<string>();

            foreach (var item in TableRowsContent)
            {
                foreach (var cell in item.FindElements(By.TagName("td")))
                {
                    list.Add(cell.Text);
                }
            }

            return list;
        }

        private List<int> ProductsQuantity()
        { 
            var list = new List<int>();

            foreach (var item in TableProductQuantity)
            {
                foreach (var cell in item.FindElements(By.TagName("input")))
                {
                    list.Add(int.Parse(cell.GetAttribute("value")));
                }
            }

            return list;
        }

        private List<double> ProductsPrice() 
        {
            var list = new List<double>();

            foreach (var item in TableProductUnitPrice)
            {
                var temp = item.Text.Remove(0, 1);
                list.Add(double.Parse(temp));
            }

            return list;
        }

        private double EstimateEcoTax()
        {
            var ecoTax = CountOfProducts() * ECO_TAX;
            
            return ecoTax;
        }

        private double EstimateVAT()
        { 
            var subTotal = SelectCartTotalInfo(TablePrice.SubTotal).Text.Remove(0, 1);
            var vat = (double.Parse(subTotal) * VAT_TAX) / 100;


            return vat;
        }

        private int CountOfProducts()
        {
            var count = 0;

            foreach (var item in ProductsQuantity())
            {
                count += item;
            }

            return count;
        }
    }
}