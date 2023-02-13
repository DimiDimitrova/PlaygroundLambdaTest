
namespace ECommerce.Pages
{
    public partial class CheckoutPage
    {
        private const int PASSWORD_MIN_SIZE = 4;
        private const int PASSWORD_MAX_SIZE = 20;
        private const int FIRST_NAME_MIN_SIZE = 1;
        private const int FIRST_NAME_MAX_SIZE = 32;
        private const int LAST_NAME_MIN_SIZE = 1;
        private const int LAST_NAME_MAX_SIZE = 32;
        private const int TELEPHONE_MIN_SIZE = 3;
        private const int TELEPHONE_MAX_SIZE = 32;
        private const int ADDRESS_MIN_SIZE = 3;
        private const int ADDRESS_MAX_SIZE = 128;
        private const int CITY_MIN_SIZE = 2;
        private const int CITY_MAX_SIZE = 128;
        private const int POST_CODE_MIN_SIZE = 2;
        private const int POST_CODE_MAX_SIZE = 10;

        public void CheckoutPageIsLoaded()
        {
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("checkout/checkout"));
        }

        public void AssertProductInCheckoutPage(ProductInfo product)
        {
            var temp = CheckoutTable();

            Assert.IsTrue(temp.Contains(product.Price),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Price));

            Assert.IsTrue(temp[1].Contains(product.ProductName),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.ProductName));

            Assert.IsTrue(temp[1].Contains(product.Model),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Model));
        }

        public void AssertQuantityAndTotalAreCorrect(int expectedQty, Product product)
        {
            Assert.IsTrue(QuentityField(product).GetAttribute("value").Equals(expectedQty.ToString()),
                Utils.QTY_ERROR, QuentityField(product).Text, expectedQty.ToString());

            var unitPriceBeforeUpdate = UnitPrice(product).Text.Remove(0, 1);

            var unitPrice = double.Parse(unitPriceBeforeUpdate) * expectedQty;
            _driver.WaitForAjax();
            var totalProductSum = TotalOnProduct(product).Text.Remove(0, 1);
            Assert.IsTrue(totalProductSum.Equals(String.Format("{0:0.00}", unitPrice)),
               String.Format(Utils.NOT_EQUAL_ERROR, totalProductSum, unitPrice));
        }

        public void AssertProductPresentInCheckout(ProductInfo product)
        {
            var content = CheckoutProducts();

            Assert.IsTrue(content.Contains(product.Price),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Price));
            Assert.IsTrue(content.Contains(product.ProductName),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.ProductName));
            Assert.IsTrue(content.Contains(product.Model),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product.Model));
        }

        public void AssertTotalPriceIsCorrect()
        {
            var temp = Total.Text.Remove(0, 1);

            Assert.IsTrue(temp.Equals(String.Format("{0:0.00}", TotalPriceWithTaxes())),
            String.Format(Utils.NOT_EQUAL_ERROR, temp, String.Format("{0:0.00}", TotalPriceWithTaxes())));
        }

        public void AssertMakePurchaseFailedWithIncorrectFirstName()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE + " " +
                String.Format(Utils.FIRST_NAME_ERROR, FIRST_NAME_MIN_SIZE, FIRST_NAME_MAX_SIZE));
            Assert.IsTrue(FirstNameError.Text.Equals(String.Format(Utils.FIRST_NAME_ERROR, FIRST_NAME_MIN_SIZE, FIRST_NAME_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithIncorrectLastName()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);
            Assert.IsTrue(LastNameError.Text.Equals(String.Format(Utils.LAST_NAME_ERROR, LAST_NAME_MIN_SIZE, LAST_NAME_MAX_SIZE)));
        }
        public void AssertMakePurchaseFailedWithIncorrectPostCode()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);
            Assert.IsTrue(PostCodeError.Text.Equals(String.Format(Utils.POSTCODE_ERROR, POST_CODE_MIN_SIZE, POST_CODE_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithIncorrectAddress()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);
            Assert.IsTrue(AddressError.Text.Equals(String.Format(Utils.ADDRESS_ERROR, ADDRESS_MIN_SIZE, ADDRESS_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithIncorrectCity()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);
            Assert.IsTrue(CityError.Text.Equals(String.Format(Utils.CITY_ERROR, CITY_MIN_SIZE, CITY_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithNotMatchesPassword()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
               Utils.PASSWORD_CONFIRMATION_ERROR);
        }

        public void AssertMakePurchaseFailedWithEmptyConfirmPasswordField()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);

            Assert.IsTrue(PasswordConfirmationError.Text.Equals(String.Format(Utils.PASSWORD_CONFIRMATION_ERROR)));
        }

        public void AssertMakePurchaseFailedWithExistEmail()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);
            Assert.That(EmailError.Displayed);
        }

        public void AssertMakePurchaseFailedWithIncorrectTelephone()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);

            Assert.IsTrue(TelephoneError.Text.Equals(String.Format(Utils.TELEPHONE_ERROR, TELEPHONE_MIN_SIZE, TELEPHONE_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithIncorrectPassword()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);

            Assert.IsTrue(TelephoneError.Text.Equals(String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE)));
        }

        public void AssertMakePurchaseFailedWithIncorrectCountry()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
                Utils.ORDER_IS_MADE);

            Assert.IsTrue(CountryError.Text.Equals(Utils.COUNTRY_ERROR));
        }

        public void AssertMakePurchaseFailedWithoutCheckedConditions()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Checkout"),
               Utils.ORDER_IS_MADE);

            Assert.IsTrue(TersmWarningMessage.Displayed);
        }

        public void AssertEcoTaxIsCorrect()
        {
            var temp = SelectCheckoutTotalInfo(TablePrice.EcoTax).Text.Remove(0, 1);
            var estimatedEcoTax = EstimateEcoTax();

            Assert.AreEqual(estimatedEcoTax, double.Parse(temp),
                String.Format(Utils.NOT_EQUAL_ERROR, temp, estimatedEcoTax.ToString()));
        }

        public void AssertVatIsCorrect()
        {
            var totalVat = SelectCheckoutTotalInfo(TablePrice.VAT).Text.Remove(0, 1);
            var estimatedVat = EstimateVAT();
            Assert.IsTrue(totalVat.Equals(String.Format("{0:0.00}", estimatedVat)),
              String.Format(Utils.NOT_EQUAL_ERROR, totalVat, String.Format("{0:0.00}", estimatedVat)));
        }
    }
}