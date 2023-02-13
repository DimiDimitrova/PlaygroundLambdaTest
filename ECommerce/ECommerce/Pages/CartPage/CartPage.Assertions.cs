
namespace ECommerce.Pages
{
    public partial class CartPage
    {
        private const int POSTCODE_MIN_SIZE_LENGTH = 2;
        private const int POSTCODE_MAX_SIZE_LENGTH = 10;

        public void AssertProductAddedToCart(ProductInfo product)
        {
            var temp = ShoppingTableContent();

            Assert.IsTrue(temp.Contains(product.Price),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Price));
            Assert.IsTrue(temp.Contains(product.ProductName),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.ProductName));
            Assert.IsTrue(temp.Contains(product.Model),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Model));
        }

        public void AssertVoucherAddedToCart(string recipientName, double amount)
        {
            var temp = ShoppingTableContent();
            var voucher = String.Format("${0:0.00} Gift Certificate for {1}", amount, recipientName);

            Assert.Contains(voucher, temp);
        }

        public void AssertCartPageIsOpen()
        {
            BreadcrumbSection.AssertMenuIsOpen("Shopping Cart");
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("cart"),
                String.Format(Utils.PAGE_ERROR, "Cart"));
        }

        public void AssertQuantityAndTotalAreCorrect(int expectedQty, ProductInfo product)
        {
            Assert.IsTrue(QuentityField(product.ProductName).GetAttribute("value").Equals(expectedQty.ToString()),
                Utils.QTY_ERROR, QuentityField(product.ProductName).Text, expectedQty.ToString());

            var unitPriceBeforeUpdate = UnitPrice(product.ProductName).Text.Replace("$", "").Replace("£", "").Replace("€", "");

            var unitPrice = double.Parse(unitPriceBeforeUpdate) * expectedQty;
            var totalProductSum = Total(product.ProductName).Text.Remove(0, 1);
            Assert.IsTrue(totalProductSum.Equals(String.Format("{0:0.00}", unitPrice)),
               String.Format(Utils.NOT_EQUAL_ERROR, totalProductSum, unitPrice));
        }

        public void AssertMessageForEmptyCartIsDisplayed()
        {
            Assert.IsTrue(ShoppingCartIsEmptyMessage.Displayed, Utils.NOT_EMPTY_CART_MESSAGE);
        }

        public void AssertFlatRateIsApplied(string appliedRate)
        {
            var temp = appliedRate.Remove(0, 22);
            var prices = GetPrices();

            Assert.AreEqual(prices[1], double.Parse(temp),
                  String.Format(Utils.NOT_EQUAL_ERROR, temp, prices[1]));
        }

        public void AssertEstimateShippingTaxesErrorsPresent()
        {
            Assert.IsTrue(RegionInvalidMessage.Displayed,
                  String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, RegionInvalidMessage.Text));
            Assert.IsTrue(PostCodeInvalidMessage.Displayed,
                  String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, PostCodeInvalidMessage.Text));
        }

        public void AssertPostCodeEstimateTaxesErrorPresent()
        {
            Assert.IsTrue(PostCodeInvalidMessage.Text.Equals(String.Format(Utils.POSTCODE_ERROR, POSTCODE_MIN_SIZE_LENGTH, POSTCODE_MAX_SIZE_LENGTH)));
        }

        public void AssertGiftCertificateIsApplied()
        {
            Assert.IsFalse(GiftError.Displayed,
                Utils.GIFT_CERTIFICATE_ERROR);
        }

        public void AssertTotalPriceIsCorrect()
        {
            var temp = SelectCartTotalInfo(TablePrice.Total).Text.Remove(0, 1);

            Assert.IsTrue(temp.Equals(String.Format("{0:0.00}", TotalPriceProducts())),
             String.Format(Utils.NOT_EQUAL_ERROR, temp, String.Format("{0:0.00}", TotalPriceProducts())));
        }

        public void AssertEcoTaxIsCorrect()
        {
            var temp = SelectCartTotalInfo(TablePrice.EcoTax).Text.Remove(0, 1);
            var estimatedEcoTax = EstimateEcoTax();

            Assert.AreEqual(estimatedEcoTax, double.Parse(temp),
                String.Format(Utils.NOT_EQUAL_ERROR, temp, estimatedEcoTax.ToString()));
        }

        public void AssertVatIsCorrect()
        {
            var totalVat = SelectCartTotalInfo(TablePrice.VAT).Text.Remove(0, 1);
            var estimatedVat = EstimateVAT();
            Assert.IsTrue(totalVat.Equals(String.Format("{0:0.00}", estimatedVat)),
              String.Format(Utils.NOT_EQUAL_ERROR, totalVat, String.Format("{0:0.00}", estimatedVat)));
        }

        public void AssertShoppingCartIsEmpty()
        {
            _driver.WaitForAjax();
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("checkout/cart"));
            Assert.IsTrue(ShoppingCartIsEmptyMessage.Displayed);
        }
    }
}