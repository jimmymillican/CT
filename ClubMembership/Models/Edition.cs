using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class Edition
    {
        public int EditionId { get; set; }

        [Display(Name = "Trainer")]
        public string Title { get; set; }

        [Display(Name = "Trainer Notes")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}