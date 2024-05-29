# HexpatCombiner
Combines multiple ImHex hexpat files into one, useful for folder structured hexpats that you want to upload to the ImHex-Patterns repo.   

![](https://img.shields.io/badge/Code%20Readability-None-961414)   
I don't code C# so I have barely any idea what I'm doing.

# How to use
```
HexpatCombiner.exe {string filePath} {bool addCombinationComment}
```
addCombinationComment prevents HexpatCombiner from adding this to the start:
```
/*
==========================
 * COMBINED HEXPAT 
==========================
*/
```
and this to the end:
```
/*
==============================
 * END COMBINED HEXPAT
==============================
*/
```

# Naming, imports
To import a file, use ``combine``   

ex: ``combine "./packets/C2S/requestWorldData.hexpat";``   

To name the uncombined file, add this into the files you want to combine into the main one:   

``#pragma combinerName {name}``   

ex: ``#pragma combinerName requestWorldData``

Note: Don't add this pragma into the main file where all the imports are.   
Also note: I may be using pragma wrong or something IDK much about C syntax n stuff.
