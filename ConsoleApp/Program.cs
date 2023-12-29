using ConsoleApp.Services;
class Program
{
    static void Main()
    {
        MenuService.LoadContacts();

        Console.Clear();
        MenuService.MainMenu();

        Console.ReadKey();
    }
}
