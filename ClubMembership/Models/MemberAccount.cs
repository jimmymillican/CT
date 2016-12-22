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
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }

        [Display(Name = "Account Type")]
        public virtual AccountType AccountTypeId { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Account Closure Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Blocked")]
        public bool? Blocked { get; set; }

        [Display(Name = "Blocked Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BlockedDate { get; set; }

        [Display(Name = "Suspended")]
        public bool? Suspended { get; set; }

        [Display(Name = "Suspended Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuspendedDate { get; set; }

        [Display(Name = "Account Charge")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float Amount { get; set; }

        [Display(Name = "Account Notes")]
        public string AdditionalDetails { get; set; }

        [Display(Name = "Account Name")]
        public string AccountFullDescription
        {
            get
            {
                return Member.FullName + " (Account: " + MemberAccountId + ")";
            }
        }

        public virtual ICollection<MemberAccountTransaction> MemberAccountTransactions { get; set; }

    }
}