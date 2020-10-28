using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public interface IPayment
    {
        public Payment CreatePayment(Payment payment);

        public IEnumerable<Payment> AllPayments { get; }

        public Payment GetById(int? id);
    }
}
