
using apbd2.Models;

namespace apbd2.Services;

public class RentalService
{
    private const int MaxRentalDays = 30;

    public List<Equipment> EquipmentList { get; } = new();
    public List<User> Users { get; } = new();
    public List<Rental> Rentals { get; } = new();

    private readonly PenaltyCalculator calculator = new();

    public void AddEquipment(Equipment equipment)
    {
        if (EquipmentList.Any(e => e.Id == equipment.Id))
            throw new InvalidOperationException(
                "Equipment already registered.");

        EquipmentList.Add(equipment);
    }

    public void AddUser(User user)
    {
        if (Users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException(
                "User already registered.");

        Users.Add(user);
    }

    public Rental RentEquipment(
        User user, Equipment equipment, int days)
    {
        if (!Users.Contains(user))
            throw new InvalidOperationException(
                "User is not registered.");

        if (!EquipmentList.Contains(equipment))
            throw new InvalidOperationException(
                "Equipment is not registered.");

        if (days < 1 || days > MaxRentalDays)
            throw new ArgumentException(
                $"Rental duration must be between 1 and {MaxRentalDays} days.");

        if (!equipment.IsAvailable)
            throw new InvalidOperationException(
                "Equipment is unavailable.");

        int activeRentals = Rentals.Count(
            r => r.User.Id == user.Id && r.IsActive);

        if (activeRentals >= user.GetRentalLimit())
            throw new InvalidOperationException(
                "Rental limit exceeded.");

        Rental rental = new Rental(user, equipment, days);

        Rentals.Add(rental);
        equipment.ChangeStatus(EquipmentStatus.Rented);

        return rental;
    }

    public void ReturnEquipment(Rental rental, DateTime returnDate)
    {
        if (!Rentals.Contains(rental))
            throw new InvalidOperationException(
                "Rental not found.");

        if (!rental.IsActive)
            throw new InvalidOperationException(
                "Equipment already returned.");

        if (returnDate < rental.RentalDate)
            throw new ArgumentException(
                "Invalid return date.");

        decimal penalty = calculator.Calculate(
            rental.DueDate, returnDate);

        rental.CompleteReturn(returnDate, penalty);

        rental.Equipment.ChangeStatus(
            EquipmentStatus.Available);
    }

    public void MarkUnavailable(Equipment equipment)
    {
        if (!EquipmentList.Contains(equipment))
            throw new InvalidOperationException(
                "Equipment not found.");

        if (Rentals.Any(r =>
            r.Equipment.Id == equipment.Id && r.IsActive))
            throw new InvalidOperationException(
                "Equipment is currently rented.");

        equipment.ChangeStatus(EquipmentStatus.Maintenance);
    }

    public void MarkDamaged(Equipment equipment)
    {
        if (!EquipmentList.Contains(equipment))
            throw new InvalidOperationException(
                "Equipment not found.");

        if (Rentals.Any(r =>
            r.Equipment.Id == equipment.Id && r.IsActive))
            throw new InvalidOperationException(
                "Equipment is currently rented.");

        equipment.ChangeStatus(EquipmentStatus.Damaged);
    }

    public void MarkAvailable(Equipment equipment)
    {
        if (!EquipmentList.Contains(equipment))
            throw new InvalidOperationException(
                "Equipment not found.");

        if (Rentals.Any(r =>
            r.Equipment.Id == equipment.Id && r.IsActive))
            throw new InvalidOperationException(
                "Equipment is currently rented.");

        equipment.ChangeStatus(EquipmentStatus.Available);
    }

    public IEnumerable<Equipment> GetAvailableEquipment()
    {
        return EquipmentList.Where(
            e => e.Status == EquipmentStatus.Available);
    }

    public IEnumerable<Rental> GetActiveRentals(User user)
    {
        return Rentals.Where(
            r => r.User.Id == user.Id && r.IsActive);
    }

    public IEnumerable<Rental> GetOverdueRentals()
    {
        return Rentals.Where(
            r => r.IsActive && r.DueDate < DateTime.Now);
    }
}
