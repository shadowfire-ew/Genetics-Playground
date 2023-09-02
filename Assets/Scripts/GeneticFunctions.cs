using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneticFunctions
{

    [RuntimeInitializeOnLoadMethod]
    static void TEST()
    {
        ushort bit = 2;
        uint q = 1;
        Debug.Log($"Before shift: {q} {Convert.ToString(q,2)}");
        q <<= bit;
        Debug.Log($"After shift: {q} {Convert.ToString(q, 2)}");
        uint a = 0b_1011_0101;
        uint b = 0b_1100_1011;
        Debug.Log($"Before Crossover Bit {bit}:\nA: {a} {Convert.ToString(a,2)}\nB: {b} {Convert.ToString(b,2)}");
        CrossoverBit(ref a, ref b, bit);
        Debug.Log($"After Crossover Bit {bit}:\nA: {a} {Convert.ToString(a, 2)}\nB: {b} {Convert.ToString(b, 2)}");
        CrossoverNibble(ref a, ref b, 1);
        Debug.Log($"After Crossover Nibble {1}:\nA: {a} {Convert.ToString(a, 2)}\nB: {b} {Convert.ToString(b, 2)}");
    }

    public static void CrossoverBit(ref uint a, ref uint b, ushort bitNum)
    {
        uint bit = 1;
        bit <<= bitNum;
        if ((a&bit) != (b&bit))
        {
            a ^= bit;
            b ^= bit;
        }
    }

    public static void CrossoverNibble(ref uint a, ref uint b, ushort nibbleNum)
    {
        uint nibble = 0b_1111;
        nibble <<= (nibbleNum * 4);
        CrossoverBits(ref a, ref b, nibble);
    }

    public static void CrossoverBits(ref uint a, ref uint b, uint bits)
    {
        uint aComp = a & bits;
        uint bComp = b & bits;
        uint bitsToChange = aComp ^ bComp;
        if (bitsToChange != 0)
        {
            a ^= bitsToChange;
            b ^= bitsToChange;
        }
    }
}
