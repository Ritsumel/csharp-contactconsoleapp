using ClassLibrary.Shared.Interfaces;
using ClassLibrary.Shared.Models;
using ClassLibrary.Shared.Services;

namespace ConsoleApp.Services
{
    internal class MenuService
    {
        private static readonly IContactService _ContactService = new ContactService();
        private static List<IContact> _Contacts;

        static MenuService()
        {
            _Contacts = new List<IContact>();
        }

        public static void AddContactMenu()
        {
            IContact contact = new Contact();

            Console.Write("Enter your first name: ");
            contact.FirstName = Console.ReadLine()!;

            Console.Write("Enter your last name: ");
            contact.LastName = Console.ReadLine()!;

            Console.Write("Enter your email: ");
            contact.Email = Console.ReadLine()!;

            Console.Write("Enter your phone number: ");
            contact.PhoneNumber = Console.ReadLine()!;

            Console.Write("Enter your address information: ");
            contact.Address = Console.ReadLine()!;

            _ContactService.AddContactToList(contact);
        }

        public static void ShowAllContactsMenu()
        {
            LoadContacts(); // Load to make sure everything is up to date

            int selectedOption = 1; // Start with the first option

            var contacts = _ContactService.GetContactsFromList();
            if (contacts is not null && contacts.Any())
            {
                bool exitMenu = false;
                while (!exitMenu)
                {
                    Console.Clear();
                    Console.WriteLine("List of Contacts:");

                    // Display options with arrow
                    for (int i = 1; i <= contacts.ToList().Count; i++)
                    {
                        Console.WriteLine($"{(selectedOption == i ? "->" : "  ")} {GetContactDisplayName(contacts.ToList()[i - 1])}");
                    }

                    Console.WriteLine("Use arrow keys to navigate, Enter to view details, or 0 to go back.");

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedOption = Math.Max(1, selectedOption - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selectedOption = Math.Min(contacts.ToList().Count(), selectedOption + 1);
                            break;
                        case ConsoleKey.Enter:
                            if (selectedOption >= 1 && selectedOption <= contacts.Count())
                            {
                                ViewContactDetails(contacts.ToList()[selectedOption - 1]);
                                // Reset selected option after viewing details
                                selectedOption = 1;
                            }
                            break;
                        case ConsoleKey.D0:
                            exitMenu = true; // Go back to the main menu
                            break;
                        default:
                            Console.WriteLine("Invalid key. Please try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No contacts found.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static string GetContactDisplayName(IContact contact)
        {
            return $"{contact.FirstName} {contact.LastName}";
        }

        private static void ViewContactDetails(IContact contact)
        {
            Console.Clear();
            Console.WriteLine("Contact Details:");
            Console.WriteLine($"First Name: {contact.FirstName}");
            Console.WriteLine($"Last Name: {contact.LastName}");
            Console.WriteLine($"Address: {contact.Address}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");

            Console.WriteLine("Press any key to go back to the list of contacts...");
            Console.ReadKey();
        }

        public static void LoadContacts()
        {
            try
            {
                _Contacts = _ContactService.GetContactsFromList().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Contacts: {ex.Message}");
            }
        }

        public static void RemoveContactMenu()
        {
            Console.Write("Enter the email of the contact to remove: ");
            string emailToRemove = Console.ReadLine()!;

            var contactToRemove = _ContactService.GetContactFromList(emailToRemove);

            if (contactToRemove != null)
            {
                Console.WriteLine("Contact found:");
                ViewContactDetails(contactToRemove);

                Console.Write("Are you sure you want to remove this contact? (Y/N): ");
                string confirm = Console.ReadLine()?.Trim().ToUpper()!;

                if (confirm == "Y")
                {
                    bool removed = _ContactService.RemoveContact(contactToRemove);
                    if (removed)
                    {
                        Console.WriteLine("Contact removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to remove the contact.");
                    }
                }
                else
                {
                    Console.WriteLine("Contact removal cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        public static void MainMenu()
        {
            int selectedOption = 1;

            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("Main Menu:");

                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine($"{(selectedOption == i ? "->" : "  ")} {GetMenuOptionText(i)}");
                }

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = Math.Max(1, selectedOption - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = Math.Min(4, selectedOption + 1);
                        break;
                    case ConsoleKey.Enter:
                        HandleMenuOption(selectedOption);
                        break;
                    default:
                        Console.WriteLine("Invalid key. Please try again.");
                        break;
                }
            }
        }

        private static string GetMenuOptionText(int option)
        {
            switch (option)
            {
                case 1:
                    return "Add a new contact";
                case 2:
                    return "Show all contacts";
                case 3:
                    return "Remove a contact by email";
                case 4:
                    return "Exit";
                default:
                    return "Invalid option";
            }
        }

        private static void HandleMenuOption(int selectedOption)
        {
            Console.Clear();

            switch (selectedOption)
            {
                case 1:
                    AddContactMenu();
                    break;
                case 2:
                    ShowAllContactsMenu();
                    break;
                case 3:
                    RemoveContactMenu();
                    break;
                case 4:
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
