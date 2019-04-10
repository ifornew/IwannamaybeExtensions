using Common.Extensions.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{

    /************************************************************
     * 关于hashAlgorithm参数值有：MD5、SHA1、SHA256、SHA384、SHA512
     * 
     * RSA加密解密：私钥解密，公钥加密。
     * RSA数字签名-俗称加签验签：私钥加签，公钥验签。  
     * RSA加密解密：私钥解密，公钥加密。
     * RSA数字签名-俗称加签验签：私钥加签，公钥验签。  
     * RSA加密解密：私钥解密，公钥加密。
     * RSA数字签名-俗称加签验签：私钥加签，公钥验签。 
     * ☆☆☆☆【注意这里所有的加密结果及加签结果都是base64的】☆☆☆☆☆
     */
    public abstract class EasyRSA
    {
        #region 密钥
        public static string GenerateXmlPrivateKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            return rsa.ToXmlString(true);
        }

        public static string GenerateXmlPublicKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            return rsa.ToXmlString(false);
        }

        #endregion

        #region  加密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publicKeyCSharp"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptCSharp(string publicKeyCSharp, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publicKeyCSharp);

            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有               
            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA加密PEM秘钥
        /// </summary>
        /// <param name="publicKeyPEM"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptPEM(string publicKeyPEM, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.LoadPublicKeyPEM(publicKeyPEM);

            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有               
            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);

            return Convert.ToBase64String(cipherbytes);
        }
        #endregion

        #region 解密
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKeyCSharp"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string DecryptCSharp(string privateKeyCSharp, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privateKeyCSharp);
            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有        
            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKeyPEM"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string DecryptPEM(string privateKeyPEM, string data, string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.LoadPrivateKeyPEM(privateKeyPEM);
            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有        
            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
        }
        #endregion


        #region 加签
        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="privateKeyPEM">私钥</param>
        /// <param name="data">待签名的内容</param>
        /// <returns></returns>
        public static string RSASignPEM(string data, string privateKeyPEM, string hashAlgorithm = "MD5", string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.LoadPrivateKeyPEM(privateKeyPEM);//加载私钥   
            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
            return Convert.ToBase64String(HashbyteSignature);
        }
        /// <summary>
        /// RSA签名CSharp
        /// </summary>
        /// <param name="privateKeyCSharp">私钥</param>
        /// <param name="data">待签名的内容</param>
        /// <returns></returns>
        public static string RSASignCSharp(string data, string privateKeyCSharp, string hashAlgorithm = "MD5", string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyCSharp);//加载私钥   
            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
            return Convert.ToBase64String(HashbyteSignature);
        }

        #endregion

        #region 验签

        /// <summary> 
        /// 验证签名PEM
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static bool VerifyPEM(string data, string publicKeyPEM, string signature, string hashAlgorithm = "MD5", string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //导入公钥，准备验证签名
            rsa.LoadPublicKeyPEM(publicKeyPEM);
            //返回数据验证结果
            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
            byte[] rgbSignature = Convert.FromBase64String(signature);

            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
        }

        /// <summary> 
        /// 验证签名CSharp
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static bool VerifyCSharp(string data, string publicKeyCSharp, string signature, string hashAlgorithm = "MD5", string encoding = "UTF-8")
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //导入公钥，准备验证签名
            rsa.FromXmlString(publicKeyCSharp);
            //返回数据验证结果
            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
            byte[] rgbSignature = Convert.FromBase64String(signature);

            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
        }
        #endregion
    }
}