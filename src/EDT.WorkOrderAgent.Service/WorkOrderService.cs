using EDT.WorkOrderAgent.Service.Models;

namespace EDT.WorkOrderAgent.Service;

public class WorkOrderService
{
    private static List<WorkOrder> workOrders = new List<WorkOrder>
            {
                new WorkOrder { WorkOrderName = "9050100", ProductName = "A5E900100", ProductVersion = "001/AB", Quantity = 100, Status = "Ready" },
                new WorkOrder { WorkOrderName = "9050101", ProductName = "A5E900101", ProductVersion = "001/AB", Quantity = 200, Status = "Ready" },
                new WorkOrder { WorkOrderName = "9050102", ProductName = "A5E900102", ProductVersion = "001/AB", Quantity = 300, Status = "InProcess" },
                new WorkOrder { WorkOrderName = "9050103", ProductName = "A5E900103", ProductVersion = "001/AB", Quantity = 400, Status = "InProcess" },
                new WorkOrder { WorkOrderName = "9050104", ProductName = "A5E900104", ProductVersion = "001/AB", Quantity = 500, Status = "Completed" }
            };

    public WorkOrder GetWorkOrderInfo(string orderName)
    {
        return workOrders.Find(o => o.WorkOrderName == orderName);
    }

    public string UpdateWorkOrderStatus(string orderName, string newStatus)
    {
        var workOrder = this.GetWorkOrderInfo(orderName);
        if (workOrder == null)
            return "Operate Failed : The work order is not existing!";
        // Update status if it is valid
        workOrder.Status = newStatus;
        return "Operate Succeed!";
    }

    public string ReduceWorkOrderQuantity(string orderName, int newQuantity)
    {
        var workOrder = this.GetWorkOrderInfo(orderName);
        if (workOrder == null)
            return "Operate Failed : The work order is not existing!";

        // Some business checking logic like this
        if (workOrder.Status == "Completed")
            return "Operate Failed : The work order is completed, can not be reduced!";
        if (newQuantity <= 1 || newQuantity >= workOrder.Quantity)
            return "Operate Failed : The new quantity is invalid!";
        // Update quantity if it is valid
        workOrder.Quantity = newQuantity;
        return "Operate Succeed!";
    }
}