using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        [Display(Name = "Transaction Type")]
        public string Description { get; set; }


    }
}