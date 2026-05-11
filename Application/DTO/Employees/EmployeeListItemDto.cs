using Domain.Enums;

namespace Application.DTO.Employees;

public record EmployeeListItemDto(
    string Id,
    string FullName,
    string Email,
    string JobTitle,
    string DepartmentName,
    string Status
);