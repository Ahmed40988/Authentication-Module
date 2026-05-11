using Domain.Entities.AuthModules;
using Domain.Entities.Departments;
using Domain.Enums;

namespace Domain.Entities.Employees
{
 
    public class Employee : BaseEntity
    {
        public string Id { get; private set; }
        public string JobTitle { get; private set; } = string.Empty;
        public EmployeeStatus Status { get; private set; } 
        public DateTime HireDate { get; private set; } = DateTime.UtcNow;
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; } = default!;
        public User User { get; private set; } = default!;

        private Employee() { }

        public Employee(
            string userId,
            string jobTitle,
            Guid departmentId,
            DateTime? hireDate = null)
        {
            Id = userId;
            SetJobTitle(jobTitle);
            SetDepartment(departmentId);
            SetHireDate(hireDate);
            Status = EmployeeStatus.Active;
        }

        public Result<bool> ChangeStatus(EmployeeStatus status)
        {
            Status = status;
            return Result<bool>.Success(true);
        }

        private void SetJobTitle(string jobTitle)
        {
            if (string.IsNullOrWhiteSpace(jobTitle))
                throw new ArgumentException(LocalizationKeys.Required);

            JobTitle = jobTitle.Trim();
        }
        private void SetHireDate(DateTime? hireDate)
        {
            var finalDate = hireDate ?? DateTime.UtcNow;

            if (finalDate > DateTime.UtcNow)
                throw new ArgumentException(LocalizationKeys.InvalidDate);

            HireDate = finalDate;
        }
        private void SetDepartment(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                throw new ArgumentException(LocalizationKeys.Invalid);

            DepartmentId = departmentId;
        }
        public Result<bool> Update(
    string jobTitle,
    Guid departmentId,
    EmployeeStatus status)
        {
            SetJobTitle(jobTitle);
            SetDepartment(departmentId);

            Status = status;

            return Result<bool>.Success(true);
        }
        public Result<bool> UpdateHireDate(DateTime hireDate)
        {
            if (hireDate > DateTime.UtcNow)
                return Result<bool>.Failure(LocalizationKeys.InvalidDate);

            HireDate = hireDate;
            return Result<bool>.Success(true);
        }
        public Result<bool> Delete()
        {
            Deactivate();
            return Result<bool>.Success(true);
        }
    }
}
