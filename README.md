[![Website Stephen Allen Games](http://www.stephenallengames.co.uk/images/logo.gif)](http://www.stephenallengames.co.uk/games.php)

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=9PUGQGE4XDE4C&currency_code=GBP)

# BuildStripper
Build Stripper gets the build pipeline to totally ignore select folders, and reverts back after.  
<br/><br/>
Version 1.0  Credits:
<br/>
[JesusLuvsYooh](https://github.com/JesusLuvsYooh) StephenAllenGames.co.uk  
#WaterCooler gang for letting me Rubber Duck to them.
<br/><br/>
1: This file must be in an "Editor" folder (Unity/Assets/Editor) for example.
<br/>
2: Set "buildStrippedServer" bool to false to disable, when doing client builds, true is meant for headless servers.
<br/>
3: Edit the "folderPaths" list, making sure it only includes items that server build does not need, such as Audio an Textures.  
<br/><br/>
This script should drastically lower your build size, and optimise your game;  
<br/>
Build size differences in an example project:<br/>
Regular server build: 1 GB<br/>
An un-named Asset store plugin: 200 MB<br/>
This BuildStripper: 60mb<br/>
<br/>
Image preview of build process.<br/>
![BuildStripperImage1](https://user-images.githubusercontent.com/57072365/147373339-f707e24b-64aa-4bdb-bed1-e7a233e08a56.jpg)
<br/><br/>
Optional Unity Menu buttons, for manually calling, just un-comment them in the script.  
Please be aware this is version 1, an Alpha/Beta, code needs to be optimised and tidied up. 
<br/><br/>
Enjoy!  ^_^
  
 
