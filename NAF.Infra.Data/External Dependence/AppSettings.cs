namespace NAF.Infra.Data.External_Dependence
{
    public class AppSettings
    {
        public JWT? JWT { get; set; }
        public string? WebUrl { get; set; }
        public Gmail Gmail { get; set; }
    }

    public class JWT
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Key { get; set; }
    }

    public class Gmail
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
    }
}
