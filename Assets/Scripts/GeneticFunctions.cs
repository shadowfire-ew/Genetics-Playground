using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GeneticFunctions
{
    const uint BASE_NIBBLE = 0b_1111;

    [RuntimeInitializeOnLoadMethod]
    static void TEST()
    {
        byte bit = 2;
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
        uint genome = 0b_0000_0000;
        MutateRandomBit(ref genome, 1);
        Debug.Log($"Genome mutated by random bit {Convert.ToString(genome,2)}");
        MutateRandomNibble(ref genome, 1);
        Debug.Log($"Genome with random nibble mutated {Convert.ToString(genome,2)}");
        MutateRandomBits(ref genome, 1);
        Debug.Log($"Genome with random bits mutated {Convert.ToString(genome,2)}");
    }

    public static void CrossoverBit(ref uint a, ref uint b, byte bitNum)
    {
        if (bitNum >= sizeof(uint) * 8)
            throw new ArgumentException("bit number out of range");
        uint bit = 1;
        bit <<= bitNum;
        if ((a&bit) != (b&bit))
        {
            a ^= bit;
            b ^= bit;
        }
    }

    public static void CrossoverNibble(ref uint a, ref uint b, byte nibbleNum)
    {
        if (nibbleNum >= sizeof(uint) * 2)
            throw new ArgumentException("nibbles out of range");
        uint nibble = BASE_NIBBLE << (nibbleNum * 4);
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

    public static void FlipBit(ref uint g, byte bitNum)
    {
        if (bitNum >= sizeof(uint) * 8)
            throw new ArgumentException("bit number out of range");
        uint bit = 1;
        bit <<= bitNum;
        g ^= bit;
    }

    public static void FlipNibble(ref uint g, byte nibbleNum)
    {
        if (nibbleNum >= sizeof(uint) * 2)
            throw new ArgumentException("nibbles out of range");
        uint nibble = BASE_NIBBLE << (nibbleNum * 4);
        FlipBits(ref g, nibble);
    }

    public static void FlipBits(ref uint g, uint bits)
    {
        g ^= bits;
    }

    public static void MutateRandomBit(ref uint g, float chance)
    {
        if (UnityEngine.Random.value <= chance)
        {
            byte bitNum = (byte)UnityEngine.Random.Range(0, sizeof(uint) * 8);
            FlipBit(ref g, bitNum);
        }
    }

    public static void MutateRandomNibble(ref uint g, float chance)
    {
        if (UnityEngine.Random.value <= chance)
        {
            byte nibbleNum = (byte)UnityEngine.Random.Range(0, sizeof(uint) * 2);
            FlipNibble(ref g, nibbleNum);
        }
    }

    public static void MutateRandomBits(ref uint g, float chance)
    {
        if (UnityEngine.Random.value <= chance)
        {
            // Generate 2 sets of 16 random bits
            uint a = (uint)UnityEngine.Random.Range(0, 1 << 16);
            //
            uint b = (uint)UnityEngine.Random.Range(0, 1 << 16)<<16;
            uint bits = a + b;
            FlipBits(ref g, bits);
        }
    }
}
