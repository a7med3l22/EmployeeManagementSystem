using AI_Makers_TechAssessment.Services.Interfaces;
using AI_Makers_TechAssessment.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AI_Makers_TechAssessment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IMapper mapper,
            IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeCreateVM model)
        {
            var mobiles = model.MobileNumbers?
        .Where(m => !string.IsNullOrWhiteSpace(m))
        .ToList() ?? new();
            //this is Presentation Validation not Business Validation
            if (!mobiles.Any())
            {
                //ModelState يحتوي نتائج الـ Validation الخاصة بالـ Model ويستخدم لمعرفة إذا كانت البيانات المرسلة صالحة أم لا.
                ModelState.AddModelError(nameof(model.MobileNumbers),
                    "At least one mobile number is required");
            }

            if (mobiles.Any(m => !Regex.IsMatch(m, @"^01[0125]\d{8}$")))
            {
                ModelState.AddModelError(nameof(model.MobileNumbers),
                    "Mobile number must be valid Egyptian number");
            }

            if (model.PhotoFile is not null &&
                !model.PhotoFile.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError(nameof(model.PhotoFile),
                    "Only image files are allowed");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _employeeService.GetAllDepartments(); //Dynamic Object يستخدم لنقل البيانات من Controller إلى View.

                return View(model); // للحفاظ على البيانات التي أدخلها المستخدم وإظهار أخطاء التحقق.
            }

            await _employeeService.AddEmployee(model);

            return RedirectToAction(nameof(Index)); //لتطبيق Post-Redirect-Get Pattern ومنع إعادة إرسال النموذج عند تحديث الصفحة.
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            ViewBag.Departments = await _employeeService.GetAllDepartments();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveEmployee(int EmployeeId)
        {
            await _employeeService.RemoveEmployee(EmployeeId);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdSpecAsync(
                e => e.Id == id
                //, e => e.Department
               , e => e.MobileNumbers
            );

            if (employee == null)
                return NotFound();

            var model = _mapper.Map<EmployeeEditVM>(employee);

            model.CurrentPhoto = employee.Photo;

            ViewBag.Departments = await _employeeService.GetAllDepartments();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeEditVM model)
        {
            var mobiles = model.MobileNumbers?
       .Where(m => !string.IsNullOrWhiteSpace(m))
       .ToList() ?? new();

            if (!mobiles.Any())
            {
                ModelState.AddModelError(nameof(model.MobileNumbers),
                    "At least one mobile number is required");
            }

            if (mobiles.Any(m => !Regex.IsMatch(m, @"^01[0125]\d{8}$")))
            {
                ModelState.AddModelError(nameof(model.MobileNumbers),
                    "Mobile number must be valid Egyptian number");
            }

            if (model.PhotoFile is not null &&
                !model.PhotoFile.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError(nameof(model.PhotoFile),
                    "Only image files are allowed");
            }

            if (!ModelState.IsValid)
            {
                var existing = await _employeeService.GetByIdSpecAsync(
                    e => e.Id == id
                   //,e => e.Department
                   // , e => e.MobileNumbers
                );

                if (existing == null)
                    return NotFound();

               
                model.CurrentPhoto = existing.Photo;

                ViewBag.Departments = await _employeeService.GetAllDepartments();

                return View(model);
            }

            await _employeeService.UpdateEmployee(model, id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> FilterEmployee(string? employeeName = null, int? DepartmentId = null)
        {
            var employees = await _employeeService.FilterEmployee(employeeName, DepartmentId);

            ViewBag.Departments = await _employeeService.GetAllDepartments();
            ViewBag.SelectedName = employeeName; //للحفاظ على قيمة البحث داخل الصفحة بعد تنفيذ الفلترة.
            ViewBag.SelectedDept = DepartmentId;
            return View("Index", employees);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();
            ViewBag.Departments = await _employeeService.GetAllDepartments();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Reset()
        {
            return RedirectToAction(nameof(Index)); //إعادة تحميل الصفحة بدون أي فلاتر أو شروط بحث.
        }

    }
}

//يمكن نقل Validation المكررة إلى Custom Validation Attribute لتقليل التكرار وتحسين الصيانة.

//ViewData عبارة عن Dictionary تعتمد على string keys وتتطلب casting، بينما ViewBag عبارة عن Dynamic Wrapper فوق ViewData تسهل الوصول للبيانات بدون casting.