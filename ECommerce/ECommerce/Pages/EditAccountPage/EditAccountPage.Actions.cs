
namespace ECommerce.Pages
{
    public partial class EditAccountPage : EShopPage
    {
        public EditAccountPage(Driver driver)
            : base(driver)
        {
        }

        public void FillAllAccountInformation(PersonInfo person)
        {
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(person.FirstName);
            LastNameInput.Clear();
            LastNameInput.SendKeys(person.LastName);
            EmailInput.Clear();
            EmailInput.SendKeys(person.Email);
            TelephoneInput.Clear();
            TelephoneInput.SendKeys(person.Telephone);
        }
    }
}