namespace Resgrid.Config
{
	public static class CacheConfig
	{
		public static string RedisConnectionString = "redis:6379,Password=,allowAdmin=True";
		public static string ApiBearerTokenKeyName = "_ApiBarerToken";
		public static string WebsiteSessionKeyNamePrefix = $"RGWebAuthSession-";
	}
}
