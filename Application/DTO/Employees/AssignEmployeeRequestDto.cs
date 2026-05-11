namespace Application.DTO.Employees;

public record AssignEmployeeRequestDto(
    string UserId,
    string JobTitle,
    Guid DepartmentId,
    DateTime? HireDate
);