using System;

namespace PAL
{
    public class SchoolConfigurationPAL
    {
        public long SchoolID { get; set; }
        public float NextLevelPercentage { get; set; }
        public string NotificationDays { get; set; }
    }

    public class SchoolSemesterConfigurationPAL
    {
        public long ID { get; set; }
        public long SchoolID { get; set; }
        public DateTime Sem1StartDate { get; set; }
        public DateTime Sem1EndDate { get; set; }
        public DateTime Sem2StartDate { get; set; }
        public DateTime Sem2EndDate { get; set; }
    }
}
