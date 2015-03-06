using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithTodayPage
{
    [TestFixture]
    class AndPositiveTestingEditExistingTask
    {
        private DataDbContext _dataDbContext;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void SetUp()
        {
            _dataDbContext= new DataDbContext();
            _driver = new ChromeDriver(@"C:\");
            _wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(5));
        }
        [Test]
        public void TestingEditTask()
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

            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#main-content > section > div > div.row.mt.ng-scope > div > section > div > div.task-content > ul > li:nth-child(1) > div.task-title > div > a > i")));
            var editbutton =_driver.FindElement(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.row.mt.ng-scope div.col-md-12 section.task-panel.tasks-widget div.panel-body div.task-content ul.task-list li.ng-scope div.task-title div.pull-right.hidden-phone a.btn.btn-primary.btn-xs"));
            editbutton.Click();

            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]")));
            var edittaskform =_driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]"));
            edittaskform.Clear();
            edittaskform.SendKeys("И снова думаю о FRAMEWORK!");

            var savechanges = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/button"));
            savechanges.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li/div[2]/span[1]")));

            Assert.IsTrue(_driver.PageSource.Contains("И снова думаю о FRAMEWORK!"));
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        } 

    }
}
