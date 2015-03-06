using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithArchivePage
{
    [TestFixture]

    class AndPositiveTestingAddNewTasks
    {
        private DataDbContext _dataDbContext;

        [SetUp]
        public void SetUp()
        {
            _dataDbContext = new DataDbContext();
        }

        [Test]
        public void TestingAddNewTasks()
        {
            var assignments = _dataDbContext.Assignments;
            _dataDbContext.Assignments.RemoveRange(assignments);
            _dataDbContext.SaveChanges();

            IWebDriver driver = new ChromeDriver(@"C:\");
            driver.Manage().Window.Maximize();
            driver.Url = "http://localhost:62564/#/archive";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var tomorrow = DateTime.Today.AddDays(1);

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.ng-scope a.btn.btn-success.btn-sm.pull-left")));
            var addnewtaskbutton =driver.FindElement(By.CssSelector("html body.ng-scope section#container section#main-content section.wrapper div.ng-scope div.ng-scope a.btn.btn-success.btn-sm.pull-left"));
            addnewtaskbutton.Click();

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("html body.ng-scope.modal-open section#container section#main-content section.wrapper div.ng-scope div#myModal.modal.fade.ng-scope.in div.modal-dialog div.modal-content div.modal-body form.ng-pristine.ng-invalid.ng-invalid-required input.form-control.ng-pristine.ng-invalid.ng-invalid-required.ng-touched")));
            var newtaskform =driver.FindElement(By.CssSelector("html body.ng-scope.modal-open section#container section#main-content section.wrapper div.ng-scope div#myModal.modal.fade.ng-scope.in div.modal-dialog div.modal-content div.modal-body form.ng-pristine.ng-invalid.ng-invalid-required input.form-control.ng-pristine.ng-invalid.ng-invalid-required.ng-touched"));
            newtaskform.SendKeys("1");
            
            var duedate = driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[2]"));
            duedate.SendKeys(string.Format("{0}.{1}.{2}", tomorrow.Day, tomorrow.Month, tomorrow.Year));

            var createnewtask = driver.FindElement(By.CssSelector("#myModal > div.modal-dialog > div > div.modal-body > form > button"));
            createnewtask.Click();

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[2]/a")));
            var addnewtaskbutton2 = driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div[1]/div/section/div/div[2]/a"));
            addnewtaskbutton2.Click();

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
            var newtaskforma = driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]"));
            newtaskforma.SendKeys("2");

            var duedate1 = driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[2]"));
            duedate1.SendKeys(string.Format("{0}.{1}.{2}", tomorrow.Day, tomorrow.Month, tomorrow.Year));

            var createnewtask2 = driver.FindElement(By.CssSelector("#myModal > div.modal-dialog > div > div.modal-body > form > button"));
            createnewtask2.Click();
            

            //wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/section/aside/div/ul/li[3]/a/span")));
            //var nextweek = driver.FindElement(By.XPath("/html/body/section/aside/div/ul/li[3]/a/span"));
            //nextweek.Click();
            //wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div[2]/div/section/div[2]/div[1]/ul/li/div[2]/span[1]")));
            //Assert.IsTrue(driver.PageSource.Contains("С framework былобы гораздо быстрее писать тесты"));
            //driver.Close();
        }
    }
}
