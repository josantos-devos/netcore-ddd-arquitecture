using ErrorOr;

namespace Domain.DomainErrors;

public static partial class DomainErrors
{
    public static partial class Customer
    {
        public static Error PhoneNumberWithBadFormat => Error.Validation("Customer.PhoneNumber", "Phone Number is not valid.");

        public static Error AddressWithBadFormat => Error.Validation("Customer.Address", "Address is not valid.");
    }
}