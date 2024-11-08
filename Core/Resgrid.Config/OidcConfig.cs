#pragma warning disable S2223 // Non-constant static fields should not be visible
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable S1104 // Fields should not have public accessibility

namespace Resgrid.Config
{
	public static class OidcConfig
	{
		public static string Key = "";

		public static string ConnectionString = "Server=db;Database=ResgridOIDC;User Id=sa;Password=Resgrid123!!;MultipleActiveResultSets=True;TrustServerCertificate=true;";

		public static int AccessTokenExpiryMinutes = 1440;

		public static int RefreshTokenExpiryDays = 365;

		public static int NonMobileRefreshTokenExpiryDays = 2;

		public static string EncryptionCert = "";

		public static string SigningCert = "";
	}
}

#pragma warning restore CA2211 // Non-constant fields should not be visible
#pragma warning restore S2223 // Non-constant static fields should not be visible
#pragma warning restore S1104 // Fields should not have public accessibility
