using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.IO;

namespace VNN
{
    public partial class Form1 : Form
    {
        //public const string PATH_TO_DATA_FILE = @"D:\PROJECTS\VisualNeuralNetwork\VNN\VNN\data.json";
        public NN Network;
        public FileModel DATA;
        public bool is_learning;
        public Task learning_task;
        PanWebsite Website;
        private bool website_started;
    }
}
