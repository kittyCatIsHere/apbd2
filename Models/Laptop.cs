
namespace apbd2.Models;

public class Laptop : Equipment
{
    public int RamGB { get; set; }
    public string Processor { get; set; }

    public Laptop(string name, int ramGB, string processor)
        : base(name)
    {
        RamGB = ramGB;
        Processor = processor;
    }

    public override string GetDetails()
    {
        return $"{Name} - RAM: {RamGB}GB, CPU: {Processor}";
    }
}
