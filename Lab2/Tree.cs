using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    
   public class TreeNode
   {
        public Persona Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Persona data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public TreeNode()
        {
        }

        private TreeNode root; // Raíz del árbol

        //Insertar Elementos
        public void Insert(Persona persona)
        {
            root = InsertRec(root, persona);
        }

        private TreeNode InsertRec(TreeNode root, Persona persona)
        {
            if (root == null)
            {
                root = new TreeNode(persona);
                return root;
            }

            if (persona.name.CompareTo(root.Data.name) < 0)
            {
                root.Left = InsertRec(root.Left, persona);
            }
            else if (persona.name.CompareTo(root.Data.name) > 0)
            {
                root.Right = InsertRec(root.Right, persona);
            }

            return root;
        }

        //Actualizar 
        public void ActualizarPersona(Persona persona)
        {
            root = ActualizarRec(root, persona);
        }

        private TreeNode ActualizarRec(TreeNode root, Persona persona)
        {
            if (root == null)
            {
                // La persona no se encontró en el árbol, puedes manejarlo según tus requisitos.
                return null;
            }

            if (persona.name.CompareTo(root.Data.name) < 0)
            {
                root.Left = ActualizarRec(root.Left, persona);
            }
            else if (persona.name.CompareTo(root.Data.name) > 0)
            {
                root.Right = ActualizarRec(root.Right, persona);
            }
            else
            {
                // Persona encontrada, realizar la actualización aquí.
                root.Data = persona;
            }

            return root;
        }

        //Eliminar 
        public void EliminarPersona(string nombre, string dpi)
        {
            root = EliminarRec(root, nombre, dpi);
        }

        private TreeNode EliminarRec(TreeNode root, string nombre, string dpi)
        {
            if (root == null)
            {
                return root;
            }

            if (nombre.CompareTo(root.Data.name) < 0)
            {
                root.Left = EliminarRec(root.Left, nombre, dpi);
            }
            else if (nombre.CompareTo(root.Data.name) > 0)
            {
                root.Right = EliminarRec(root.Right, nombre, dpi);
            }
            else
            {
                // Encontramos el nodo a eliminar
                if (root.Left == null)
                {
                    return root.Right;
                }
                else if (root.Right == null)
                {
                    return root.Left;
                }

                // Nodo con dos hijos: obtenemos el sucesor inorden (nodo más pequeño en el subárbol derecho)
                root.Data = FindMinValue(root.Right);

                // Eliminamos el sucesor inorden
                root.Right = EliminarRec(root.Right, root.Data.name, root.Data.dpi);
            }

            return root;
        }

        private Persona FindMinValue(TreeNode node)
        {
            Persona minValue = node.Data;
            while (node.Left != null)
            {
                minValue = node.Left.Data;
                node = node.Left;
            }
            return minValue;
        }

        //Busqueda
        public List<Persona> BuscarPersonasPorNombre(string nombre)
        {
            List<Persona> personasEncontradas = new List<Persona>();
            BuscarPorNombreRec(root, nombre, personasEncontradas);
            return personasEncontradas;
        }

        private void BuscarPorNombreRec(TreeNode node, string nombre, List<Persona> result)
        {
            if (node == null)
            {
                return;
            }

            // Realiza un recorrido inorden para buscar por nombre
            BuscarPorNombreRec(node.Left, nombre, result);
            if (node.Data.name == nombre)
            {
                result.Add(node.Data);
            }
            BuscarPorNombreRec(node.Right, nombre, result);
        }


        //mostrar
        public void MostrarArbol()
        {
            MostrarArbolRec(root);
        }

        private void MostrarArbolRec(TreeNode node)
        {
            if (node != null)
            {
                MostrarArbolRec(node.Left);
                Console.WriteLine($"Nombre: {node.Data.name}");
                Console.WriteLine($"DPI: {node.Data.dpi}");
                Console.WriteLine($"Fecha de Nacimiento: {node.Data.datebirth}");
                Console.WriteLine($"Dirección: {node.Data.address}");
                Console.WriteLine("Compañías:");
                foreach (var compañia in node.Data.companies)
                {
                    Console.WriteLine($"- {compañia}");
                }
                Console.WriteLine("-------------------------------");
                MostrarArbolRec(node.Right);
            }
        }



    }
}
