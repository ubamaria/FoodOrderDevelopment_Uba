using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodOrderDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required] 
        public string ClientFIO { get; set; }
    }
}
