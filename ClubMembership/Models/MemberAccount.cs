using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class MemberAccount
    {
        [Display(Name = "Account ID")]
        public int MemberAccountId { get; set; }

        [Display(Name = "Member")]
        public virtual Member MemberId { get; set; }

        [Display(Name = "Account Type")]
        public virtual AccountType AccountTypeId { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Account Closure Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

    }
}