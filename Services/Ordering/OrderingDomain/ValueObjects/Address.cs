namespace OrderingDomain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? Email { get; } = default!;
    public string Street { get; } = default!;
    public string City { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;
    public string Country { get; } = default!;

    protected Address() { }
    private Address(string firstName, string lastName, string? email, string street, string city, string state, string zipCode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public static Address Of(string firstName, string lastName, string? email, string street, string city, string state, string zipCode, string country)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrEmpty(street, nameof(street));
        ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
        ArgumentException.ThrowIfNullOrEmpty(state, nameof(state));
        ArgumentException.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
        ArgumentException.ThrowIfNullOrEmpty(country, nameof(country));

        return new Address(firstName, lastName, email, street, city, state, zipCode, country);
    }
}
