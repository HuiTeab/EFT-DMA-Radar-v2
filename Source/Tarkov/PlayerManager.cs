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
        private ulong playerProfile { get; set; }
        private ulong skillsManager { get; set; }

        public Dictionary<string, float> OriginalValues { get; }
        public ulong proceduralWeaponAnimationPtr { get; set; }
        public bool isADS { get; set; }

        /// <summary>
        /// Stores the different skills that can be modified
        /// </summary>
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

        /// <summary>
        /// Creates new PlayerManager object
        /// </summary>
        public PlayerManager(ulong localGameWorld)
        {
            playerBase = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
            playerProfile = Memory.ReadPtr(playerBase + 0x588);
            skillsManager = Memory.ReadPtr(playerProfile + 0x60);

            isADS = Memory.ReadValue<bool>(proceduralWeaponAnimationPtr + 0x1BD);

            OriginalValues = new Dictionary<string, float>()
            {
                ["MagDrillsLoad"] = -1,
                ["MagDrillsUnload"] = -1,
                ["JumpStrength"] = -1,
                ["WeightStrength"] = -1,
                ["ThrowStrength"] = -1,
                ["SearchDouble"] = -1,
                ["Mask"] = 125,
                ["AimingSpeed"] = -1
            };
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

            if (OriginalValues["AimingSpeed"] == -1)
            {
                OriginalValues["AimingSpeed"] = aimingSpeed;
            }

            if (on && aimingSpeed != 6f)
            {
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x1DC, 6f);
            }
            else if (!on && aimingSpeed != OriginalValues["AimingSpeed"])
            {
                Memory.WriteValue(proceduralWeaponAnimationPtr + 0x1DC, OriginalValues["AimingSpeed"]);

                OriginalValues["AimingSpeed"] = -1;
            }
        }

        /// <summary>
        /// Modifies the players skill buffs
        /// </summary>
        public void SetMaxSkill(Skills skill, bool revert = false)
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
                            Memory.WriteValue<float>(magDrillsLoad + 0x30, (revert ? OriginalValues["MagDrillsLoad"] : Program.Config.MagDrillSpeed * 10));
                            break;
                        }
                    case Skills.MagDrillsUnload:
                        {
                            var magDrillsUnload = Memory.ReadPtr(skillsManager + 0x188);

                            if (OriginalValues["MagDrillsUnload"] == -1)
                            {
                                OriginalValues["MagDrillsUnload"] = Memory.ReadValue<float>(magDrillsUnload + 0x30);
                            }
                            Memory.WriteValue<float>(magDrillsUnload + 0x30, (revert ? OriginalValues["MagDrillsUnload"] : Program.Config.MagDrillSpeed * 10));
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
    }
}