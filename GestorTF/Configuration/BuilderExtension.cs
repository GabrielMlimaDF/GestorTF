namespace GestorTF.Configuration
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            AppConfiguration.ConnectionString = builder.Configuration.GetConnectionString("Banco") ?? string.Empty;
        }
    }
}