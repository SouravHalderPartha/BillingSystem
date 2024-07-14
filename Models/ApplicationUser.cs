using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace BillingSystem.Models
{
    public class ApplicationUser
    {
    }

    public class User
    {
       public int UserId { get; set; }
       public string UserName { get; set; }
       public string Role { get; set; }

    }

    public class Client
    {
        public int ClientId { get; set; }
        public int UserId { get; set; }

    }

    public class BillingInvoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string LinkServiceId { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
        public int BillingDurationDays { get; set; }
        public int DaysInMonths { get; set; }
        public int BillingYear { get; set; }
        public int BillingMonth { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CapacityQty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal BillAmount { get; set; }
    }

    public class OpeningInvoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string LinkServiceId { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
        public int BillingDurationDays { get; set; }
        public int DaysInMonths { get; set; }
        public int BillingYear { get; set; }
        public int BillingMonth { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CapacityQty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal BillAmount { get; set; }
        public int? ClientStatus { get; set; }
        public User? CreatedBy { get; set; }

        public User? ModifiedBy { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

       

    }
}
