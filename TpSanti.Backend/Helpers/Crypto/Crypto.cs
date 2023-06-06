using System.Security.Cryptography;
using System.Text;

namespace WebAPIAFA.Helpers.Crypto
{
    public class Crypto: ICrypto
    {
        byte[] _key;
        byte[] _iv;

        public Crypto()
        {
            _key = Encoding.ASCII.GetBytes("ENCODE123456");
            _iv = Encoding.ASCII.GetBytes("ENCODE*123456");

            int keySize = 32;
            int ivSize = 16;
            Array.Resize(ref _key, keySize);
            Array.Resize(ref _iv, ivSize);
        }

        public byte[] EncryptFile(byte[] inputPdf)
        {
            byte[] encripted;

            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputPdf.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputPdf, 0, inputPdf.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }

                encripted = ms.ToArray();
            }

            return encripted;
        }

        public byte[] DecryptFile(byte[] inputpdf)
        {
            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputpdf))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputpdf, 0, inputpdf.Length);
                    objCryptoStream.FlushFinalBlock();

                    return ms.ToArray();
                }
            }
        }

        public string EncryptText(string inputText)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
            byte[] encripted;

            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }

                encripted = ms.ToArray();
            }

            return Convert.ToBase64String(encripted);
        }

        public string DecryptText(string inputText)
        {
            byte[] inputBytes = Convert.FromBase64String(inputText);
            byte[] resultBytes = new byte[inputBytes.Length];
            string cleanText = String.Empty;

            RijndaelManaged cripto = new RijndaelManaged();

            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Read))
                {

                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        cleanText = sr.ReadToEnd();
                    }
                }
            }

            return cleanText;
        }

    }
}
