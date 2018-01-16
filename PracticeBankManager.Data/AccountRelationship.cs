using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeBankManager.Data
{
    public class AccountRelationship
    {
        [Key]
        public int AccountRelationshipId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}
