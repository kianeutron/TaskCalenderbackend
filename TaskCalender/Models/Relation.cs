using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskCalender.Models
{
    public class Relation
    {
        [Key]
        public int rel_Id { get; set; }
        public string rel_Name { get; set; }
        public string rel_Lastname { get; set; }
        public string rel_Nickname { get; set; }
        public string rel_BSN { get; set; }
        public string rel_Code { get; set; }
        public string rel_Email { get; set; }
        public string rel_CellPhone { get; set; }
        public string rel_Phone1 { get; set; }

        // Navigation properties
        public ICollection<Principal> Principals { get; set; }
        public ICollection<OrderInspector> OrderInspectors { get; set; }
    }
} 