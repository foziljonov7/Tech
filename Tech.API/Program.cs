using Tech.API.Helpers.Configurations;
using Tech.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
	.AddEndpointsApiExplorer()
	.AddDbConfigure(builder.Configuration)
	.AddAutoMapper(typeof(MappingProfile))
	.AddServiceConfigure()
	.AddValidatorConfigure()
	.AddConfigureCors()
	.AddSwaggerService()
	.AddJwtService(builder.Configuration)
	.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger().UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
