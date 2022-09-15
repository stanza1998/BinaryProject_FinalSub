using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinaryCityProject.Models
{
    public class Client
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Client_Index { get; set;}

#nullable enable
        
        public string? Client_Key { get;  set; }
        [Required(ErrorMessage = "Client Name is required")]
        public string? Client_Name { get; set;}
        public int? Contact_Total { get; set; }

        [NotMapped]
        public Contact[]? Contacts { get; set; }

    }
}