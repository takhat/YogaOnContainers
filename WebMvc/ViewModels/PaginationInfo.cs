using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.ViewModels
{
    public class PaginationInfo
    {
        public long TotalItems { get; set; } // showing 10 out of 60 items. this holds the value 60
        public int ItemsPerPage { get; set; } // showing 10 out of 60 items. this holds 10
        public int ActualPage { get; set; } //Page number you are on
        public int TotalPages { get; set; } // how many pages exists
        public string Previous { get; set; } // if something is there on Previous page
        public string Next { get; set; } //if something is there on Next page
    }
}
