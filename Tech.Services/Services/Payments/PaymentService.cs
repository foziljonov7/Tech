using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Tech.DAL.DTOs.PaymentDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Generics;
using Tech.Services.Interfaces.Payments;

namespace Tech.Services.Services.Payments;

public class PaymentService(
    IRepository<Payment> repository,
    IMapper mapper) : IGettable<GetPaymentDto>, IPayment<GetPaymentDto>
{
    public async Task<IEnumerable<GetPaymentDto>> GetPaymentByCoursesAsync(int id, CancellationToken cancellation = default)
    {
        var payments = await repository.SelectAllAsync(x => x.CourseId == id, null, cancellation);

        if (payments is null)
            throw new CustomException(404, "Payment not found");

        var mapped = mapper.Map<IEnumerable<GetPaymentDto>>(payments);
        return mapped;
    }

    public async Task<IEnumerable<GetPaymentDto>> GetPaymentByStudentAsync(int id, CancellationToken cancellation = default)
    {
        var payments = await repository.SelectAllAsync(x => x.StudentId == id, null, cancellation);

        if (payments is null)
            throw new CustomException(404, "Payments not found");

        var mapped = mapper.Map<IEnumerable<GetPaymentDto>>(payments);
        return mapped;
    }

    public async Task<IEnumerable<GetPaymentDto>> RetreiveAllAsync(CancellationToken cancellation = default)
    {
        var payments = await repository.SelectAllAsync(null, null, cancellation);

        if (payments is null)
            throw new CustomException(404, "Payment not found");

        var mapped = mapper.Map<IEnumerable<GetPaymentDto>>(payments);
        return mapped;
    }

    public async Task<GetPaymentDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
    {
        var payment = await repository.SelectAsync(x => x.Id == id, null, cancellation);

        if (payment is null)
            throw new CustomException(404, "Payment not found");

        var mapped = mapper.Map<GetPaymentDto>(payment);
        return mapped;
    }
}
