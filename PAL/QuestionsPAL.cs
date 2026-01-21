namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class QuestionsPAL
    {
        public long ID { get; set; }
        public long CategoryID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Question { get; set; }
        public string DescriptionLink { get; set; }
        public bool Status { get; set; }
    }
}

