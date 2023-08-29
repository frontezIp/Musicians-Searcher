using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chat.DataAccess.Converters
{
    public class DateOnlyToDateTimeConverter : ValueConverter<DateOnly,DateTime>
    {
        public DateOnlyToDateTimeConverter()
            : base(d => d.ToDateTime(TimeOnly.MinValue),
                  d => DateOnly.FromDateTime(d))
        { }
    }
}
