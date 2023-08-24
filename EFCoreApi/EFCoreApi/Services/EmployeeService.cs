using AutoMapper;
using EFCoreApi.Data;
using EFCoreApi.Models;
using EFCoreApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            List<Employees> employeesList = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(employeesList);
            
        }

        public async Task<EmployeeDto> GetEmployeeById(Guid employeeId)
        {
            Employees employee = await _context.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            Employees employee = _mapper.Map<EmployeeDto,Employees>(employeeDto);
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<Employees, EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            Employees employee = _mapper.Map<EmployeeDto, Employees>(employeeDto);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<Employees, EmployeeDto>(employee);
        }

        public async Task<bool> DeleteEmployee(Guid employeeId)
        {
            try {
                
                Employees employee = _context.Employees.Find(employeeId);
                if (employee == null) {
                    return false;

                }
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> Search(string name)
        {
            IQueryable<Employees> query = _context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name)
                            || e.LastName.Contains(name));
            }
            List<Employees>  empList = await query.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(empList);
        }
    }
}
