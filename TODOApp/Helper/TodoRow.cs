using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOApp.Helper
{
    public class TodoRow
    {
        private readonly IWebDriver driver;
        private readonly IWebElement todoRowElement;
        private By completeChckBxLoc = By.CssSelector("input[ng-model='todo.completed']");
        private By deleteChckBxLoc = By.CssSelector("button[ng-click='removeTodo(todo)']");

        public TodoRow(IWebDriver driver, IWebElement todoRowElement)
        {
            this.driver = driver;
            this.todoRowElement = todoRowElement;
        }

        public string TaskText => todoRowElement.Text;
        public bool IsCompleted => todoRowElement.GetAttribute("class").Contains("completed");

        public void DeleteTodo()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(todoRowElement).Build().Perform();
            todoRowElement.FindElement(deleteChckBxLoc).Click();
        }

        public void MarkAsComplete()
        {
            todoRowElement.FindElement(completeChckBxLoc).Click();
        }


    }
}
