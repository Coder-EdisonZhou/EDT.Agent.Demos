using EDT.WorkOrderAgent.Service.Models;

namespace EDT.WorkOrderAgent.Service;

public interface IWorkOrderService
{
    string GetWorkOrderInfo(string orderName);
    WorkOrder GetWorkOrderModel(string orderName);
    string UpdateWorkOrderStatus(string orderName, string newStatus);
    string ReduceWorkOrderQuantity(string orderName, int newQuantity);
}
