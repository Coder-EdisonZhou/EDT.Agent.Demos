namespace EDT.WorkOrderAgent.Service.Models;

public class WorkOrder
{
    public string WorkOrderName { get; set; }
    public string ProductName { get; set; }
    public string ProductVersion { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
}