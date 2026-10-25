
namespace apbd2.Services;

public class PenaltyCalculator
{
    private const decimal DailyPenalty = 10m;

    public decimal Calculate(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
        {
            return 0;
        }

        int lateDays = (int)Math.Ceiling(
            (returnDate - dueDate).TotalDays
        );

        return lateDays * DailyPenalty;
    }
}
