
using apbd2.Models;
using apbd2.Services;

// Create services
RentalService rentalService = new RentalService();
ReportService reportService = new ReportService();

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

foreach (var equipment in rentalService.EquipmentList)
{
    Console.WriteLine(equipment.GetDetails());
}

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

// 4. Invalid rental - unavailable equipment
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

// 7. Available equipment
Console.WriteLine("\n=== AVAILABLE EQUIPMENT ===");

foreach (var equipment in rentalService.GetAvailableEquipment())
{
    Console.WriteLine(equipment.GetDetails());
}

// 8. Mark equipment unavailable
Console.WriteLine("\n=== MAINTENANCE ===");

rentalService.MarkUnavailable(projector);

Console.WriteLine($"{projector.Name} marked unavailable");

// 9. Active rentals
Console.WriteLine("\n=== STUDENT ACTIVE RENTALS ===");

foreach (var rental in rentalService.GetActiveRentals(student))
{
    Console.WriteLine(rental.Equipment.Name);
}

// 10. Overdue rentals
Console.WriteLine("\n=== OVERDUE RENTALS ===");

foreach (var rental in rentalService.GetOverdueRentals())
{
    Console.WriteLine(rental.Equipment.Name);
}

// 11. Final report
reportService.PrintReport(rentalService);




Console.WriteLine("\n=== STUDENT RENTAL LIMIT TEST ===");

User student2 = new User("Mehmet", "Kaya", UserType.Student);
rentalService.AddUser(student2);

Laptop laptop3 = new Laptop("Lenovo ThinkPad", 16, "Intel i5");
Camera camera2 = new Camera("Sony Alpha", 24, true);
Projector projector2 = new Projector("BenQ", 3500, "4K");

rentalService.AddEquipment(laptop3);
rentalService.AddEquipment(camera2);
rentalService.AddEquipment(projector2);

// First rental
rentalService.RentEquipment(student2, laptop3, 7);
Console.WriteLine("First rental successful.");

// Second rental
rentalService.RentEquipment(student2, camera2, 7);
Console.WriteLine("Second rental successful.");

// Third rental - should fail
try
{
    rentalService.RentEquipment(student2, projector2, 7);
    Console.WriteLine("ERROR: Third rental was allowed!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Third rental blocked: {ex.Message}");
}




Console.WriteLine("\n=== EMPLOYEE RENTAL LIMIT TEST ===");

User employee2 = new User("John", "Smith", UserType.Employee);
rentalService.AddUser(employee2);

// Create 6 equipment items
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

// Display final report
reportService.PrintReport(rentalService);
