using Domain.Enums;

namespace Application.DTO.Employees;

public record EmployeeResponseDto(
    string Id,
    string FullName,
    string Email,
    string PhoneNumber,
    string JobTitle,
    Guid DepartmentId,
    DateTime HireDate,
      string Status
);
