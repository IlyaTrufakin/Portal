using Portal.Services.Hash;

namespace Portal.Services.Kdf
{
    public class PbKdf1Service : IKdfService
    {
        //инжекция сервисов в другие сервисі здійснюєтся через незмінні поля та конструктор
        private readonly IHashService _hashService;
        private int iterationCount;
        private int dkLength;
        public PbKdf1Service(IHashService hashService)
        {
            _hashService = hashService;
            this.iterationCount = 3;
            this.dkLength = 32;
        }
        public void Config(int iterationCount, int dkLength)
        {
            this.iterationCount = iterationCount;
            this.dkLength = dkLength;

        }

        public string GetDerivedKey(string password, string salt)
        {
            String t1 = _hashService.Digest(salt + password);
            for (int i = 1; i < iterationCount; i++)
            {
                t1 = _hashService.Digest(t1);
            }
            if (t1.Length >= dkLength)
            {
                return t1[..dkLength];
            }
            else
            {
                char[] addon = new char[dkLength - t1.Length];
                for (int i = 0; i < addon.Length; i++)
                {
                    addon[i] = '0';

                }
                return t1 + new string(addon);
            }

        }
    }
}
