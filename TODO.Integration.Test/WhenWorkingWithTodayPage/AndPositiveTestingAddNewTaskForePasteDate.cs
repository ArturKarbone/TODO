using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithTodayPage
{
    [TestFixture]
    class AndPositiveTestingAddNewTaskForePasteDate
    {
         private DataDbContext _dataDbContext;
         private IWebDriver _driver;
         private WebDriverWait _wait;

        [SetUp]
        public void SetUp()
        {
            _dataDbContext = new DataDbContext();
            _driver = new ChromeDriver(@"C:\");
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var assignments = _dataDbContext.Assignments;
            _dataDbContext.Assignments.RemoveRange(assignments);
            _dataDbContext.SaveChanges();
        }

        [Test]
        public void TestingPastDate()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:62564/#/today");

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/a")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div[1]/a")));
            var addnewtaskbutton = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div[1]/a"));
            addnewtaskbutton.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
            var newtaskform = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]"));
            newtaskform.SendKeys("С framework былобы гораздо быстрее писать тесты");
            
            var duedate = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[2]"));
            duedate.SendKeys(DateTime.Today.AddDays(-1).ToShortDateString());

            var createnewtask = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/button"));
            createnewtask.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='toast-container']/div/div[3]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='toast-container']/div/div[3]")));
            Assert.IsTrue(_driver.PageSource.Contains("Ошибка"));
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _dataDbContext.Dispose();
        } 
    }
}
