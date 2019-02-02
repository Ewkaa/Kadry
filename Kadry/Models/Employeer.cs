using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kadry.Models
{
    public class Employeer
    {
        private static readonly SQLConnection _sqlConnection = new SQLConnection();

        public int Id { get; set; }

        [Display(Name = "Nazwisko: ")]
        public string Surname { get; set; }

        [Display(Name = "Imię: ")]
        public string Firstname { get; set; }

        [Display(Name = "Drugie imię: ")]
        public string MiddleName { get; set; }

        [Display(Name = "PESEL: ")]
        public string Pesel { get; set; }

        [Display(Name = "Data urodzenia: ")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Płeć: ")]
        public Sex Sex { get; set; }

        public Login Login { get; set; }

        [Display(Name = "Stanowisko pracy: ")]
        public Workplace Workplace { get; set; }

        [Display(Name = "Urlop: ")]
        public Holiday Holiday { get; set; }

        [Display(Name = "Typ umowy o pracę: ")]
        public ContractType ContractType { get; set; }

        [Display(Name = "Świadectwo zdrowia: ")]
        public Medical Medical { get; set; }

        [Display(Name = "Wynagrodzenie: ")]
        public Salary Salary { get; set; }
        public Hours Hours { get; set; }

        [Display(Name = "Data zawarcia umowy: ")]
        public DateTime ContractDate { get; set; }

        [Display(Name = "Data zakończenia umowy: ")]
        public DateTime ContractEndDate { get; set; }

        public static List<Sex> GetSexList()
        {
            return _sqlConnection.SexList();
        }

        public static List<ContractType> GetContractTypeList()
        {
            return _sqlConnection.ContractTypeList();
        }

        public static List<Workplace> GetWorkplaceList()
        {
            return _sqlConnection.WorkplaceList();
        }
    }
}