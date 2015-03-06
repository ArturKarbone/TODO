using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithArchivePage
{
    [TestFixture]
    class AndPositiveTestingExistingTask
    {
        private DataDbContext _dataDbContext;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void SetUp()
        {
            _dataDbContext= new DataDbContext();
            _driver = new ChromeDriver(@"C:\");
            _wait =new WebDriverWait(_driver,TimeSpan.FromHours(5));
            var assignments = _dataDbContext.Assignments;
            _dataDbContext.Assignments.RemoveRange(assignments);
            _dataDbContext.SaveChanges();
        }

        [Test]
        public void TestingEditingTask()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:62564/#/archive");

            _wait.Until(ExpectedConditions.ElementExists(By.LinkText("Add New Task")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Add New Task")));
            var addnewtaskbutton = _driver.FindElement(By.LinkText("Add New Task"));
            addnewtaskbutton.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
            var createtaskname = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]"));
            createtaskname.SendKeys("Hello Again!");

            var duedate = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[2]"));
            duedate.SendKeys(DateTime.Today.AddDays(1).ToShortDateString());

            var createnewtask = _driver.FindElement(By.CssSelector("#myModal > div.modal-dialog > div > div.modal-body > form > button"));
            createnewtask.Click();
            Thread.Sleep(1000);

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/div/a[1]/i")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/div/a[1]/i")));
            var editbtn = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/div/a[1]/i"));
            editbtn.Click();
            Thread.Sleep(1000);

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]")));
            var edittaskform = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]"));
            edittaskform.Clear();
            edittaskform.SendKeys("Hello  FRAMEWORK!");

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]")));
            var duedateedit = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]"));
            duedateedit.SendKeys(DateTime.Today.AddDays(2).ToShortDateString());

            var savechanges = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/button"));
            savechanges.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/button")));
            Thread.Sleep(1000);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _dataDbContext.Dispose();
        }
    }
}
