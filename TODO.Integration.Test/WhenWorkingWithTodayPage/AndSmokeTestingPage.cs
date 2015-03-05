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
        [Test]
        public void TestingPageObject()
        {
            IWebDriver driver = new ChromeDriver(@"E:\QA\lib");
            driver.Url = "http://localhost:62564/#/today";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));


            var archive = driver.FindElement(By.CssSelector("#nav-accordion > li.mt > a > i"));
            archive.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope h3.ng-scope")));



            var today = driver.FindElement(By.CssSelector("#nav-accordion > li:nth-child(4) > a > span"));
            today.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope h3.ng-binding.ng-scope")));


            var nextweek = driver.FindElement(By.CssSelector("#nav-accordion > li:nth-child(5) > a > span"));
            nextweek.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.row.mt.ng-scope div.col-md-12 section.task-panel.tasks-widget div.panel-heading div.pull-left h5.ng-binding")));



            var deer = driver.FindElement(By.XPath("/html/body/section/aside/div/ul/p/a/img"));
            deer.Click();
            driver.Close();
        }
    }
}
