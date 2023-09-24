using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab2.TreeNode;

namespace Lab2
{
    public class CSVData //clase para leer el archivo csv
    {
        public CSVData()
        {
        }

        public CSVData(string v1, string v2)
        {
            operacion = v1;
            JSONData = v2;
        }

        public string operacion { get; set; }
        public string JSONData { get; set; }

    }



    public class Persona
    {
        public Persona(string nameC, string dpiC, string datebirthC, string adrressC)
        {
            name = nameC;
            dpi = dpiC;
            datebirth = datebirthC;
            address = adrressC;
        }

        public Persona()
        {
            
        }


        public string name { get; set; }
        public string dpi { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
        public List<string> companies { get; set; }
    }

   

    internal class operaciones
    {
        //lista personas
        public static List<Persona> JsonPersonas = new List<Persona>();        
        string nombre, dpi, compañias, cumpleaños, direccion;
        public static List<Persona> JsonDataInsert = new List<Persona>();
        TreeNode insertarObjeto = new TreeNode(); //arbol 
        TreeNode insertar = new TreeNode(); //arbol   


        public void Csv()
        {
            string[] CsvLines = System.IO.File.ReadAllLines(@"C:\Users\maria\Desktop\input (1) (1).csv");
                     
            List<CSVData> insert = new List<CSVData>();
            List<CSVData> patch = new List<CSVData>();
            List<CSVData> eliminar = new List<CSVData>();

            for (int i = 0; i < CsvLines.Length; i++)
            {
                string[] rowdata = CsvLines[i].Split(';'); // lee el separador ";" 
                CSVData record = new CSVData(rowdata[0], rowdata[1]); //se inserta en la clase que contiene el jsondata y la operacion

                if (rowdata[0] == "INSERT")
                {
                    insert.Add(record);
                }
                else if (rowdata[0] == "PATCH")
                {
                    patch.Add(record);
                }
                else if (rowdata[0] == "DELETE")
                {
                    eliminar.Add(record);
                }

            }

            //Insertar datos en JsonPersona(datos ya codificados)
            foreach (var item in insert)
            {
                Persona persona = JsonConvert.DeserializeObject<Persona>(item.JSONData);
                JsonDataInsert.Add(persona);
                insertar.Insert(persona);

                nombre = persona.name;
                dpi = persona.dpi;
                cumpleaños = persona.datebirth;
                direccion = persona.address;


                string encodedDpi = EncodeData(persona.dpi);

                List<string> companies1 = new List<string>();
                companies1.Clear();
                for (int m = 0; m < persona.companies.Count; m++)
                {                                    
                    string encodedCompany = EncodeData(persona.companies[m]);
                    companies1.Add(encodedCompany);                                    
                }
                
                JsonPersonas.Add(new Persona
                {
                    name = nombre,
                    dpi = encodedDpi, // Usa el texto codificado de dpi
                    datebirth = cumpleaños,
                    address = direccion,
                    companies = companies1// Usa el texto codificado de compañías

                }); 

                insertarObjeto.Insert(new Persona
                {
                    name = nombre,
                    dpi = encodedDpi, // Usa el texto codificado de dpi
                    datebirth = cumpleaños,
                    address = direccion,
                    companies = companies1// Usa el texto codificado de compañías

                });
            }

            

            //Actualizar los datos
            List<Persona> JsonPersonasPatch = new List<Persona>();
            List<Persona> JsonPersonasAux = new List<Persona>();
            foreach (var item in patch)
            {
                Persona personaPatch = JsonConvert.DeserializeObject<Persona>(item.JSONData);
                JsonPersonasAux.Add(personaPatch);  

                nombre = personaPatch.name;
                dpi = personaPatch.dpi;
                cumpleaños = personaPatch.datebirth;
                direccion = personaPatch.address;


                string encodedDpi = EncodeData(personaPatch.dpi);

                List<string> companies2 = new List<string>();
                companies2.Clear();
                for (int m = 0; m < personaPatch.companies.Count; m++)
                {
                    compañias = personaPatch.companies[m];
                    string encodedCompany = EncodeData(personaPatch.companies[m]);
                    companies2.Add(encodedCompany);

                }

                JsonPersonasPatch.Add(new Persona
                {
                    name = nombre,
                    dpi = encodedDpi, // Usa el texto codificado de dpi
                    datebirth = cumpleaños,
                    address = direccion,
                    companies = companies2// Usa el texto codificado de compañías
                });
                             

            }

            foreach (var item1 in JsonPersonasPatch)
            {
                var personaAactualizar = JsonPersonas.FirstOrDefault(p => p.name == item1.name && p.datebirth == item1.datebirth);

                if (personaAactualizar != null)
                {
                    // Actualizar los datos
                    personaAactualizar.datebirth = item1.datebirth;
                    personaAactualizar.address = item1.address;
                    personaAactualizar.name = item1.name;
                    personaAactualizar.companies = item1.companies;
                    insertarObjeto.ActualizarPersona(personaAactualizar);
                }
            }

            

            foreach (var item2 in JsonPersonasAux)
            {
                var personaAactualizar = JsonDataInsert.FirstOrDefault(p => p.name == item2.name && p.datebirth == item2.datebirth);
                

                if (personaAactualizar != null)
                {
                    // Actualizar los datos
                    personaAactualizar.datebirth = item2.datebirth;
                    personaAactualizar.address = item2.address;
                    personaAactualizar.name = item2.name;
                    personaAactualizar.companies = item2.companies;
                    insertar.ActualizarPersona(personaAactualizar);

                    
                }
            }

            

            //Eliminar registros codificados 
            List<Persona> JsonPersonasDelete = new List<Persona>();
            List<Persona> JsonPersonasaux = new List<Persona>();
            foreach (var item in eliminar)
            {
                Persona personaDelete = JsonConvert.DeserializeObject<Persona>(item.JSONData);
                JsonPersonasaux.Add(personaDelete);

                nombre = personaDelete.name;
                dpi = personaDelete.dpi;
                cumpleaños = personaDelete.datebirth;
                direccion = personaDelete.address;

                string encodedDpi = EncodeData(personaDelete.dpi);

                List<string> companies3 = new List<string>();
                companies3.Clear();        
                for (int m = 0; m < personaDelete.companies.Count; m++)
                {
                    compañias = personaDelete.companies[m];

                    string encodedCompany = EncodeData(personaDelete.companies[m]);
                    companies3.Add(encodedCompany);
                }

                JsonPersonasDelete.Add(new Persona
                {
                    name = nombre,
                    dpi = encodedDpi, // Usa el texto codificado de dpi
                    datebirth = cumpleaños, 
                    address = direccion,
                    companies = companies3// Usa el texto codificado de compañías
                });               

            }

            foreach (var itemDelete1 in JsonPersonasaux)
            {
                JsonDataInsert.RemoveAll(persona =>
                   persona.name == itemDelete1.name && persona.dpi == itemDelete1.dpi); 

                insertar.EliminarPersona(itemDelete1.name, itemDelete1.dpi);

            }

            foreach (var itemDelete in JsonPersonasDelete)
            {
                JsonPersonas.RemoveAll(persona =>
                   persona.name == itemDelete.name && persona.dpi == itemDelete.dpi); 

                insertarObjeto.EliminarPersona(itemDelete.name,itemDelete.dpi);
            }           
        }


