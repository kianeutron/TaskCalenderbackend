using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskCalender.Models
{
    public class Setting
    {
        [Key]
        public int set_Id { get; set; }
        public string set_Name { get; set; }

        // Navigation property
        public ICollection<SettingValue> SettingValues { get; set; }
    }
} 