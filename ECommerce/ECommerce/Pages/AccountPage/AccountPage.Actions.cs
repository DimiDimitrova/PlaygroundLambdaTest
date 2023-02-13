
namespace ECommerce.Pages
{
    public partial class AccountPage : EShopPage
    {
        public AccountPage(Driver driver) 
            : base(driver)
        {
        }

        public void OpenMenuFromNavbar(Navbar menu)
        {
            AccountNavbar(menu).Click();
        }
    }
}