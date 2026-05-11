namespace Application.DTO.Employees;

public record EmployeeCreateRequestDto(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    string JobTitle,
    Guid DepartmentId,
    DateTime? HireDate
);
