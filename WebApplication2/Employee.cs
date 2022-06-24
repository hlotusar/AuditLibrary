using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Attributes;

namespace WebApplication2
{
    [Auditable]
    public class Employee
    {
        [Key]
        public int Empid { get; set; }
        [Auditable]
        public string Empname { get; set; } = null!;
        public string City { get; set; } = null!;
    }

    public class DtoEmployee
    {
        public string Empname { get; set; } = null;
        public string City { get; set; } = null;

        public Employee ToEmployee()
        {
            return new Employee() { Empname = this.Empname, City = this.City };
        }
    }
}
