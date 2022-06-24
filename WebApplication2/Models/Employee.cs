using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        public string City { get; set; }
    }
}
