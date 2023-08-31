using PDBG.CRM.WEB.Models.JsonEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_clients")]
    public class Client
    {       
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public Client(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            phone = Regex.Replace(phone, @"[^\d]", "");
            phone = phone.Remove(0, 1).Insert(0, "7");
            Phone = phone;
        }        
    }
}
