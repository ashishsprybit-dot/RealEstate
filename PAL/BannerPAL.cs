namespace PAL
{
    using System;

    public class BannerPAL
    {
        private string _BannerFile;
        private string _Details;
        private bool _ExternalLink;
        private string _ExternalLinkURL;
        private int _ID;
        private int _LinkType;
        private int _MenuID;
        private string _StaticURL;
        private bool _Status;
        private string _Title;
        private string _Title2;

        public string BannerFile
        {
            get
            {
                return this._BannerFile;
            }
            set
            {
                this._BannerFile = value;
            }
        }

        public string Details
        {
            get
            {
                return this._Details;
            }
            set
            {
                this._Details = value;
            }
        }

        public bool ExternalLink
        {
            get
            {
                return this._ExternalLink;
            }
            set
            {
                this._ExternalLink = value;
            }
        }

        public string ExternalLinkURL
        {
            get
            {
                return this._ExternalLinkURL;
            }
            set
            {
                this._ExternalLinkURL = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public int LinkType
        {
            get
            {
                return this._LinkType;
            }
            set
            {
                this._LinkType = value;
            }
        }

        public int MenuID
        {
            get
            {
                return this._MenuID;
            }
            set
            {
                this._MenuID = value;
            }
        }

        public string StaticURL
        {
            get
            {
                return this._StaticURL;
            }
            set
            {
                this._StaticURL = value;
            }
        }

        public bool Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string Title2
        {
            get
            {
                return this._Title2;
            }
            set
            {
                this._Title2 = value;
            }
        }
    }
}

