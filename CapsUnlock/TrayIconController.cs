using System;
using System.Diagnostics;
using System.Windows.Forms;
using CapsUnlock.Properties;
using System.Drawing;
using Microsoft.Win32;
using AutostartManagement;
using System.Collections.Generic;

namespace CapsUnlock
{
    /// <summary>
    /// 
    /// </summary>
    class TrayIconController : IDisposable
    {
        private NotifyIcon TrayIcon;
        private KeyboardMapper Mapper;
        private AutostartManager AutostartHelper;

        public TrayIconController()
        {
            // Instantiate the NotifyIcon object.
            TrayIcon = new NotifyIcon();
            Mapper = new KeyboardMapper();

            // autostart manager
            bool registerShortcutForAllUser = false;
            AutostartHelper = new AutostartManager(Application.ProductName, Application.ExecutablePath, registerShortcutForAllUser);
        }

        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks
            //ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            TrayIcon.Icon = Resources.enabled;
            TrayIcon.Text = Application.ProductName + " ver. " + Application.ProductVersion + "\r\nRight click me!";
            TrayIcon.Visible = true;

            // Attach a context menu.
            //ni.ContextMenuStrip = new ContextMenus().Create();
            TrayIcon.ContextMenuStrip = PopulateMenu(new ContextMenuStrip());

