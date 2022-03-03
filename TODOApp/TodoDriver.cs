using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOApp.Pages;

namespace TODOApp
{
    public class TodoDriver : IDisposable
    {

        public IWebDriver WebDriver { get; set; }

        public TodoDriver()
        {
            WebDriver = new ChromeDriver();
        }
        public void Dispose()
        {
            WebDriver.Close();
        }

        public TodoPage NavigatetoTodoPage()
        {
            WebDriver.Navigate().GoToUrl("http://todomvc.com/examples/angularjs/#/");
            return new TodoPage(WebDriver);
        }
    }
}
