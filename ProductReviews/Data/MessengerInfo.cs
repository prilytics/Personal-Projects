using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReviews.Data
{
    public class MessengerInfo
    {
        public string ThreadName { get; set; }
        public string MessengerID { get; set; }
        public string MessengerLink { get; set; }
        public string MessengerName { get; set; }
        public string MessengerUserName { get; set; }
        public Nullable<bool> MessengerIsVerified { get; set; }
        public Nullable<bool> MessengerIsUser { get; set; }
        public string MessengerType { get; set; }

    }
    public class MessengerInfoOrderID: MessengerInfo
    {
        public MessengerInfoOrderID (MessengerInfo mi)
        {
            this.ThreadName = mi.ThreadName;
            this.MessengerID = mi.MessengerID;
            this.MessengerLink = mi.MessengerLink;
            this.MessengerName = mi.MessengerName;
            this.MessengerUserName = mi.MessengerUserName;
            this.MessengerIsVerified = mi.MessengerIsVerified;
            this.MessengerIsUser = mi.MessengerIsUser;
            this.MessengerType = mi.MessengerType;
        }
        public int OrderID { get; set; }
        public string UpdateStatus { get; set; } 
    }
}