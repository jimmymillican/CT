﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMembership.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }

        [Display(Name = "Account Type Name")]
        public string Description { get; set; }


    }
}