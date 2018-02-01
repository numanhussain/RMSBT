using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMCandidateIndex
    {
        public long CandidateID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string CNIC { get; set; }
        public string BloodGroup { get; set; }
        public string MartialStatus { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string LandlineNo { get; set; }
        public string CellNo { get; set; }
        public string EmailID { get; set; }
        public string Domicile { get; set; }
        public string Nationality { get; set; }
        public string Objective { get; set; }
        public int ImageID { get; set; }
    }
}
