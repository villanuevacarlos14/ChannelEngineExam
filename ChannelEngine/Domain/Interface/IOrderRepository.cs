using System.Linq.Expressions;

namespace Domain.Interface;

public interface IOrderRepository
{
    Task<InProgress> Get(Expression<Func<InProgress, bool>> expression);
}