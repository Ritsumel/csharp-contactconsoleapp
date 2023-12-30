using ClassLibrary.Shared.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary.Shared.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService = new FileService(); // File service for handling file operations
    private List<IContact>? _Contacts; // List to store contacts
    private readonly string _filePath = @"..\..\..\..\..\contacts.json"; // Path to the contacts file

    public ContactService()
    {
        _Contacts = new List<IContact>(); // Initialize the list of contacts
    }

    public ContactService(IFileService fileService)
    {
        _fileService = fileService; // Constructor allowing injection of a custom file service
    }

    public bool AddContactToList(IContact Contact)
    {
        try
        {
            if (_Contacts != null)
            {
                // Check if a contact with the same email already exists
                if (!_Contacts.Any(x => x.Email == Contact.Email))
                {
                    _Contacts.Add(Contact);

                    // Serialize the list of contacts to JSON and save it to the file
                    string json = JsonConvert.SerializeObject(_Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                    var result = _fileService.SaveContentToFile(_filePath, json);
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - AddContactToList:: " + ex.Message);
        }
        return false;
    }

    public IContact? GetContactFromList(string? email)
    {
        try
        {
            GetContactsFromList(); // Ensure the list is loaded from the file

            var contact = _Contacts?.FirstOrDefault(x => x.Email == email);
            return contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - GetContactFromList:: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<IContact> GetContactsFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath); // Get content from the file
            if (!string.IsNullOrEmpty(content))
            {
                // Deserialize the content to a list of contacts and return it
                _Contacts = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                return _Contacts;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - GetContactsFromList:: " + ex.Message);
        }
        return null!;
    }

    public bool RemoveContact(IContact contact)
    {
        try
        {
            _Contacts?.Remove(contact); // Remove the contact from the list

            // Serialize the updated list of contacts to JSON and save it to the file
            string json = JsonConvert.SerializeObject(_Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            var result = _fileService.SaveContentToFile(_filePath, json);

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - RemoveContact:: " + ex.Message);
        }
        return false;
    }
}
