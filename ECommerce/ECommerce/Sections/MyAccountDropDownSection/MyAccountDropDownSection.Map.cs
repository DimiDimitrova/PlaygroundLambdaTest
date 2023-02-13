
namespace ECommerce.Sections
{
    public partial class MyAccountDropDownSection
    {
        public IWebElement MyAccountMenu(MyAccountDropDown menu)
        {
            return _driver.FindElement(By.XPath($"//ul[contains(@class,'dropdown-menu')]//following::a[contains(@href,'{menu.GetEnumDescription()}')]"));
        }
    }
}