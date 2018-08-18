# CapsUnlocked
<img align="right" src="https://raw.githubusercontent.com/reclaimed/capsunlocked/master/README-files/context-menu.v0.5.png">
A Windows utility that maps the CapsLock to something useful. The main purpose was to give the users uninterrupted experience of switching the input language on the both Windows and Mac computers (differences between PC and Mac hotkeys is a source of permanent confusion if choose the words carefully).

Written in C# for .NET Framework 4.6.1.

### What does it do
In short, it makes the CapsLock useful again.

The program sits in the System Tray and waits for Caps Lock and once it happened, CapsUnlocked performs one of the pre-programmed actions (so no need to mess with command lines, batch files and like this).

To change the action, you just right-click the System Tray icon and select the action that suits your intentions. No need to reload, no need to reboot the whole PC, nothing.

### Actions (i.e. what the CapsLock can be mapped to)
* Switch Input Language:
  * Win+Space (a newly promoted standard in Windows 8 and up)
  * Alt+Shift (a default for most of the classic Windows installations)
  * Ctrl+Shift
* Single key mappings:
  * Escape (VIM users love it)
  * Left CTRL (Emacs users are in love with it)
  * TAB
* Accessibility:
  * Win+Ctrl+Enter (Windows Narrator)
  * Win+Plus (Windows Magnifier)
* Miscellaneous:
  * Ctrl+S (save; people with OCD love this mapping!)
  * Win+D (minimize all windows and show the desktop)
  * Win+Comma (peek at the desktop)
  * Mute/Unmute speakers (great for gaming at the workplace)

### Settings 
* Start with Windows (no need to look where is that StartUp folder sitting in Windows 10)
* Disabled (if you want to give a chance to good old Caps Lock)
* Disabled in Fullscreen (for the genuine gaming experience)
* Make noises - meep, meep, meep
* Shift+CapsLock as CapsLock (for those who love to shout sometimes)

### Download and install
There's no installer at the moment, so just [download](https://github.com/reclaimed/CapsUnlocked/releases/download/0.5/CapsUnlockedPreview.zip) it, unpack and run.

It's not a portable, though, as the settings are stored in Windows Registry. Will fix it in the next releases. On the other hand, no much settings for now.

