using System;
using GTOPPER_ClassLibrary;
using Microsoft.AspNetCore.Hosting;

namespace GTOPPER_ServiceCore
{
    class Program
    { 
        static void Main(string[] args )
        { 
           GTOPPER_ENCRYPTOR.Encrypt("insert string conn here");
            //string xx=  GTOPPER_ENCRYPTOR.Decrypt("Wrb1L2hh+29IQu99CfrgA4ZLW0pxN9fat5wNc06wIr28wBdcxKkz3pXT7UCbfvN2/eupEUM2lVrTn5Mkmo8lEkxDtGPVX25HlNETpStnbFwMXpS1EOWRu3pxxoRnoVahTGcNa1xVrkMm2FNrcLTvtb7PbvufLkM17fDKxprUTfBgfhT2O5sq3r9z+wJ1qnwygyMUqI/sCYblCy61Gay5kg==");
            //Console.WriteLine($"Old Conn {xx}");
            Console.ReadLine();
             
        }

   
    }
}
