using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ListofProcessofTreeParentandChild
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayProcessTree();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void DisplayProcessTree()
        {
            Process[] processes = Process.GetProcesses();
            TreeNode rootNode = new TreeNode("Started Tasks");
            foreach (Process process in processes)
            {
                if (process.Id == process.SessionId)
                {
                    TreeNode parentNode = new TreeNode($"{process.ProcessName} (ID: {process.Id})");
                    parentNode.Tag = process.Id;
                    rootNode.Nodes.Add(parentNode);
                    AddChildProcesses(process, parentNode);
                }
            }
            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();
        }

        private void AddChildProcesses(Process parentProcess, TreeNode parentNode)
        {
            Process[] childProcesses = Process.GetProcesses();
            foreach (Process process in childProcesses)
            {
                if (process.Id != parentProcess.Id && process.Id != process.SessionId && process.Id != 0 && process.SessionId != 0 && process.SessionId == parentProcess.Id)
                {
                    TreeNode childNode = new TreeNode($"{process.ProcessName} (ID: {process.Id})");
                    childNode.Tag = process.Id;
                    parentNode.Nodes.Add(childNode);
                    AddChildProcesses(process, childNode);
                }
            }
        }
    }
}
