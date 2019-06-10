using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using slipkeCST.Controllers;
using slipkeCST.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Net;

namespace slipkeCST.Test
{
    [TestClass]
    public class TestPersonController
    {
        [TestMethod]
        public void GetPeopleTest()
        {
            var context = new TestPersonContext();
            context.People.Add(new Person() { Id = 1, FirstName = "TestFN", LastName = "TestLN", Address = "TestA", City = "TestC", State = "TestS", Zip = "TestZ" });
            context.People.Add(new Person() { Id = 2, FirstName = "TestFN", LastName = "TestLN", Address = "TestA", City = "TestC", State = "TestS", Zip = "TestZ" });
            context.People.Add(new Person() { Id = 3, FirstName = "TestFN", LastName = "TestLN", Address = "TestA", City = "TestC", State = "TestS", Zip = "TestZ" });

            PersonController controller = new PersonController(context);
            var result = controller.GetPeople() as TestPersonDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public async Task DeletePersonTest()
        {
            var context = new TestPersonContext();
            var person = GetTestPerson();
            context.People.Add(person);

            var controller = new PersonController(context);
            var result = await controller.DeletePerson(1) as OkNegotiatedContentResult<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual(person.Id, result.Content.Id);
        }

        [TestMethod]
        public async Task GetPersonTest()
        {
            var context = new TestPersonContext();
            var person = GetTestPerson();
            context.People.Add(person);

            var controller = new PersonController(context);
            var result = await controller.GetPerson(1) as OkNegotiatedContentResult<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual(person.Id, result.Content.Id);
        }

        [TestMethod]
        public async Task PutPersonTestSuccess()
        {
            var person = GetTestPerson();

            var controller = new PersonController(new TestPersonContext());
            var result = await controller.PutPerson(1, person) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public async Task PutPersonTestFail()
        {
            var person = GetTestPerson();

            var controller = new PersonController(new TestPersonContext());
            var result = await controller.PutPerson(2, person);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostPersonTest()
        {
            var person = GetTestPerson();

            var controller = new PersonController(new TestPersonContext());
            var result = await controller.PostPerson(person) as CreatedAtRouteNegotiatedContentResult<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "GetPerson");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.FirstName, person.FirstName);
        }

        Person GetTestPerson()
        {
            return new Person() { Id = 1, FirstName = "TestFN", LastName = "TestLN", Address = "TestA", City = "TestC", State = "TestS", Zip = "TestZ" };
        }
    }
}
