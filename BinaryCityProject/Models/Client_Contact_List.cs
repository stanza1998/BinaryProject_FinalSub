using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinaryCityProject.Models
{
    public class Client_Contact_List
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CC_Index { get; set; }

        public string? Client_Key { get; set; }
        public string? Contact_Key { get; set; }

        [NotMapped]
        public Client? Client { get; set; }
        [NotMapped]
        public Contact[]? contacts { get; set; }



    }
}