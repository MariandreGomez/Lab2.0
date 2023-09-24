using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Lab2
{

    class HuffmanNode : IComparable<HuffmanNode>
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public int CompareTo(HuffmanNode other)
        {
            return Frequency.CompareTo(other.Frequency);
        }
    }

   
    class HuffmanTree
    {
        public HuffmanNode Root { get; private set; }

        public HuffmanTree(List<KeyValuePair<char, int>> frequencyTable)
        {
            PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();

            foreach (var entry in frequencyTable)
            {
                priorityQueue.Enqueue(new HuffmanNode { Symbol = entry.Key, Frequency = entry.Value });
            }

            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                var newNode = new HuffmanNode
                {
                    Symbol = '\0',
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right,
                };

                priorityQueue.Enqueue(newNode);
            }

            Root = priorityQueue.Dequeue();
        }


    }

    class HuffmanEncoding
    {
        public static Dictionary<char, string> BuildHuffmanTable(HuffmanNode node, string prefix = "")
        {
            var huffmanTable = new Dictionary<char, string>();

            if (node.Left == null && node.Right == null)
            {
                huffmanTable[node.Symbol] = prefix;
            }
            else
            {
                var leftTable = BuildHuffmanTable(node.Left, prefix + "0");
                var rightTable = BuildHuffmanTable(node.Right, prefix + "1");

                foreach (var kvp in leftTable)
                {
                    huffmanTable[kvp.Key] = kvp.Value;
                }

                foreach (var kvp in rightTable)
                {
                    huffmanTable[kvp.Key] = kvp.Value;
                }
            }

            return huffmanTable;
        }


        public static string Encode(string input, Dictionary<char, string> huffmanTable)
        {
            var encoded = new StringBuilder();

            foreach (char c in input)
            {
                if (huffmanTable.ContainsKey(c))
                {
                    encoded.Append(huffmanTable[c]);
                }
                else
                {
                    throw new ArgumentException("Character not found in Huffman table.");
                }
            }

            return encoded.ToString();
        }

        

        public static string Decode(string input, HuffmanNode root)
        {
            var decoded = new StringBuilder();
            HuffmanNode currentNode = root;

            foreach (char bit in input)
            {
                if (bit == '0')
                {
                    currentNode = currentNode.Left;
                }
                else if (bit == '1')
                {
                    currentNode = currentNode.Right;
                }

                if (currentNode.Left == null && currentNode.Right == null)
                {
                    decoded.Append(currentNode.Symbol);
                    currentNode = root;
                }
            }

            return decoded.ToString();
        }
    }


}
