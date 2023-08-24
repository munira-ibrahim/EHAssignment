using EFCoreApi.Models;
using EFCoreApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace EFCoreApi.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(Guid employeeId);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);

        Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);

        Task<bool> DeleteEmployee(Guid employeeId);

        Task<IEnumerable<EmployeeDto>> Search(string name);


    }
}
