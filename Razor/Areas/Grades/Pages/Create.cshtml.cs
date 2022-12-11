using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor.Context;
using Razor.Entities;

namespace Razor.Areas.Grades.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Razor.Context.University _context;

        public CreateModel(Razor.Context.University context)
        {
            _context = context;
        }
        
        public IActionResult OnGetId(int? id)
        {
            if (id != null)
            {
                ViewData["StudentsId"] = new SelectList(_context.Students.Where(t => t.StudentId == id).ToArray(), "StudentId", "StudentName");
            }
            else
            {
                ViewData["StudentsId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            }

            return Page();
        }

        public IActionResult OnGet()
        {
            ViewData["StudentsId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            return Page();
        }

        [BindProperty]
        public Grade Grade { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        { 
            if (_context.Grades == null || Grade == null) 
            {
              return Page(); 
            }
            
            _context.Grades.Add(Grade); 
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}
