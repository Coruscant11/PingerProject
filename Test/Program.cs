using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string nbr = "131.45";
            Console.WriteLine(double.Parse(nbr.Replace('.', ',')));
            Console.Read();
        }

        static async void Start()
        {
            
            await Launch();
            Console.WriteLine("bite");
        }

        static async Task Launch()
        {
            // Declare an HttpClient object, and increase the buffer size. The
            // default buffer size is 65,536.
            HttpClient client =
                new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            // Create and start the tasks. As each task finishes, DisplayResults 
            // displays its length.
            Task<int> download1 =
                ProcessURLAsync("http://www.developpez.com/", client);
            Task<int> download2 =
                ProcessURLAsync("https://www.google.fr/webhp?hl=fr", client);
            Task<int> download3 =
                ProcessURLAsync("https://pomf.is/", client);

            // Await each task.
            int length1 = await download1;
            int length2 = await download2;
            int length3 = await download3;

            int total = length1 + length2 + length3;

            // Display the total count for the downloaded websites.
            Console.WriteLine("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }

        static async Task<int> ProcessURLAsync(string url, HttpClient client)
        {
            var byteArray = await client.GetByteArrayAsync(url);
            DisplayResults(url, byteArray);
            return byteArray.Length;
        }

        static void DisplayResults(string url, byte[] content)
        {
            Console.WriteLine(url + "\n" + content.Length);
        }
    }
}
