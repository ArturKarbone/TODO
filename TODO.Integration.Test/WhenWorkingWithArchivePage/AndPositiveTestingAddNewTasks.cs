﻿using System;
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

    class AndPositiveTestingAddNewTasks
    {
        private DataDbContext _dataDbContext;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        
        [SetUp]
        public void SetUp()
        {
            _dataDbContext = new DataDbContext();
            _driver = new ChromeDriver(@"C:\");
            _wait= new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var assignments = _dataDbContext.Assignments;
            _dataDbContext.Assignments.RemoveRange(assignments);
            _dataDbContext.SaveChanges();
        }
        [Test]
        public void TestingAddNewTasks()
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

            for (int  i = 1;  i <=3;  i++)
            {
                Assert.IsInstanceOf<IWebElement>(_driver.FindElement(By.XPath(string.Format("//*[@id='main-content']/section/div/div[1]/div/section/div/div[1]/ul/li[{0}]/div[2]/span[1]",i))));
            }
            
            var assignments = _dataDbContext.Assignments;
            Assert.IsTrue(assignments.Count() == 3);


        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _dataDbContext.Dispose();
        } 
    }
}
