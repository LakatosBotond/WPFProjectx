using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    public class Customer { [PrimaryKey, AutoIncrement] 
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; } 
        public string City { get; set; } 
        public string Country { get; set; } 
        public DateTime BirthDate { get; set; } }
}
