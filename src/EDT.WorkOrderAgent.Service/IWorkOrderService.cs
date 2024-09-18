using EDT.WorkOrderAgent.Service.Models;

namespace EDT.WorkOrderAgent.Service;

public interface IWorkOrderService
{
    WorkOrder GetWorkOrderInfo(string orderName);
    string UpdateWorkOrderStatus(string orderName, string newStatus);
    string ReduceWorkOrderQuantity(string orderName, int newQuantity);
}
