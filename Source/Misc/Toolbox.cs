using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace eft_dma_radar.Source.Tarkov
{
    public class Toolbox
    {
        private readonly Thread _workerThread;
        private bool noRecoilSwayToggled = false;
        private bool nightVisionToggled = false;
        private bool thermalVisionToggled = false;
        private bool doubleSearchToggled = false;
        private bool jumpPowerToggled = false;
        private bool throwPowerToggled = false;
        private bool juggernautToggled = false;
        private bool magDrillsToggled = false;

        public Toolbox(ulong localGameWorld)
        {
            this._workerThread = new Thread((ThreadStart)delegate
            {
                while (Memory.LocalPlayer is null)
                {
                    Thread.Sleep(100);
                }

                while (IsSafeToWriteMemory())
                {
                    this.ToolboxWorker();
                    Thread.Sleep(500);
                }

                Program.Log("LocalPlayer found, initializing toolbox");
            });

            this._workerThread.Priority = ThreadPriority.BelowNormal;
            this._workerThread.IsBackground = true;
            this._workerThread.Start();
        }

        private bool IsSafeToWriteMemory()
        {
            return Memory.InGame && Memory.LocalPlayer is not null;
        }

        private void ToolboxWorker()
        {
            if (Game.CameraManager is not null)
            {
                if (!Game.CameraManager.IsReady)
                {
                    Game.CameraManager.UpdateCamera();
                } else
                {
                    Game.CameraManager.VisorEffect(Program.Config.NoVisorEnabled);

                    if (Program.Config.OpticThermalVisionEnabled && Memory.PlayerManager.isADS)
                    {
                        Game.CameraManager.OpticThermalVision(true);
                    }
                    else if (!Program.Config.OpticThermalVisionEnabled && !Memory.PlayerManager.isADS)
                    {
                        Game.CameraManager.OpticThermalVision(false);
                    }

                    if (Program.Config.ThermalVisionEnabled && !thermalVisionToggled)
                    {
                        thermalVisionToggled = true;
                        Game.CameraManager.ThermalVision(true);
                    }
                    else if (!Program.Config.ThermalVisionEnabled && thermalVisionToggled)
                    {
                        thermalVisionToggled = false;
                        Game.CameraManager.ThermalVision(false);
                    }

                    if (Program.Config.NightVisionEnabled && !nightVisionToggled)
                    {
                        nightVisionToggled = true;
                        Game.CameraManager.NightVision(true);
                    }
                    else if (!Program.Config.NightVisionEnabled && nightVisionToggled)
                    {
                        nightVisionToggled = false;
                        Game.CameraManager.NightVision(false);
                    }
                }
                
            }

            Memory.PlayerManager.isADS = Memory.ReadValue<bool>(Memory.PlayerManager.proceduralWeaponAnimationPtr + 0x1BD);
            Memory.PlayerManager.SetNoRecoilSway(Program.Config.NoRecoilSwayEnabled);

            //if (Program.Config.NoRecoilSwayEnabled && !noRecoilSwayToggled)
            //{
            //    noRecoilSwayToggled = true;
            //    Memory.PlayerManager.SetNoRecoilSway(true);
            //}
            //else if (!Program.Config.NoRecoilSwayEnabled && noRecoilSwayToggled)
            //{
            //    noRecoilSwayToggled = false;
            //    Memory.PlayerManager.SetNoRecoilSway(false);
            //}

            if (Program.Config.DoubleSearchEnabled && !doubleSearchToggled)
            {
                doubleSearchToggled = true;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.SearchDouble);
            } else if (!Program.Config.DoubleSearchEnabled && doubleSearchToggled)
            {
                doubleSearchToggled = false;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.SearchDouble, true);
            }

            if (Program.Config.JumpPowerEnabled && !jumpPowerToggled)
            {
                jumpPowerToggled = true;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.JumpStrength);
            }
            else if (!Program.Config.JumpPowerEnabled && jumpPowerToggled)
            {
                jumpPowerToggled = false;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.JumpStrength, true);
            }

            if (Program.Config.ThrowPowerEnabled && !throwPowerToggled)
            {
                throwPowerToggled = true;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.ThrowStrength);
            }
            else if (!Program.Config.ThrowPowerEnabled && throwPowerToggled)
            {
                throwPowerToggled = false;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.ThrowStrength, true);
            }

            if (Program.Config.IncreaseMaxWeightEnabled && !juggernautToggled)
            {
                juggernautToggled = true;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.WeightStrength);
            }
            else if (!Program.Config.IncreaseMaxWeightEnabled && juggernautToggled)
            {
                juggernautToggled = false;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.WeightStrength, true);
            }

            if (Program.Config.MagDrillsEnabled && !magDrillsToggled)
            {
                magDrillsToggled = true;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.MagDrillsLoad);
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.MagDrillsUnload);
            }
            else if (!Program.Config.DoubleSearchEnabled && doubleSearchToggled)
            {
                magDrillsToggled = false;
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.MagDrillsLoad, true);
                Memory.PlayerManager.SetMaxSkill(PlayerManager.Skills.MagDrillsUnload, true);
            }
        }
    }
}
