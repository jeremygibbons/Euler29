using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler29
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxA = 100;
            int maxB = 100;

            List<PrimeFactorRep> uniqueNumbers = new List<PrimeFactorRep>();

            foreach(int a in Enumerable.Range(2, maxA - 1))
            {
                foreach(int b in Enumerable.Range(2, maxB - 1))
                {
                    PrimeFactorRep p = PrimeFactorRep.ConvertPowerAOfBToPrimeFactors(a, b);
                    if(uniqueNumbers.Contains(p) == false) {
                        uniqueNumbers.Add(p);
                    }
                }
            }

            Console.WriteLine(uniqueNumbers.Count());
            Console.ReadLine();
        }
    }

    class PrimeFactorRep : Dictionary<int, int>
    {
        private static readonly List<int> primesBelow100 = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97}; 

        int _base;
        int _exp;
        int _primeFactors;
        public bool Equals(PrimeFactorRep other)
        {
            if (other._primeFactors != this._primeFactors)
                return false;
            if (this.Count != other.Count)
                return false;
            foreach (var pair in this)
                if (!(pair.Value == other[pair.Key]))
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PrimeFactorRep);
        }

        public static PrimeFactorRep ConvertPowerAOfBToPrimeFactors(int a, int b)
        {
            PrimeFactorRep p = GetPrimeFactorRep(a);
            p._base = a;
            p._exp = b;
            List<int> keys = p.Keys.ToList();
            foreach(int k in keys)
            {
                p._primeFactors = p._primeFactors | (1 << primesBelow100.IndexOf(k));
                p[k] = p[k] * b;
            }
            return p;
        }

        static PrimeFactorRep GetPrimeFactorRep(int n)
        {
            PrimeFactorRep p = new PrimeFactorRep();

            int d = 2;
            int count = 0;
            while(n > 1)
            {
                if(n % d == 0)
                {
                    count++;
                    n = n / d;
                } else
                {
                    if (count > 0)
                        p.Add(d, count);

                    d += (d == 2) ? 1 : 2;
                }    
            }
            p.Add(d, count);
            return p;
        }
    }
}
