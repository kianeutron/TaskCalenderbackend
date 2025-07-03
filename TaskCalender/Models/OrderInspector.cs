using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskCalender.Models
{
    public class OrderInspector
    {
        [Key]
        public int ordinsp_Id { get; set; }
        public int Order_ord_Id { get; set; }
        public int Inspector_rel_Id { get; set; }
        public bool IsTeamLeader { get; set; }
        public DateTime? InspectionDate { get; set; }
        public DateTime? InspectionDateTo { get; set; }

        // Navigation properties
        [ForeignKey("Order_ord_Id")]
        public Order Order { get; set; }
        [ForeignKey("Inspector_rel_Id")]
        public Relation Inspector { get; set; }
    }
} 