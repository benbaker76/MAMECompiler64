# MAME Compiler 64

### Description

This is a Front End to make compiling MAME/MAME64 and apply diff patches as easy as possible.

Requires the .NET 2.0 Runtime

### Instructions

How to compile MAMEUI 0147 u3 (Skip the MAMEUI steps to compile standard MAME)

1. Download MinGW from http://www.sendspace.com/file/xxb8j0 and extract to C:\MinGW
2. Install MAME Compiler
3. Create a folder C:\MinGW\source\MAME0147u3
4. Download the hiscore hi_147u3.txt from http://forum.arcadecontrols.com/index.php/topic,64298.0.html and copy it into the C:\MinGW\patches (you can rename it to hi_147u2.diff if you like but it doesn't matter)
5. Download the mame0147s.zip from http://mamedev.org/release.html and extract to C:\MinGW\source\MAME0147u3
6. Download the updates from http://mamedev.org/updates.html (0147u1_diff, 0147u2_diff and 0147u3_diff) and extract them to C:\MinGW\patches
7. Download MAMEUI source from http://www.mameui.info/ extract somewhere then copy the src folder to C:\MinGW\source\MAME0147u3
8. Apply the official MAME patches 0147u1_diff, 0147u2_diff then 0147u3_diff. Apply them by browsing to each one selecting it from the file browser then selecting "Apply Patch"
9. Now apply the Hiscore patch you downloaded from step 3 (Eg. hi_147u3.diff or hi_147u3.txt depending on how you saved it)
10. Set your "MAME Source Folder" to C:\MinGW\source\MAME0147u3 and check you have the WinUI build option selected under OSD
11. Click GO! to compile.

NOTE: If you get an error try pressing GO! again.

### Older Versions of MAME

For MAME versions earlier than 0143u1 you cannot use MAME Compiler v1.24 to compile MAME you must use v1.17. You can download MAME Compiler v1.17 from http://headsoft.com.au/download/mame/MC64Setup117.exe.

http://mamedev.org/oldrel.html

### U Release Diff Patches

You can get the Official MAME diff patches for u releases from http://mamedev.org/updates/

### Hiscore Diff Patches

Newer hiscore diff's are available for download from http://forum.arcadecontrols.com/index.php?topic=64298.0

### Applying Patches

To apply them do in this order:

1. Apply xxxu1.diff
2. Apply xxxu2.diff
3. Apply xxxu3.diff
4. Apply hi_xxxu3.diff
5. Compile

### Version History

- 27-6-15 - v2.0.163 - Added support for STRIP_SYMBOLS
- 1-6-15 - v2.0.162 - Support for "arcade" and "mess" sub targets
- 3-5-15 - v2.0.161 - Update for new build tools and MAME 0161+
- 31-1-15 - v2.0.1 - Fix for MAME 0158+
- 18-11-14 - v2.0 - UI Update, new options added, auto-download source and patches
- 12-11-14 - v1.3.2 - Fix for compiling MAME 0154+
- 1-5-14 - v1.3.1 - Added fix for output in MAMEUI 0153+ and FASTDEBUG option
- 27-12-12 - v1.3 - Many new compiler options added
- 13-12-12 - v1.24 - Updated for new toolchain, added Force Direct Input option
- 21-08-11 - v1.23 - Updated for new toolchain
- 29-05-11 - v1.22 - Updated for new toolchain
- 15-05-11 - v1.21 - MAMEUI compile no longer uses MAMEUI.mak
- 20-09-10 - v1.20 - Updated to latest MinGW
- 05-05-10 - v1.19 - Added "Optimize for: None" option (should solve png2bdc.exe crash issue)
- 08-02-10 - v1.18 - Added support for 64-bit MinGW MAME 0.136u1+
- 04-10-08 - V1.17 - Added "Disable Warnings as Error" to use when compiling 127u6
- 08-07-08 - V1.16 - Added support for compiling MAME 0147
- 25-06-08 - V1.15 - Added support for compiling MAME 0105
- 01-04-08 - V1.14 - Added AMD64 and AthlonXP optimization options
- 01-04-08 - V1.13 - Added more error handling, updated patches
- 22-02-08 - V1.12 - Added support for compiling MAMEUI
- 14-02-08 - V1.11 - Added new architecture flags
- 13-02-08 - V1.1 - Fixed compiling for seattle.c games
- 11-02-08 - V1.0 - Adding support for compiling MAME64
- 05-12-07 - V0.5 - Fixed compiling for 0.116
- 21-10-07 - V0.4 - Fixed make error for 0.120+
- 07-10-07 - V0.3 - Fixed compiling for < 0.118u5
- 01-10-07 - V0.2 - Added color coded output, timers and MKChamp's current diff releases
- 30-09-07 - V0.1 - First Release

### Thanks

Thanks to MKChamp for the hiscore diff's.