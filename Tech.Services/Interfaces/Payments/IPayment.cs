namespace Tech.Services.Interfaces.Payments;

public interface IPayment<TEntity>
{
    Task<IEnumerable<TEntity>> GetPaymentByCoursesAsync(int id, CancellationToken cancellation = default);
    Task<IEnumerable<TEntity>> GetPaymentByStudentAsync(int id, CancellationToken cancellation = default);
}
