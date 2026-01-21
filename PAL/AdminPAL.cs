namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class AdminPAL
    {
        private int _AdminType;

        public int AdminType
        {
            get
            {
                return this._AdminType;
            }
            set
            {
                this._AdminType = value;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string EmailID { get; set; }

        public string FirstName { get; set; }

        public int ID { get; set; }

        public string LastName { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public string UserName { get; set; }
        public string ImageName { get; set; }
    }
}

