using Tech.Services.Commons.Constants;

namespace Tech.Services.Helpers;

public static class TimeHelper
{
	public static DateTime GetServerTime()
		=> DateTime.UtcNow.AddHours(TimeConstants.UTC);
}
