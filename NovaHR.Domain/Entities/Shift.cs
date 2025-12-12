using NovaHR.Domain.Exceptions;

namespace NovaHR.Domain.Entities
{
    public class Shift // Ca làm việc
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public int BreakMinutes { get; private set; }

        // For EF Core
        protected Shift() { }

        public Shift(string name, DateTime start, DateTime end, int breakMinutes)
        {
            if (end <= start)
                throw new DomainException("Giờ kết thúc phải sau giờ bắt đầu.");

            Id = Guid.NewGuid();
            Name = name;
            StartTime = start;
            EndTime = end;
            BreakMinutes = Math.Max(0, breakMinutes);
        }
    }
}
