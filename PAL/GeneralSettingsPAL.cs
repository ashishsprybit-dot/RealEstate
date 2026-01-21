namespace PAL
{
    using System;

    public class GeneralSettingsPAL
    {
        private long _id = 0L;
        private string _keyword = string.Empty;
        private string _value = string.Empty;

        public long ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Keyword
        {
            get
            {
                return this._keyword;
            }
            set
            {
                this._keyword = value;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
}

