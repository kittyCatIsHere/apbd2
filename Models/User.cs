
namespace apbd2.Models;

public enum UserType
{
    Student,
    Employee
}

public class User
{
    public Guid Id { get; } = Guid.NewGuid();

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType Type { get; set; }

    public User(string firstName, string lastName, UserType type)
    {
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }

    public int GetRentalLimit()
    {
        return Type == UserType.Student ? 2 : 5;
    }
}
