namespace CMSPlus.Domain.Models;

public class BaseCreateViewModel
{
    public string SystemName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string CreatorId { get; set; }
}