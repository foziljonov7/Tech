using Tech.Domain.Entities;

namespace Tech.Services.Interfaces.Payments;

public interface IPayment<TEntity, TPost>
{
    Task<TEntity> PostPaymentForCourseAsync(TPost entity, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetPaymentByCoursesAsync(int id, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetPaymentByStudentAsync(int id, CancellationToken cancellation = default);
}
