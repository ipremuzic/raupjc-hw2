using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Zad3
{
    [TestClass]
    public class UnitTest1
    {

        //TodoItem test methods

        [TestMethod]
        public void MarkAsCompleted_Test()
        {
            TodoItem item= new TodoItem("TodoItem 1.");

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

            Assert.IsTrue(item.GetHashCode()==item.GetHashCode());
            Assert.IsFalse(item.GetHashCode()==item2.GetHashCode());
        }


        //TodoRepository test methods

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddGet_Test()
        {
            ITodoRepository repository=new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");

            Assert.IsNull(repository.Get(item.Id));
            Assert.AreEqual(item, repository.Add(item));
            Assert.AreEqual(item, repository.Get(item.Id));

            TodoItem item2=new TodoItem("TodoItem 2.");

            Assert.AreEqual(item2, repository.Add(item2));
            Assert.AreEqual(item, repository.Get(item.Id));

            try
            {
                repository.Add(item);
            }
            catch (DuplicateTodoItemException ex)
            {
                Assert.AreEqual(ex.Message, $"duplicate id: {item.Id}");
            }

            repository.Add(item2);
        }


        [TestMethod]
        public void remove_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");
            repository.Add(item);

            Assert.AreEqual(item, repository.Get(item.Id));
            Assert.IsTrue(repository.Remove(item.Id));
            Assert.IsNull(repository.Get(item.Id));
            Assert.IsFalse(repository.Remove(item2.Id));
        }

        [TestMethod]
        public void update_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");

            Assert.AreEqual(item, repository.Update(item));
            Assert.AreEqual(repository.Get(item.Id),repository.Update(item));

            item.DateCompleted = DateTime.MaxValue;
            item.DateCreated = DateTime.MinValue;
            repository.Update(item);

            Assert.AreEqual(repository.Get(item.Id).DateCompleted, repository.Update(item).DateCompleted);
            Assert.AreEqual(repository.Get(item.Id).DateCreated, repository.Update(item).DateCreated);
        }


        [TestMethod]
        public void MarkAsCompleted2_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");

            Assert.IsFalse(repository.MarkAsCompleted(item.Id));

            repository.Add(item);
            Assert.IsFalse(item.IsCompleted);

            Assert.IsTrue(repository.MarkAsCompleted(item.Id));
            Assert.IsTrue(item.IsCompleted);
            Assert.IsFalse(repository.MarkAsCompleted(item.Id));

        }

        [TestMethod]
        public void GetAll_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");
            item.DateCreated = DateTime.MinValue;
            TodoItem item2 = new TodoItem("TodoItem 2.");
            TodoItem item3 = new TodoItem("TodoItem 3.");
            item3.DateCreated=DateTime.MaxValue;
            repository.Add(item);
            repository.Add(item2);
            repository.Add(item3);

            List<TodoItem> list = new List<TodoItem>(){ item, item2, item3};

            Assert.IsTrue(list.OrderByDescending(t => t.DateCreated)
                              .ToList()
                              .SequenceEqual(repository.GetAll()));
        }


        [TestMethod]
        public void GetActive_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");
            TodoItem item3 = new TodoItem("TodoItem 3.");
            TodoItem item4 = new TodoItem("TodoItem 4.");
            TodoItem item5 = new TodoItem("TodoItem 5.");

            item2.MarkAsCompleted();
            item4.MarkAsCompleted();

            repository.Add(item);
            repository.Add(item2);
            repository.Add(item3);
            repository.Add(item4);
            repository.Add(item5);

            List<TodoItem> list= new List<TodoItem>(){item, item3, item5};

            Assert.IsTrue(new HashSet<TodoItem>(list).SetEquals(repository.GetActive()));
            Assert.AreEqual(list.Count, repository.GetActive().Count);                      //nepotrebno vjv
        }

        [TestMethod]
        public void GetCompleted_Test()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");
            TodoItem item3 = new TodoItem("TodoItem 3.");
            TodoItem item4 = new TodoItem("TodoItem 4.");
            TodoItem item5 = new TodoItem("TodoItem 5.");

            item2.MarkAsCompleted();
            item4.MarkAsCompleted();

            repository.Add(item);
            repository.Add(item2);
            repository.Add(item3);
            repository.Add(item4);
            repository.Add(item5);

            List<TodoItem> list = new List<TodoItem>() { item2, item4 };

            Assert.IsTrue(new HashSet<TodoItem>(list).SetEquals(repository.GetCompleted()));
            Assert.AreEqual(list.Count, repository.GetCompleted().Count);                      //nepotrebno vjv
        }

        [TestMethod]
        public void GetFiltered()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("TodoItem 1.");
            TodoItem item2 = new TodoItem("TodoItem 2.");
            TodoItem item3 = new TodoItem("TodoItem 3.");
            TodoItem item4 = new TodoItem("TodoItem 4.");
            TodoItem item5 = new TodoItem("TodoItem 5.");

            item2.MarkAsCompleted();
            item4.MarkAsCompleted();

            repository.Add(item);
            repository.Add(item2);
            repository.Add(item3);
            repository.Add(item4);
            repository.Add(item5);

            List<TodoItem> list = new List<TodoItem>() {item2, item4};

            Assert.IsTrue(new HashSet<TodoItem>(list).SetEquals(repository.GetFiltered(t => t.IsCompleted)));
        }

    }
}