            LoadSettings();

        }

        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately
            TrayIcon.Dispose();
        }

        private void LoadSettings()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + Application.ProductName);
            string configVersion = key.GetValue("Version").ToString();
            if (key != null)
            {
                if (configVersion != Application.ProductVersion)
                {
                    // todo: something to do if the version is not the same
                }

                Mapper.SettingsDisableInFullscreen = bool.Parse(key.GetValue("SettingsDisableInFullscreen").ToString());
                Mapper.SettingsEnableSound = bool.Parse(key.GetValue("SettingsEnableSound").ToString());
                Mapper.SettingsShiftCapsLock = bool.Parse(key.GetValue("SettingsShiftCapsLock").ToString());
                Mapper.SelectedAction = key.GetValue("SelectedAction").ToString();
            }
            else
            {
                Mapper.SettingsDisableInFullscreen = false;
                Mapper.SettingsEnableSound = false;
                Mapper.SettingsShiftCapsLock = false;
                Mapper.SelectedAction = Actions.DefaultAction;
                SaveSettingsAndUpdateMenu();
            }

            DisplaySettingsInMenu();
        }

        private void SaveSettingsAndUpdateMenu()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + Application.ProductName);
            key.SetValue("Version", Application.ProductVersion);
            key.SetValue("SettingsDisableInFullscreen", Mapper.SettingsDisableInFullscreen);
            key.SetValue("SettingsEnableSound", Mapper.SettingsEnableSound);
            key.SetValue("SettingsShiftCapsLock", Mapper.SettingsShiftCapsLock);
            key.SetValue("SelectedAction", Mapper.SelectedAction);
            key.Close();

            DisplaySettingsInMenu();
        }

        private void DisplaySettingsInMenu()
        {
            // update the menu
            SetMenuItemChecked(TrayIcon.ContextMenuStrip, "SettingsAutostart", AutostartHelper.IsAutostartEnabled());
            SetMenuItemChecked(TrayIcon.ContextMenuStrip, "SettingsDisabled", Mapper.SettingsDisabled);
            SetMenuItemChecked(TrayIcon.ContextMenuStrip, "SettingsDisableInFullscreen", Mapper.SettingsDisableInFullscreen);
            SetMenuItemChecked(TrayIcon.ContextMenuStrip, "SettingsEnableSound", Mapper.SettingsEnableSound);
            SetMenuItemChecked(TrayIcon.ContextMenuStrip, "SettingsShiftCapsLock", Mapper.SettingsShiftCapsLock);
            MenuSelectedActionCheck(Mapper.SelectedAction);
        }


        private void SetMenuItemChecked(ContextMenuStrip menu, string itemName, bool check)
        {
            // well looks weird but in the normal case there shouldn't be manu items in search results
            foreach (ToolStripItem i in menu.Items.Find(itemName, true))
            {
                if (i.Name == itemName)
                {
                    ToolStripMenuItem menuitem = (ToolStripMenuItem)i;
                    menuitem.Checked = check;
                }
            }
        }

        // todo pass the menu instance to work with
        private void MenuSelectedActionCheck(string actionName)
        {
            // look through all the menu items to find the one with given name.
            foreach (ToolStripItem i in TrayIcon.ContextMenuStrip.Items)
            {
                if (i.Name == actionName)
                {
                    ToolStripMenuItem mi = (ToolStripMenuItem)i;
                    mi.Checked = true;
                }
                else if (i.Name.StartsWith("Action"))
                {
                    ToolStripMenuItem mi = (ToolStripMenuItem)i;
                    mi.Checked = false;
                }
            }
        }


        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // Handle mouse button clicks
            //if (e.Button == MouseButtons.Left) {}

        }

        private ContextMenuStrip PopulateMenu(ContextMenuStrip menu)
        {
            // Add the default menu options.
            //ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;

            // About
            item = new ToolStripMenuItem();
            item.Font = new Font(item.Font, item.Font.Style | FontStyle.Bold);
            item.Enabled = false;
            item.Text = Application.ProductName + " ver. " + Application.ProductVersion;
            //item.Image = Resources.application;
            menu.Items.Add(item);

            // Open Github page
            item = new ToolStripMenuItem();
            item.Text = "     Webpage";
            item.Click += new EventHandler(OpenGithub_Click);
            menu.Items.Add(item);

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Group title
            item = new ToolStripMenuItem();
            item.Enabled = false;
            item.Text = "Settings:";
            //item.Image = Resources.gears;
            menu.Items.Add(item);

            menu.Items.Add(CreateSettingsItem("Start with Windows", "SettingsAutostart"));
            menu.Items.Add(CreateSettingsItem("Disabled until restart", "SettingsDisabled"));
            menu.Items.Add(CreateSettingsItem("Disable in Fullscreen", "SettingsDisableInFullscreen"));
            menu.Items.Add(CreateSettingsItem("Make noises", "SettingsEnableSound"));
            menu.Items.Add(CreateSettingsItem("Shift+CapsLock as CapsLock", "SettingsShiftCapsLock"));

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Add actions
            AddActionsToMenu(menu, Actions.Collection);

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Exit
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            //item.Image = Resources.Exit;
            menu.Items.Add(item);

            return menu;
        }

        private ToolStripMenuItem CreateSettingsItem(string title, string name)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = "    " + title;
            item.Name = name;
            item.Click += new EventHandler(Settings_Click);
            return item;
        }

        private ContextMenuStrip AddActionsToMenu(ContextMenuStrip menu, List<ActionProperties> actions)
        {
            foreach (ActionProperties action in Actions.Collection)
            {
                switch (action.Type)
                {
                    case "separator":
                        // separator
                        menu.Items.Add(new ToolStripSeparator());
                        break;
                    case "title":
                        // group title
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Enabled = false;
                        item.Text = action.Title + ":";
                        item.Name = action.Name;
                        menu.Items.Add(item);
                        break;
                    case "item":
                        item = new ToolStripMenuItem();
                        item.Text = "    " + action.Title;
                        item.Name = action.Name;
                        item.Click += new EventHandler(Action_Click);
                        menu.Items.Add(item);
                        break;

                }

            }

            return menu;
        }

        // commands ================================================================================

        // for test purposes
        void Dummy_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", null);
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void OpenGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/reclaimed/capsunlocked");
        }


        // settings ================================================================================

        void Settings_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
            MenuSelectedActionCheck(item.Name);

            switch (menuitem.Name)
            {
                case "SettingsAutostart":
                    if (AutostartHelper.IsAutostartEnabled() == true)
                    {
                        AutostartHelper.DisableAutostart();
                    }
                    else
                    {
                        AutostartHelper.EnableAutostart();
                    }
                    SaveSettingsAndUpdateMenu();
                    break;
                case "SettingsDisabled":
                    Mapper.SettingsDisabled = !Mapper.SettingsDisabled;
                    SaveSettingsAndUpdateMenu();
                    break;
                case "SettingsDisableInFullscreen":
                    Mapper.SettingsDisableInFullscreen = !Mapper.SettingsDisableInFullscreen;
                    SaveSettingsAndUpdateMenu();
                    break;
                case "SettingsEnableSound":
                    Mapper.SettingsEnableSound = !Mapper.SettingsEnableSound;
                    SaveSettingsAndUpdateMenu();
                    break;
                case "SettingsShiftCapsLock":
                    Mapper.SettingsShiftCapsLock = !Mapper.SettingsShiftCapsLock;
                    SaveSettingsAndUpdateMenu();
                    break;
            }
        }

        // actions ==============================================================================

        void Action_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;

            //ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
            // mark the selected action in menu with a check
            //MenuSelectedActionCheck(item.Name);

            // tell Mapper that the action was changed
            Mapper.SelectedAction = item.Name;

            // save settings
            SaveSettingsAndUpdateMenu();
        }


    }
}