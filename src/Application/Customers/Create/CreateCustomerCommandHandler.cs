using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return DomainErrors.Customer.PhoneNumberWithBadFormat;
            }

            if (Address.Create(request.Country, request.Line1) is not Address address)
            {
                return DomainErrors.Customer.AddressWithBadFormat;
            }

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                request.Name,
                request.LastName,
                request.Email,
                phoneNumber,
                address,
                true
            );

            await _customerRepository.Add(customer);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateCustomer.Failure", ex.Message);
        }
    }
}
