using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using PZ_SERVER_LIBRARY;

namespace PZ_SERVER
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        if(data == "WL")
                        {
                            PZ_SERVER_LIBRARY.Search szukaj = new PZ_SERVER_LIBRARY.Search();
                            string szukana = szukaj.znajdzLek("Apap");
                            Byte[] data2 = System.Text.Encoding.ASCII.GetBytes(szukana);
                            stream.Write(data2, 0, data2.Length);
                        }
                        else
                        {
                            Byte[] data2 = System.Text.Encoding.ASCII.GetBytes("Zalogowano");
                            stream.Write(data2, 0, data2.Length);
                        }
                        //zapis do bazy danych rejestracji
                        

                        // Process the data sent by the client.

                    }
                    var cryptoServiceProvider = new RSACryptoServiceProvider(2048); //2048 - Długość klucza
                    var privateKey = cryptoServiceProvider.ExportParameters(true); //Generowanie klucza prywatnego
                    var publicKey = cryptoServiceProvider.ExportParameters(false); //Generowanie klucza publiczny

                    string publicKeyString = PZ_SERVER_LIBRARY.MyTcpListener.GetKeyString(publicKey);
                    string privateKeyString = PZ_SERVER_LIBRARY.MyTcpListener.GetKeyString(privateKey);

                    Console.WriteLine("KLUCZ PUBLICZNY: ");
                    Console.WriteLine(publicKeyString);
                    Console.WriteLine("-------------------------------------------");


                    Console.WriteLine("KLUCZ PRYWATNY: ");
                    Console.WriteLine(privateKeyString);
                    Console.WriteLine("-------------------------------------------");


                    string textToEncrypt = data;
                    Console.WriteLine("TEKST DO ZASZYFROWANIA: ");
                    Console.WriteLine(textToEncrypt);
                    Console.WriteLine("-------------------------------------------");

                    string encryptedText = PZ_SERVER_LIBRARY.MyTcpListener.Encrypt(textToEncrypt, publicKeyString); //Szyfrowanie za pomocą klucza publicznego
                    Console.WriteLine("ZASZYFROWANY TEXT: ");
                    Console.WriteLine(encryptedText);
                    Console.WriteLine("-------------------------------------------");

                    string decryptedText = PZ_SERVER_LIBRARY.MyTcpListener.Decrypt(encryptedText, privateKeyString); //Odszyfrowywanie za pomocą klucza prywatnego

                    Console.WriteLine("ODSZYFROWANY TEXT: ");
                    Console.WriteLine(decryptedText);

                    data = publicKeyString;

                    //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("abc");

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", "abc");



                    // Shutdown and end connection
                    //client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        } 
    }
}
