using Microsoft.EntityFrameworkCore;
using RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Employee Add(Employee newEmployee)
        {
            //_context.Empoyees.Add(newEmployee);
            //_context.SaveChanges();
            _context.Database.ExecuteSqlRaw("spAddNewEmployee {0}, {1}, {2}, {3}",
                                            newEmployee.Name, newEmployee.Email,
                                            newEmployee.PhotoPath, newEmployee.Department);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _context.Empoyees.Find(id);

            if(employeeToDelete != null)
            {
                _context.Empoyees.Remove(employeeToDelete);
                _context.SaveChanges();
            }

            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Empoyees;

            if (dept.HasValue)
                query = query.Where(x => x.Department == dept.Value);

            return query.GroupBy(x => x.Department)
                .Select(x => new DeptHeadCount()
                {
                    Department = x.Key.Value,
                    Count = x.Count()
                }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //return _context.Empoyees;
            return _context.Empoyees.FromSqlRaw<Employee>("SELECT * FROM Empoyees").ToList();
        }

        public Employee GetEmployee(int id)
        {
            //return _context.Empoyees.Find(id);
            return _context.Empoyees
                .FromSqlRaw<Employee>("CodeFirstSpGetEmoloyeeById {0}", id)
                .ToList().FirstOrDefault();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _context.Empoyees;

            return _context.Empoyees.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }


        public Employee Update(Employee updatedEmployee)
        {
            var employee = _context.Empoyees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedEmployee;
        }
    }
}
