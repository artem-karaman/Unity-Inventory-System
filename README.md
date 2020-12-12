[![Hits-of-Code](https://hitsofcode.com/github/namarakM/Unity-Inventory-System?branch=main)](https://hitsofcode.com/github/namarakM/Unity-Inventory-System?branch=main/view?branch=main/)

[Hits-of-Code-Overview](https://hitsofcode.com/view/github/namarakM/Unity-Inventory-System?branch=main)

Table of Contents
<details>
  
1. [Intro](#intro)
2. [Used plugins](#plugins-to-add)
3. [How to install](#how-to-install)

</details>

## Intro
Inventory system for unity android project.

Tests with Unity Editor version - 2020.1.8f1

## Used plugins

  <summary>Plugins and packages to add:</summary>
  
- UniRx - version 7.1.0 - https://github.com/neuecc/UniRx/releases/tag/7.1.0 - unity package - upm
  
- UniTask - version 2.0.36 - https://github.com/Cysharp/UniTask/releases/tag/2.0.36 - unity package - upm

- Extenject - version 9.2.0 - https://openupm.com/packages/com.svermeulen.extenject/ - open upm

- Unity reorderable list - https://github.com/cfoulston/Unity-Reorderable-List - unity package - upm 

or 

Add to manifest

```yaml
{
  "dependencies": 
  {
    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
    "com.malee.reorderablelist": "https://github.com/cfoulston/Unity-Reorderable-List.git",
    "com.neuecc.unirx": "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts"
    ...
  }
}
```

and Extenject via open upm - https://openupm.com/packages/com.svermeulen.extenject/



## How to install
Install this package via upm:

Package:

https://github.com/namarakM/Unity-Inventory-System.git?path=/Unity-Inventory-System/Assets/InventorySystem

How to use it:

[Unity-Manual](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
