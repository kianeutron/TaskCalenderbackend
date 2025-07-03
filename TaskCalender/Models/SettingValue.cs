using System.ComponentModel.DataAnnotations;

namespace TaskCalender.Models
{
    public class SettingValue
    {
        [Key]
        public int setval_Id { get; set; }
        public int Settings_set_Id { get; set; }
        public string setval_Name { get; set; }

        // Navigation property
        public Setting Setting { get; set; }
    }
} 