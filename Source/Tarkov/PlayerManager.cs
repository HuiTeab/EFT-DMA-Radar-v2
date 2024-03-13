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
        private ulong playerProfile { get; set; }
        private ulong skillsManager { get; set; }

        public Dictionary<string, float> OriginalValues { get; }
        public ulong proceduralWeaponAnimationPtr { get; set; }
        public bool isADS { get; set; }

        public enum Skills
        {
            MagDrillsLoad,
            MagDrillsUnload,
            JumpStrength,
            ThrowStrength,
            WeightStrength,
            SearchDouble,
            ADS
        }

        public PlayerManager(ulong localGameWorld) {
            playerBase = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
            playerProfile = Memory.ReadPtr(playerBase + 0x588);
            skillsManager = Memory.ReadPtr(playerProfile + 0x60);

            isADS = Memory.ReadValue<bool>(proceduralWeaponAnimationPtr + 0x1BD);

            OriginalValues = new Dictionary<string, float>()
            {
                ["NoSwayBreathEffect"] = -1,
                ["NoSwayWalkEffect"] = -1,
                ["NoSwayMotionEffect"] = -1,
                ["NoSwayForceEffect"] = -1,
                ["MagDrillsLoad"] = -1,
                ["MagDrillsUnload"] = -1,
                ["JumpStrength"] = -1,
                ["WeightStrength"] = -1,
                ["ThrowStrength"] = -1,
                ["SearchDouble"] = -1,
                // TO DO:
                // 1f -> 7f safe?
                ["ADSModifier"] = -1
            };
        }

        public void SetMaxSkill(Skills skill, bool revert=false)
        {
            try
            {
                switch (skill)
                {
                    case Skills.MagDrillsLoad:
                        {
                            var magDrillsLoad = Memory.ReadPtr(skillsManager + 0x180);

                            if (OriginalValues["MagDrillsLoad"] == -1)
                            {
                                OriginalValues["MagDrillsLoad"] = Memory.ReadValue<float>(magDrillsLoad + 0x30);
                            }
                            Memory.WriteValue<float>(magDrillsLoad + 0x30, (revert ? OriginalValues["MagDrillsLoad"] : Program.Config.MagDrillSpeed));
                            break;
                        }
                    case Skills.MagDrillsUnload:
                        {
                            var magDrillsUnload = Memory.ReadPtr(skillsManager + 0x188);

                            if (OriginalValues["MagDrillsUnload"] == -1)
                            {
                                OriginalValues["MagDrillsUnload"] = Memory.ReadValue<float>(magDrillsUnload + 0x30);
                            }
                            Memory.WriteValue<float>(magDrillsUnload + 0x30, (revert ? OriginalValues["MagDrillsUnload"] : Program.Config.MagDrillSpeed));
                            break;
                        }
                    case Skills.JumpStrength:
                        {
                            var jumpStrength = Memory.ReadPtr(skillsManager + 0x60);

                            if (OriginalValues["JumpStrength"] == -1)
                            {
                                OriginalValues["JumpStrength"] = Memory.ReadValue<float>(jumpStrength + 0x30);
                            }
                            Memory.WriteValue<float>(jumpStrength + 0x30, (revert ? OriginalValues["JumpStrength"] : Program.Config.JumpPowerStrength / 10));
                            break;
                        }
                    case Skills.WeightStrength:
                        {
                            var weightStrength = Memory.ReadPtr(skillsManager + 0x50);

                            if (OriginalValues["WeightStrength"] == -1)
                            {
                                OriginalValues["WeightStrength"] = Memory.ReadValue<float>(weightStrength + 0x30);
                            }

                            Memory.WriteValue<float>(weightStrength + 0x30, (revert ? OriginalValues["WeightStrength"] : 0.5f));
                            break;
                        }
                    case Skills.ThrowStrength:
                        {
                            var throwStrength = Memory.ReadPtr(skillsManager + 0x70);

                            if (OriginalValues["ThrowStrength"] == -1)
                            {
                                OriginalValues["ThrowStrength"] = Memory.ReadValue<float>(throwStrength + 0x30);
                            }

                            // value between 0.035f & 1f
                            Memory.WriteValue<float>(throwStrength + 0x30, (revert ? OriginalValues["ThrowStrength"] : Program.Config.ThrowPowerStrength / 10));
                            break;
                        }
                    case Skills.SearchDouble:
                        {
                            var searchDouble = Memory.ReadPtr(skillsManager + 0x4C0);
                            Memory.WriteValue<bool>(searchDouble + 0x30, Program.Config.DoubleSearchEnabled);
                            break;
                        }

                }
            }
            catch (Exception e)
            {
                throw new Exception($"ERROR Setting Max Skill: #{skill}");
            }
        }

        public void SetNoRecoil(bool on){
            var shootinggPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x48);
            var newShotRecoilPtr = Memory.ReadPtr(shootinggPtr + 0x18);

            Memory.WriteValue<bool>(newShotRecoilPtr + 0x41, !on);
        }

        public void SetNoSway(bool on)
        {
            var breathEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x28);
            var walkEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x30);
            var motionEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x38);
            var forceEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x40);
            var breathEffectorValue = Memory.ReadValue<float>(breathEffectorPtr + 0xA4);
            
            if (breathEffectorValue != 0.0f || OriginalValues["NoSwayBreathEffect"] == -1)
            {
                if (OriginalValues["NoSwayBreathEffect"] == -1)
                {
                    OriginalValues["NoSwayBreathEffect"] = breathEffectorValue;
                    OriginalValues["NoSwayWalkEffect"] = Memory.ReadValue<float>(walkEffectorPtr + 0x44);
                    OriginalValues["NoSwayMotionEffect"] = Memory.ReadValue<float>(motionEffectorPtr + 0xD0);
                    OriginalValues["NoSwayForceEffect"] = Memory.ReadValue<float>(forceEffectorPtr + 0x30);
                }

                Memory.WriteValue<float>(breathEffectorPtr + 0xA4, (on ? 0.0f : OriginalValues["NoSwayBreathEffect"]));
                Memory.WriteValue<float>(walkEffectorPtr + 0x44, (on ? 0.0f : OriginalValues["NoSwayWalkEffect"]));
                Memory.WriteValue<float>(motionEffectorPtr + 0xD0, (on ? 0.0f : OriginalValues["NoSwayMotionEffect"]));
                Memory.WriteValue<float>(forceEffectorPtr + 0x30, (on ? 0.0f : OriginalValues["NoSwayForceEffect"]));
            }
        }

        public void MaxStamina(bool on){
            var physicalPtr = Memory.ReadPtr(playerBase + 0x598);
            var movementContextPtr = Memory.ReadPtr(playerBase + 0x40);
            var handsStaminaPtr = Memory.ReadPtr(physicalPtr + 0x40);
            var staminaPtr = Memory.ReadPtr(physicalPtr + 0x38);
            var currentHandsStamina = Memory.ReadValue<float>(handsStaminaPtr + 0x48);
            var maxHandsStamina = Memory.ReadValue<float>(physicalPtr + 0xC8);
            var currentStamina = Memory.ReadValue<float>(staminaPtr + 0x48);
            var maxStamina = Memory.ReadValue<float>(physicalPtr + 0xC0);
            try {
                //23 = falling
                //6 = jumping
                //[D0] <CurrentState>k__BackingField : EFT.BaseMovementState
                ulong movementState = Memory.ReadValue<ulong>(movementContextPtr + 0xD0);
                byte curMovementName = Memory.ReadValue<byte>(movementState + 0x21);
                byte moveState = (byte)(on ? (byte)23 : (byte)5);

                Memory.WriteValue<byte>(movementState + 0x21, moveState);
                Memory.WriteValue<float>(handsStaminaPtr + 0x48, moveState == (byte)23 ? maxHandsStamina : currentHandsStamina);
                Memory.WriteValue<float>(staminaPtr + 0x48, moveState == (byte)23 ? maxStamina : currentStamina);
            }catch{}
        }
    }
}