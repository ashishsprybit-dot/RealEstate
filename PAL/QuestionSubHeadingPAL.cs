namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class QuestionSubHeadingPAL
    {
        public long ID { get; set; }
        public long QuestionID { get; set; }
        public string SubHeading { get; set; }
        public DateTime CreatedOn { get; set; }       
        public int SequenceNo { get; set; }         
    }
}

