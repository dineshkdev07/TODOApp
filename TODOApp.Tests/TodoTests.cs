using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TODOApp.Pages;

namespace TODOApp.Tests
{
    [TestFixture]
    public class TodoTests
    {
        TodoDriver driver;
        TodoPage todopage;

        [SetUp]
        public void Init()
        {
            driver = new TodoDriver();
            todopage = driver.NavigatetoTodoPage();
        }

        [Test]
        public void AddNewTodo()
        {
            var taskname = "Test Adding new todo";
            todopage.AddTodo(taskname);
            Assert.AreEqual(taskname, todopage.TodosList.Last().TaskText);
        }

        [Test]
        public void MarkTodoAsComplete()
        {
            var taskname = "Test Completing new todo";
            todopage.AddTodo(taskname);
            var newtask = todopage.FindTodo(taskname);
            Assert.AreEqual(taskname, newtask.TaskText);
            newtask.MarkAsComplete();
            Assert.IsTrue(newtask.IsCompleted);
            Assert.AreEqual(0, todopage.GetCount());
        }


        [Test]
        public void DeleteTodo()
        {
            var taskname = "Test Delete new todo";
            todopage.AddTodo(taskname);
            var newtask = todopage.FindTodo(taskname);
            Assert.AreEqual(taskname, newtask.TaskText);
            newtask.DeleteTodo();
            Assert.AreEqual(0, todopage.TodosList.Count);
        }

        [Test]
        public void FilterTodos()
        {
            todopage.AddTodo("ActiveTask");
            todopage.AddTodo("CompletedTask");
            todopage.FindTodo("CompletedTask").MarkAsComplete();
            Assert.AreEqual(2, todopage.TodosList.Count);

            todopage.SelectActiveView();
            Assert.AreEqual(1, todopage.TodosList.Count);
            Assert.AreEqual("ActiveTask", todopage.TodosList.First().TaskText);

            todopage.SelectCompletedView();
            Assert.AreEqual(1, todopage.TodosList.Count);
            Assert.AreEqual("CompletedTask", todopage.TodosList.First().TaskText);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Dispose();
        }
    }
}
