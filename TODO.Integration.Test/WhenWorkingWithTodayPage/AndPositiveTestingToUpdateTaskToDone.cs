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
        }
        [Test]
        public void TestingTaskDone()
        {
            var assignments = _dataDbContext.Assignments;
            _dataDbContext.Assignments.RemoveRange(assignments);
            _dataDbContext.SaveChanges();

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:62564/#/today");
            
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.ng-scope a.btn.btn-success.btn-sm.pull-left")));
            var addnewtaskbutton = _driver.FindElement(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.ng-scope a.btn.btn-success.btn-sm.pull-left"));
            addnewtaskbutton.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope.modal-open section#container section#main-content section.wrapper div.ng-scope div#myModal.modal.fade.ng-scope.in div.modal-dialog div.modal-content div.modal-body form.ng-pristine.ng-invalid.ng-invalid-required input.form-control.ng-pristine.ng-invalid.ng-invalid-required.ng-touched")));
            var newtaskform = _driver.FindElement(By.CssSelector("html body.ng-scope.modal-open section#container section#main-content section.wrapper div.ng-scope div#myModal.modal.fade.ng-scope.in div.modal-dialog div.modal-content div.modal-body form.ng-pristine.ng-invalid.ng-invalid-required input.form-control.ng-pristine.ng-invalid.ng-invalid-required.ng-touched"));

            newtaskform.SendKeys("С framework былобы гораздо быстрее писать тесты");
            var createnewtask = _driver.FindElement(By.CssSelector("#myModal > div.modal-dialog > div > div.modal-body > form > button"));
            createnewtask.Click();
            
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[1]/a/i")));
            var donebutton =_driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[1]/a/i"));
            donebutton.Click();

            var archive = _driver.FindElement(By.CssSelector("#nav-accordion > li.mt > a > i"));
            archive.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/span[2]")));
            
            Assert.IsTrue(_driver.PageSource.Contains("Done"));
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        } 
    }
}
