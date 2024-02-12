namespace Domain.ValueObjects;

public partial record Address
{
    public Address(string country, string line1)
    {
        Country = country;
        Line1 = line1;
    }

    public string Country { get; set; }
    
    public string Line1 { get; set; }

    public static Address? Create(string country, string line1)
    {
        if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(line1))
        {
            return null;
        }

        return new Address(country, line1);
    }
}