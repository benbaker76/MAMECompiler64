# MAME Compiler 64

## Description

This is a front end to make compiling MAME/MAME64 and apply diff patches as easy as possible.

Requires the .NET 2.0 Runtime

## Screenshots

![](/images/mc01.png)

![](/images/mc02.png)

![](/images/mc03.png)

![](/images/mc04.png)

![](/images/mc05.png)

## Instructions

How to compile **MAME** with the Hiscore patch:

1. Click the **Downloads** tab
2. If this is your first time running MAME Compiler put a tick next to **Build Tools**
3. Put a tick next to **MAME Source** and the **Hiscore Diff Patch**
4. Click the **Download Selected** button and wait for the files to finish downloading and extracting
5. Click the **Diff Patch** tab
6. Click the magnifying glass icon to browse and select the Hiscore diff patch (Eg. hi_163.txt)
7. Click the **Apply Patch** button to apply the Hiscore diff patch
8. Click the **GO!** button to compile

To compile **MAMEUI**:

1. Click the **Downloads** tab
2. Put a tick next to **MAMEUI Source**
3. Click the **Download Selected** button and wait for the file to finish downloading and extracting
4. Click the **Build Options** tab
5. Select **WinUI** from the **OSD** drop down list
6. Click the **GO!** button to compile

**NOTE**: To compile an Arcade only build of MAME go to the **Build Options** tab and select **Arcade** from the **Sub Target** drop down list.

## Older Versions of MAME

For MAME versions earlier than **0143u1** you cannot use **MAME Compiler v1.24** to compile MAME you must use **v1.17**. You can download **MAME Compiler v1.17** from [here](/download/mame/MC64Setup117.exe).

[http://mamedev.org/oldrel.html](http://mamedev.org/oldrel.html)

## U Release Diff Patches

You can get the Official MAME diff patches for u releases from [http://mamedev.org/updates/](http://mamedev.org/updates/)

## HiScore Diff Patches

Newer hiscore diff's are available for download from [http://forum.arcadecontrols.com/index.php?topic=64298.0](http://forum.arcadecontrols.com/index.php?topic=64298.0)

## Applying Diff Patches

To apply them do in this order:

1. Apply xxxu1.diff
2. Apply xxxu2.diff
3. Apply xxxu3.diff
4. Apply hi_xxxu3.diff
5. Compile

## Version History

- 4-8-16 - v2.0.176 - Added support for EOL Conversion and Prefix Strip options for patching
- 3-6-16 - v2.0.173 - Added support for creating diff's
- 26-2-16 - v2.0.171 - Support for MAME 0171+
- 5-1-16 - v2.0.169 - Support for MAME 0169+
- 3-1-16 - v2.0.168 - Support for new build tools
- 31-8-15 - v2.0.165 - Update for MAMEUI 0165+ download & extraction
- 24-8-15 - v2.0.164 - Improved downloading and extracting
- 27-6-15 - v2.0.163 - Added support for STRIP_SYMBOLS
- 1-6-15 - v2.0.162 - Support for "arcade" and "mess" sub targets
- 3-5-15 - v2.0.161 - Update for new build tools and MAME 0161+
- 31-1-15 - v2.0.1 - Fix for MAME 0158+
- 18-11-14 - v2.0 - UI Update, new options added, auto-download source and patches
- 12-11-14 - v1.3.2 - Fix for compiling MAME 0154+
- 01-05-14 - v1.3.1 - Added fix for output in MAMEUI 0153+ and FASTDEBUG option
- 27-12-12 - v1.3 - Many new compiler options added
- 13-12-12 - v1.24 - Updated for new toolchain, added Force Direct Input option
- 21-08-11 - v1.23 - Updated for new toolchain
- 29-05-11 - v1.22 - Updated for new toolchain
- 15-05-11 - v1.21 - MAMEUI compile no longer uses MAMEUI.mak
- 20-09-10 - v1.20 - Updated to latest MinGW
- 05-05-10 - v1.19 - Added "Optimize for: None" option (should solve png2bdc.exe crash issue)
- 08-02-10 - v1.18 - Added support for 64-bit MinGW MAME 0.136u1+
- 04-10-08 - v1.17 - Added "Disable Warnings as Error" to use when compiling 127u6
- 08-07-08 - v1.16 - Added support for compiling MAME 0126
- 25-06-08 - v1.15 - Added support for compiling MAME 0105
- 01-04-08 - v1.14 - Added AMD64 and AthlonXP optimization options
- 01-04-08 - v1.13 - Added more error handling, updated patches
- 22-02-08 - v1.12 - Added support for compiling MAMEUI
- 14-02-08 - v1.11 - Added new architecture flags
- 13-02-08 - v1.1 - Fixed compiling for seattle.c games
- 11-02-08 - v1.0 - Adding support for compiling MAME64
- 05-12-07 - v0.5 - Fixed compiling for 0.116
- 21-10-07 - v0.4 - Fixed make error for 0.120+
- 07-10-07 - v0.3 - Fixed compiling for < 0.118u5
- 01-10-07 - v0.2 - Added color coded output, timers and MKChamp's current diff releases.
- 30-09-07 - v0.1 - First Release

## Thanks

Thanks to MKChamp for the hiscore diff's.
