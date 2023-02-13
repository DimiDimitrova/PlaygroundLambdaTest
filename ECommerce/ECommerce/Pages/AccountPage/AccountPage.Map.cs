
namespace ECommerce.Pages
{
    public partial class AccountPage
    {
        public IWebElement WarningMessage => _driver.FindElement(By.XPath("//*[@class='text-danger']"));
        public IWebElement AffliateAccount => _driver.FindElement(By.XPath("//*[contains(@class,'fa-bullhorn')]//parent::a"));

        public IWebElement AccountNavbar(Navbar navbar)
        {
            return _driver.FindElement(By.XPath($"//*[@id='column-right']/div/a[contains(@href,'{navbar.GetEnumDescription()}')]"));
        }
    }
}