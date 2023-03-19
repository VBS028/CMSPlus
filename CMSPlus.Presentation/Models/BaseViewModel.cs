namespace CMSPlus.Domain.Models;

public abstract class BaseViewModel
{
    public BaseViewModel()
    {
        CreatedOnUtc = UpdatedOnUtc = DateTime.UtcNow;
    }
    public int Id { get; set; }
    public DateTime? CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
}