
namespace ECommerce.Pages
{
    public partial class HomePage : EShopPage
    {
        public HomePage(Driver driver)
            : base(driver)
        {
        }

        public string Url => "https://ecommerce-playground.lambdatest.io";

        public void SearchByCategory(CategoryInSearchBox category)
        {
            _driver.GoToUrl(Url);
            SelectCategory(category);
            SearchButton.Click();
            _driver.WaitForAjax();
        }

        public void SearchByManufacturer(Brand manufacturer)
        {
            _driver.GoToUrl(Url);
            SearchInput.SendKeys(manufacturer.ToString());
            SearchButton.Click();
            _driver.WaitForAjax();
        }

        public void Search(string search)
        {
            _driver.GoToUrl(Url);
            SearchInput.SendKeys(search);
            SearchButton.Click();
            _driver.WaitForAjax();
        }

        public void SearchByTopCategory(Categories category)
        {
            _driver.GoToUrl(Url);
            ShopByCategoryButton.Click();
            GetTopCategoryByName(category.GetEnumDescription()).Click();
            _driver.WaitForAjax();
        }

        public void SelectByBrandInMegaMenu(Brand brand)
        {
            MainNavigationSections.MoveToMainMenu(MainMenu.MegaMenu);
            _driver.WaitForAjax();
            var temp = MegaMenu.Mobiles;
            GetMenuName(temp.GetEnumDescription(), brand.ToString()).Click();
            _driver.WaitForAjax();
        }

        public void SelectBySoundSystemInMegaMenu(SoundSystem system)
        {
            MainNavigationSections.MoveToMainMenu(MainMenu.MegaMenu);

            var temp = MegaMenu.SoundSystem;
            GetMenuName(temp.GetEnumDescription(), system.GetEnumDescription()).Click();
            _driver.WaitForAjax();
        }

        private void SelectCategory(CategoryInSearchBox category)
        {
            AllCategoriesDropDown.Click();
            GetCategoryById((int)category).Click();
        }
    }
}