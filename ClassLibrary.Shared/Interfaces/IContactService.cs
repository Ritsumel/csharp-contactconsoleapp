namespace ClassLibrary.Shared.Interfaces;

public interface IContactService
{
    bool AddContactToList(IContact contact);
    IEnumerable<IContact> GetContactsFromList();
    IContact? GetContactFromList(string? email);
    bool RemoveContact(IContact contact);
}
