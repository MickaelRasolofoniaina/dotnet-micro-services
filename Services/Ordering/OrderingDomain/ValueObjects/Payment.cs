namespace OrderingDomain.ValueObjects;

public record Payment
{
    private const int CvvLength = 3;
    private const int CardNumberLength = 16;

    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string Cvv { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() { }

    private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrEmpty(expiration, nameof(expiration));
        ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cardNumber.Length, CardNumberLength, nameof(cardNumber));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, CvvLength, nameof(cvv));

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }

}
