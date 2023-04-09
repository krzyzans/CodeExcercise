using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace CodeExcercise.tests.ControllerTests
{
    [TestFixture]
    public class ControllerTests
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly string baseAddress = "/api/Customer";

        private readonly ClientCustomer customerOne = new ClientCustomer("Test1","Test11");
        private readonly ClientCustomer customerTwo = new ClientCustomer("Test2","Test22");
        private string customerOneJson = string.Empty;
        private string customerTwoJson = string.Empty;
        private Guid ident;

        public ControllerTests()
        {
            this.factory = new WebApplicationFactory<Program>();
        }

        [OneTimeSetUp]
        public void SetupOneTime()
        {
            customerOneJson = JsonSerializer.Serialize(customerOne);
            customerTwoJson = JsonSerializer.Serialize(customerTwo);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Order(1)]
        public async Task CreateCustomers()
        {
            // Arrange
            var client = factory.CreateDefaultClient();

            // Act
            var responseOne = await client.PostAsync(baseAddress, new StringContent(customerOneJson, Encoding.UTF8, "application/json"));
            var responseTwo = await client.PostAsync(baseAddress, new StringContent(customerTwoJson, Encoding.UTF8, "application/json"));

            // Assert
            responseOne.EnsureSuccessStatusCode(); 
            responseTwo.EnsureSuccessStatusCode(); 

            //RecordData
            ident = JsonSerializer.Deserialize<Guid>(await responseOne.Content.ReadAsStringAsync());
        }

        [Test]
        [Order(2)]
        public async Task ReadCustomers()
        {
            // Arrange
            var client = factory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync(baseAddress);
            IEnumerable<Customer> customerList = JsonSerializer.Deserialize<IEnumerable<Customer>>(await response.Content.ReadAsStringAsync())
                                                 ?? Enumerable.Empty<Customer>();

            // Assert
            response.EnsureSuccessStatusCode();
            
            Assert.That(() => customerList.Count() == 2);
        }

        [Test]
        [Order(3)]
        public async Task DeleteCustomers()
        {
            // Arrange
            var client = factory.CreateDefaultClient();

            // Act
            var responseDelete = await client.DeleteAsync(string.Concat(baseAddress,$"/{ident}"));
            var responseGet = await client.GetAsync(baseAddress);
            IEnumerable<Customer> customerList = JsonSerializer.Deserialize<IEnumerable<Customer>>(await responseGet.Content.ReadAsStringAsync())
                                                 ?? Enumerable.Empty<Customer>();

            // Assert
            responseDelete.EnsureSuccessStatusCode();
            responseGet.EnsureSuccessStatusCode();
            
            Assert.That(() => customerList.Count() == 1 && !customerList.Any(c => c.Ident == ident));
        }
    }
}