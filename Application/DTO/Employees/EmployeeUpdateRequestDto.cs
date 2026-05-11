using Domain.Enums;

namespace Application.DTO.Employees;

public record EmployeeUpdateRequestDto(
    string? JobTitle,
    Guid? DepartmentId,
    DateTime? HireDate,
    EmployeeStatus? Status
);
