using HorizonHotelWebsite.Models.Entities.user;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Entities.payment
{
    public class Payment
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

        [DisplayName("Cvv 3 Number")]
        [Required]
        public string cvvCode { get; set; }

        public virtual User User { get; set; }


    }
}
