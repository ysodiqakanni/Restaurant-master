using System;

namespace DomainModel
{
    public class WorkingHour:BaseEntity
    {
        public string Day { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
    }
}