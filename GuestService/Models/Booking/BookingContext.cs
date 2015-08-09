namespace GuestService.Models.Booking
{
    using GuestService.Data;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BookingContext
    {
        public void Prepare(BookingModel form, ReservationState reservation)
        {
            this.Reservation = reservation;
            this.Form = new BookingModel();
            if (form != null)
            {
                this.Form.PartnerAlias = form.PartnerAlias;
                this.Form.PartnerSessionID = form.PartnerSessionID;
                this.Form.CustomerName = form.CustomerName;
                this.Form.CustomerAddress = form.CustomerAddress;
                this.Form.CustomerEmail = form.CustomerEmail;
                this.Form.CustomerMobile = form.CustomerMobile;
                this.Form.BookingNote = form.BookingNote;
                this.Form.PromoCodes = form.PromoCodes;
            }
            if (this.Reservation != null)
            {
                if (this.Reservation.orders != null)
                {
                    this.Reservation.orders.ForEach(delegate (ReservationOrder m)
                    {
                        BookingOrderModel order = new BookingOrderModel();
                        order.ReservationOrder = m;
                        if (form != null && form.Orders != null)
                        {
                            BookingOrderModel formOrder = form.Orders.FirstOrDefault((BookingOrderModel o) => o != null && o.BookingOrder != null && o.BookingOrder.orderid == m.id);
                            if (formOrder != null)
                            {
                                order.BookingOrder = formOrder.BookingOrder;
                            }
                        }
                        this.Form.Orders.Add(order);
                    });
                }
            }
            bool arg_17A_1;
            if (this.Reservation != null)
            {
                arg_17A_1 = (this.Reservation.errors.FirstOrDefault((ReservationError m) => m.isstop) == null);
            }
            else
            {
                arg_17A_1 = false;
            }
            this.IsBookingEnabled = arg_17A_1;

            
        }

        public string BookingOperationId { get; set; }

        public BookingModel Form { get; set; }

        public bool IsBookingEnabled { get; set; }

        public ReservationState Reservation { get; set; }

        public List<PaymentMode> PaymentModes { get; set; }
    }
}

