using Application.Customers.Create;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;

namespace Application.Customers.UnitTests;

public class CreateCustomerCommandHandlerUnitTest
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerUnitTest()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    //1. QUE VAMOS A PROBAR
    //2. EL ESCENARIO
    //3. LO QUE DEBE ARROJAR

    [Fact]
    public async void HanldeCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        //Arrange
        //Se configuran los parametros de entrada de nuestra prueba unitaria.
        CreateCustomerCommand command = new("Jonathan", "Santos", "test@test.com", "8099999999", "", "");

        //Act
        //Se ejecuta el metodo a probar de nuestra prueba unitaria.
        var result = await _handler.Handle(command, default);

        //Assert
        //Se verifican los datos de retorno de nuestro metodo probado en la prueba unitatia.
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(DomainErrors.Customer.PhoneNumberWithBadFormat.Code);
    }
}