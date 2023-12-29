namespace ClassLibrary.Shared.Interfaces;

public interface IContact
{
    Guid Id { get; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
}
