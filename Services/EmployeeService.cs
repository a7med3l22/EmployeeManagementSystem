    using AI_Makers_TechAssessment.Models.Dto;
    using AI_Makers_TechAssessment.Models.Entities;
    using AI_Makers_TechAssessment.Repositories;
    using AI_Makers_TechAssessment.Repositories.Interfaces;
    using AI_Makers_TechAssessment.Services.Interfaces;
    using AI_Makers_TechAssessment.ViewModels;
    using AutoMapper;
    using System.Linq.Expressions;

    namespace AI_Makers_TechAssessment.Services
    {
        public class EmployeeService : IEmployeeService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        public async Task AddEmployee(EmployeeCreateVM vm)
        {
            var repo = _unitOfWork.GetEmployeeRepo();

            var emailExists = await repo.EmailExistsAsync(vm.Email!);
            if (emailExists)
                throw new Exception("Email already exists");

            var emp = new Employee
            {
                FullName = vm.FullName!,
                Email = vm.Email!,
                JobTitle = vm.JobTitle!,
                DepartmentId = vm.DepartmentId,
                IsActive = vm.IsActive,
                HireDate = DateTime.Now
            };

           
            if (vm.PhotoFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/employees");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.PhotoFile.FileName);

                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.PhotoFile.CopyToAsync(stream);
                }

                emp.Photo = $"images/Employees/{fileName}";
            }

            if (vm.MobileNumbers != null && vm.MobileNumbers.Any())
            {
                emp.MobileNumbers = vm.MobileNumbers
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => new MobileNumber
                    {
                        Number = x.Trim()
                    })
                    .ToList();
            }

            await repo.AddAsync(emp);
            await _unitOfWork.saveChangesAsync();
        }
        public async Task<EmployeeVM?> GetByIdSpecAsync(Expression<Func<Employee, bool>> predicate, params Expression<Func<Employee, object>>[] funcs)
            {
                var employeeRepo = _unitOfWork.GetEmployeeRepo();
                var employee = await employeeRepo.GetByIdSpecAsync(predicate, funcs);
                return _mapper.Map<EmployeeVM?>(employee);
            }

        public async Task RemoveEmployee(int EmployeeId)
        {
            var employeeRepo = _unitOfWork.GetEmployeeRepo();

            var employee = await employeeRepo.GetByIdAsync(EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            if (!string.IsNullOrEmpty(employee.Photo))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    employee.Photo
                );

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            employeeRepo.Delete(employee);
            await _unitOfWork.saveChangesAsync();
        }

        public async Task<List<EmployeeVM>> FilterEmployee(string? employeeName, int? DepartmentId)
            {
                var employeeRepo = _unitOfWork.GetEmployeeRepo();
                var employee = await employeeRepo.SearchAsync(e =>
                (string.IsNullOrEmpty(employeeName) || e.FullName.Contains(employeeName)) 
                &&
                (!DepartmentId.HasValue || e.DepartmentId == DepartmentId.Value)
            
                ,
                e=>e.Department
                ,
                e => e.MobileNumbers
                );

                var employeesDto = _mapper.Map<List<EmployeeVM>>(employee);
                return employeesDto;

            }

        public async Task UpdateEmployee(EmployeeEditVM vm, int id)
        {
            var repo = _unitOfWork.GetEmployeeRepo();

            var employee = await repo.GetByIdSpecAsync(
                e => e.Id == id,
                e => e.MobileNumbers
            );

            if (employee == null)
                throw new Exception("Employee not found");

           
            employee.FullName = vm.FullName!;
            employee.Email = vm.Email!;
            employee.JobTitle = vm.JobTitle!;
            employee.DepartmentId = vm.DepartmentId;
            employee.IsActive = vm.IsActive;

          
            if (vm.PhotoFile != null)
            {
                if (!string.IsNullOrEmpty(employee.Photo))
                {
                    var oldPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        employee.Photo
                    );

                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(vm.PhotoFile.FileName);

                var folder = Path.Combine("wwwroot/images/employees");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.PhotoFile.CopyToAsync(stream);
                }

                employee.Photo = $"images/employees/{fileName}";
            }

          

          
            if (employee.MobileNumbers != null && employee.MobileNumbers.Any())
            {
                _unitOfWork.GetRepo<MobileNumber>()
                    .DeleteRange(employee.MobileNumbers.ToList());
            }

       
            employee.MobileNumbers = new List<MobileNumber>();

         
            if (vm.MobileNumbers != null)
            {
                foreach (var m in vm.MobileNumbers)
                {
                    if (!string.IsNullOrWhiteSpace(m))
                    {
                        employee.MobileNumbers.Add(new MobileNumber
                        {
                            Number = m.Trim(),
                            EmployeeId = employee.Id
                        });
                    }
                }
            }

          
            repo.Update(employee);
            await _unitOfWork.saveChangesAsync();
        }
        public async Task<List<EmployeeVM>> GetAllEmployees()
            {
                var employeeRepo = _unitOfWork.GetEmployeeRepo();

                var employees =await employeeRepo.GetAllSpecAsync(e => e.Department,e=>e.MobileNumbers);
                return _mapper.Map<List<EmployeeVM>>(employees);
            }

            public async Task<List<DepartmentVM>> GetAllDepartments()
            {
                var employeeRepo = _unitOfWork.GetRepo<Department>();
                var departments = await employeeRepo.GetAllAsync();
                return _mapper.Map<List<DepartmentVM>>(departments);
            }
        }
    }
