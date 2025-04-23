using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Shapes;
using System.Windows;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace CoordCalc.ClassLib
{
    public class CoordSystemsTree
    {
        private Dictionary<string,CoordSystem> _nodes;
        public CoordSystemsTree() 
        {
            _nodes = new Dictionary<string, CoordSystem>();
            CoordSystem globalSystem = CoordSystem.GetRootCoordSystem();
            AddNode(globalSystem);
        }

        public Collection<CoordSystem> Nodes
        {
            get { return new Collection<CoordSystem>(_nodes.Values.ToList()); }
        }

        public Collection<string> NodesNames
        {
            get { return new Collection<string>(_nodes.Keys.ToList()); }
        }

        public CoordSystemsTree(string filepath) : this()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string? line, line2;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        string name = parts[0].Trim();
                        string parent = parts[1].Trim();
                        CoordSystem parentSystem = _nodes[parent];

                        line2 = reader.ReadLine();
                        if (line2 == null)
                        {
                            MessageBox.Show("Reading second line of coord system from file failed", "Warning",
                                 MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }

                        Matrix4x4 matrix = StringToMatrix(line2);

                        CoordSystem new_system = new CoordSystem(matrix: matrix, name: name, parent: parentSystem);
                        AddNode(new_system);
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error reading file occured:\n" + e.Message,"Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected error ocured while reading this file:\n" + filepath + "\n" + e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static Matrix4x4 StringToMatrix(string str)
        {
            str = str.TrimStart('[').TrimEnd(']');
            string[] rows = str.Split(new string[] { "],[" }, StringSplitOptions.None);

            float[,] matrixArray = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                string[] values = rows[i].Split(',');
                for (int j = 0; j < 4; j++)
                {
                    matrixArray[i, j] = float.Parse(values[j].Trim(), CultureInfo.InvariantCulture);
                }
            }

            Matrix4x4 matrix = new Matrix4x4(
                matrixArray[0, 0], matrixArray[0, 1], matrixArray[0, 2], matrixArray[0, 3],
                matrixArray[1, 0], matrixArray[1, 1], matrixArray[1, 2], matrixArray[1, 3],
                matrixArray[2, 0], matrixArray[2, 1], matrixArray[2, 2], matrixArray[2, 3],
                matrixArray[3, 0], matrixArray[3, 1], matrixArray[3, 2], matrixArray[3, 3]
            );
            return matrix;
        }


        public static string MatrixToString(Matrix4x4 matrix)
        {
            float[,] array = new float[4, 4]
            {
        { matrix.M11, matrix.M12, matrix.M13, matrix.M14 },
        { matrix.M21, matrix.M22, matrix.M23, matrix.M24 },
        { matrix.M31, matrix.M32, matrix.M33, matrix.M34 },
        { matrix.M41, matrix.M42, matrix.M43, matrix.M44 }
            };

            string[] rows = new string[4];
            for (int i = 0; i < 4; i++)
            {
                string[] values = new string[4];
                for (int j = 0; j < 4; j++)
                {
                    values[j] = array[i, j].ToString(CultureInfo.InvariantCulture);
                }
                rows[i] = "[" + string.Join(",", values) + "]";
            }

            return "[" + string.Join(",", rows) + "]";
        }


        public void AddNode(CoordSystem coordSystem)
        {
            string name = coordSystem.Name;
            if (_nodes.ContainsKey(name))
            {
                MessageBox.Show($"Coord system with name {name} already exists, " +
                    $"requested coord system will not be Created", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                _nodes[name] = coordSystem;
            }
        }

        public CoordSystem GetRootSystem()
        {
            return _nodes["0"];
        }

        public void DeleteNodeAndDescendants(CoordSystem coordSystem)
        {
            if (_nodes.ContainsKey(coordSystem.Name))
            {
                _nodes.Remove(coordSystem.Name);
            }

            foreach (var child in coordSystem.Children.ToList())
            {
                DeleteNodeAndDescendants(child);
            }

            if (coordSystem.Parent != null)
            {
                coordSystem.Parent.Children.Remove(coordSystem);
            }

            coordSystem.Children.Clear();
        }

        public int GetNumberOfDescendants(CoordSystem coordSystem)
        {
            int count = 0;
            foreach (var child in coordSystem.Children)
            {
                count++;
                count += GetNumberOfDescendants(child);
            }
            return count;
        }

        public class Transformation
        {
            public Transformation(Matrix4x4 matrix, string from, string to)
            {
                Matrix = matrix;
                From = from;
                To = to;
            }
            public required Matrix4x4 Matrix { get; init; }
            public required string From { get; init; }
            public required string To { get; init; }
        }
    }
}
