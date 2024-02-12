namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetById(CustomerId id);

    Task Add(Customer customer);
}