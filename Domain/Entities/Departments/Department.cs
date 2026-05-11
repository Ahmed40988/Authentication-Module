using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Entities.Base;
using Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Departments
{ 

public class Department : BaseEntity
{
    public Guid Id { get; private set; }
    public LocalizedString Name { get; private set; } = default!;
    public LocalizedString? Description { get; private set; }

    private readonly List<Employee> _employees = new();
    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    private Department() { }

        public Department(string? nameEn, string? nameAr, string? descEn = null, string? descAr = null)
        {
            Id = Guid.NewGuid();
            SetName(nameEn, nameAr);
            SetDescription(descEn, descAr);
        }

        public Result<bool> Update(string? nameEn, string? nameAr, string? descEn, string? descAr)
        {
            SetName(nameEn, nameAr);
            SetDescription(descEn, descAr);

            return Result<bool>.Success(true);
        }



        private void SetName(string? en, string? ar)
        {
            Name = LocalizedString.Create(en, ar);
        }

        private void SetDescription(string? en, string? ar)
        {
            if (string.IsNullOrWhiteSpace(en) && string.IsNullOrWhiteSpace(ar))
            {
                Description = null;
                return;
            }

            Description = LocalizedString.Create(en, ar);
        }

        public Result<bool> Delete()
    {
        if (_employees.Any())
            return Result<bool>.Failure(LocalizationKeys.CannotDelete);

        Deactivate();
        return Result<bool>.Success(true);
    }

    public Result<bool> AddEmployee(Employee employee)
    {
        if (employee is null)
            return Result<bool>.Failure(LocalizationKeys.Invalid);

        if (_employees.Any(e => e.Id == employee.Id))
            return Result<bool>.Failure(LocalizationKeys.AlreadyExists);

        _employees.Add(employee);
        return Result<bool>.Success(true);
    }

    public Result<bool> RemoveEmployee(string employeeId)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

        if (employee is null)
            return Result<bool>.Failure(LocalizationKeys.NotFound);

        _employees.Remove(employee);
        return Result<bool>.Success(true);
    }
}
}
