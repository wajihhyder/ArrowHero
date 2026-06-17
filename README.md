# Arrow Hero

A 2D mobile archery game built in Unity. You aim a bow by touch, draw it back, and release physics-driven arrows that ricochet off walls to hit enemies. Includes scoring, dodging, headshots, a coin shop, weapon upgrades, and ten levels to clear.

![Unity](https://img.shields.io/badge/Unity-2022.3_LTS-222C37?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-game_logic-239120?logo=csharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Android_(mobile)-3DDC84?logo=android&logoColor=white)
![Code License: MIT](https://img.shields.io/badge/Code_License-MIT-green.svg)

## Demo


## The game

You drag on the screen to draw the bow and rotate it around its pivot, and a dotted line previews where the arrow will go. Let go and the arrow flies as a real Rigidbody2D, so it arcs, hits, and bounces. Each arrow can ricochet a few times off walls and the ground before it's spent, which turns a lot of levels into little bank-shot puzzles. Hit an enemy in the body for damage, or land a headshot for an instant kill and bonus score. You've got a limited number of arrows per level, and the enemies shoot back, so you can't just spray. Your score for a level depends upon how many arrows you used, hits taken and headshots. 

Between levels there's a hub: a shop where you spend coins on better bows (more damage), an equipment screen to switch your active weapon, and a level map that tracks which levels you've unlocked and how many crowns you earned on each.

## How it's built

The gameplay code is in `Assets/Scripts/`. The pieces that were the most interesting to get
right:

- **Aiming and shooting** (`Weapon.cs`, `Arm.cs`, `armRotator.cs`): touch input drives the bow rotation around a pivot, with a draw gesture, a shot cooldown, and audio feedback for the draw and release.
- **Arrow physics** (`Arrow.cs`, `EnemyArrow.cs`): arrows move by velocity and reflect off surfaces using `Vector2.Reflect`, with a bounce budget so they eventually expire. Collisions are resolved by tag (head / body / wall / ground) and feed damage, score, and floating combat text.
- **Trajectory preview** (`Prediction.cs`): a `LineRenderer` driven by `Physics2D.Raycast` draws the aim line up to the first surface it hits.
- **Progress that persists** (`PersistentData.cs`): a `DontDestroyOnLoad` singleton holds unlocked levels, crowns, coins, owned weapons and the equipped weapon's damage across the ten level scenes.
- **Shop and equipment** (`ShopCanvas.cs`, `EquipmentCanvas.cs`, `ShopObject.cs`): buying gates on your coin balance, marks items owned, and equipping swaps the active weapon's damage.
- **Glue**: `UIManager.cs` drives the home / level / shop / settings / pause / game-over screens, `SoundManager.cs` handles SFX, and `ScoreManager.cs` tracks score.

## Tech

Unity **2022.3 LTS**, C#, the 2D and Mobile feature sets, an on-screen joystick, and TextMeshPro. Ten levels are built as separate scenes loaded from the level map.

## Project layout

```
Assets/Scripts/     the game code (aiming, arrows, enemies, UI, progression, shop)
Assets/Scenes/      Main scene + Level1..Level10
Assets/Sprites/, Sounds/, Animation/, Prefabs/   game assets
```

## License & credits

The gameplay code in `Assets/Scripts/` is my own work, released under the **MIT License** (see [`LICENSE`](LICENSE)). The bundled third-party packs — the **Joystick Pack**, **Dynamic Floating Text** (damage numbers), **TextMesh Pro**, and the 2D art packs (**Miniature Army 2D**, **Puzzle stage & settings GUI Pack**) — are used under and retain their own original licenses.
