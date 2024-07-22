using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject_MVC.Models
{
	[Table("Departments",Schema ="HR")]
	public class Department
	{
		[Key]
		[Display(Name ="ID")]
        public int DepartmentId { get; set; }


		[Display(Name = "Department Name")]
		[Column(TypeName ="varchar(200)")]
        public string DepartmentName { get; set; } = string.Empty;

    }
}
