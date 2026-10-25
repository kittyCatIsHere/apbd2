
namespace apbd2.Models;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; }

    public EquipmentStatus Status { get; private set; }
        = EquipmentStatus.Available;

    public bool IsAvailable =>
        Status == EquipmentStatus.Available;

    protected Equipment(string name)
    {
        Name = name;
    }

    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
    }

    public abstract string GetDetails();
}
