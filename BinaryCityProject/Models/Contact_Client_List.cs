using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryCityProject.Models
{
    public class Contact_Client_List
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CC_Index { get; set; }

#nullable enable
        public string? Contact_Key { get; set; }
        public string? Client_Key { get; set; }
    }
}