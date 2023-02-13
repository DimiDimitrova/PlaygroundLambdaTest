
namespace ECommerce.Pages
{
    public partial class ConfirmPage
    {
        public void VerifyPriceList(List<double> list)
        {
            Assert.AreEqual(list[0], GetPrices().ElementAt(0),
                String.Format(Utils.SUM_ERROR, TablePrice.SubTotal.GetEnumDescription()));


            double totalSum = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                totalSum += list.ElementAt(i);
            }

            Assert.AreEqual(double.Parse(TotalSum.Text.Remove(0, 1)), totalSum,
                String.Format(Utils.SUM_ERROR, TablePrice.Total.GetEnumDescription()));
        }

        public void AssertTotalSum()
        {
            double total = TotalPriceProducts() + double.Parse(ConfirmPrice(TablePrice.FlatShippingRate).Text.Remove(0, 1));
            Assert.AreEqual(double.Parse(TotalSum.Text.Remove(0, 1)), total,
                String.Format(Utils.SUM_ERROR, TablePrice.Total.GetEnumDescription()));
        }

        public void AssertProductQuantityIsEdited(int quantity, Product product)
        {
            Assert.AreEqual(quantity, int.Parse(ProductQuantity(product).Text),
                String.Format(Utils.NOT_EQUAL_ERROR, int.Parse(ProductQuantity(product).Text), quantity));
        }

        public void AssertPaymentInfo(PaymentAddressInfo address)
        {
            string[] content = PaymentTable.Text.Split("\r\n");
            var regionCountry = String.Format("{0},{1}", address.Region, address.Country);
            var postCodeCity = String.Format("{0} {1}", address.City, address.PostCode);

            Assert.AreEqual(address.Address, content[1],
                String.Format(Utils.PAYMENT_ERROR, address.Address));

            Assert.AreEqual(postCodeCity, content[2]);
            Assert.AreEqual(regionCountry, content[3],
                String.Format(Utils.PAYMENT_ERROR, regionCountry));
        }

        public void AssertProductContentIsCorrect(ProductInfo product)
        {
            var temp = ConfirmProductContent();

            Assert.IsTrue(temp.Contains(product.ProductName),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.ProductName));

            Assert.IsTrue(temp.Contains(product.Price),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Price));

            Assert.IsTrue(temp.Contains(product.Model),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Model));
        }

        public void ConfirmPageIsLoaded()
        {
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("checkout/confirm"));
        }
    }
}