namespace PAL
{
    using System;

    public class CMSPAL
    {
        private string _BodyFooterText;
        private string _CMSMetaDescription;
        private string _CMSMetaKeyword;
        private string _CMSMetaTitle;
        private string _HeadText;
        private int _ID;
        private string _ImageName;
        private string _PageDescription;
        private string _PageTitle;
        private string _PageUrl;

        public string BodyFooterText
        {
            get
            {
                return this._BodyFooterText;
            }
            set
            {
                this._BodyFooterText = value;
            }
        }

        public string CMSMetaDescription
        {
            get
            {
                return this._CMSMetaDescription;
            }
            set
            {
                this._CMSMetaDescription = value;
            }
        }

        public string CMSMetaKeyword
        {
            get
            {
                return this._CMSMetaKeyword;
            }
            set
            {
                this._CMSMetaKeyword = value;
            }
        }

        public string CMSMetaTitle
        {
            get
            {
                return this._CMSMetaTitle;
            }
            set
            {
                this._CMSMetaTitle = value;
            }
        }

        public string HeadText
        {
            get
            {
                return this._HeadText;
            }
            set
            {
                this._HeadText = value;
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

        public string ImageName
        {
            get
            {
                return this._ImageName;
            }
            set
            {
                this._ImageName = value;
            }
        }

        public string PageDescription
        {
            get
            {
                return this._PageDescription;
            }
            set
            {
                this._PageDescription = value;
            }
        }

        public string PageTitle
        {
            get
            {
                return this._PageTitle;
            }
            set
            {
                this._PageTitle = value;
            }
        }

        public string PageUrl
        {
            get
            {
                return this._PageUrl;
            }
            set
            {
                this._PageUrl = value;
            }
        }
    }
}

