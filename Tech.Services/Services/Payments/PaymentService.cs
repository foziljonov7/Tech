using AutoMapper;
using Tech.DAL.DTOs.PaymentDTOs;
using Tech.DAL.DTOs.RegistryDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Generics;
using Tech.Services.Interfaces.Payments;

namespace Tech.Services.Services.Payments;

public class PaymentService(
    IRepository<Payment> repository,
    IRegistry registryService,
    IMapper mapper) : IGettable<GetPaymentDto>, IPayment<GetPaymentDto, PaymentForCourseDto>
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

    public async Task<GetPaymentDto> PostPaymentForCourseAsync(PaymentForCourseDto entity, CancellationToken cancellation = default)
    {
        try
        {
            var reg = new RegistryDto
            {
                Credit = (double)entity.Amount,
                Debit = 0
            };

            var registry = await registryService.PostRegistryAsync(reg, cancellation);
            var mapped = mapper.Map<Payment>(entity);

            mapped.RegistryId = (registry as Registry).Id;

            await repository.AddAsync(mapped, cancellation);
            await repository.SaveAsync(cancellation);

            var result = mapper.Map<GetPaymentDto>(mapped);
            return result;
        }
        catch(Exception ex)
        {
            throw new CustomException(404, "Registry not found!");
        }
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
