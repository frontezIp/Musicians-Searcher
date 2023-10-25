namespace Shared.Utilities
{
    public static class TimeSpanUtility
    {
        public static TimeSpan? FromMinutesNullable(double? value)
        {
            return value == null ? null : TimeSpan.FromMinutes(value.Value);
        }
    }
}
