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

        static Matrix4x4 StringToMatrix(string str)
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
    }
}
