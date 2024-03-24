using OpenTK.Graphics.ES11;
using SkiaSharp;
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
        private ulong playerBase { get; set; }
        public Dictionary<string, float> OriginalValues { get; }
        public ulong proceduralWeaponAnimationPtr { get; set; }

        /// <summary>
        /// Creates new PlayerManager object
        /// </summary>
        public PlayerManager(ulong localGameWorld)
        {
            playerBase = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
        }

        /// <summary>
        /// Enables / disables weapon recoil & sway
        /// </summary>
        public void SetNoRecoilSway(bool on)
        {
            Memory.WriteValue(proceduralWeaponAnimationPtr + 0x138, on ? 0 : (int)OriginalValues["Mask"]);
        }

        /// <summary>
        /// Enables / disables instant ads, changes per weapon
        /// </summary>
        public void SetInstantADS(bool on)
        {
            var aimingSpeed = Memory.ReadValue<float>(proceduralWeaponAnimationPtr + 0x1DC);

            if (on && aimingSpeed != 7)
            {
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x1DC, 7f);
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x2A4, 0f);
            }
            else if (!on && aimingSpeed != 1)
            {
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x1DC, 1f);
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x2A4, 0.2f);
            }
        }

        /// <summary>
        /// Changes player movement state
        /// </summary>
        public void SetMovementState(bool enabled)
        {
            var movementContext = Memory.ReadPtr(playerBase + Offsets.Player.MovementContext);
            var currentBaseMovementState = Memory.ReadPtr(movementContext + 0xD0);
            if (enabled)
            {
                if (Memory.ReadValue<byte>(currentBaseMovementState + 0x21) == 5)
                {
                    Memory.WriteValue<byte>(currentBaseMovementState + 0x21, 6);
                }
            }
            else
            {
                if (Memory.ReadValue<byte>(currentBaseMovementState + 0x21) == 6)
                {
                    Memory.WriteValue<byte>(currentBaseMovementState + 0x21, 5);
                }
            }
        }

        /// <summary>
        /// Sets maximum stamina based on current stamina
        /// </summary>
        public void SetMaxStamina()
        {
            var physical = Memory.ReadPtr(playerBase + 0x598);
            var stamina = Memory.ReadPtr(physical + 0x38);
            var handsStamina = Memory.ReadPtr(physical + 0x40);
            float staminaCapacity = Memory.ReadValue<float>(physical + 0xC0);
            float handsStaminaCapacity = Memory.ReadValue<float>(physical + 0xC8);
            Memory.WriteValue<float>(stamina + 0x48, staminaCapacity);
            Memory.WriteValue<float>(handsStamina + 0x48, handsStaminaCapacity);

            //[B0] Fatigue : Single
            //var fatigue = Memory.ReadValue<float>(physical + 0xB0);
            //Console.WriteLine($"Fatigue: {fatigue}");

        }
    }
}