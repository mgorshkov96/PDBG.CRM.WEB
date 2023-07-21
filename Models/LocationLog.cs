using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Converters;
//using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Runtime.InteropServices;

namespace PDBG.CRM.WEB.Models
{
    [PrimaryKey(nameof(LocDate), nameof(EmployeeId))]
    [Table("t_location_log")]
    public class LocationLog
    {
        public LocationLog()
        {
            LocDate = DateTime.Now.AddHours(3);
        }

        [Required]
        [Column("loc_date")]
        public DateTime LocDate { get; set; }

        [Required]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        //public Employee Employee { get; set; }

        [Required]
        public decimal Lat { get; set; }

        [Required]
        public decimal Lng { get; set; }
    }
}
