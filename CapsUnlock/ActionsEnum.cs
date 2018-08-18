using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapsUnlock
{

    public struct ActionProperties
    {
        public string Type;     // "item", "title", "separator"
        public string Title;    // "hello world"
        public string Name;     // "mnuHelloWorld"
        public ActionProperties(string type, string title, string name)
        {
            Type = type;
            Title = title;
            Name = name;
        }
    }

    public static class Actions
    {
        public static string DefaultAction = "ActionAltShift.";

        public static List<ActionProperties> Collection = new List<ActionProperties>
        {
            new ActionProperties("title", "Language Switcher:", ""),
            new ActionProperties("item", "Win+Space", "ActionWinSpace."),
            new ActionProperties("item", "Alt+Shift", "ActionAltShift."),
            new ActionProperties("item", "Ctrl+Shift", "ActionCtrlShift."),

            new ActionProperties("separator", "", ""),
            new ActionProperties("title", "Single key mapping:", ""),
            new ActionProperties("item", "Ctrl", "ActionCtrl."),
            new ActionProperties("item", "Tab", "ActionTab."),
            new ActionProperties("item", "Escape", "ActionEscape."),

            new ActionProperties("separator", "", ""),
            new ActionProperties("title", "Accessibility:", ""),
            new ActionProperties("item", "Win+Ctrl+Enter (narrator)", "ActionWinCtrlEnter."),
            new ActionProperties("item", "Win+Plus (magnifier)", "ActionWinPlus."),

            new ActionProperties("separator", "", ""),
            new ActionProperties("title", "Other:", ""),
            new ActionProperties("item", "Ctrl+S (save)", "ActionCtrlS."),
            new ActionProperties("item", "Win+D (show desktop)", "ActionWinD."),
            new ActionProperties("item", "Win+Comma (peek at desktop", "ActionWinComma."),
            new ActionProperties("item", "Mute/Unmute speakers", "ActionMuteSpeaker."),
            //new ActionProperties("item", "Mute microphone", "ActionMuteMic."),
            //new ActionProperties("item", "Mute both", "ActionMuteBoth."),
            //new ActionProperties("item", "Calculator", "ActionCalculator."),
            //new ActionProperties("item", "Hold for Precise mouse mode", "ActionMousePrecise."),
            //new ActionProperties("item", "CapsLock with benefits", "ActionCapsLockPlus."),

        };
    }

}
