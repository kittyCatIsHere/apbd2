
namespace apbd2.Models;

public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();

    public User User { get; }
    public Equipment Equipment { get; }

    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }

    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnDate == null;

    public bool? WasReturnedOnTime =>
        ReturnDate.HasValue
            ? ReturnDate.Value <= DueDate
            : null;

    public Rental(User user, Equipment equipment, int days)
    {
        if (days <= 0)
            throw new ArgumentException(
                "Rental duration must be positive.");

        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(days);
    }

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        if (!IsActive)
            throw new InvalidOperationException(
                "Equipment already returned.");

        if (returnDate < RentalDate)
            throw new ArgumentException(
                "Return date cannot be before rental date.");

        if (penalty < 0)
            throw new ArgumentException(
                "Penalty cannot be negative.");

        ReturnDate = returnDate;
        Penalty = penalty;
    }
}
