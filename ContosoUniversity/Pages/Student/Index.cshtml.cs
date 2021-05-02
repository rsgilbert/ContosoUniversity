using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Options;


namespace ContosoUniversity.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly MvcOptions _mvcOptions;


        public IndexModel(SchoolContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }

        public IList<Models.Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.Take(
                _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
        }
    }
}
