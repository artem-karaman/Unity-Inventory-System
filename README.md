Table of Contents
1. [Intro](#intro)
2. [Plugins to add](#plugins-to-add)
3. [How to install](#how-to-install)


## Intro
Unity Editor version - 2020.1.8f1

## Plugins to add
<details>
  <summary>Plugins and packages to add:</summary>
  
- UniRx - version 7.1.0 - https://github.com/neuecc/UniRx/releases/tag/7.1.0 - unity package - upm
  
- UniTask - version 2.0.36 - https://github.com/Cysharp/UniTask/releases/tag/2.0.36 - unity package - upm

- Extenject - version 9.2.0 - asset store

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

and Extenject form asset store

</details>

## How to install
- Install this package via upm:

https://github.com/namarakM/Unity-Inventory-System.git?path=/Unity-Inventory-System/Assets/InventorySystem
