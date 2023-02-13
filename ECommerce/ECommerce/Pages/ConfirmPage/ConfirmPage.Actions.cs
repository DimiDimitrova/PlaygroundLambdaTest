
namespace ECommerce.Pages
{
    public partial class ConfirmPage : EShopPage
    {
        public ConfirmPage(Driver driver)
            : base(driver)
        {
        }

        public void ConfirmOrder()
        {
            ConfirmOrderButton.Click();
        }

        private List<double> GetPrices()
        {
            List<double> listPrice = new List<double>()
            {
            double.Parse(ConfirmPrice(TablePrice.SubTotal).Text.Remove(0, 1)),
            double.Parse(ConfirmPrice(TablePrice.FlatShippingRate).Text.Remove(0, 1)),
            double.Parse(ConfirmPrice(TablePrice.EcoTax).Text.Remove(0, 1)),
            double.Parse(ConfirmPrice(TablePrice.VAT).Text.Remove(0, 1))
            };

            return listPrice;
        }

        public List<string> ConfirmProductContent()
        {
            var list = new List<string>();

            foreach (var item in ProductsContent)
            {
                foreach (var cell in item.FindElements(By.TagName("td")))
                {
                    list.Add(cell.Text);
                }
            }

            return list;
        }

        public List<double> UnitPriceList()
        {
            var list = new List<double>();

            foreach (var item in UnitPriceContent)
            {
                list.Add(double.Parse(item.Text.Remove(0, 1)));
            }

            return list;
        }

        public List<int> QuantityList()
        {
            var list = new List<int>();

            foreach (var item in QuantityContent)
            {
                list.Add(int.Parse(item.Text));
            }

            return list;
        }

        public double TotalPriceProducts()
        {
            var quantity = QuantityList();
            var price = UnitPriceList();

            double total = 0;

            for (int i = 0; i < quantity.Count; i++)
            {
                total += quantity.ElementAt(i) * price.ElementAt(i);
            }

            return total;
        }
    }
}