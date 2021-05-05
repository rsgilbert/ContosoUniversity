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
using Microsoft.Extensions.Configuration;




namespace ContosoUniversity.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly MvcOptions _mvcOptions;
        private readonly IConfiguration Configuration;


        public IndexModel(SchoolContext context, IOptions<MvcOptions> mvcOptions,
                IConfiguration configuration)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
            Configuration = configuration;
        }

        // sort
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.Student> Students { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString != null)
            {
                // A search value has been provided
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

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

            var pageSize = Configuration.GetValue("PageSize", 3);
            Students = await PaginatedList<Models.Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
