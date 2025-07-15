using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Entities
{
    [Table("User")]
    public class User : Base
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("email_address")]
        public string EmailAddress { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("user_type")]
        public string UserType { get; set; }
        [Column("user_image")]
        public string UserImage { get; set; } = string.Empty;

    }
}
