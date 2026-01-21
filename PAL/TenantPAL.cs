namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class TenantPAL
    {
        public int ID { get; set; }
        public string ImageName { get; set; }
        public string Logo { get; set; }
        
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string CasesCode { get; set; }
        public bool Status { get; set; }
    }
}

