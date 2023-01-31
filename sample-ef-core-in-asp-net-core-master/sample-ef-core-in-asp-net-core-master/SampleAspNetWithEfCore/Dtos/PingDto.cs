using System;

namespace SampleAspNetWithEfCore.Dtos
{
    public class PingDto
    {
        public PingDto(bool useUtc, string messageSuffix)
        {
            IsUtc = useUtc;
            Now = useUtc ? DateTime.UtcNow : DateTime.Now;
            Message = "Server is alive!" + messageSuffix;
        }

        public bool IsUtc { get; set; }
        public DateTime Now { get; }
        public string Message { get; }
    }
}
