namespace BlogEngineNet.API.Utils;

public class AppSettings
{
    public string Secret { get; set; }
    public Jwt Jwt { get; set; }
}

public class Jwt
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}