using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskCalender.Models
{
    public class Order
    {
        [Key]
        public int ord_Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remark { get; set; }
        public string OrderNumber { get; set; }

        // Navigation property
        public ICollection<OrderInspector> OrderInspectors { get; set; }
    }
} 