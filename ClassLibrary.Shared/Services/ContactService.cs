using ClassLibrary.Shared.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary.Shared.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService = new FileService();
    private List<IContact> _Contacts;
    private readonly string _filePath = @"c:\SCHOOL\CSHARP\Assignment\contacts.json";

    public ContactService()
    {
        _Contacts = new List<IContact>();
    }

    /// <summary>
    /// Add a Contact to a Contact list
    /// </summary>
    /// <param name="Contact">a Contact of type IContact</param>
    /// <returns>Return true if successful, or false if it fails or Contact already exists</returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool AddContactToList(IContact Contact)
    {
        try
        {
            if (!_Contacts.Any(x => x.Email == Contact.Email))
            {
                _Contacts.Add(Contact);

                string json = JsonConvert.SerializeObject(_Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                var result = _fileService.SaveContentToFile(_filePath, json);
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - AddContactToList:: " + ex.Message);
        }
        return false;
    }

    public bool AddContactsToList(IEnumerable<IContact> Contacts)
    {
        try
        {
            _Contacts.AddRange(Contacts);

            string json = JsonConvert.SerializeObject(_Contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            var result = _fileService.SaveContentToFile(_filePath, json);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ContactService - AddContactsToList:: " + ex.Message);
        }
        return false;
    }

    public IContact? GetContactFromList(string? email)
    {
        try
        {
            GetContactsFromList();

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
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
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
            _Contacts?.Remove(contact);

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
