
namespace ECommerce.Pages
{
    public partial class CheckoutPage : EShopPage
    {
        private const int ECO_TAX = 2;
        private const int VAT_TAX = 20;

        public CheckoutPage(Driver driver)
            : base(driver)
        {
        }

        public void FillPaymentAddress(PaymentAddressInfo billingAddress)
        {
            BillingPaymentFirstAddressField.SendKeys(billingAddress.Address);
            BillingPaymentCityField.SendKeys(billingAddress.City);
            BillingPaymentPostCodeField.SendKeys(billingAddress.PostCode);
            BillingPaymentCountryDropDown.Click();
            CountryOption(billingAddress.Country).Click();
            BillingPaymentRegionField.Click();
            _driver.WaitForAjax();

            try
            {
                billingAddress.Region = RegionOption("1").Text;
                RegionOption("1").Click();
            }
            catch (WebDriverTimeoutException)
            {
                billingAddress.Region = RegionOption("0").Text;
                RegionOption("0").Click();
            }
        }

        public void CheckConditions(Account option)
        {
            switch (option)
            {
                case Account.Login:
                TermsCheckbox.Click();
                break;
                case Account.RegisterAccount:
                PrivacyPoliceCheckbox.Click();
                TermsCheckbox.Click();
                break;
                case Account.GuestAccount:
                TermsCheckbox.Click();
                break;
                default:
                break;
            }
        }

        public List<string> CheckoutProducts()
        {
            var list = new List<string>();

            foreach (var item in CheckoutTableContent)
            {
                foreach (var cell in item.FindElements(By.TagName("td")))
                {
                    list.Add(cell.Text);
                }
            }

            return list;
        }

        public void ContinuePurchase()
        {
            _driver.WaitUntilPageLoadsCompletely();
            ContinueButton.Click();
            _driver.WaitForAjax();
        }

        public void FillPersonalDetails(PersonInfo personInfo)
        {
            FirstNameField.SendKeys(personInfo.FirstName);
            LastNameField.SendKeys(personInfo.LastName);
            EmailField.SendKeys(personInfo.Email);
            TelephoneField.SendKeys(personInfo.Telephone);
        }

        public void FillPasswordFields(string password)
        {
            PaymentPassword.SendKeys(password);
            PaymentConfirmPassword.SendKeys(password);
        }

        public void FillPasswordFields(string password, string confirmPassword)
        {
            PaymentPassword.SendKeys(password);
            PaymentConfirmPassword.SendKeys(confirmPassword);
        }

        public void FillAccountDetails(PersonInfo person, PaymentAddressInfo paymentAddress, Account option)
        {
            AccountOption(option).Click();

            if (option != Account.Login)
            {
                FillPersonalDetails(person);
                FillPaymentAddress(paymentAddress);
            }
            else
            {
                FillLoginAccount(person.Email, person.Password);
                _driver.WaitForAjax();
                ExistingPaymentAddress.Click();
            }

            CheckConditions(option);
            _driver.WaitForAjax();
        }

        public List<double> GetPrices()
        {

            var pricesString = GetPriceLikeString();

            List<double> prices = new List<double>();

            foreach (var price in pricesString)
            {
                prices.Add(double.Parse(price));
            }

            return prices;
        }

        public List<string> CheckoutTable()
        {
            var list = new List<string>();

            foreach (var item in CheckoutTableContent)
            {
                foreach (var cell in item.FindElements(By.TagName("td")))
                {
                    list.Add(cell.Text);
                }
            }

            return list;
        }

        public void UpdateProductQuantity(int newQuantity, Product product)
        {
            QuentityField(product).Clear();
            QuentityField(product).SendKeys(newQuantity.ToString());
            _driver.WaitUntilPageLoadsCompletely();           
        }

        public double TotalPriceWithTaxes()
        {
            double totalSum = 0;
            for (int i = 0; i < GetPrices().Count; i++)
            {
                totalSum += GetPrices().ElementAt(i);
            }

            return totalSum;
        }

        private double EstimateEcoTax()
        {
            var ecoTax = CountOfProducts() * ECO_TAX + ECO_TAX;

            return ecoTax;
        }

        private void FillLoginAccount(string email, string password)
        {
            LoginEmail.SendKeys(email);
            LoginPassword.SendKeys(password);
            LoginButton.Click();
        }

        private double EstimateVAT()
        {
            var subTotal = SelectCheckoutTotalInfo(TablePrice.SubTotal).Text.Remove(0, 1);
            var vat = (double.Parse(subTotal) * VAT_TAX) / 100;


            return vat;
        }

        private int CountOfProducts()
        {
            var count = 0;

            foreach (var item in ProductsQuantityCheckout())
            {
                count += item;
            }

            return count;
        }

        private List<int> ProductsQuantityCheckout()
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

        private List<string> GetPriceLikeString()
        {
            List<string> listPrice = new List<string>()
            {
            SelectCheckoutTotalInfo(TablePrice.SubTotal).Text.Remove(0,1),
            SelectCheckoutTotalInfo(TablePrice.FlatShippingRate).Text.Remove(0,1),
            SelectCheckoutTotalInfo(TablePrice.EcoTax).Text.Remove(0, 1),
            SelectCheckoutTotalInfo(TablePrice.VAT).Text.Remove(0, 1)
            };

            return listPrice;
        }
    }
}