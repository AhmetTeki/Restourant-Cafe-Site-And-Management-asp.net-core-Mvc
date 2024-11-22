using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
        public string Name { get; set; }
		[Required]
		public string LastName { get; set; }
		[NotMapped]
        public string Rol { get; set; }
    }
}
