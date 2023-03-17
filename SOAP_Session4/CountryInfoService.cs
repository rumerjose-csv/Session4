using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;
using System;

namespace SOAP_Session4
{
    [TestClass]
    public class CountryInfoService
    {
        //global variable

        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryList =
                new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ListOfCountryNamesByCodeAscendingTest()
        {
            var locList = countryList.ListOfCountryNamesByCode();
            var locListAsc = locList.OrderBy(isoCode=> isoCode.sISOCode);
            Assert.IsTrue(Enumerable.SequenceEqual(locList, locListAsc), "Country code is not in ascending order");
        }

        [TestMethod]
        public void CountryNameTest()
        {
            //Variable for an existing country in the database
            var countryName = countryList.CountryName("AE");
            //Variable for a country that is not in the database
            var countryName2 = countryList.CountryName("WAG");

            //Assert to ensure that country name exist in the database
            Assert.AreEqual("United Arab Emirates", countryName, "Country code not existing in database");
            //Assert to ensure that country that is not existent is detected
            Assert.AreEqual("Country not found in the database", countryName2, "Country is in the database");
        }

        [TestMethod]
        public void LastEntryTest()
        {
            var lastEntryCountry = countryList.ListOfCountryNamesByCode().Last();
            var countryName = countryList.CountryName(lastEntryCountry.sISOCode);

            Assert.AreEqual(lastEntryCountry.sName, countryName, "Not the same");
        }
    }

}