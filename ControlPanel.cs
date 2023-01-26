using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputOverlay
{
    public partial class ControlPanel : Form
    {
        private Overlay overlay = new Overlay();

        public ControlPanel()
        {
            InitializeComponent();
            numX.Value = decimal.Parse(FileManagement.GetValue("X:"));
            numY.Value = decimal.Parse(FileManagement.GetValue("Y:"));
            cbTopMost.Checked = Convert.ToBoolean(FileManagement.GetValue("TopMost:"));
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            FileManagement.SaveTextInFile("X: ", numX.Value.ToString());
            RestartProcess();
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            FileManagement.SaveTextInFile("Y: ", numY.Value.ToString());
            RestartProcess();
        }

        private void cbTopMost_CheckedChanged(object sender, EventArgs e)
        {
            FileManagement.SaveTextInFile("TopMost: ", cbTopMost.Checked.ToString());
            RestartProcess();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            FileManagement.DefaultContent();
            numX.Value = 2;
            numY.Value = 545;
            cbTopMost.Checked = true;
            RestartProcess();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            if(CheckProcess().Contains("on"))
            {
                StartProcess();
            }
            else
            {
                CloseProcess();
            }
            btnRun.Text = CheckProcess();
        }

        public string CheckProcess()
        {
            if (overlay.Visible)
            {
                return "Turn off";
            }
            return "Turn on";
        }

        public void RestartProcess()
        {
            overlay.ReadValues();
        }

        public void StartProcess()
        {
            overlay.Show();
        }

        public void CloseProcess()
        {
            overlay.Hide();
        }
    }
}
