# CapsUnlocked ([download](https://github.com/reclaimed/CapsUnlocked/releases))
<img align="right" src="https://raw.githubusercontent.com/reclaimed/capsunlocked/master/README-files/context-menu.png">
A Windows freeware utility for CapsLock remapping.

## Requirements

* Windows 7/8/8.1/10 either x86 or amd64
* .NET Framework 4.6.1
* a keyboard with CapsLock

## Description

_CapsUnlocked_ is a Windows opensource, freeware utility distributed under the MIT license, 
that allows users to configure CapsLock to perform an alternative behaviour.

It was created with IT people and mechanical engineers in mind:
1. It provides an uninterrupted experience of switching of the input languages for users of Windows AND Macs
2. CapsLock is useless for programmers. On the other hand, programmers from non-western countries switch the languages very often (for example, the code in English, but comments and a group chat in Russian)
3. Extra features like mapping to VIM and Emacs hotkeys were added on user demand

To install you need to [download](https://github.com/reclaimed/CapsUnlocked/releases) 
the installer (usually .exe file) or a portable version (distributed as .zip archive file) 
and install or unpack downloaded file. To get it to work, respectively:  
* find it in Start Menu (**Programs->CapsUnlocked->Run CapsUnlocked**) and click to run
* or enter the folder with unpacked archive and open the executable file **CapsUnlocked.exe** 

## Options and actions

When running, all options of CapsUnlocked are available by **right click on the System Tray icon**.

All changes the user have committed will be applied and saved immediately. No need to restart or click "save configuration" or like that.

##### Options
* _Start with Windows_ - if marked, the program will start when the user logs on. Remember to run the program if the path changed.
* _Disabled until restart_ - if marked, the program won't work until the option is unmarked or CapsUnlocked has been restarted.
* _Disable in Fullscreen_ - if marked, the program won't work when the fullscreen video mode is activated. Useful for the games that utilize CapsLock.
* _Make noises_ - produce meeps, clicks, and other sounds every time when you press CapsLock
* _Shif+CapsLock as CapsLock_ - if you still need the original CapsLock functionality, it could be achieved with help of this option. Pressed with SHIFT, CapsLock toggles the CAPITAL mode. In case you need to shout in the Slack.

##### Actions
* Switch Input Language:
  * _Win+Space_ - a newly promoted standard in Windows 8 and up.
  * _Alt+Shift_ - a default for the most of the classic Windows installations.
  * _Ctrl+Shift_ - alternative popular keyboard layout changing hotkeys.
* Single key mappings:
  * _Escape_ - VIM users are crazy for ESC
  * _Control_ - Emacs users love the Left CTRL nobody knows why
  * _Tab_ - nobody likes TAB but let it be
* Accessibility:
  * _Win+Ctrl+Enter_ - Windows Narrator
  * _Win+Plus_ - Windows Magnifier
* Miscellaneous:
  * _Ctrl+S_ - a "save document" hotkey in close reach will save somebody from the carpal tunnel syndrome
  * _Win+D_ - minimize all windows and show the desktop
  * _Win+Comma_ - peek at the desktop
  * _Mute/Unmute speakers_ - great for gaming at the workplace
  * _Deactivate CapsLock_ - filters out CapsLock without a substitute action


## Q&A

Q: What is it for?  
A: For assigning alternative functions on CapsLock key.

Q: What does it do?  
A: The program is filtering out all CapsLock codes from the keyboard data stream and 
calling internal routines instead to allow the user to remap CapsLock to Escape, Control etc.

Q: What do I need to map CapsLock to Escape or language switch or anything?  
A: To download this program and install it. Simple as that.

Q: Are there any alternatives?  
A: There are many freeware utilities to map CapsLock in Windows.    
* [keyla](https://github.com/Ryzhehvost/keyla/wiki/en_Screenshots) - a GUI opensource utility, simple yet powerful.
* [AutoHotKey](http://www.autohotkey.com/) - a free, opensource software automation scripting language. In the context of CapsLock mapping, it's an obvious overkill. I have been using it, but at some point got tired of its bottled power.
* [Punto Switcher](http://www.punto.ru/) - a sophisticated, feature-rich GUI program from the Russian internet company Yandex. I would like it, but it feels overcomplicated and still lacking some key features.
* [lswitch](https://haali.su/winutils/) - a console utility, what means you need to make it auto-start by yourself. Freeware and opensource.
* [Capslang](http://flydom.ru/capslang/) - a console utility.

Q: "CapsUnlocked"? But what does it mean, anyway?   
A: An allusion to Django Unchained.

Q: Are you using it to switch keyboard input languages with CapsLock?  
A: Yeah, on the daily basis.


## Work in progress

* Mouse High Precision mode
* A different sound in different modes and for different languages
* Mute microphone
* Auto-VIM/Emacs mapping when an SSH window is active
