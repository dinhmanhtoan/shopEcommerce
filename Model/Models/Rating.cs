using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public partial class Rating
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }
        public DateTime? DateApprove { get; set; }
        public int? Status { get; set; }

        [Required]
        public decimal Scores { get; set; }
    }
}
