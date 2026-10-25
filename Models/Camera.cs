
namespace apbd2.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public bool HasFlash { get; set; }

    public Camera(string name, int megapixels, bool hasFlash)
        : base(name)
    {
        Megapixels = megapixels;
        HasFlash = hasFlash;
    }

    public override string GetDetails()
    {
        return $"{Name} - {Megapixels}MP, Flash: {HasFlash}";
    }
}
