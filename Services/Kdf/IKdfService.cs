namespace Portal.Services.Kdf
{
    //KDF     key derivation function by RFC: 2898
    public interface IKdfService
    {
        void Config(int iterationCount, int dkLength);
        public string GetDerivedKey(string password, string salt);
       }
}