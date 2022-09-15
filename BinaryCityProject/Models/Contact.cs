using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinaryCityProject.Models
{
    public class Contact
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Contact_Index { get; set; }

        public string? Contact_Key { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string? Contact_Mail { get; set; }

        [Required(ErrorMessage = "Name Required required")]
        public string? Contact_Name { get; set; }

        [Required(ErrorMessage = "Surname Required required")]
        public string? Contact_Name_Surname { get; set; }

        public string? Contact_Number { get; set; }
        [NotMapped]
        public Client[]? Clients { get; set; }

    }

}