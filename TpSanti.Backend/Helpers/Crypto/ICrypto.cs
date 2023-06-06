namespace WebAPIAFA.Helpers.Crypto
{
    public interface ICrypto
    {
        public byte[] EncryptFile(byte[] inputPdf);
        public byte[] DecryptFile(byte[] inputpdf);
        public string EncryptText(string inputText);
        public string DecryptText(string inputText);
    }
}
