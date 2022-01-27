using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void TestLoginFail()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("asd");
            driver.FindElement(By.Id("Password")).SendKeys("asd");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("html/body/div[1]/div/form/div/div[1]/div/ul/li")).Text;
            if (s == "Wrong Username and password combination !")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestLoginOk()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/span")).Text;
            if (s == "Welcome szymon")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestPickUpFail()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody/tr[5]/td[3]/a")).Click();
            driver.FindElement(By.Id("Code")).SendKeys("123");
            driver.FindElement(By.Id("PickUpCode")).Clear();
            driver.FindElement(By.Id("PickUpCode")).SendKeys("123");
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/div[2]/div")).Text;
            if (s == "Wrong details!")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestPickUpNotValid()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody/tr[5]/td[3]/a")).Click();
            driver.FindElement(By.Id("Code")).SendKeys("123");
            driver.FindElement(By.Id("PickUpCode")).Clear();
            driver.FindElement(By.Id("PickUpCode")).SendKeys("abc");
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/div[1]/div/ul/li")).Text;
            if (s == "The value 'abc' is not valid for Pick Up Code.")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestPickUpOk()
        {
            string code = "5309";
            string pickUpCode = "8947";
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody/tr[5]/td[3]/a")).Click();
            driver.FindElement(By.Id("Code")).SendKeys(code);
            driver.FindElement(By.Id("PickUpCode")).Clear();
            driver.FindElement(By.Id("PickUpCode")).SendKeys(pickUpCode);
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/div[2]/div")).Text;
            if (s != "Parcel picked up")
            {
                Assert.Fail();
                return;
            }
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            IWebElement table = driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody"));
            IReadOnlyCollection<IWebElement> tableRows = table.FindElements(By.TagName("tr")).ToList();
            foreach(var r in tableRows)
            {
                IReadOnlyCollection<IWebElement> tableColumns = r.FindElements(By.TagName("td")).ToList();
                if (tableColumns.Any())
                {
                    if (tableColumns.First().Text == code)
                    {
                        Assert.Fail();
                        return;
                    }
                }
            }
            Assert.Pass();
        }
        [Test]
        public void TestNewParcel()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            IWebElement table = driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody"));
            IReadOnlyCollection<IWebElement> tableRows = table.FindElements(By.TagName("tr")).ToList();
            int rowsBefore = tableRows.Count;

            driver.FindElement(By.XPath("/html/body/div[1]/div/p/a")).Click();

            driver.FindElement(By.Id("Type")).Clear();
            driver.FindElement(By.Id("Type")).SendKeys("1");
            driver.FindElement(By.Id("DestinationParcelLockerID")).SendKeys("2");
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            table = driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody"));
            tableRows = table.FindElements(By.TagName("tr")).ToList();

            int rowsAfter = tableRows.Count;

            if (rowsAfter - rowsBefore == 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }
        [Test]
        public void TestNewParcelNotValid()
        {
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("Login")).SendKeys("Szymon");
            driver.FindElement(By.Id("Password")).SendKeys("hutnik");
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            IWebElement table = driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody"));
            IReadOnlyCollection<IWebElement> tableRows = table.FindElements(By.TagName("tr")).ToList();
            int rowsBefore = tableRows.Count;

            driver.FindElement(By.XPath("/html/body/div[1]/div/p/a")).Click();

            driver.FindElement(By.Id("Type")).Clear();
            driver.FindElement(By.Id("Type")).SendKeys("test");
            driver.FindElement(By.Id("DestinationParcelLockerID")).SendKeys("test");
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            table = driver.FindElement(By.XPath("/html/body/div[1]/div/table/tbody"));
            tableRows = table.FindElements(By.TagName("tr")).ToList();

            int rowsAfter = tableRows.Count;

            if (rowsAfter == rowsBefore)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestNewUser()
        {
            string name = "Jan";
            string password = "Kowalski";
            string login = "Jan";
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[2]")).Click();
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys(name);
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys(login);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/input")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[2]")).Click();

            driver.FindElement(By.Id("Login")).SendKeys(login);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.XPath("html/body/div[1]/div/form/div/button")).Click();
            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/span")).Text;
            if (s == "Welcome "+ name)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestNewUserExist()
        {
            string name = "Jan";
            string password = "Kowalski";
            string login = "Jan";
            driver.Navigate().GoToUrl("https://localhost:44357/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a[2]")).Click();
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys(name);
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys(login);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/input")).Click();


            driver.FindElement(By.XPath("/html/body/nav/div/div/div/a[3]")).Click();


            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys(name);
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys(login);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/input")).Click();

            string s = driver.FindElement(By.XPath("/html/body/div[1]/div/form/div/div[2]/div")).Text;
            if (s == "Login exist")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }
    }
}