        public void BuscarYDecodificar()
        {
            string encodedText = "";
            string decodedText = "";
            string encodedText2 = "";
            string decodedText2 = "";
            List<string> companiesaux = new List<string>();
            Console.WriteLine("Que nombre desea buscar");
            string nombreSearch = Console.ReadLine();

            List<Persona> personasEncontradas = JsonPersonas.Where(p => p.name == nombreSearch).ToList();
            List<Persona> personasEncontradas2 = JsonDataInsert.Where(p => p.name == nombreSearch).ToList();
            List<Persona> personasEncontradas3 = insertar.BuscarPersonasPorNombre(nombreSearch); //busco en mi arbol 


            if (personasEncontradas2.Any())
            {                              
                    foreach (var item in personasEncontradas2)                   {

                        Dictionary<char, int> frequencies = CalculateFrequencies(item.dpi);
                        var huffmanTree = new HuffmanTree(frequencies.ToList());
                        Dictionary<char, string> huffmanTable = HuffmanEncoding.BuildHuffmanTable(huffmanTree.Root);
                        encodedText = HuffmanEncoding.Encode(item.dpi, huffmanTable);
                        decodedText = HuffmanEncoding.Decode(encodedText, huffmanTree.Root); 
                        
                        companiesaux.Clear();
                        for (int i = 0; i < item.companies.Count; i++)
                        {

                            compañias = item.companies[i];

                            Dictionary<char, int> frequencies2 = CalculateFrequencies(compañias);
                            var huffmanTree2 = new HuffmanTree(frequencies2.ToList());
                            Dictionary<char, string> huffmanTable2 = HuffmanEncoding.BuildHuffmanTable(huffmanTree2.Root);
                            encodedText2 = HuffmanEncoding.Encode(compañias, huffmanTable2);
                            decodedText2 = HuffmanEncoding.Decode(encodedText2, huffmanTree2.Root);
                            companiesaux.Add(decodedText2);
                        }

                        Console.WriteLine($"Nombre: {item.name}");
                        Console.WriteLine($"DPI: {decodedText}");
                        Console.WriteLine($"Fecha de Nacimiento: {item.datebirth}");
                        Console.WriteLine($"Dirección: {item.address}");
                        Console.WriteLine("Compañías:");
                        foreach (var compañia in companiesaux)
                        {
                            Console.WriteLine($"- {compañia}");
                        }
                        Console.WriteLine("-------------------------------");
                    }                          
            }
            else
            {
                Console.WriteLine("No se encontro ninguna persona");
            }
        }


        private string EncodeData(string data)
        {
            Dictionary<char, int> frequencies = CalculateFrequencies(data);
            var huffmanTree = new HuffmanTree(frequencies.ToList());
            Dictionary<char, string> huffmanTable = HuffmanEncoding.BuildHuffmanTable(huffmanTree.Root);
            return HuffmanEncoding.Encode(data, huffmanTable);
        }

        private string DecodeData(string encodedData, HuffmanTree huffmanTree)
        {
           
            return HuffmanEncoding.Decode(encodedData, huffmanTree.Root);
        }

        private HuffmanTree GetHuffmanTree(string data)
        {
            Dictionary<char, int> frequencies = CalculateFrequencies(data);
            return new HuffmanTree(frequencies.ToList());
        }

        static Dictionary<char, int> CalculateFrequencies(string text)
        {
            var frequencies = new Dictionary<char, int>();

            foreach (char c in text)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c]++;
                }
                else
                {
                    frequencies[c] = 1;
                }
            }
            return frequencies;
        }
    }
}
