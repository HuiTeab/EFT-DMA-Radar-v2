using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eft_dma_radar.Source.Tarkov
{

    /// <summary>
    /// Class to manage local player write operations
    /// </summary>
    public class PlayerManager
    {
        private ulong proceduralWeaponAnimationPtr;
        private ulong skillsManager;
        private int originalProceduralWeaponAnimationMask;

        public PlayerManager(ulong localGameWorld) {
            var playerBase = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
            var playerProfile = Memory.ReadPtr(playerBase + 0x588);
            skillsManager = Memory.ReadPtr(playerProfile + 0x60);

            originalProceduralWeaponAnimationMask = Memory.ReadValue<int>(proceduralWeaponAnimationPtr + 0x138);
        }

        public void SetNoRecoilAndSway(bool enabled) {
            var mask = Memory.ReadValue<int>(proceduralWeaponAnimationPtr + 0x138);
            if (mask == 0)
            {
                mask = 125;
            }
            if (enabled) {
                originalProceduralWeaponAnimationMask = mask;
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x138, 0);
            } else {
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x138, originalProceduralWeaponAnimationMask);
            }
        }


    }
}