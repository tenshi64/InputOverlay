using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;

namespace InputOverlay
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Int32 i);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        IDictionary<string, Panel> keys = new Dictionary<string, Panel>();

        private void Overlay_Load(object sender, EventArgs e)
        {
            FileManagement.CreateDirectory();
            FileManagement.CreateFile();
            ReadValues();
            this.BackColor = Color.CadetBlue;
            this.TransparencyKey = Color.CadetBlue;

            foreach(Panel key in Controls.OfType<Panel>())
            {
                keys[key.Name] = key;
            }
            Task.Run(GetKey);
        }

        public void ChangeLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public void ReadValues()
        {
            this.Location = new Point(int.Parse(FileManagement.GetValue("X:")), int.Parse(FileManagement.GetValue("Y:")));
            this.TopMost = Convert.ToBoolean(FileManagement.GetValue("TopMost:"));
        }

        private void GetKey()
        {
            while (true)
            {
                Thread.Sleep(5);
                for(int i = 0; i <= 300; i++)
                {
                    if(GetAsyncKeyState(i))
                    {
                        var converter = new KeysConverter();
                        string key = converter.ConvertToString(i);
                        if (keys.ContainsKey("btn" + key))
                        {
                            UpdateUI.KeyPressed(keys, key, null);
                        }
                        else
                        {
                            if(keys.ContainsKey("btn" + key + 1) && keys.ContainsKey("btn" + key + 2))
                            {
                                UpdateUI.KeyPressed(keys, key, 1);
                                UpdateUI.KeyPressed(keys, key, 2);
                            }


                            if (GetAsyncKeyState((int)Keys.Oem2))
                            {
                                UpdateUI.KeyPressed(keys, "OemQuestion", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem3))
                            {
                                UpdateUI.KeyPressed(keys, "Oemtilde", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem4))
                            {
                                UpdateUI.KeyPressed(keys, "OemOpenBrackets", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem6))
                            {
                                UpdateUI.KeyPressed(keys, "OemCloseBrackets", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem5))
                            {
                                UpdateUI.KeyPressed(keys, "OemPipe", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem1))
                            {
                                UpdateUI.KeyPressed(keys, "OemSemicolon", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem7))
                            {
                                UpdateUI.KeyPressed(keys, "OemQuotes", null);
                            }
                        }
                    }
                    else
                    {
                        var converter = new KeysConverter();
                        string key = converter.ConvertToString(i);
                        if (keys.ContainsKey("btn" + key))
                        {
                            UpdateUI.KeyUp(keys, key, null);
                        }
                        else
                        {
                            if (keys.ContainsKey("btn" + key + 1) && keys.ContainsKey("btn" + key + 2))
                            {
                                UpdateUI.KeyUp(keys, key, 1);
                                UpdateUI.KeyUp(keys, key, 2);
                            }


                            if (GetAsyncKeyState((int)Keys.Oem2))
                            {
                                UpdateUI.KeyUp(keys, "OemQuestion", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem3))
                            {
                                UpdateUI.KeyUp(keys, "Oemtilde", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem4))
                            {
                                UpdateUI.KeyUp(keys, "OemOpenBrackets", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem6))
                            {
                                UpdateUI.KeyUp(keys, "OemCloseBrackets", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem5))
                            {
                                UpdateUI.KeyUp(keys, "OemPipe", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem1))
                            {
                                UpdateUI.KeyUp(keys, "OemSemicolon", null);
                            }
                            if (GetAsyncKeyState((int)Keys.Oem7))
                            {
                                UpdateUI.KeyUp(keys, "OemQuotes", null);
                            }
                        }
                    }
                }
            }
        }
    }
}
