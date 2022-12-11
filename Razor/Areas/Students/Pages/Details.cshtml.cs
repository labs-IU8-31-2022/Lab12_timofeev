using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor.Context;
using Razor.Entities;

namespace Razor.Areas.Students.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Razor.Context.University _context;

        public DetailsModel(Razor.Context.University context)
        {
            _context = context;
        }
        
        public Student Student { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }
            
            return RedirectToPage("/Create", "Id", new { area = "Grades", id });
        }
    }
}
