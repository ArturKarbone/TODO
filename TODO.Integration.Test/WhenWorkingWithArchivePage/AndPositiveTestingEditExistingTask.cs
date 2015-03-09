using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TODO.Data.Context;

namespace TODO.Integration.Test.WhenWorkingWithArchivePage
{
    [TestFixture]
    public class AndPositiveTestingEditExistingTask
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
            for (int i = 1; i <= 3; i++)
            {
                _wait.Until(ExpectedConditions.ElementExists(By.LinkText("Add New Task")));
                _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Add New Task")));
                var addnewtaskbutton = _driver.FindElement(By.LinkText("Add New Task"));
                addnewtaskbutton.Click();

                _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]")));
                var newtaskform = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[1]"));
                newtaskform.SendKeys(i.ToString("g"));

                var duedate = _driver.FindElement(By.XPath("//*[@id='myModal']/div[2]/div/div[2]/form/input[2]"));
                duedate.SendKeys(DateTime.Today.AddDays(1).ToShortDateString());

                var createnewtask = _driver.FindElement(By.CssSelector("#myModal > div.modal-dialog > div > div.modal-body > form > button"));
                createnewtask.Click();
                Thread.Sleep(1000);
            }

            for (int i = 1; i <= 3; i++)
            {
                
                var edit = _driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li{0}/div[2]/div/a[1]", i != 3 ? "[1]" : string.Empty)));
                edit.Click();

                _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]")));
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]")));
                var editduedate = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[2]"));
                editduedate.SendKeys(DateTime.Today.AddDays(3).ToShortDateString());

                _wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]")));
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]")));
                var edittaskform = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/input[1]"));
                edittaskform.SendKeys(i.ToString("g"));

                var savechanges = _driver.FindElement(By.XPath("//*[@id='main-content']/section/div/div/div/section/div[2]/div/div/form/button"));
                savechanges.Click();

                Thread.Sleep(1000);
            }

            int x = 1;
            do
            {
                  var done = _driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li[{0}]/div[1]/a", x )));
                  done.Click();
                  Thread.Sleep(1000);
                x++;
            } 
            while (x<=3);
            for (int i = 1; i <= 3; i++)
            {
                var name = string.Format("{0}{1}", i.ToString("g"), i.ToString("g"));
                Assert.IsTrue(name == _driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li[{0}]/div[2]/span[1]",i))).Text);
                Assert.IsTrue(_driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li[{0}]/div[2]/span[2]",i))).Text == "Done");

                var date = _driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li[{0}]/div[2]/div/span",i))).Text;
                var actualdate = DateTime.Parse( date.Substring(5));
                Assert.IsTrue( actualdate.ToShortDateString() == DateTime.Today.AddDays(3).ToShortDateString() );
            }
            var assignments = _dataDbContext.Assignments.OrderBy(a => a.Id);

            x = 1; 
            foreach (var assignment in assignments)
            {
                Assert.IsTrue(assignment.Done);
                var name = string.Format("{0}{1}", x.ToString("g"), x.ToString("g"));
                Assert.AreEqual(name,assignment.Name);
                Assert.IsTrue(assignment.DueDate.ToShortDateString() == DateTime.Today.AddDays(3).ToShortDateString());
                x++;
            } 
           

        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _dataDbContext.Dispose();
        }
    }
}
