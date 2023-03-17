using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;
using System;

namespace SOAP_Session4
{
    [TestClass]
    public class CountryInfoService
    {
        //global variable for country list

        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryList =
                new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ListOfCountryNamesByCodeAscendingTest()
        {
            //Gets the country code along with the country name with it
            var locList = countryList.ListOfCountryNamesByCode();
            //Gets the country code along with the country name with it but this lists the database in ascending order
            var locListAsc = locList.OrderBy(isoCode => isoCode.sISOCode);
            Assert.IsTrue(Enumerable.SequenceEqual(locList, locListAsc), "Country code is not in ascending order");
        }

        [TestMethod]
        public void CountryNameTest()
        {
            //Variable for an existing country in the database
            var countryName = countryList.CountryName("AE");
            //Variable for a country that is not in the database
            var countryName2 = countryList.CountryName("--");

            //Assert to ensure that country name exist in the database
            Assert.AreEqual("United Arab Emirates", countryName, "Country code not existing in database");
            //Assert to ensure that country that is not existent is detected
            Assert.AreEqual("Country not found in the database", countryName2, "Country is in the database");
        }

        [TestMethod]
        public void LastEntryTest()
        {
            //Gets the country code along with the country name with it
            var lastEntryCountry = countryList.ListOfCountryNamesByCode().Last();

            //Gets the country name from the assigned last country code
            var lastCountry = countryList.CountryName(lastEntryCountry.sISOCode);

            Assert.AreEqual(lastEntryCountry.sName, lastCountry, "Not the same");
        }
    }

}