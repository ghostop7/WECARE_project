using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTI_SGHE.Data;
using SMTI_SGHE.Models;
using SMTI_SGHE.Models.Entity;

namespace SMTI_SGHE.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SGHEDbContext dbContext;

        public ProjectsController(SGHEDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectViewModel viewModel)
        {
            var project = new Project
            {
                ProjectCode = viewModel.ProjectCode,
                ProjectTitle = viewModel.ProjectTitle,
                DueDate = viewModel.DueDate,
            };

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        { 
            var projects = await dbContext.Projects.ToListAsync();

            return View(projects);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await dbContext.Projects.FindAsync(id);

            return View(project);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Project viewModel)
        {
            var project = await dbContext.Projects.FindAsync(viewModel.Id);

            if (project is not null)
            {
                project.ProjectCode = viewModel.ProjectCode;
                project.ProjectTitle = viewModel.ProjectTitle;
                project.DueDate = viewModel.DueDate;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Projects");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Project viewModel)
        {
            var project = await dbContext.Projects.
                AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (project is not null)
            {
                dbContext.Projects.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Projects");
        }
    }
}
