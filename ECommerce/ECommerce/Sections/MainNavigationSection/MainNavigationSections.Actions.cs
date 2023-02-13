
namespace ECommerce.Sections
{
    public partial class MainNavigationSections
    {
        private readonly Driver _driver;

        public MainNavigationSections(Driver driver)
        {
            _driver = driver;
        }

        public void OpenMenu(MainMenu menu)
        {
            _driver.WaitForAjax();
            SelectMenu(menu).Click();
        }

        public void MoveToMainMenu(MainMenu menu)
        {
            _driver.WaitForAjax();
            _driver.MoveToElement(SelectMenu(menu));
        }
    }
}