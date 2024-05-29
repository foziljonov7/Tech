using Tech.DAL.DTOs.ExportDTOs;

namespace Tech.Services.Interfaces.Exports;

public interface IExport
{
    Task<FileResultDto> ExportToExcelAsync(CancellationToken cancellation = default);
}
