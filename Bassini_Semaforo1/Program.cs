using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bassini_Semaforo1
{
    class Program

    {
        static string txt = "";
        static int numB = 0;
        static SemaphoreSlim S = new SemaphoreSlim(1); //Quando l'inizializzo a verde

        static void Main(string[] args)
        {

            while (true) {
                txt = "";
                numB = 0;
                Thread t1 = new Thread(() => Incrementa());
                Thread t2 = new Thread(() => Decrementa());
                t1.Start();
                t2.Start();
                while (t1.IsAlive) { }
                while (t2.IsAlive) { }

                Console.WriteLine(txt);
                Console.WriteLine(txt.Length);
                Console.WriteLine(numB);
                Console.ReadLine();
            }
            

            Console.ReadLine();
        }
        static void Incrementa()
        {
            for (int i = 0; i < 10000; i++)
            {
                

                S.Wait();
                txt = txt + "a";
                S.Release();
                //N=4
                //Reg1=N;
                //Reg1=4
                //Reg1=Reg1+1
                //Reg1=5
                // output Reg1 su N
                //N=5

                //N = N + 1;
            }
        }
        static void Decrementa()
        {
            for (int i = 0; i < 10000; i++)
            {
                S.Wait();
               
                if (txt.Length > 0)
                
                {
                    string lastChar = txt.Substring(txt.Length - 1, 1);
                    if (lastChar != "b")
                    {
        
                        txt = txt.Substring(0, txt.Length-1);
                    }
                    else
                    {
                        txt = txt + "b";
                        numB++;
                    }
                    
                }
                else
                {
                    txt = txt + "b";
                    numB++;
                }
                

                S.Release();
                //Reg2=N;
                //Reg2=4
                //Reg2=Reg2-1
                //Reg2=3
                // output Reg2 su N
                // N=3
            }
        }
    }
}
