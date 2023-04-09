using Abstraction.Repositories;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.CQRS.Handlers;
using CodeExcercise.CQRS.Notification;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CodeExcercise.tests.CQRSTests.HandlersTests
{
    [TestFixture]
    public class DeleteCustomHandlerTests
    {
        private DeleteCustomerHandler deleteCustomerHandler;

        private readonly Guid customerGuid = Guid.NewGuid();
        private DeleteCustomerRequest request;

        Mock<IRepository<DatabaseCustomer>> customerRepository;

        [SetUp]
        public void Setup()
        {
            request = new DeleteCustomerRequest(customerGuid);
            customerRepository = new Mock<IRepository<DatabaseCustomer>>();

            deleteCustomerHandler = new DeleteCustomerHandler(
                NullLogger<DeleteCustomerHandler>.Instance,
                customerRepository.Object
                );
        }

        [Test]
        public async Task CreateRepositoryIsExecuted()
        {
            // Act
            await deleteCustomerHandler.Handle(request, CancellationToken.None);

            // Assert
            customerRepository
                .Verify(v => v.Delete(customerGuid,CancellationToken.None), Times.Once);
        }
    }
}
