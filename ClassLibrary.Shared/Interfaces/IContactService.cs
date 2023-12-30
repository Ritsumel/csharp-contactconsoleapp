namespace ClassLibrary.Shared.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Add a contact to a contact list
    /// </summary>
    /// <param name="Contact">a Contact of type IContact</param>
    /// <returns>Returns true if successful, or false if it fails or Contact already exists</returns>
    /// <exception cref="NotImplementedException"></exception>
    bool AddContactToList(IContact contact);

    /// <summary>
    /// Retrieves all contacts from the file
    /// </summary>
    /// <returns>Returns the list of contacts if successful retrieving contacts, or null if it fails</returns>
    IEnumerable<IContact> GetContactsFromList();

    /// <summary>
    /// Retrieves a contact from the list based on email
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Returns contact if successful retrieving contacts, or null if it fails</returns>
    IContact? GetContactFromList(string? email);

    /// <summary>
    /// Removes a contact from the list
    /// </summary>
    /// <param name="contact"></param>
    /// <returns>Returns true if the contact was successfully removed, or false if it fails</returns>
    bool RemoveContact(IContact contact);
}
