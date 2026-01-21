namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class TenantAdminPAL
    {       
        public DateTime CreatedOn { get; set; }

        public string EmailID { get; set; }

        public string FirstName { get; set; }

        public int ID { get; set; }
        public int SchoolID { get; set; }

        public string LastName { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public int Status { get; set; }

        public string UserName { get; set; }
        public string ImageName { get; set; }
        public string GoogleID { get; set; }        
    }
}

