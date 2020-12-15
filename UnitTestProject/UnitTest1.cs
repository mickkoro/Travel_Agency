using System;
using Travel_Agency;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() //вывод туров
        {
            Tours tours = new Tours("manager");
            string[] expected = { "Tour_name", "Tour_description", "Tour_dateFrom", "Tour_dateTo", "Tour_price" };
            string[] t = tours.test("showTours");
            int count = t.Length;
            Assert.AreEqual(5, count);
            for(int i = 0; i < 5; i++)
            {
                Assert.AreEqual(expected[i], t[i]);
            }
        }
        [TestMethod]
        public void TestMethod2() //вывод групп
        {
            Tours tours = new Tours("manager");
            string[] expected = { "Tour_name", "Group_number" };
            string[] t = tours.test("showGroups");
            int count = t.Length;
            Assert.AreEqual(2, count);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(expected[i], t[i]);
            }
        }
        [TestMethod]
        public void TestMethod3() //цена типа double
        {
            Tours tours = new Tours("manager");
            string expected = "System.Double";
            string[] t = tours.test("correctType");
            Assert.AreEqual(expected, t[0]);
        }
        [TestMethod]
        public void TestMethod4() //изменение логина
        {
            string testLog = "TestUser";
            string expected = "TestUser1";
            MyAccount account = new MyAccount(testLog);
            string actual = account.testUpdate(expected);
            Assert.AreEqual(expected, actual);
            account.testUpdate(testLog);
        }
        [TestMethod]
        public void TestMethod5() //добавление группы
        {
            TravelAgent agent = new TravelAgent("TestUser");
            int expected = agent.testCount() + 1;
            agent.addTest();
            int actual = agent.testCount();
            agent.deleteTest();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod6() //удаление группы
        {
            TravelAgent agent = new TravelAgent("TestUser");
            agent.addTest();
            int expected = agent.testCount() - 1;
            agent.deleteTest();
            int actual = agent.testCount();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod7() // проверка паспортных данных
        {
            TravelAgent agent = new TravelAgent("TestUser");
            string res = agent.test();
            Assert.AreEqual(res, "YES");
        }
        [TestMethod]
        public void TestMethod8() //проверка на то, что пользователь с таким логином и паролем не существует
        {
            int expected = -1;
            LoginForm login = new LoginForm();
            int actual = login.test("User", "User");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMethod9() //добавление тура
        {
            Tours tour = new Tours("manager");
            int expected = tour.countRows() + 1;
            AddTourOrGroup newTour = new AddTourOrGroup("manager");
            newTour.addTour();
            int actual = tour.countRows();
            newTour.deleteTour();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod10() //удаление тура
        {
            Tours tour = new Tours("agent1");
            AddTourOrGroup newTour = new AddTourOrGroup("agent1");
            newTour.addTour();
            int expected = tour.countRows() - 1;
            newTour.deleteTour();
            int actual = tour.countRows();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Script1() //сценарий для менеджера: авторизация, просмотр туров, добавление и удаление тура
        {
            LoginForm login = new LoginForm();
            string role = login.role("manager");
            Assert.AreEqual("Менеджер", role);
            int pass = login.correctPass("manager", "manager");
            Assert.AreEqual(1, pass);
            TestMethod1();
            TestMethod9();
            TestMethod10();
        }

        [TestMethod]
        public void Script2() //сценарий для турагента: авторизация, изменение логина, добавление и удаление туристической группы
        {
            LoginForm login = new LoginForm();
            string role = login.role("agent1");
            Assert.AreEqual("Турагент", role);
            int pass = login.correctPass("agent1", "agent1");
            Assert.AreEqual(1, pass);
            TestMethod4();
            TestMethod5();
            TestMethod6();
        }

    }
}
