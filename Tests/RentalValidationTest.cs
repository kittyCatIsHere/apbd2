
using apbd2.Models;
using apbd2.Services;

namespace apbd2.Tests;

public static class RentalValidationTests
{
    public static void Run()
    {
        Console.WriteLine("\n=== RENTAL VALIDATION TESTS ===");

        RentalService service = new RentalService();

        User student = new User(
            "Test", "Student", UserType.Student);

        Laptop laptop = new Laptop(
            "Test Laptop", 16, "Intel i5");

        service.AddUser(student);
        service.AddEquipment(laptop);

        // Test 1: Zero days
        try
        {
            service.RentEquipment(student, laptop, 0);
            Console.WriteLine("FAIL: Zero days accepted");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("PASS: Zero days rejected");
        }

        // Test 2: More than 30 days
        try
        {
            service.RentEquipment(student, laptop, 31);
            Console.WriteLine("FAIL: 31 days accepted");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("PASS: 31 days rejected");
        }

        // Test 3: Maximum allowed duration
        Rental rental = service.RentEquipment(
            student, laptop, 30);

        Console.WriteLine(
            rental.IsActive
                ? "PASS: 30 days accepted"
                : "FAIL: 30-day rental inactive");

        // Test 4: Already rented equipment
        try
        {
            service.RentEquipment(student, laptop, 5);
            Console.WriteLine("FAIL: Unavailable equipment rented");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("PASS: Duplicate rental rejected");
        }
    }
}
