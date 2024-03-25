namespace Rent.DAL.DTO;

public class BillToCreateDto
{
    public Guid TenantId { get; set; }

    public Guid AssetId { get; set; }

    public decimal BillAmount { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? EndDate { get; set; }
}