using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eft_dma_radar.Source.Tarkov
{
    public class CameraManager
    {
        private ulong _unityBase;
        private ulong _opticCamera;
        private ulong _fpsCamera;
        public bool IsReady => _opticCamera != 0 && _fpsCamera != 0;

        public CameraManager(ulong unityBase)
        {
            _unityBase = unityBase;
        }

        private bool GetCamera()
        {
            try
            {
                var addr = Memory.ReadPtr(_unityBase + 0x0179F500);
                for (int i = 0; i < 500; i++)
                {
                    var allCameras = Memory.ReadPtr(addr + 0x0);
                    var camera = Memory.ReadPtr(allCameras + (ulong)i * 0x8);

                    if (camera != 0)
                    {
                        var cameraObject = Memory.ReadPtr(camera + Offsets.GameObject.ObjectClass);
                        var cameraNamePtr = Memory.ReadPtr(
                            cameraObject + Offsets.GameObject.ObjectName
                        );

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
                            _opticCamera = cameraObject;
                        }
                        if (cameraName.Contains("FPS Camera", StringComparison.OrdinalIgnoreCase))
                        {
                            _fpsCamera = cameraObject;
                        }
                        if (_opticCamera != 0 && _fpsCamera != 0)
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
            return await Task.Run(() => GetCamera());
        }

        private void UpdateCamera()
        {
            if (_unityBase == 0)
                return;
            if (_opticCamera == 0 || _fpsCamera == 0)
            {
                GetCamera();
            }
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
        /// public static function to turn thermalvision on and off
        /// </summary>
        ///
        public void ThermalVision(bool on)
        {
            if (_fpsCamera == 0)
            {
                UpdateCamera();
            }
            if (_fpsCamera != 0)
            {
                var fpsThermalComponent = GetComponentFromGameObject(_fpsCamera, "ThermalVision");
                var thermalOn = Memory.ReadValue<bool>(fpsThermalComponent + 0xE0);

                if (on != thermalOn)
                {
                    Memory.WriteValue(fpsThermalComponent + 0xE0, !thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE1, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE2, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE3, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE4, thermalOn);
                    Memory.WriteValue(fpsThermalComponent + 0xE5, thermalOn);
                }
            }
        }

        /// <summary>
        /// public static function to turn nightvision on and off
        /// </summary>
        ///
        public void NightVision(bool on)
        {
            if (_fpsCamera == 0)
            {
                UpdateCamera();
            }
            if (_fpsCamera != 0)
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
        /// public static function to turn visor on and off
        /// </summary>
        ///
        public void VisorEffect(bool on)
        {
            if (_fpsCamera == 0)
            {
                UpdateCamera();
            }
            if (_fpsCamera != 0)
            {
                var fpsVisorComponent = GetComponentFromGameObject(_fpsCamera, "VisorEffect");

                Memory.WriteValue(fpsVisorComponent + 0xC0, on ? 0.0f : 1.0f);
            }
        }

        /// <summary>
        /// public static function to turn optic thermalvision on and off
        /// </summary>
        ///
        public void OpticThermalVision(bool on)
        {
            if (_opticCamera == 0)
            {
                UpdateCamera();
            }
            if (_opticCamera != 0)
            {
                ulong opticComponent = 0;
                ulong opticThermalVision = 0;
                var component = Memory.ReadPtr(_opticCamera + 0x30);
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
                        opticThermalVision = Memory.ReadPtr(fieldsPtr_ + 0x28);
                        break;
                    }
                }

                Memory.WriteValue(opticComponent + 0x38, on);
                Memory.WriteValue(opticThermalVision + 0xE0, on);
            }
        }
    }
}
