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

        // sort
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Models.Student> Students { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;

            IQueryable<Models.Student> studentsIQ = from s in _context.Students select s;

            // filter by searchString
            // Contains is case-sensitive on SQLite and case-insensitive on SQL Server
            if(!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                    || s.FirstMidName.Contains(searchString));
            }
            switch(sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;

            }
            Students = await studentsIQ.AsNoTracking().Take(
                _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
        }
    }
}
