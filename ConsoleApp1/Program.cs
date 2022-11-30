using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string n,n_upper,nr;
            long s1,aux1;
            double aux2,s2,p,c,rest;
            int b1, b2, power1, power2,index,last,num,r,zec;
            bool sign,comma;

            Console.WriteLine("Introduce-ti numarul care urmeaza sa fie convertit.");
            n=Console.ReadLine();
            n_upper=n.ToUpper();
            List<string> digits = new List<string>();
            foreach (char e in n_upper)
            {
                nr=e.ToString();  //se converteste fiecare cifra a numarul in string
                digits.Add(nr);
            }

            for(int i = 0; i < digits.Count; i++)
            {
                if (digits[i].Contains("A"))
                    digits[i] = "10";
                if (digits[i].Contains("B"))
                    digits[i] = "11";
                if (digits[i].Contains("C"))
                    digits[i] = "12";
                if (digits[i].Contains("D"))   //pentru bazele mai mari de 10 se convertesc literele in cifre
                    digits[i] = "13";
                if (digits[i].Contains("E"))
                    digits[i] = "14";
                if (digits[i].Contains("F"))
                    digits[i] = "15";
            }
          
            sign = digits.Contains("-");
            if (sign)                   //se stabileste daca numarul este negativ si se elimina "-" daca este necesar
                digits.Remove("-");
            
            if (digits.Contains ("."))
            {
                Console.WriteLine("Delimtatorul partii zecimale este ',' nu '.'.Reporneste programul si introdu numarul corect.");
                return;
            }
          
            Console.WriteLine("Introduce-ti baza originala");
            b1=int.Parse(Console.ReadLine());
            
            Console.WriteLine("Introduce-ti baza in care doriti sa convertiti");
            b2 =int.Parse(Console.ReadLine());

            if(b1<2 || b1 > 16)
            {
                Console.WriteLine("Programul converteste doar numere in bazele 2 pana la 16");
                return;
            }

            comma = digits.Contains(",");
            if (!comma)
            {
                digits.Add(",");    //daca numarul este intreg se adauga o virgula ca string pentru a elimina exceptiile
            }
            
            index=digits.IndexOf(",");
            List<string> before_comma = digits.GetRange(0, index);
            // se separa stringurile in partea de dinainte de virgula si in cea dupa virgula
            last = digits.Count() - before_comma.Count;
            List<string> after_comma = digits.GetRange(index,last);
            after_comma.Remove(after_comma[0]);//se elimina virgula

            List<int> before_comma_int = new List<int>();
            List<int> after_comma_int=new List<int>();

            foreach (string element in before_comma)
            {
                num = int.Parse(element);
                before_comma_int.Add(num);
            }
            before_comma_int.Reverse();       //se convertesc stringurile in int pentru calcule

            foreach (string element in after_comma)
            {
                num = int.Parse(element);
                after_comma_int.Add(num);
            }

            foreach (int element in before_comma_int)
            {
                
                if (element >= b1)
                {
                    Console.WriteLine($"Numarul introdus nu este in baza {b1}.Reporniti programul si introduce-ti numarul corect");
                    return;
                }
            }
            
            foreach (int element in after_comma_int)
            {
                
                if (element >= b1)
                {
                    Console.WriteLine($"Numarul introdus nu este in baza {b1}.Reporniti programul si introduce-ti numarul corect");
                    return;
                }
            }

            power1 = 0;
            s1 = 0;
            foreach(int element in before_comma_int)
            {
                aux1 =element*(long)Math.Pow(b1,power1);
                power1 += 1;
                s1 += aux1;
            }                                               //se converteste numarul in baza 10
            
            power2 = 1;
            s2 = 0;
            foreach (int element in after_comma_int)
            {
                aux2=element/((double)Math.Pow(b1,power2));
                power2 +=1;
                s2 += aux2;
            }
            
            List<string>second_conversion_before_comma=new List<string>();

            if (s1 == 0)
            {
                second_conversion_before_comma.Add("0");
            }
            while (s1 != 0)                                  //se converteste numarul din baza 10
            {
                r = (int)s1 % b2;
                nr=r.ToString();
                second_conversion_before_comma.Add(nr);
                s1 /= b2;
            }
            second_conversion_before_comma.Reverse();

            List<string>second_conversion_after_comma=new List<string>();
            zec = 1;
            if (s2 > 0)
            {
                Console.WriteLine("Cate zecimale doriti sa calculati?");
                zec = int.Parse(Console.ReadLine());
            }

            rest = 1;
            while (rest!=0)
            {
                p = s2 * b2;
                c=Math.Truncate(p);
                nr =c.ToString();
                second_conversion_after_comma.Add(nr);
                if (second_conversion_after_comma.Count()>(zec-1))
                {
                    break;
                }
                rest = p-c;
                s2 = rest;
            }
            
            for (int i = 0; i < second_conversion_before_comma.Count; i++)
            {
                if (second_conversion_before_comma[i].Contains("10"))
                    second_conversion_before_comma[i] = "A";
                if (second_conversion_before_comma[i].Contains("11"))
                    second_conversion_before_comma[i] = "B";
                if (second_conversion_before_comma[i].Contains("12"))
                    second_conversion_before_comma[i] = "C";
                if (second_conversion_before_comma[i].Contains("13"))
                    second_conversion_before_comma[i] = "D";
                if (second_conversion_before_comma[i].Contains("14"))
                    second_conversion_before_comma[i] = "E";
                if (second_conversion_before_comma[i].Contains("15"))
                    second_conversion_before_comma[i] = "F";
            }

            for (int i = 0; i < second_conversion_after_comma.Count; i++)
            {
                if (second_conversion_after_comma[i].Contains("10"))
                    second_conversion_after_comma[i] = "A";
                if (second_conversion_after_comma[i].Contains("11"))
                    second_conversion_after_comma[i] = "B";
                if (second_conversion_after_comma[i].Contains("12"))
                    second_conversion_after_comma[i] = "C";
                if (second_conversion_after_comma[i].Contains("13"))
                    second_conversion_after_comma[i] = "D";
                if (second_conversion_after_comma[i].Contains("14"))
                    second_conversion_after_comma[i] = "E";
                if (second_conversion_after_comma[i].Contains("15"))
                    second_conversion_after_comma[i] = "F";
            }

            if (sign == true)
            {
                Console.WriteLine($"{n} in baza {b1} este -{String.Join("", second_conversion_before_comma)},{String.Join("", second_conversion_after_comma)} in baza {b2}");
            }
            else if (sign == false)
            {
                Console.WriteLine($"{n} in baza {b1} este {String.Join("", second_conversion_before_comma)},{String.Join("", second_conversion_after_comma)} in baza {b2}");
            }
            
            Console.WriteLine();
        }
    }
}
