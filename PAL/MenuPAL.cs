namespace PAL
{
    using System;

    public class MenuPAL
    {
        private int _CMSID;
        private bool _ExternalLink;
        private string _ExternalLinkURL;
        private int _ID;
        private int _LevelNo;
        private int _LinkType;
        private string _MenuURL;
        private string _Name;
        private int _ParentID;
        private int _Position;
        private int _ShowInMenu;
        private string _StaticURL;
        private bool _Status;

        public int CMSID
        {
            get
            {
                return this._CMSID;
            }
            set
            {
                this._CMSID = value;
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

        public int LevelNo
        {
            get
            {
                return this._LevelNo;
            }
            set
            {
                this._LevelNo = value;
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

        public string MenuURL
        {
            get
            {
                return this._MenuURL;
            }
            set
            {
                this._MenuURL = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public int ParentID
        {
            get
            {
                return this._ParentID;
            }
            set
            {
                this._ParentID = value;
            }
        }

        public int Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                this._Position = value;
            }
        }

        public int ShowInMenu
        {
            get
            {
                return this._ShowInMenu;
            }
            set
            {
                this._ShowInMenu = value;
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
    }
}

