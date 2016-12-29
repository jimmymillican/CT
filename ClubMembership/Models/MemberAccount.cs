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

        [Display(Name = "Account ID")]
        public string AccountId
        {
            get { return ("0000000000" + MemberAccountId).ToString().Substring(Math.Max(0, ("0000000000" + MemberAccountId).ToString().Length - 4)); }
        }

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

        private Boolean _blocked = false;

        [Display(Name = "Blocked")] 
        public Boolean Blocked
        {
            get { return _blocked; }
            set { _blocked = value; }
        }

        [Display(Name = "Blocked Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BlockedDate { get; set; }

        private Boolean _suspended = false;

        [Display(Name = "Suspended")]
        public Boolean Suspended
        {
            get { return _suspended; }
            set { _suspended = value; }
        }

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
                return Member.FullName + " (Acc: " + AccountId + ")";
            }
        }

        public virtual ICollection<MemberAccountTransaction> MemberAccountTransactions { get; set; }

        public virtual ICollection<MemberAccountLinkedMember> MemberAccountLinkedMembers { get; set; }
    }
}