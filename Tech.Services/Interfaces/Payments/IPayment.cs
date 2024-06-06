using Tech.Domain.Entities;

namespace Tech.Services.Interfaces.Payments;

public interface IPayment<TEntity, TPost>
{
    Task<TEntity> PostPaymentForCourseAsync(TPost entity, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetPaymentByCoursesAsync(long id, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetPaymentByStudentAsync(long id, CancellationToken cancellation = default);
}
