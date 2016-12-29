using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberAccountLinkedMember
    { 
        [Display(Name = "Id")]
        public int MemberAccountLinkedMemberId { get; set; }

        [Display(Name = "Account Id")]
        public int MemberAccountId { get; set; }
        
        public virtual MemberAccount MemberAccount { get; set; }

        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }

        [Display(Name = "Relationship Type")]
        public int RelationshipTypeId { get; set; }

        public virtual RelationshipType RelationshipType { get; set; }

        [Display(Name = "Additional Details")]
        public string AdditionalDetails { get; set; }

        private DateTime _date = DateTime.Now;
        public DateTime DateAdded
        {
            get { return _date; }
            set { _date = value; }
        }



    }
}