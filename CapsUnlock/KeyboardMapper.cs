using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinputManager;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;
using System.Globalization;

namespace CapsUnlock
{

    //public enum KeyboardMapperActions
    //{
    //    SwitchLayout,
    //    MuteSound,
    //    Escape,
    //    CtrlS,
    //    Tab,
    //    CapsLock
    //}

    class KeyboardMapper
    {

        public bool SettingsDisabled { get; set; } = false;
        public bool SettingsDisableInFullscreen { get; set; } = false;
        public bool SettingsEnableSound { get; set; } = false;
        public bool SettingsShiftCapsLock { get; set; } = false;
        public bool SettingsHoldForCapsLock { get; set; } = false;

        //public KeyboardMapperActions CurrentAction { get; set; } = KeyboardMapperActions.CapsLock;

        public string SelectedAction { get; set; } = "actionAltShift";

        KeyboardHook keyboardHook = new KeyboardHook();
        InputSimulator inputSimulator = new InputSimulator();

        //Stopwatch stopWatch = new Stopwatch();
        private VirtualKeyCode? KeyCurrentlyDown = null;
        private bool ShiftIsDown = false;

        ushort SoundFrequency = 800;
        ushort SoundDuration = 20;

        public KeyboardMapper()
        {
            keyboardHook.OnKeyboardEvent += KeyboardHook_OnKeyboardEvent;
            keyboardHook.Install();
        }

        ~KeyboardMapper()
        {
            keyboardHook.Uninstall();
        }


        private bool KeyboardHook_OnKeyboardEvent(uint key, BaseHook.KeyState keyState)
        {
            switch (key)
            {
                case 16:
                case 160:
                case 161:
                    ShiftIsDown = (keyState == BaseHook.KeyState.Keydown);
                    break;

            }

            // pass the original keypresses on to Windows, if...
            if (key != 20) return false;
            if (SettingsDisabled == true) return false;
            if (CheckFullscreen.IsForegroundFullScreen() && SettingsDisableInFullscreen) return false;
            if (SettingsShiftCapsLock && ShiftIsDown) return false;

            if (keyState == BaseHook.KeyState.Keydown)
            {
                CapsLockPressed();

            }
            else if (keyState == BaseHook.KeyState.Keyup)
            {
                CapsLockReleased();
            }

            // if it was CapsLock, tell the hooker to consume the keypress
            return true;
        }

        private void CapsLockPressed()
        {
            switch (SelectedAction)
            {
                case "ActionCtrlShift.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.LSHIFT);
                    break;
                case "ActionAltShift.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LMENU, VirtualKeyCode.LSHIFT);
                    break;
                case "ActionWinSpace.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.SPACE);
                    break;
                case "ActionCtrl.":
                    KeyCurrentlyDown = VirtualKeyCode.LCONTROL;
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    break;
                case "ActionTab.":
                    KeyCurrentlyDown = VirtualKeyCode.TAB;
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.TAB);
                    break;
                case "ActionEscape.":
                    KeyCurrentlyDown = VirtualKeyCode.ESCAPE;
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.ESCAPE);
                    break;
                case "ActionWinCtrlEnter.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(
                        new List<VirtualKeyCode> { VirtualKeyCode.LWIN, VirtualKeyCode.LCONTROL },
                        VirtualKeyCode.RETURN
                        );
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.RETURN);
                    break;
                case "ActionWinPlus.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.OEM_PLUS);
                    break;
                case "ActionCtrlS.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S);
                    break;
                case "ActionWinD.":
                    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);
                    break;
                case "ActionWinComma.":
                    KeyCurrentlyDown = VirtualKeyCode.LWIN;
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LWIN);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.OEM_COMMA);
                    break;
                case "ActionMuteSpeaker.":
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
                    break;
                case "ActionMuteMic.":
                    // todo
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BROWSER_HOME);
                    break;
                case "ActionMuteBoth.":
                    // todo
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BROWSER_HOME);
                    break;
                case "ActionCalculator.":
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LAUNCH_APP2);
                    break;
                case "ActionMousePrecise.":
                    // todo
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BROWSER_HOME);
                    break;
                case "actionCapsLockPlus.":
                    // todo additional features
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CAPITAL);
                    break;



            }

            if (SettingsEnableSound)
            {
                //int index = InputLanguage.InstalledInputLanguages.IndexOf()
                SoundGenerator.PlayBeep(SoundFrequency, SoundDuration);
                // todo: obtain the global language index and produce sound with its freq
                //if (SoundFrequency>1000)
                //{
                //    SoundFrequency = 800;
                //} else
                //{
                //    SoundFrequency = 1200;
                //}
            }

            if (SettingsHoldForCapsLock)
            {
                //if (holdFlag == 0)
                //{
                //    stopWatch.Start();
                //    holdFlag = 1;
                //}

                //while (stopWatch.ElapsedMilliseconds <= 600)
                //{

                //}

                //if (stopWatch.ElapsedMilliseconds >= 500 && holdFlag == 1)
                //{
                //    stopWatch.Stop();
                //    stopWatch.Reset();
                //    holdFlag = 2;
                //    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CAPITAL);
                //    return;
                //}
            }


        }

        private void CapsLockReleased()
        {

            if (KeyCurrentlyDown != null)
            {
                inputSimulator.Keyboard.KeyUp((VirtualKeyCode)KeyCurrentlyDown);
                KeyCurrentlyDown = null;
            }
        }




    } //class
} // namespace
