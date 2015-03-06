using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TODO.Integration.Test.WhenWorkingWithTodayPage
{
    [TestFixture]
    public class AndSmokeTestingPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver(@"C:\");
            _wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(5));
            
        }
        [Test]
        public void TestingPageObject()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:62564/#/today");
           
            var archive = _driver.FindElement(By.CssSelector("#nav-accordion > li.mt > a > i"));
            archive.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope h3.ng-scope")));
            
            var today = _driver.FindElement(By.CssSelector("#nav-accordion > li:nth-child(4) > a > span"));
            today.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope h3.ng-binding.ng-scope")));
            
            var nextweek = _driver.FindElement(By.CssSelector("#nav-accordion > li:nth-child(5) > a > span"));
            nextweek.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.row.mt.ng-scope div.col-md-12 section.task-panel.tasks-widget div.panel-heading div.pull-left h5.ng-binding")));
            
            var deer = _driver.FindElement(By.XPath("/html/body/section/aside/div/ul/p/a/img"));
            deer.Click();
            }
              [TearDown]
              public void TearDown()
              {
                _driver.Quit();
              } 
    }
}
