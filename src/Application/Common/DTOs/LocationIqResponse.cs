namespace Application.Common.DTOs;

public class LocationIqResponse
{
    public string Display_name { get; set; } = "";
    public Address Address { get; set; } = new();
}

public class Address
{
    public string City { get; set; } = null!;
    public string Town { get; set; } = null!;
    public string Village { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
}