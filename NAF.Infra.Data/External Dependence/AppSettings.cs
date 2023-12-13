namespace NAF.Infra.Data.External_Dependence
{
    public class AppSettings
    {
        public JWT? JWT { get; set; }
    }

    public class JWT
    {
        public string? Key { get; set; }
    }
}
