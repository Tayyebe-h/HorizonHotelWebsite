using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.booking;
using HorizonHotelWebsite.Models.Entities.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class PaymentRepo : IPayment
    {
        private readonly DataBaseContext _dataBaseContext;

        public PaymentRepo(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public IEnumerable<Payment> AllPayments
        {
            get
            {
                return _dataBaseContext.Payments;
            }
        }

        public Payment CreatePayment(Payment payment)
        {
            if(payment != null)
            {
                _dataBaseContext.Payments.Add(payment);
                _dataBaseContext.SaveChanges();
                return payment;
            }
            return payment;
        }

        public Payment GetById(int? id)
        {
            return _dataBaseContext.Payments.FirstOrDefault(p => p.PaymentId == id);
        }
    }
}
