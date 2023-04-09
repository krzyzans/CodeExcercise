using Abstraction.Repositories;
using Abstraction.Services;
using CodeExcercise.Common.Models.Domain;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.CQRS.Handlers;
using CodeExcercise.CQRS.Requests;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CodeExcercise.tests.CQRSTests.HandlersTests
{
    [TestFixture]
    public class CreateCustomHandlerTests
    {
        private CreateCustomerHandler createCustomerHandler;

        private CreateCustomerRequest request =
            new CreateCustomerRequest(new ClientCustomer("Test1", "Test11"));

        Mock<IValidationService<Customer>> validationService;
        Mock<IMapService<Customer, DatabaseCustomer>> mapperService;
        Mock<IRepository<DatabaseCustomer>> customerRepository;

        [SetUp]
        public void Setup()
        {
            validationService = new Mock<IValidationService<Customer>>();
            mapperService = new Mock<IMapService<Customer, DatabaseCustomer>>();
            customerRepository = new Mock<IRepository<DatabaseCustomer>>();

            createCustomerHandler = new CreateCustomerHandler(
                NullLogger<CreateCustomerHandler>.Instance, 
                validationService.Object,
                mapperService.Object,
                customerRepository.Object
                );
        }

        [Test]
        public async Task ValidationIsExecuted()
        {
            // Act
            await createCustomerHandler.Handle(request, CancellationToken.None);

            // Assert
            validationService
                .Verify(v => v.ValidateAndThrow(It.IsAny<Customer>(),CancellationToken.None), Times.AtLeastOnce);
        }

        [Test]
        public async Task CreateRepositoryIsExecuted()
        {
            // Act
            await createCustomerHandler.Handle(request, CancellationToken.None);

            // Assert
            customerRepository
                .Verify(v => v.Create(It.IsAny<DatabaseCustomer>(),CancellationToken.None), Times.AtLeastOnce);
        }
    }
}
