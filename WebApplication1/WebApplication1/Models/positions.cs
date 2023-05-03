using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class positions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string position_Id { get; set; }
        public string position_Name { get; set; }
        public float baseSalary { get; set; }
        public float salaryIncreaseRate { get; set; }

        
       
    }
}
