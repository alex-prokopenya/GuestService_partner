namespace GuestService.Models.Booking
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;
    using GuestService.Data;

    public class ShoppingCart : IDisposable
    {
        private HttpSessionStateBase _session;
        private const string SessionStateName = "shoppingCart";

        public ShoppingCart()
        {
            this.Orders = new List<BookingOrder>();
        }

        public void Clear()
        {
            this.PartnerAlias = null;
            this.PartnerSessionID = null;
            this.Orders.Clear();
        }

        public static ShoppingCart CreateFromSession(HttpSessionStateBase session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            ShoppingCart cart = null;
            string str = session["shoppingCart"] as string;
            if (!string.IsNullOrEmpty(str))
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(str);
            }
            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            cart._session = session;
            return cart;
        }

        public void Dispose()
        {
            this.Save();
        }

        public void Save()
        {
            if (this._session == null)
            {
                throw new Exception("_session is null");
            }
            this._session["shoppingCart"] = JsonConvert.SerializeObject(this);
        }

        public bool IsOrders
        {
            get
            {
                return ((this.Orders != null) && (this.Orders.Count > 0));
            }
        }

        public List<BookingOrder> Orders { get; private set; }

        public string PartnerAlias { get; set; }

        public string PartnerSessionID { get; set; }
    }
}

