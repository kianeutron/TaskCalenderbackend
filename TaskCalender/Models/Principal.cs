using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCalender.Models
{
    public class Principal
    {
        [Key]
        public int pcl_Id { get; set; }
        public string pcl_UserName { get; set; }
        public string pcl_Password { get; set; }
        public string pcl_Email { get; set; }
        public int? Relation_rel_Id { get; set; }

        // Navigation property
        [ForeignKey("Relation_rel_Id")]
        public Relation Relation { get; set; }
    }
} 