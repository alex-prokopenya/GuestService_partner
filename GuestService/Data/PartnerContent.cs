using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Data
{
    public class PartnerContent
    {
        private int userId = 0;

        private int partnerId = 0;

        private string header = "";

        private string footer = "";

        private string imageName = "";

        private string title = "";

        private string description = "";

        private string keywords = "";

        public string Header
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
            }
        }

        public string Footer
        {
            get
            {
                return footer;
            }

            set
            {
                footer = value;
            }
        }

        public string ImageName
        {
            get
            {
                return imageName;
            }

            set
            {
                imageName = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Keywords
        {
            get
            {
                return keywords;
            }

            set
            {
                keywords = value;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public int PartnerId
        {
            get
            {
                return partnerId;
            }

            set
            {
                partnerId = value;
            }
        }
    }
}
