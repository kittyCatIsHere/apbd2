
using apbd2.Models;
using apbd2.Services;

// Create services
RentalService rentalService = new RentalService();
ReportService reportService = new ReportService();
EquipmentDisplayService displayService = new EquipmentDisplayService();

// 1. Add equipment
Laptop laptop = new Laptop("Dell XPS", 16, "Intel i7");
Camera camera = new Camera("Canon EOS", 24, true);
Projector projector = new Projector("Epson", 3000, "Full HD");
Laptop laptop2 = new Laptop("HP EliteBook", 32, "Intel i9");

rentalService.AddEquipment(laptop);
rentalService.AddEquipment(camera);
rentalService.AddEquipment(projector);
rentalService.AddEquipment(laptop2);

Console.WriteLine("=== EQUIPMENT ADDED ===");
displayService.DisplayAll(rentalService.EquipmentList);

// 2. Add users
User student = new User("Ali", "Yilmaz", UserType.Student);
User employee = new User("Ayse", "Demir", UserType.Employee);

rentalService.AddUser(student);
rentalService.AddUser(employee);

Console.WriteLine("\n=== USERS ADDED ===");
Console.WriteLine($"{student.FirstName} - Student");
Console.WriteLine($"{employee.FirstName} - Employee");

// 3. Successful rental
Console.WriteLine("\n=== SUCCESSFUL RENTAL ===");

Rental rental1 = rentalService.RentEquipment(student, laptop, 7);

Console.WriteLine($"{student.FirstName} rented {laptop.Name}");
displayService.DisplayAll(rentalService.EquipmentList);

// 4. Invalid rental
Console.WriteLine("\n=== INVALID RENTAL TEST ===");

try
{
    rentalService.RentEquipment(employee, laptop, 3);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Blocked: {ex.Message}");
}

// 5. Return on time
Console.WriteLine("\n=== ON-TIME RETURN ===");

rentalService.ReturnEquipment(
    rental1,
    rental1.RentalDate.AddDays(5)
);

Console.WriteLine($"Penalty: {rental1.Penalty} PLN");

// 6. Late return
Console.WriteLine("\n=== LATE RETURN ===");

Rental rental2 = rentalService.RentEquipment(employee, camera, 3);

rentalService.ReturnEquipment(
    rental2,
    rental2.DueDate.AddDays(2)
);

Console.WriteLine($"Penalty: {rental2.Penalty} PLN");

// 7. Mark equipment under maintenance
Console.WriteLine("\n=== MAINTENANCE ===");

rentalService.MarkUnavailable(projector);

Console.WriteLine($"{projector.Name} marked unavailable");

// 8. Display equipment status
displayService.DisplayAll(rentalService.EquipmentList);

// 9. Display available equipment
displayService.DisplayAvailable(rentalService.EquipmentList);

// 10. Student rental limit test
Console.WriteLine("\n=== STUDENT RENTAL LIMIT TEST ===");

User student2 = new User("Mehmet", "Kaya", UserType.Student);
rentalService.AddUser(student2);

Laptop laptop3 = new Laptop("Lenovo ThinkPad", 16, "Intel i5");
Camera camera2 = new Camera("Sony Alpha", 24, true);
Projector projector2 = new Projector("BenQ", 3500, "4K");

rentalService.AddEquipment(laptop3);
rentalService.AddEquipment(camera2);
rentalService.AddEquipment(projector2);

rentalService.RentEquipment(student2, laptop3, 7);
Console.WriteLine("First rental successful.");

rentalService.RentEquipment(student2, camera2, 7);
Console.WriteLine("Second rental successful.");

try
{
    rentalService.RentEquipment(student2, projector2, 7);
    Console.WriteLine("ERROR: Third rental was allowed!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Third rental blocked: {ex.Message}");
}

// 11. Employee rental limit test
Console.WriteLine("\n=== EMPLOYEE RENTAL LIMIT TEST ===");

User employee2 = new User("John", "Smith", UserType.Employee);
rentalService.AddUser(employee2);

for (int i = 1; i <= 6; i++)
{
    Laptop testLaptop = new Laptop(
        $"Test Laptop {i}", 16, "Intel i7");

    rentalService.AddEquipment(testLaptop);

    try
    {
        rentalService.RentEquipment(employee2, testLaptop, 7);
        Console.WriteLine($"Rental {i} successful.");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Rental {i} blocked: {ex.Message}");
    }
}

// 12. Active rentals for student
Console.WriteLine("\n=== STUDENT ACTIVE RENTALS ===");

foreach (var rental in rentalService.GetActiveRentals(student2))
{
    Console.WriteLine(rental.Equipment.Name);
}

// 13. Overdue rentals
Console.WriteLine("\n=== OVERDUE RENTALS ===");

foreach (var rental in rentalService.GetOverdueRentals())
{
    Console.WriteLine(rental.Equipment.Name);
}

// 14. Final equipment list
displayService.DisplayAll(rentalService.EquipmentList);

// 15. Final report
reportService.PrintReport(rentalService);
