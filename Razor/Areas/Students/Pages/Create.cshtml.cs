using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor.Context;
using Razor.Entities;

namespace Razor.Areas.Students.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Razor.Context.University _context;

        public CreateModel(Razor.Context.University context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GroupsId"] = new SelectList(_context.Groups, "GroupId", "GroupName");
        ViewData["Type"] = new SelectList(new List<string> { "Бюджет", "Платное", "Целевое", "БВИ" });
        return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
