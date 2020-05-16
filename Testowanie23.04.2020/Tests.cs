using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testowanie23._04._2020
{
    class NUnitTest
    {
        IWebDriver driver;
        //public void TestApp()
        //{
        //    Initialise();
        //    OpenAppTest();
        //    EndTest();
        //}

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
        }

        [Test(Description = "1. Sprawdź czy strona do testowania się odpala")]
        public void OpenAppTest()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/";
            Assert.IsTrue(driver.Title.Equals("Selenium: Beginners Guide"));
        }

        [Test(Description = "2. Sprawdź czy w DIVie 'divontheleft' znajduje się tekst 'Assert that this text is on the page'.")]
        public void TextIsOnTheSite()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            Assert.IsTrue(driver.FindElement(By.Id("divontheleft")).Text.Equals("Assert that this text is on the page"));
        }

        [Test(Description = "3. Sprawdź czy pole tekstowe jest gotowe do wklejenia wartości'")]
        public void IsFieldRedyToPutValue()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            Assert.IsTrue(driver.FindElement(By.Id("html5div")).Text.Equals("To be used after the AJAX section of the book"));
        }

        [Test(Description = "4. Sprawdź czy tekst wczytał się ajaxem po kliknięciu buttona")]
        public void TextProperLoad()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            driver.FindElement(By.Id("secondajaxbutton")).Click();
            Assert.IsTrue(driver.FindElement(By.Id("html5div")).Text.Contains("I have been added with a timeout"));
        }

        [Test(Description = "5. Sprawdź czy otworzyło się nowe okienko po kliknięciu linku.")]
        public void OpenNewTab()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            driver.FindElement(By.Id("multiplewindow")).Click();
            IList<string> allWindowHandles = driver.WindowHandles;
            Assert.IsTrue(allWindowHandles.Count > 1) ;
        }


        [Test(Description = "6. Sprawdź cz po kliknięciu przycisku stworzyło się nowe ciasteczko.")]
        public void IsSecondCookiePresent()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter8";
            driver.FindElement(By.Id("secondCookie")).Click();
            Assert.IsTrue(driver.Manage().Cookies.AllCookies.Count() == 2);
        }


        [Test(Description = "7. Sprawdzenie czy wciśnięcie buttonu home page wraca do głównej strony.")]
        public void ClickBackToHome()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            driver.FindElement(By.LinkText("Home Page")).Click();
            Assert.IsTrue(driver.Url.Equals("http://book.theautomatedtester.co.uk/"));
        }

        [Test(Description = "8. Sprawdz czy w komunikacie pojawia się wpisany tekst")]
        public void IsTypedTextInComunnicat()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter4";
            driver.FindElement(By.Id("blurry")).SendKeys("test");
            driver.FindElement(By.Id("selectLoad")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();
            Assert.IsTrue(alertText.Contains("test"));
        }

        [Test(Description = "9. Sprawdź czy przejechanie myszką po polu wyświetla komunikat.")]
        public void TestMouseOver()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter4";
            Actions builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.Id("hoverOver"))).Perform();
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();
            Boolean containText = alertText.Contains("on MouseOver worked");
            Assert.IsTrue(containText);

        }
        [Test(Description = "10. Sprawdź czy radioButton jest zaznaczony.")]
        public void IsRadioButtonSelected()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            driver.FindElement(By.Id("radiobutton")).Click();
            Assert.IsTrue(driver.FindElement(By.Id("radiobutton")).Selected);
        }


        [Test(Description = "11. Sprawdź czy ID elementu zmienia się po każdym przeładowaniu strony.")]
        public void RandomId()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter2";
            string firsId = driver
                .FindElement(By.XPath("//div[text() = 'This element has a ID that changes every time the page is loaded']"))
                .GetAttribute("id");
            driver.Navigate().Refresh();
            string secodId = driver
                .FindElement(By.XPath("//div[text() = 'This element has a ID that changes every time the page is loaded']"))
                .GetAttribute("id");
            Assert.IsFalse(firsId.Equals(secodId));
        }


        [Test(Description = "12. Sprawdzenie czy DIV został poprawnie załadowany przez AJAXa po kliknięciu przycisku. ")]
        public void LoadDivByAjax()
        {
            driver.Url = "http://book.theautomatedtester.co.uk/chapter1";
            driver.FindElement(By.Id("loadajax")).Click();
            Assert.IsTrue(driver.FindElement(By.Id("ajaxdiv")).Text.Contains("AJAX"));
        }


        [TearDown]
        public void EndTests()
        {
            driver.Close();
        }

    }
}
