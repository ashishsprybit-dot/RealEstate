using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.ComponentModel;
namespace Utility.Security
{
    public class EncryptDescrypt
    {
        private static string EncryptKey = ConfigurationManager.AppSettings["EncryptKey"];
        public static string EncryptString(string strMessage)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(EncryptKey));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(strMessage);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string DecryptString(string strMessage)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(EncryptKey));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(strMessage);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }          
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

    }

    public class Rijndael128Algorithm
    {
        //Rijndael 128 byte Encryption
        //Encrypt in Hexadecimal format or Base64Encoding
        private static string EncryptKey = Convert.ToString(ConfigurationManager.AppSettings["EncryptKey"]);
        public enum EncodingType
        {
            HEX = 0,
            BASE_64 = 1
        }

        private static byte[] IV_16 = new byte[] { 15, 199, 56, 77, 244, 126, 107, 239, 9, 10, 88, 72, 24, 202, 31, 108 };
        private static string _key = EncryptKey;
        private static EncodingType _encodingType = EncodingType.HEX;
        private static byte[] SALT_BYTES = new byte[] { 162, 27, 98, 1, 28, 239, 64, 30, 156, 102, 223 };

        #region Public Functions

        [Description("The format in which content is returned after encryption, or provided for decryption")]
        public static EncodingType Encoding
        {
            get { return _encodingType; }
            set { _encodingType = value; }
        }

        public static string EncryptString(string strEncryptString)
        {
            string _content = null;
            byte[] cipherBytes = null;
            cipherBytes = _Encrypt(strEncryptString);

            if (_encodingType == EncodingType.HEX)
            {
                _content = BytesToHex(cipherBytes);
            }
            else
            {
                _content = System.Convert.ToBase64String(cipherBytes);
            }
            return _content;
        }

        public static string DecryptString(string strDecryptString)
        {
            //byte[] encText = null;
            byte[] clearText = null;
            try
            {
                clearText = _Decrypt(strDecryptString);
            }
            catch
            {
                return string.Empty;
            }
            return System.Text.Encoding.UTF8.GetString(clearText);
        }



        #endregion

        #region Shared Cryptographic Functions

        private static byte[] _Encrypt(byte[] Content)
        {
            byte[] cipherBytes = null;
            int NumBytes = 0;

            SymmetricAlgorithm provider = default(SymmetricAlgorithm);

            provider = new RijndaelManaged();
            NumBytes = 128;
            try
            {
                //Encrypt the string
                cipherBytes = SymmetricEncrypt(provider, Content, _key, NumBytes);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException(ex.Message, ex.InnerException);
            }
            finally
            {
                //Free any resources held by the SymmetricAlgorithm provider
                provider.Clear();
            }

            return cipherBytes;
        }

        private static byte[] _Encrypt(string Content)
        {
            return _Encrypt(System.Text.Encoding.UTF8.GetBytes(Content));
        }

        private static byte[] _Decrypt(byte[] Content)
        {
            string encText = System.Text.Encoding.UTF8.GetString(Content);

            if (_encodingType == EncodingType.BASE_64)
            {
                //We need to convert the content to Hex before decryption
                encText = BytesToHex(System.Convert.FromBase64String(encText));
            }
            SymmetricAlgorithm provider = default(SymmetricAlgorithm);
            byte[] clearBytes = null;
            int NumBytes = 0;
            provider = new RijndaelManaged();
            NumBytes = 128;
            try
            {
                clearBytes = SymmetricDecrypt(provider, encText, _key, NumBytes);
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            finally
            {
                //Free any resources held by the SymmetricAlgorithm provider
                provider.Clear();
            }
            //Now return the plain text content
            return clearBytes;
        }

        private static byte[] _Decrypt(string Content)
        {
            return _Decrypt(System.Text.Encoding.UTF8.GetBytes(Content));
        }

        private static byte[] SymmetricEncrypt(SymmetricAlgorithm Provider, byte[] plainText, string key, int keySize)
        {
            //All symmetric algorithms inherit from the SymmetricAlgorithm base class, to which we can cast from the original crypto service provider
            byte[] ivBytes = null;
            ivBytes = IV_16;
            Provider.KeySize = keySize;

            //Generate a secure key based on the original password by using SALT
            byte[] keyStream = DerivePassword(key, keySize / 8);

            //Initialize our encryptor object
            ICryptoTransform trans = Provider.CreateEncryptor(keyStream, ivBytes);

            //Perform the encryption on the textStream byte array
            byte[] result = trans.TransformFinalBlock(plainText, 0, plainText.GetLength(0));

            //Release cryptographic resources
            Provider.Clear();
            trans.Dispose();

            return result;
        }

        private static byte[] SymmetricDecrypt(SymmetricAlgorithm Provider, string encText, string key, int keySize)
        {
            //All symmetric algorithms inherit from the SymmetricAlgorithm base class, to which we can cast from the original crypto service provider
            byte[] ivBytes = null;
            ivBytes = IV_16;
            //Generate a secure key based on the original password by using SALT
            byte[] keyStream = DerivePassword(key, keySize / 8);

            //Convert our hex-encoded cipher text to a byte array
            byte[] textStream = HexToBytes(encText);
            Provider.KeySize = keySize;

            //Initialize our decryptor object
            ICryptoTransform trans = Provider.CreateDecryptor(keyStream, ivBytes);

            //Initialize the result stream
            byte[] result = null;

            try
            {
                //Perform the decryption on the textStream byte array
                result = trans.TransformFinalBlock(textStream, 0, textStream.GetLength(0));
            }
            catch (Exception ex)
            {
                throw new System.Security.Cryptography.CryptographicException("The following exception occurred during decryption: " + ex.Message);
            }
            finally
            {
                //Release cryptographic resources
                Provider.Clear();
                trans.Dispose();
            }
            return result;
        }


        #endregion

        #region Utility Functions

        // BytesToHex: Converts a byte array to a hex-encoded string
        private static string BytesToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder();
            for (int n = 0; n <= bytes.Length - 1; n++)
            {
                hex.AppendFormat("{0:X2}", bytes[n]);
            }
            return hex.ToString();
        }

        // HexToBytes: Converts a hex-encoded string to a byte array
        private static byte[] HexToBytes(string Hex)
        {
            int numBytes = Hex.Length / 2;
            byte[] bytes = new byte[numBytes];
            for (int n = 0; n <= numBytes - 1; n++)
            {
                string hexByte = Hex.Substring(n * 2, 2);
                bytes[n] = (byte)int.Parse(hexByte, System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }

        // DerivePassword: This takes the original plain text key and creates a secure key using SALT
        private static byte[] DerivePassword(string originalPassword, int passwordLength)
        {
            Rfc2898DeriveBytes derivedBytes = new Rfc2898DeriveBytes(originalPassword, SALT_BYTES, 5);
            return derivedBytes.GetBytes(passwordLength);
        }

        #endregion
    }

    public class Security
    {
        private static string EncryptKey = Config.EncryptKey;

        // By creating private constructor this class will not allow to create an object of this class 
        private Security()
        {
        }

        #region Simple Encrypt/Decrypt string using MD5 and TripleDES Crypto Algorithm
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file
            string key = EncryptKey;

            //if (useHashing){
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            //}
            //else
            //keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //Get your key from config file to open the lock!
            string key = EncryptKey;

            //if (useHashing)
            //{
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            //}
            //else
            //    keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region Encrypt/Decrypt string using AES 256 bits key algorithm
        /// <summary>
        /// Encrypt original data string using AES 256 algorithm
        /// </summary>
        /// <param name="toEncrypt">String you want to encrypt</param>
        public static string AES256Encrypt(string toEncrypt)
        {
            InitializeVariables();

            // Get the key from config file        
            Key.Text = EncryptKey;
            Data dataEncrypt = new Data(toEncrypt);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ValidateKeyAndIv(true);
            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(dataEncrypt.Bytes, 0, dataEncrypt.Bytes.Length);
            cs.Close();
            ms.Close();
            return Convert.ToString((new Data(ms.ToArray())).Base64);
        }

        /// <summary>
        /// Decrypt AES 256 encrypted string
        /// </summary>
        /// <param name="CipherString">Encrypted data string</param>
        public static string AES256Decrypt(string CipherString)
        {
            InitializeVariables();

            // Get the key from config file        
            Key.Text = EncryptKey;
            Data dataDecrypt = new Data(CipherString);
            byte[] byteData = Data.FromBase64(dataDecrypt.Text.Trim());
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteData, 0, byteData.Length);
            byte[] b = new Byte[byteData.Length];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            try
            {
                cs.Read(b, 0, byteData.Length);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("Unable to decrypt data. The provided key may be invalid.", ex);
            }
            finally { cs.Close(); }
            return Convert.ToString((new Data(b)).Text);
        }
        #endregion

        #region classes & functions used for AES 256 bits key algorithm
        //AES Encryption with 256 bits key
        private static Data _key;
        private static Data _iv;
        private static SymmetricAlgorithm _crypto;
        private const string _DefaultIntializationVector = "%1Az=-@qT";

        /// <summary>
        /// The key used to encrypt/decrypt data
        /// </summary>
        private static Data Key
        {
            get { return _key; }
            set
            {
                _key = value;
                _key.MaxBytes = _crypto.LegalKeySizes[0].MaxSize / 8;
                _key.MinBytes = _crypto.LegalKeySizes[0].MinSize / 8;
                _key.StepBytes = _crypto.LegalKeySizes[0].SkipSize / 8;
            }
        }

        /// <summary>
        /// Using the default Cipher Block Chaining (CBC) mode, all data blocks are processed using
        /// the value derived from the previous block; the first data block has no previous data block
        /// to use, so it needs an InitializationVector to feed the first block
        /// </summary>
        private static Data IntializationVector
        {
            get { return _iv; }
            set
            {
                _iv = value;
                _iv.MaxBytes = _crypto.BlockSize / 8;
                _iv.MinBytes = _crypto.BlockSize / 8;
            }
        }

        private static Data RandomKey()
        {
            _crypto.GenerateKey();
            Data d = new Data(_crypto.Key);
            return d;
        }

        /// <summary>
        /// generates a random Initialization Vector, if one was not provided
        /// </summary>
        private static Data RandomInitializationVector()
        {
            _crypto.GenerateIV();
            Data d = new Data(_crypto.IV);
            return d;
        }

        private static void ValidateKeyAndIv(bool isEncrypting)
        {
            if (_key.IsEmpty)
            {
                if (isEncrypting) { _key = RandomKey(); }
                else
                {
                    throw new CryptographicException("No key was provided for the decryption operation!");
                }
            }
            if (_iv.IsEmpty)
            {
                if (isEncrypting) { _iv = RandomInitializationVector(); }
                else
                {
                    throw new CryptographicException("No initialization vector was provided for the decryption operation!");
                }
            }
            _crypto.Key = _key.Bytes;
            _crypto.IV = _iv.Bytes;
        }

        private static void InitializeVariables()
        {
            _crypto = new RijndaelManaged();
            ///-- make sure key and IV are always set, no matter what
            Key = RandomKey();
            ///if (useDefaultInitializationVector ){
            IntializationVector = new Data(_DefaultIntializationVector);
            ///}else{
            ///Me.IntializationVector = RandomInitializationVector();
            ///}
        }

        private class Data
        {
            private byte[] _b;
            private int _MaxBytes = 0;
            private int _MinBytes = 0;
            private int _StepBytes = 0;

            /// <summary>
            /// Determines the default text encoding for this Data instance
            /// </summary>
            private System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding("Windows-1252");

            public Data(byte[] b) { _b = b; }

            /// <summary>
            /// Creates new encryption data with the specified string; 
            /// will be converted to byte array using default encoding
            /// </summary>
            public Data(string s) { this.Text = s; }

            /// <summary>
            /// Creates new encryption data using the specified string and the 
            /// specified encoding to convert the string to a byte array.
            /// </summary>
            public Data(string s, System.Text.Encoding encoding)
            {
                this.Encoding = encoding;
                this.Text = s;
            }

            /// <summary>
            /// returns true if no data is present
            /// </summary>
            public bool IsEmpty
            {
                get
                {
                    if (_b == null || _b.Length == 0)
                        return true;
                    return false;
                }
            }

            /// <summary>
            /// allowed step interval, in bytes, for this data; if 0, no limit
            /// </summary>
            public int StepBytes
            {
                get { return _StepBytes; }
                set { _StepBytes = value; }
            }

            /// <summary>
            /// allowed step interval, in bits, for this data; if 0, no limit
            /// </summary>
            public int StepBits
            {
                get { return _StepBytes * 8; }
                set { _StepBytes = value / 8; }
            }

            /// <summary>
            /// minimum number of bytes allowed for this data; if 0, no limit
            /// </summary>
            public int MinBytes
            {
                get { return _MinBytes; }
                set { _MinBytes = value; }
            }

            /// <summary>
            /// maximum number of bytes allowed for this data; if 0, no limit
            /// </summary>
            public int MaxBytes
            {
                get { return _MaxBytes; }
                set { _MaxBytes = value; }
            }

            /// <summary>
            /// maximum number of bits allowed for this data; if 0, no limit
            /// </summary>
            public int MaxBits
            {
                get { return _MaxBytes * 8; }
                set { _MaxBytes = value / 8; }
            }

            /// <summary>
            /// Returns the byte representation of the data; 
            /// This will be padded to MinBytes and trimmed to MaxBytes as necessary!
            /// </summary>
            public byte[] Bytes
            {
                get
                {
                    if (_MaxBytes > 0)
                    {
                        if (_b.Length > _MaxBytes)
                        {
                            byte[] b = new byte[_MaxBytes];
                            Array.Copy(_b, b, b.Length);
                            _b = b;
                        }
                    }
                    if (_MinBytes > 0)
                    {
                        if (_b.Length < _MinBytes)
                        {
                            byte[] b = new byte[_MinBytes];
                            Array.Copy(_b, b, _b.Length);
                            _b = b;
                        }
                    }
                    return _b;
                }
                set { _b = value; }
            }

            /// <summary>
            /// Sets or returns text representation of bytes using the default text encoding
            /// </summary>
            public string Text
            {
                get
                {
                    if (_b == null) { return string.Empty; }
                    else
                    {
                        /// need to handle nulls here; oddly, C# will happily convert
                        /// nulls into the string whereas VB stops converting at the
                        /// first null!
                        int i = Array.IndexOf(_b, Convert.ToByte(0));
                        if (i >= 0) { return this.Encoding.GetString(_b, 0, i); }
                        else { return this.Encoding.GetString(_b); }
                    }
                }
                set { _b = this.Encoding.GetBytes(value); }
            }

            /// <summary>
            /// returns Base64 string representation of this data
            /// </summary>
            internal static string ToBase64(byte[] b)
            {
                if (b == null || b.Length == 0) { return string.Empty; }
                return Convert.ToBase64String(b);
            }

            /// <summary>
            /// converts from a string Base64 representation to an array of bytes
            /// </summary>
            internal static byte[] FromBase64(string base64Encoded)
            {
                if (base64Encoded == null || base64Encoded.Length == 0) { return null; }
                try
                {
                    return Convert.FromBase64String(base64Encoded);
                }
                catch (System.FormatException ex)
                {
                    throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
                }
            }

            /// <summary>
            /// Sets or returns Base64 string representation of this data
            /// </summary>
            public string Base64
            {
                get { return ToBase64(_b); }
                set { _b = FromBase64(value); }
            }
        }
        #endregion
    }
}
