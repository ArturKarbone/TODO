using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithTodayPage
{
    [TestFixture]
    class AndPositiveTestingToUpdateTaskToDone
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
        public void TestingTaskDone()
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

            var createnewtask = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/button"));
            createnewtask.Click();
            
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[1]/a/i")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[1]/a/i")));
            var donebutton =_driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[1]/a/i"));
            donebutton.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='nav-accordion']/li[1]/a")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='nav-accordion']/li[1]/a")));
            var archive = _driver.FindElement(By.XPath("//*[@id='nav-accordion']/li[1]/a"));
            archive.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/span[2]")));
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/span[2]")));
            Assert.IsTrue(_driver.PageSource.Contains("Done"));
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _dataDbContext.Dispose();
        } 
    }
}
