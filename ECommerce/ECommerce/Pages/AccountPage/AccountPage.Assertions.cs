
namespace ECommerce.Pages
{
    public partial class AccountPage
    {
        public void AssertThatUserIsLogged()
        {
            Assert.That(AccountNavbar(Navbar.Logout).Displayed);
        }

        public void AssertThatUserIsNotLogged()
        {
            Assert.That(AccountNavbar(Navbar.Login).Displayed, Is.True, Utils.LOGGED_MESSAGE);
        }
    }
}