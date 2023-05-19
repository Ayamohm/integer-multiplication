using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
        /// 
        //public static byte[] AddArrays(byte[] x, byte[] y)
        //{
        //    int l = 0;
        //    if (x.Length > y.Length)
        //    {
        //        l = x.Length;
        //    }
        //    else
        //    {
        //        l = y.Length;
        //    }
        //    byte[] sum = new byte[l];
        //    for (int i = 0; i < l; i++)
        //    {
        //        sum[i] = (byte)(x[i] + y[i]);
        //    }
        //    return sum;
        //}
        //public static byte[] multi(byte[] x, byte[] y)
        //{
        //    int l = 0;
        //    if (x.Length > y.Length)
        //    {
        //        l = x.Length;
        //    }
        //    else
        //    {
        //        l = y.Length;
        //    }
        //    byte[] multp = new byte[l];
        //    for (int i = 0; i < l; i++)
        //    {
        //        multp[i] = (byte)(x[i] + y[i]);
        //    }
        //    return multp;
        //}
        private static byte[] Add(byte[] x, byte[] y)
        {
            int n = 0;
            if (x.Length < y.Length)
            {
                n = x.Length;
            }
            else
            {
                n = y.Length;
            }
            byte[] result = new byte[n];
            byte carry = 0;
            for (int i = 0; i < n; i++)
            {
                byte xi = i < x.Length ? x[i] : (byte)0;
                byte yi = i < y.Length ? y[i] : (byte)0;
                byte sum = (byte)(xi + yi + carry);
                result[i] = sum;
                carry = (byte)(sum >> 8);
            }
            if (carry != 0)
            {
                throw new InvalidOperationException("Cannot add two numbers of different sign.");
            }
            return result;
        }

        private static byte[] Subtract(byte[] x, byte[] y)
        {
            int n = 0;
            if (x.Length < y.Length)
            {
                n = x.Length;
            }
            else
            {
                n = y.Length;
            }
            byte[] result = new byte[n];
            byte borrow = 0;
            for (int i = 0; i < n; i++)
            {
                byte xi = i < x.Length ? x[i] : (byte)0;
                byte yi = i < y.Length ? y[i] : (byte)0;
                byte diff = (byte)(xi - yi - borrow);
                result[i] = diff;
                borrow = (byte)(diff >> 8);
            }
            if (borrow != 0)
            {
                throw new InvalidOperationException("Cannot subtract two numbers of different sign.");
            }
            return result;
        }

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {


            //base
            //int n = Math.Max(X.Length, Y.Length); //if condition
            if (N <= 1)
            {
                return new byte[] { (byte)(X[0] * Y[0]) };
            }
            else
            {
                int n = 0;
                if (X.Length < Y.Length)
                {
                  n = X.Length;
                }
                else
                {
                    n = Y.Length;
                }
                int m = n / 2;
                byte[] x1 = X.Take(m).ToArray();
                byte[] x2 = X.Skip(m).ToArray();
                byte[] y1 = Y.Take(m).ToArray();
                byte[] y2 = Y.Skip(m).ToArray();

                byte[] z0 = IntegerMultiply(x2, y2, m);
                byte[] z2 = IntegerMultiply(x1, y1, m);

                byte[] x1x2 = Add(x1, x2);
                byte[] y1y2 = Add(y1, y2);
                byte[] z1 = Subtract(Subtract(IntegerMultiply(x1x2, y1y2, m), z2), z0);

                byte[] result = new byte[2 * N];
                Array.Copy(z2, 0, result, 0, z2.Length);
                Array.Copy(z1, 0, result, m, z1.Length);
                Array.Copy(z0, 0, result, 2 * m, z0.Length);
                return result;
            }
        }      
        #endregion
    }
}
