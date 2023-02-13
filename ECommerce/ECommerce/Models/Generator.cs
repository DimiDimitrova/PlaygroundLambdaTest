
using AutoFixture;
using static OpenQA.Selenium.PrintOptions;

namespace ECommerce
{
    public class Generator
    {
        private Fixture fixture;
        public Generator()
        {
            fixture = new Fixture();
        }

        public PersonInfo CreatePersonInfo()
        {
            var person = fixture.Build<PersonInfo>()
               .With(x => x.Email, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .Create();

            return person;
        }

        public PersonInfo CreatePersonWithSpecificFirstName(int firstNameSize)
        {
            return fixture.Build<PersonInfo>()
              .With(x => x.FirstName, string.Join(string.Empty, fixture.CreateMany<char>(firstNameSize)))
              .With(x => x.Email, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
              .Create();
        }

        public PersonInfo CreatePersonWithSpecificLastName(int lastNameSize)
        {
            return fixture.Build<PersonInfo>()
               .With(x => x.LastName, string.Empty)
               .With(x => x.Email, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .Create();
        }

        public PersonInfo CreatePersonWithSpecificTelephone(int telephoneSize)
        {
            return fixture.Build<PersonInfo>()
                .With(x => x.Telephone, string.Join(string.Empty, fixture.CreateMany<char>(telephoneSize)))
                .With(x => x.Email, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .Create();
        }

        public PersonInfo CreatePersonWithSpecificPassword(int passwordSize)
        {
            return fixture.Build<PersonInfo>()
                .With(x => x.Password, string.Join(string.Empty, fixture.CreateMany<char>(passwordSize)))
                .With(x => x.Email, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .Create();
        }

        public PersonInfo CreatePersonWithRegisteredCredentials()
        {
            return fixture.Build<PersonInfo>()
                .With(x => x.Email, AccountInfo.Email)
                .With(x => x.Password, AccountInfo.Password)
                .Create();
        }

        public PersonInfo GetRegisteredUser()
        {
            var person = new PersonInfo();
            person.Email = AccountInfo.Email;
            person.Password = AccountInfo.Password;

            return person;
        }

        public PaymentAddressInfo CreatePaymentAddress()
        {
            return fixture.Create<PaymentAddressInfo>();
        }

        public PaymentAddressInfo CreatePaymentWithSpecificAddress(int addressSize)
        {
            return fixture.Build<PaymentAddressInfo>()
                .With(x => x.Address, string.Join(string.Empty, fixture.CreateMany<char>(addressSize)))
                .Create();
        }

        public PaymentAddressInfo CreatePaymentWithSpecificCity(int citySize)
        {
            return fixture.Build<PaymentAddressInfo>()
                 .With(x => x.City, string.Join(string.Empty, fixture.CreateMany<char>(citySize)))
                 .Create();
        }

        public PaymentAddressInfo CreatePaymentWithSpecificPostCode(int postCodeSize)
        {
            return fixture.Build<PaymentAddressInfo>()
                .With(x => x.PostCode, string.Join(string.Empty, fixture.CreateMany<char>(postCodeSize)))
                .Create();
        }

        public PaymentAddressInfo CreatePaymentWithSelectCountryOption()
        {
            return fixture.Build<PaymentAddressInfo>()
                .With(x => x.Country, Country.PleaseSelect)
                .Create();
        }


        public RecipientInfo CreateRecipient()
        {
            return fixture.Build<RecipientInfo>()
               .With(x => x.RecipientEmail, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .Create();
        }

        public RecipientInfo CreateRecipientWithSpecificName(int nameSize)
        {
            return fixture.Build<RecipientInfo>()
               .With(x => x.RecipientEmail, string.Join(string.Empty, fixture.Create<string>()) + "@abv.bg")
               .With(x => x.RecipientName, string.Join(string.Empty, fixture.CreateMany<char>(nameSize)))
               .Create();
        }

        public RecipientInfo CreateRecipientWithSpecificEmail(int emailSize, string endEmail)
        {
            return fixture.Build<RecipientInfo>()
               .With(x => x.RecipientEmail, string.Join(string.Empty, fixture.CreateMany<char>(emailSize) + $"{endEmail}"))
               .Create();
        }

        public string CreateText()
        {
            return fixture.Create<string>();
        }

        public string CreateTextWithSpecificLength(int length)
        {
            return fixture.Create<string>().Substring(0, length);
        }
    }
}