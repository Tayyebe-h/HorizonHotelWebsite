using HorizonHotelWebsite.Models.Entities.payment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.ViewsModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }

        [DisplayName("Card Number")]
        [Required]
        public string CardNo { get; set; }

        [DisplayName("Cardholder Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Expiry (MM/YY)")]
        [Required]
        public string ExpiryDate { get; set; }

        [DisplayName("Cvv3 Number")]
        [Required]
        public string CvvCode { get; set; }

        public IEnumerable<Payment> Payments { get; set; }

    }
}
