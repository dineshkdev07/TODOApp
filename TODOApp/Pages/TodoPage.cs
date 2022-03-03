using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOApp.Helper;

namespace TODOApp.Pages
{
    public class TodoPage
    {
        private readonly IWebDriver driver;

        private By todoInputLoc = By.CssSelector(".new-todo");

        private By allTodosListLoc = By.CssSelector(".todo-list li[ng-repeat*='todo in todos']");

        private By todoCountLoc = By.CssSelector(".todo-count .ng-binding");
        private IWebElement newTaskInputElement => driver.FindElement(todoInputLoc);

        public TodoPage(IWebDriver driver)
        {
            this.driver = driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(a => driver.FindElement(todoInputLoc).Displayed);
        }

        public void AddTodo(string newTaskName)
        {
            newTaskInputElement.Click();
            newTaskInputElement.SendKeys(newTaskName + Keys.Enter);
        }

        public void SelectActiveView()
        {
            driver.FindElement(By.LinkText("Active")).Click();
        }

        public void SelectCompletedView()
        {
            driver.FindElement(By.LinkText("Completed")).Click();
        }

        public IList<TodoRow> TodosList
        {
            get
            {
                var rows = driver.FindElements(allTodosListLoc);
                return rows.Select(row => new TodoRow(driver, row)).ToList();
            }
        }

        public TodoRow FindTodo(string taskName)
        {
            return TodosList.First(row => row.TaskText == taskName);
        }

        public int GetCount()
        {
            var count = Convert.ToInt32(driver.FindElement(todoCountLoc).Text);
            return count;
        }
    }
}
