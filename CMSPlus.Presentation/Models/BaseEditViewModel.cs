namespace CMSPlus.Domain.Models;

public abstract class BaseEditViewModel
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatorId { get; set; }
    public DateTime? CreatedOnUtc { get; set; }
}