using System;

namespace Application.Visitors.SaveVisitorInfo
{
    public class VisitorDto
    {
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string ReferenceLink { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public string PhysicalPath { get; set; }
        public VisitorVersionDto Browser { get; set; }
        public VisitorVersionDto OperationSystem { get; set; }
        public DeviceDto Device { get; set; }
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }
    }
}