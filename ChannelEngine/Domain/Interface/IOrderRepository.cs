using System.Linq.Expressions;

namespace Domain.Interface;

public interface IOrderRepository
{
    Task<InProgressOrders> GetInProgress();
}