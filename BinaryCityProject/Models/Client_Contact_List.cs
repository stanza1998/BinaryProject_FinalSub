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

#nullable enable
        public string? Client_Key { get; set; }
        public string? Contact_Key { get; set; }




    }
}