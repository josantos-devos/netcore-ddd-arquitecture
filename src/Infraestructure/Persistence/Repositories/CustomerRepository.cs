using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);

    public async Task<Customer?> GetById(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
}