# EFT-DMA-Radar-v2

## Description
EFT DMA Radar is a radar tool designed for Escape from Tarkov that provides real-time tracking of players and items on a 2D map.
![image](https://github.com/HuiTeab/EFT-DMA-Radar-v2/assets/151649934/d652b193-a987-40b7-8aa2-7ddd66eaae85)
![image](https://github.com/HuiTeab/EFT-DMA-Radar-v2/assets/151649934/8613ee59-f604-453a-bdea-626c0b5ba365)

## Project Structure

- **Maps**: Directory containing maps data.
- **Source**: Source code directory.
  - **Tarkov**: Directory for Tarkov-related files.
    - **ExfilManager.cs**: Manages extraction points.
    - **Game.cs**: Handles game-related functionalities.
    - **GearManager.cs**: Manages player gear.
    - **GrenadeManager.cs**: Handles grenade-related functionalities.
    - **LootManager.cs**: Manages loot items. (Work in Progress - loot tracking works but need to create cache so it would automatically refresh current game loot)
    - **Player.cs**: Manages player-related functionalities. (Work in Progress)
    - **RegisteredPlayers.cs**: Manages registered players.
    - **TarkovDevAPIManager.cs**: Manages Tarkov market-related operations.
    - **QuestManager.cs**: Quest stuff (Work in Progress)
  - **Misc**: Directory for miscellaneous files.
    - **Extensions.cs**: Contains extension methods.
    - **Misc.cs**: Contains miscellaneous functionalities.
    - **Offsets.cs**: Contains memory offsets.
    - **SKPaints.cs**: Contains SKPaint configurations.
    - **Quests.cs**: Quest stuff (OLD?)

## Usage

1. Clone the repository.
2. Ensure all necessary dependencies are in place.
3. Compile the project.
4. Run the application.

## Dependencies

- FTD3XX.dll - https://ftdichip.com/drivers/d3xx-drivers/
- leechcore.dll, vmm.dll, dbghelp.dll, symsrv.dll and vcruntime140.dll - https://github.com/ufrisk/MemProcFS/releases

## Contact
For any inquiries or assistance, feel free to contact me on Discord: keeegi_10477

## Note

Ensure all necessary files are properly included and referenced for the application to function correctly.

## Acknowledgments
This project builds upon the original work created by [Git link](https://github.com/6b45/eft-dma-radar-1) [UC Forum Thread](https://www.unknowncheats.me/forum/escape-from-tarkov/482418-2d-map-dma-radar-wip.html). I am not the original creator of this project; all credit for the initial concept and development goes to Lone. This version seeks to extend and enhance the original tool with updated functionalities and improvements.
