using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public enum MemberType
    {
        Peak, OffPeak
    }

    public enum GenderType
    {
       Female, Male, Other
    }


    public class Member
    { 
        public int Id { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Points")]
        public int Points { get; set; }

        [Display(Name = "Membership Date")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MembershipDate { get; set; }

        [Display(Name = "Member type")]
        public MemberType MemberType { get; set; }

        [Display(Name = "Member")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Member ID")]
        public string MemberID
        {
            get { return ("0000000000" + Id).ToString().Substring(Math.Max(0, ("0000000000" + Id).ToString().Length - 7)); }
        }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address1 { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Gender")]
        public GenderType Gender { get; set; }

        [Display(Name = "Telephone 1")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone1 { get; set; }

        [Display(Name = "Telephone 2")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone2 { get; set; }

        [Display(Name = "Expired")]
        public bool Expired { get; set; }

        [Display(Name = "Expiry Notes")]
        [DataType(DataType.MultilineText)]
        public string ExpiryNotes { get; set; }

        [Display(Name = "Deleted")]
        public bool Deleted { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}