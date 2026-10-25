
namespace apbd2.Models;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; }

    public bool IsAvailable { get; set; } = true;

    protected Equipment(string name)
    {
        Name = name;
    }

    public abstract string GetDetails();
}
