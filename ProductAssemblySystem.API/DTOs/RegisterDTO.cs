namespace ProductAssemblySystem.API.DTOs
{
    public record RegisterDTO(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string PostalCode,
        string Region,
        string City,
        string Street,
        string HouseNumber,
        string ApartmentNumber,
        string Password,
        string RepeatPassword);
}
