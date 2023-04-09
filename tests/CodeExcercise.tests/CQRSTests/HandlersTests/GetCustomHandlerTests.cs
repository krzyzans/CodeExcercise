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
    public class GetCustomHandlerTests
    {
        private GetCustomersHandler getCustomersHandler;

        private GetCustomersRequest request = new GetCustomersRequest();

        Mock<IMapService<Customer, DatabaseCustomer>> mapperService;
        Mock<IRepository<DatabaseCustomer>> customerRepository;

        [SetUp]
        public void Setup()
        {
            mapperService = new Mock<IMapService<Customer, DatabaseCustomer>>();
            customerRepository = new Mock<IRepository<DatabaseCustomer>>();

            getCustomersHandler = new GetCustomersHandler(
                NullLogger<GetCustomersHandler>.Instance,
                mapperService.Object,
                customerRepository.Object
                );
        }

        [Test]
        public async Task GetAllRepositoryIsExecuted()
        {
            // Act
            await getCustomersHandler.Handle(request, CancellationToken.None);

            // Assert
            customerRepository
                .Verify(v => v.GetAll(CancellationToken.None), Times.Once);
        }
    }
}
