using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Zad3
{
    [TestClass]
    public class TodoItemTests
    {
        [TestMethod]
        public void MarkAsCompleted_Test()
        {
            TodoItem item = new TodoItem("TodoItem 1.");

            Assert.IsFalse(item.IsCompleted);

            item.MarkAsCompleted();
            Assert.IsTrue(item.IsCompleted);

            Assert.IsFalse(item.MarkAsCompleted());
        }

        [TestMethod]
        public void Equals_Test()
        {
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");

            Assert.IsTrue(item.Equals(item));
            Assert.IsFalse(item.Equals(item2));
        }

        [TestMethod]
        public void GetHashCode_Test()
        {
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");

            Assert.IsTrue(item.GetHashCode() == item.GetHashCode());
            Assert.IsFalse(item.GetHashCode() == item2.GetHashCode());
        }
    }
}
