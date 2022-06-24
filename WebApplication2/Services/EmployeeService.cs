namespace WebApplication2.Services
{
    public class EmployeeService
    {
        private PostgresDemoContext _context;

        public EmployeeService()
        {
            _context = new PostgresDemoContext();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Select(x => new Employee() { Empid = x.Empid, City = x.City, Empname = x.Empname });
        }

        public Employee Post(DtoEmployee dto)
        {
            Employee employee = dto.ToEmployee();
            _context.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Put(int id,DtoEmployee dto)
        {
            Employee employee = _context.Employees.Where(x=>x.Empid==id).First();
            if ( dto!=null && employee!=null)
            {
                employee.City = dto.City??employee.City;
                employee.Empname = dto.Empname ?? employee.Empname;
                _context.SaveChanges();
            }
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Where(x => x.Empid == id).First();
            if (employee != null)
            {
                _context.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }
    }
}
