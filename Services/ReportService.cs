
using apbd2.Models;

namespace apbd2.Services;

public class ReportService
{
    public void PrintReport(RentalService service)
    {
        Console.WriteLine("\n===== RENTAL REPORT =====");

        Console.WriteLine(
            $"Total equipment: {service.EquipmentList.Count}");

        Console.WriteLine(
            $"Available equipment: {service.EquipmentList.Count(
                e => e.Status == EquipmentStatus.Available)}");

        Console.WriteLine(
            $"Rented equipment: {service.EquipmentList.Count(
                e => e.Status == EquipmentStatus.Rented)}");

        Console.WriteLine(
            $"Under maintenance: {service.EquipmentList.Count(
                e => e.Status == EquipmentStatus.Maintenance)}");

        Console.WriteLine(
            $"Damaged equipment: {service.EquipmentList.Count(
                e => e.Status == EquipmentStatus.Damaged)}");

        Console.WriteLine(
            $"Total users: {service.Users.Count}");

        Console.WriteLine(
            $"Active rentals: {service.Rentals.Count(r => r.IsActive)}");

        Console.WriteLine(
            $"Overdue rentals: {service.GetOverdueRentals().Count()}");

        Console.WriteLine(
            $"Total penalties: {service.Rentals.Sum(r => r.Penalty)} PLN");

        Console.WriteLine("=========================");
    }
}
