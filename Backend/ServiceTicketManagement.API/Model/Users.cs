using System.ComponentModel.DataAnnotations;

namespace ServiceTicketManagement.API.Model
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
