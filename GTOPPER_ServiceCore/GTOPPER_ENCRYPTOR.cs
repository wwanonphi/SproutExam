using System;
using System.IO;
using System.Security.Cryptography;
using System.Text; 
using System.Configuration;
using System.Collections.Specialized;
using IniParser;
using IniParser.Model;
using System.Linq;
using System.Diagnostics;

namespace GTOPPER_ClassLibrary
{
      class GTOPPER_ENCRYPTOR
    {
        //SqlConnection conn = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;");
        private const string Key ="SproutDB@3ncrypt";
        private const string IV = "@Syrle3nCrypti0n";
         
        public static string GetConn()
       {
            //Encrypt("Server=172.16.16.88;Database=GT_SYSTEM;User Id=gtopper;Password=Th1sisGT;");
         
            string _filePath = Directory.GetCurrentDirectory();
            Debug.WriteLine(_filePath);
            //_filePath =Directory.GetParent( Directory.GetParent( Directory.GetParent(_filePath).FullName).FullName).FullName;

            //LDAP.GOLDERTOPPER:5000

            //get environment
            //if environment == dev use dev 
            //if environment == prod use prod
            var data = new FileIniDataParser();
            IniData D = data.ReadFile(Path.Combine(_filePath,"settings.ini"));
            //Console.WriteLine( D["DB-DEV"]["CONN"].ToString());


            //CHECK ENVIRONMENT TO USE
            //... IF ENVIRONMENT == DEV THEN USE BD-DEV
            //... IF ENVIRONMENT == PROD THE USE DB-PROD

            string toret = ""; string conn = "";
            string environment = D["ENVIRONMENT"]["MYENVI"].ToString();
            conn = environment=="DEV" ? Decrypt(D["DB-DEV"]["CONN"].ToString()) : Decrypt(D["DB-PROD"]["CONN"].ToString()); 
            char[] x = conn.ToCharArray();
                
            foreach(char y in x)
            {
                if (y !=  ' ') 
                {
                    toret += y;
                } 
            }
            
            //remove \0 from the chars to string 
            string filteredConn = toret.Replace("\0", "");
             
             string f =filteredConn.Replace("UserId", "User ID");
                 f = f.Replace("IntegratedSecurity", "Intergrated Security"); 
            return f;
        }


        public static string Encrypt(string connection)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] src = Encoding.Unicode.GetBytes(connection);

            using (ICryptoTransform crypt = aes.CreateEncryptor())
            {
                byte[] dest = crypt.TransformFinalBlock(src, 0, src.Length);
                Console.WriteLine(Convert.ToBase64String(dest));
                return Convert.ToBase64String(dest);
            }
        }

        public static string Decrypt(string encryptedTex)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] src = Convert.FromBase64String(encryptedTex);
            using (ICryptoTransform crypto = aes.CreateDecryptor())
            {
                byte[] dest = crypto.TransformFinalBlock(src, 0, src.Length);
                return Encoding.UTF8.GetString(dest);
            }
        }
    
    
    
    }



}
