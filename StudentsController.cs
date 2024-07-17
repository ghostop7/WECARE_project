using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMTI_SGHE.Data;
using SMTI_SGHE.Models;
using SMTI_SGHE.Models.Entity;



namespace SMTI_SGHE.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SGHEDbContext dbContext;

        public StudentsController(SGHEDbContext DbContext)
        {
            dbContext = DbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                fName = viewModel.fName,
                lName = viewModel.lName,
                Email = viewModel.Email,
                DOB = viewModel.DOB,
                Address = viewModel.Address,
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student =await dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.fName = viewModel.fName;
                student.lName = viewModel.lName;
                student.Email = viewModel.Email;
                student.DOB = viewModel.DOB;
                student.Address = viewModel.Address;

                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List","Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students.
                AsNoTracking().FirstOrDefaultAsync(x=>x.Id == viewModel.Id);

            if(student is not null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
