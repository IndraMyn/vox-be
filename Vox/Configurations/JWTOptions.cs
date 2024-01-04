namespace Vox.Configurations
{
    public class JWTOptions
    {
        public int TokenDuration { get; set; }
        public int RefreshTokenDuration { get; set; }
        public string JWTSecret { get; set; }
    }
}
