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
        private const int PlayerAnimationOffset = 0x1A0;
        private const int EffectorOffset = 0xA4;

        private const int BreathEffector = 0x28;
        private const int WalkEffector = 0x30;
        private const int MotionEffector = 0x38;
        private const int ForceEffector = 0x40;

        private ulong _animationPtr;

        private ulong _breathPtr;
        private float _originalBreath;

        private ulong _walkPtr;
        private float _originalWalk;

        private ulong _motionPtr;
        private float _originalMotion;

        private ulong _forcePtr;
        private float _originalForce;

        private ulong playerBase;
        public PlayerManager(ulong localGameWorld) {
            playerBase = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
        }

        public void NoRecoil(bool on){
            var proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
            var shootinggPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x48);
            var newShotRecoilPtr = Memory.ReadPtr(shootinggPtr + 0x18);

            Memory.WriteValue<bool>(newShotRecoilPtr + 0x41, !on);
        }

        public void NoSway(bool on)
        {
            var proceduralWeaponAnimationPtr = Memory.ReadPtr(playerBase + 0x1A0);
            var breathEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x28);
            var walkEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x30);
            var motionEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x38);
            var forceEffectorPtr = Memory.ReadPtr(proceduralWeaponAnimationPtr + 0x40);
            //if first time, save the original values ?

            float newValue = on ? 0.0f : 1.0f;
            Memory.WriteValue<float>(breathEffectorPtr + 0xA4, newValue);
            Memory.WriteValue<float>(walkEffectorPtr + 0x44, newValue);
            Memory.WriteValue<float>(motionEffectorPtr + 0xD0, newValue);
            Memory.WriteValue<float>(forceEffectorPtr + 0x30, newValue);
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