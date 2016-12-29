using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class RelationshipType
    {
        public int RelationshipTypeId { get; set; }

        [Display(Name = "Relationship Type")]
        public string Description { get; set; }


    }
}