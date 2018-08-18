using System;
using System.Diagnostics;
using System.Windows.Forms;
using CapsUnlock.Properties;
using System.Drawing;

namespace CapsUnlock
{
	/// <summary>
	/// 
	/// </summary>
	class ContextMenus
	{
		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>ContextMenuStrip</returns>
		public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;

            // Start with Windows
            item = new ToolStripMenuItem();
            item.Text = "Start with Windows";
            //item.Click += new EventHandler(Autostart_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Open Github page
            item = new ToolStripMenuItem();
            item.Text = "Open Github page";
            // item.Click += new EventHandler(Github_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Enabled/disabled
            item = new ToolStripMenuItem();
            item.Font = new Font(item.Font, item.Font.Style | FontStyle.Bold);
            item.CheckOnClick = true;
            item.Text = "Deactivate";
            // item.Click += new EventHandler(Github_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Hold for Caps Lock
            item = new ToolStripMenuItem();
            item.CheckOnClick = true;
            item.Text = "Hold for Caps Lock";
            // item.Click += new EventHandler(Github_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);



            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Keyboard Layout Switcher
            item = new ToolStripMenuItem();
            item.Text = "Switch Keyboard Layout";
            //item.Click += new EventHandler(Layout_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Mute/Unmute Sound
            item = new ToolStripMenuItem();
            item.Text = "Mute/Unmute Sound";
            //item.Click += new EventHandler(Mute_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Escape
            item = new ToolStripMenuItem();
            item.Text = "Emulate Escape";
            //item.Click += new EventHandler(Escape_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Ctrl-S
            item = new ToolStripMenuItem();
            item.Text = "Emulate Ctrl-S";
            //item.Click += new EventHandler(CtrlS_Click);
            item.Click += new EventHandler(Dummy_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Separator.
            menu.Items.Add(new ToolStripSeparator());

            // Exit.
            item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
            //item.Image = Resources.Exit;
			menu.Items.Add(item);

			return menu;
		}

        void Dummy_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", null);
        }


        ///// <summary>
        ///// Handles the Click event of the Explorer control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //void Explorer_Click(object sender, EventArgs e)
        //{
        //	Process.Start("explorer", null);
        //}

        ///// <summary>
        ///// Handles the Click event of the About control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //void About_Click(object sender, EventArgs e)
        //{
        //	if (!isAboutLoaded)
        //	{
        //		isAboutLoaded = true;
        //		new AboutBox().ShowDialog();
        //		isAboutLoaded = false;
        //	}
        //}

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
			Application.Exit();
		}
	}
}