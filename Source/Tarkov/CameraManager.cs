using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace eft_dma_radar.Source.Tarkov
{
    public class CameraManager
    {
        private ulong _unityBase;
        private ulong _opticCamera;
        private ulong _fpsCamera;
        public bool IsReady
        {
            get
            {
                return this._opticCamera != 0 && this._fpsCamera != 0;
            }
        }

        public CameraManager(ulong unityBase)
        {
            this._unityBase = unityBase;
            this.GetCamera();
        }

        private bool GetCamera()
        {
            try
            {
                var addr = Memory.ReadPtr(this._unityBase + 0x0179F500);
                for (int i = 0; i < 500; i++)
                {
                    var allCameras = Memory.ReadPtr(addr + 0x0);
                    var camera = Memory.ReadPtr(allCameras + (ulong)i * 0x8);

                    if (camera != 0)
                    {
                        var cameraObject = Memory.ReadPtr(camera + Offsets.GameObject.ObjectClass);
                        var cameraNamePtr = Memory.ReadPtr(cameraObject + Offsets.GameObject.ObjectName);

                        var cameraName = Memory
                            .ReadString(cameraNamePtr, 64)
                            .Replace("\0", string.Empty);
                        if (
                            cameraName.Contains(
                                "BaseOpticCamera(Clone)",
                                StringComparison.OrdinalIgnoreCase
                            )
                        )
                        {
                            this._opticCamera = cameraObject;
                        }
                        if (cameraName.Contains("FPS Camera", StringComparison.OrdinalIgnoreCase))
                        {
                            this._fpsCamera = cameraObject;
                        }
                        if (this._opticCamera != 0 && this._fpsCamera != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (DMAShutdown)
            {
                throw;
            }
            return false;
        }

        public async Task<bool> GetCameraAsync()
        {
            return await Task.Run(() => this.GetCamera());
        }

        public void UpdateCamera()
        {
            if (this._unityBase == 0)
                return;
            this.GetCamera();
        }

        private ulong GetComponentFromGameObject(ulong gameObject, string componentName)
        {
            var component = Memory.ReadPtr(gameObject + 0x30);
            // Loop through a fixed range of potential component slots
            for (int i = 0x8; i < 0x500; i += 0x10)
            {
                var componentPtr = Memory.ReadPtr(component + (ulong)i);
                if (componentPtr == 0)
                    continue;
                var fieldsPtr = Memory.ReadPtr(componentPtr + 0x28);
                componentPtr = Memory.ReadPtr(componentPtr + 0x28);
                var classNamePtr = Memory.ReadPtrChain(fieldsPtr, Offsets.UnityClass.Name);
                var className = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
                //Console.WriteLine($"GetComponentFromGameObject: {className}");
                if (className.Contains(componentName, StringComparison.OrdinalIgnoreCase))
                {
                    return componentPtr;
                }
            }
            return 0;
        }

        /// <summary>
        /// public function to turn thermalvision on and off
        /// </summary>
        public void ThermalVision(bool on)
        {
            if (this.IsReady)
            {
                var fpsThermalComponent = GetComponentFromGameObject(this._fpsCamera, "ThermalVision");
		        //component.TextureMask.Stretch = false;
		        //component.TextureMask.Size = 0f;
                var thermalOn = Memory.ReadValue<bool>(fpsThermalComponent + 0xE0);
                //[40] TextureMask : BSG.CameraEffects.TextureMask
                var textureMask = Memory.ReadPtr(fpsThermalComponent + 0x40);
                //[60] Stretch : Boolean
                //[64] Size : Single
                //var color = Memory.ReadPtr(textureMask + 0x20);
                //var stretch = Memory.ReadValue<bool>(textureMask + 0x60);
                //var size = Memory.ReadValue<float>(textureMask + 0x64);
                //Console.WriteLine($"stretch: {stretch}, size: {size} color: {color}");


                if (on != thermalOn)
                {
                    Memory.WriteValue(fpsThermalComponent + 0xE0, !thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE1, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE2, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE3, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE4, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE5, thermalOn);

                    //var thermalVisionUtilities = Memory.ReadPtr(fpsThermalComponent + 0x18);
                    //var valuesCoefs = Memory.ReadPtr(thermalVisionUtilities + 0x18);
                    //flir = mainTexColorCoef: 0.7, minimumTemperatureValue: 0.01, rampShift: -0.5
                    //Memory.WriteValue(valuesCoefs + 0x10, 1.0f); //mainTexColorCoef 0.5f is default / 0.7f is flir / 1f is max red?
                    //Memory.WriteValue(valuesCoefs + 0x14, 0.00001f); //minimumTemperatureValue 0.01f is default / 0.001f is flir / detection any temp?
                    //Memory.WriteValue(thermalVisionUtilities + 0x30, 2); //0 = pink / 1 = green / 2 = white
                    
                }
            }
        }

        /// <summary>
        /// public function to turn nightvision on and off
        /// </summary>
        public void NightVision(bool on)
        {
            if (this.IsReady)
            {
                var nightVisionComponent = GetComponentFromGameObject(_fpsCamera, "NightVision");
                var nightVisionOn = Memory.ReadValue<bool>(nightVisionComponent + 0xEC);

                if (on != nightVisionOn)
                {
                    Memory.WriteValue(nightVisionComponent + 0xEC, !nightVisionOn);
                }
            }
        }

        /// <summary>
        /// public function to turn visor on and off
        /// </summary>
        public void VisorEffect(bool on)
        {
            if (this.IsReady)
            {
                var visorComponent = GetComponentFromGameObject(this._fpsCamera, "VisorEffect");
                bool visorDown = (Memory.ReadValue<float>(visorComponent + 0xC0) == 1.0f);

                if (on == visorDown)
                {
                    Memory.WriteValue(visorComponent + 0xC0, (on ? 0.0f : 1.0f));
                }
            }
        }

        /// <summary>
        /// public function to turn optic thermalvision on and off
        /// </summary>
        public void OpticThermalVision(bool on)
        {
            if (this.IsReady)
            {
                ulong opticComponent = 0;
                var component = Memory.ReadPtr(this._opticCamera + 0x30);
                var opticThermal = GetComponentFromGameObject(this._opticCamera, "ThermalVision");
                for (int i = 0x8; i < 0x100; i += 0x10)
                {
                    var fields = Memory.ReadPtr(component + (ulong)i);
                    if (fields == 0)
                        continue;
                    var fieldsPtr_ = Memory.ReadPtr(fields + 0x28);
                    var classNamePtr = Memory.ReadPtrChain(fieldsPtr_, Offsets.UnityClass.Name);
                    var className = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
                    if (className == "ThermalVision")
                    {
                        opticComponent = fields;
                        break;
                    }
                }
                Memory.WriteValue(opticComponent + 0x38, on);
                Memory.WriteValue(opticThermal + 0xE1, !on);
                Memory.WriteValue(opticThermal + 0xE2, !on);
                Memory.WriteValue(opticThermal + 0xE3, !on);
                Memory.WriteValue(opticThermal + 0xE4, !on);
                Memory.WriteValue(opticThermal + 0xE5, !on);
                //var thermalVisionUtilities = Memory.ReadPtr(opticThermal + 0x18);
                //var valuesCoefs = Memory.ReadPtr(thermalVisionUtilities + 0x18);
                //flir = mainTexColorCoef: 0.7, minimumTemperatureValue: 0.01, rampShift: -0.5
                //Memory.WriteValue(valuesCoefs + 0x10, 1f); //mainTexColorCoef 0.5f is default / 0.7f is flir / 1f is max red?
                //Memory.WriteValue(valuesCoefs + 0x14, 0.0001f); //minimumTemperatureValue 0.01f is default / 0.001f is flir / detection any temp?
                //Memory.WriteValue(thermalVisionUtilities + 0x30, 2); //0 = pink / 1 = green / 2 = white

            }
        }

        public ulong GetNightMaterial() {
            if (this.IsReady)
            {
                var nightVisionComponent = GetComponentFromGameObject(_fpsCamera, "NightVision");
                var nightVisionMaterial = Memory.ReadPtr(nightVisionComponent + 0x90);

                return nightVisionMaterial;
            }
            return 0;
        }

        public ulong GetNightColor() {
            if (this.IsReady)
            {
                var nightVisionComponent = GetComponentFromGameObject(_fpsCamera, "NightVision");
                var nightVisionColor = Memory.ReadPtr(nightVisionComponent + 0xD8);

                return nightVisionColor;
            }
            return 0;
        }
    }
}
