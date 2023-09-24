using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            operaciones operaciones1 = new operaciones();
            //string inputText = "Hello, World!";
            //Console.WriteLine("Input Text: " + inputText);
            operaciones1.Csv();
            operaciones1.BuscarYDecodificar();
            // Calcular las frecuencias de los caracteres en el texto de entrada.
            //Dictionary<char, int> frequencies = CalculateFrequencies(inputText);

            // Crear el árbol de Huffman basado en las frecuencias.
            //var huffmanTree = new HuffmanTree(frequencies.ToList());

            // Construir la tabla de codificación de Huffman.
            //Dictionary<char, string> huffmanTable = HuffmanEncoding.BuildHuffmanTable(huffmanTree.Root);

            // Codificar el texto de entrada.
            //string encodedText = HuffmanEncoding.Encode(inputText, huffmanTable);
            //Console.WriteLine("Encoded Text: " + encodedText);

            // Decodificar el texto codificado.
            //string decodedText = HuffmanEncoding.Decode(encodedText, huffmanTree.Root);
            //Console.WriteLine("Decoded Text: " + decodedText);
            Console.ReadKey();
        }

      
    }
}
