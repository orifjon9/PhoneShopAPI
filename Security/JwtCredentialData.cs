
namespace PhoneShopAPI.Security
{
    public class JwtCredentialData
    {
        private JwtCredentialData() { }
        private static JwtCredentialData _instance;
        
        public static JwtCredentialData Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new JwtCredentialData();
                }
                return _instance;
            }
        }

        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}