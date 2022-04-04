using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetworkSecurityProject
{
    class Program
    {
        // Caeser Encryption---------------------------------------------------------------------------------------
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }

        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cipher(ch, key);

            return output;
        }

        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }





        // AES encryption-----------------------------------------------------------------------------------------------------
        static void EncryptAesManaged(string raw)
        {  
                using (AesManaged aes = new AesManaged())
                { 
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    foreach (var item in encrypted)
                    {
                        Console.Write(item);
                    }
                }        
            Console.ReadKey();
        }


        static void DecryptAesManaged(string raw)
        {
            using (AesManaged aes = new AesManaged())
            {
                byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                foreach (var item in decrypted)
                {
                    Console.Write(item);
                }
                Console.WriteLine(decrypted);
            }
            Console.ReadKey();
        }






        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;  
            using (AesManaged aes = new AesManaged())
            { 
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV); 
                using (MemoryStream ms = new MemoryStream())
                {   
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {   
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            } 
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            { 
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {   
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    { 
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        //


        static void Main(string[] args)
        {
            
            Console.WriteLine("Type a string to encrypt:");
            string UserString = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Enter your Key ");
            int key = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();


            Console.WriteLine("Encrypted Data");

            string cipherText = Encipher(UserString, key);
            Console.WriteLine(cipherText);
            Console.WriteLine();


            Console.WriteLine("AES Encrypted Data");
            EncryptAesManaged(UserString);
            Console.WriteLine();


            /*  Console.WriteLine("Decrypted Data:");

              string t = Decipher(cipherText, key);
              Console.WriteLine(t);
              Console.Write("\n");
            */



            /*
             Console.WriteLine("Enter text that needs to be encrypted..");
             string data = Console.ReadLine();
             Console.WriteLine("AES Encrypted Data");
             EncryptAesManaged(data);
             Console.WriteLine();
             Console.WriteLine("AES Decryption Data");
             DecryptAesManaged(data);
             */



            Console.ReadKey();
        }
    }
}
