using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string empId { get; set; }
        public string empName{ get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public DateTime hireDate { get; set; }
        public string position_Id { get; set; }
    }
}
