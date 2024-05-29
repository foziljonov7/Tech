using Microsoft.EntityFrameworkCore;
using Tech.DAL.DbContexts;
using Tech.DAL.DTOs.AttendanceDTOs;
using Tech.DAL.DTOs.CategoryDTOs;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.DAL.DTOs.PaymentDTOs;
using Tech.DAL.DTOs.SubjectDTOs;
using Tech.Infrastructure.Interfaces;
using Tech.Infrastructure.Repositories;
using Tech.Services.Interfaces.Attendaces;
using Tech.Services.Interfaces.Courses;
using Tech.Services.Interfaces.Generics;
using Tech.Services.Interfaces.Payments;
using Tech.Services.Interfaces.Users;
using Tech.Services.Services.Attendances;
using Tech.Services.Services.Categories;
using Tech.Services.Services.Courses;
using Tech.Services.Services.Payments;
using Tech.Services.Services.Subjects;
using Tech.Services.Services.Users;

namespace Tech.API.Helpers.Configurations;

public static class ServiceConfigure
{
	public static IServiceCollection AddDbConfigure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("localhost");
		
		//DbContext configuration
		services.AddDbContext<AppDbContext>(options
			=> options.UseNpgsql(connectionString));

		return services;
	}

	public static IServiceCollection AddServiceConfigure(
		this IServiceCollection services)
	{
		//Repositories configuration
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		//Auth services
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IAccountService, AccountService>();

		//Course services
		services.AddScoped<IGettable<CourseDto>, CourseService<CourseDto>>();
		services.AddScoped<IModification<CourseDto, CourseForCreateDto, CourseForUpdateDto>, CourseService<CourseDto>>();
		services.AddScoped<IIncludable<CourseDto, string[]>, CourseService<CourseDto>>();
		services.AddScoped<ICourseEnrollment, CourseEnrollmentService>();

		//Category services
		services.AddScoped<IGettable<CategoryDto>, CategoryService>();

		//Subject services
		services.AddScoped<IGettable<SubjectDto>, SubjectService<SubjectDto>>();
		services.AddScoped<IModification<SubjectDto, SubjectForCreateDto, SubjectForUpdateDto>, SubjectService<SubjectDto>>();
		services.AddScoped<IIncludable<SubjectDto, string[]>, SubjectService<SubjectDto>>();

		//Attendance services
		services.AddScoped<IGettable<AttendanceDto>, AttendanceService>();
		services.AddScoped<IAttendance, AttendanceService>();

		//Payment services
		services.AddScoped<IGettable<GetPaymentDto>, PaymentService>();
		services.AddScoped<IPayment<GetPaymentDto, PaymentForCourseDto>, PaymentService>();

		//Registry services
		services.AddScoped<IRegistry, RegistryService>();

		return services;
	}
}
