using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace InputOverlay
{
	public static class UpdateUI
	{
		public static void KeyPressed(IDictionary<string, Panel> keys, string key, int? version)
        {
			if(keys.ContainsKey("btn" + key))
			{
                keys["btn" + key].BackColor = Color.FromArgb(66, 65, 65);
            }
			else
			{
                if (version != null)
                {
                    keys["btn" + key + version].BackColor = Color.FromArgb(66, 65, 65);
                }
            }
		}

		public static void KeyUp(IDictionary<string, Panel> keys, string key, int? version)
		{
            if (keys.ContainsKey("btn" + key))
            {
                keys["btn" + key].BackColor = SystemColors.InfoText;
            }
            else
            {
                if (version != null)
                {
                    keys["btn" + key + version].BackColor = SystemColors.InfoText;
                }
            }
		}
	}

}