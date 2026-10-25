
using apbd2.Models;

namespace apbd2.Services;

public class EquipmentDisplayService
{
    public void DisplayAll(IEnumerable<Equipment> equipmentList)
    {
        Console.WriteLine("\n=== ALL EQUIPMENT ===");

        foreach (Equipment equipment in equipmentList)
        {
            Console.WriteLine(
                $"{equipment.GetDetails()} | Status: {equipment.Status}");
        }
    }

    public void DisplayAvailable(IEnumerable<Equipment> equipmentList)
    {
        Console.WriteLine("\n=== AVAILABLE EQUIPMENT ===");

        foreach (Equipment equipment in equipmentList)
        {
            if (equipment.IsAvailable)
            {
                Console.WriteLine(
                    $"{equipment.GetDetails()} | Status: {equipment.Status}");
            }
        }
    }
}
