using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UsersCRUD.Models;

namespace UsersCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        //Click -> id(tableItemField) -> AddOrEdit -> View(Employee[id] -> AddOrEdit*view -> (use Employee instance ojb) fill data.
        //Submit -> AddOrEdit*action -> bind data from fields -> to Employee obj -> save...

        // GET: Employee/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new Employee());
            }
            else
            {
                return View(_context.Employees.FirstOrDefault(e => e.EmployeeId == id));
            }
        }

        // POST: Employee/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
