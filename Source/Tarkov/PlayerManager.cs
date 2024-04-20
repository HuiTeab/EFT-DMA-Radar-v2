namespace eft_dma_radar
{

    /// <summary>
    /// Class to manage local player write operations
    /// </summary>
    public class PlayerManager
    {
        private ulong playerBase { get; set; }
        private ulong playerProfile { get; set; }

        private ulong movementContext { get; set; }
        private ulong baseMovementState { get; set; }
        private ulong physical { get; set; }
        private ulong stamina { get; set; }
        private ulong handsStamina { get; set; }

        private ulong skillsManager { get; set; }
        private ulong magDrillsLoad { get; set; }
        private ulong magDrillsUnload { get; set; }
        private ulong jumpStrength { get; set; }
        private ulong weightStrength { get; set; }
        private ulong throwStrength { get; set; }
        private ulong searchDouble { get; set; }

        public ulong proceduralWeaponAnimation { get; set; }

        public bool isADS { get; set; }

        private Config _config { get => Program.Config; }
        public Dictionary<string, float> OriginalValues { get; }
        public Dictionary<string, bool> OriginalBools { get; }
        public Dictionary<string, float> MaxedFloats { get; }
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
            ADS,
            MagDrillsInventoryCheckAccuracy,
            MagDrillsInventoryCheckSpeed,
            MagDrillsInstantCheck,
            MagDrillsLoadProgression,
            EnduranceBreathElite,
            EnduranceBuffBreathTimeInc,
            EnduranceBuffEnduranceInc,
            EnduranceBuffJumpCostRed,
            EnduranceBuffRestoration,
            EnduranceHands,
            StrengthBuffAimFatigue,
            StrengthBuffElite,
            StrengthBuffJumpHeightInc,
            StrengthBuffLiftWeightInc,
            StrengthBuffMeleeCrits,
            StrengthBuffMeleePowerInc,
            StrengthBuffSprintSpeedInc,
            StrengthBuffThrowDistanceInc,
            ThrowingStrengthBuff,
            ThrowingEliteBuff,
            VitalityBuffBleedStop,
            VitalityBuffRegeneration,
            VitalityBuffSurviobilityInc,
            SearchBuffSpeed,
            MetabolismMiscDebuffTime,
            MetabolismEliteBuffNoDyhydration,
            AttentionEliteLuckySearch,
            HealthBreakChanceRed,
            HealthEliteAbsorbDamage,
            HealthEnergy,
            SurgerySpeed,
            StressBerserk,
            StressPain,
            DrawElite,
            DrawSpeed,
            DrawSound,
            CovertMovementSpeed,
            CovertMovementSoundVolume,
            CovertMovementLoud,
            CovertMovementEquipment,
            CovertMovementElite,
            PerceptionHearing,
            PerceptionLootDot
        }


        private ulong magDrillsInventoryCheckAccuracy { get; set; } // 0x198
        private ulong magDrillsInventoryCheckSpeed { get; set; } // 0x190
        private ulong magDrillsInstantCheck { get; set; } // 0x1A0
        private ulong magDrillsLoadProgression { get; set; } // 0x1A8
        private ulong enduranceBreathElite { get; set; } // 0x48
        private ulong enduranceBuffBreathTimeInc { get; set; } // 0x38
        private ulong enduranceBuffEnduranceInc { get; set; } // 0x20
        private ulong enduranceBuffJumpCostRed { get; set; } // 0x30
        private ulong enduranceBuffRestoration { get; set; } // 0x40 //limit unknown
        private ulong enduranceHands { get; set; } // 0x28
        private ulong strengthBuffAimFatigue { get; set; } // 0x68
        private ulong strengthBuffElite { get; set; } // 0x80
        private ulong strengthBuffJumpHeightInc { get; set; } // 0x60
        private ulong strengthBuffLiftWeightInc { get; set; } // 0x50
        private ulong strengthBuffMeleeCrits { get; set; } // 0x88
        private ulong strengthBuffMeleePowerInc { get; set; } // 0x78
        private ulong strengthBuffSprintSpeedInc { get; set; } // 0x58
        private ulong strengthBuffThrowDistanceInc { get; set; } // 0x70
        private ulong throwingStrengthBuff { get; set; } // 0x320
        private ulong throwingEliteBuff { get; set; } // 0x330
        private ulong vitalityBuffBleedStop { get; set; } // 0xA8
        private ulong vitalityBuffRegeneration { get; set; } // 0xA0
        private ulong vitalityBuffSurviobilityInc { get; set; } // 0x98
        private ulong searchBuffSpeed { get; set; } // 0x4B8
        private ulong metabolismMiscDebuffTime { get; set; } // 0x108
        private ulong metabolismEliteBuffNoDyhydration { get; set; } // 0x110
        private ulong attentionEliteLuckySearch { get; set; } // 0x170
        private ulong healthBreakChanceRed { get; set; } // 0xB0
        private ulong healthEliteAbsorbDamage { get; set; } // 0xD0
        private ulong healthEnergy { get; set; } // 0xC0
        private ulong surgerySpeed { get; set; } // 0x4D0
        private ulong stressBerserk { get; set; } // 0xF0
        private ulong stressPain { get; set; } // 0xE0
        private ulong drawElite { get; set; } // 0x348
        private ulong drawSpeed { get; set; } // 0x338
        private ulong drawSound { get; set; } // 0x340
        private ulong covertMovementSpeed { get; set; } // 0x488
        private ulong covertMovementSoundVolume { get; set; } // 0x478
        private ulong covertMovementLoud { get; set; } // 0x498
        private ulong covertMovementEquipment { get; set; } // 0x480
        private ulong covertMovementElite { get; set; } // 0x490
        private ulong perceptionHearing { get; set; } // 0x118
        private ulong perceptionLootDot { get; set; } // 0x120
        /// <summary>
        /// Creates new PlayerManager object
        /// </summary>
        public PlayerManager(ulong localGameWorld)
        {
            var scatterMap = new ScatterReadMap(1);
            var round1 = scatterMap.AddRound();
            var round2 = scatterMap.AddRound();
            var round3 = scatterMap.AddRound();
            var round4 = scatterMap.AddRound();
            var round5 = scatterMap.AddRound();

            var r1 = round1.AddEntry<ulong>(0, 0, localGameWorld, null, Offsets.LocalGameWorld.MainPlayer);
            var r2 = round2.AddEntry<ulong>(0, 1, r1, null, Offsets.Player.Profile);
            var r3 = round2.AddEntry<ulong>(0, 2, r1, null, Offsets.Player.MovementContext);
            var r4 = round3.AddEntry<ulong>(0, 3, r3, null, Offsets.MovementContext.BaseMovementState);
            var r5 = round2.AddEntry<ulong>(0, 4, r1, null, Offsets.Player.Physical);
            var r6 = round3.AddEntry<ulong>(0, 5, r5, null, Offsets.Physical.Stamina);
            var r7 = round3.AddEntry<ulong>(0, 6, r5, null, Offsets.Physical.HandsStamina);
            var r8 = round3.AddEntry<ulong>(0, 7, r2, null, Offsets.Profile.SkillManager);
            var r9 = round2.AddEntry<ulong>(0, 8, r1, null, Offsets.Player.ProceduralWeaponAnimation);
            var r10 = round4.AddEntry<bool>(0, 9, r9, null, Offsets.ProceduralWeaponAnimation.IsAiming);
            var r11 = round4.AddEntry<ulong>(0, 10, r8, null, Offsets.SkillManager.MagDrillsLoadSpeed);
            var r12 = round4.AddEntry<ulong>(0, 11, r8, null, Offsets.SkillManager.MagDrillsUnloadSpeed);
            var r13 = round4.AddEntry<ulong>(0, 12, r8, null, Offsets.SkillManager.JumpStrength);
            var r14 = round4.AddEntry<ulong>(0, 13, r8, null, Offsets.SkillManager.WeightStrength);
            var r15 = round4.AddEntry<ulong>(0, 14, r8, null, Offsets.SkillManager.ThrowStrength);
            var r16 = round4.AddEntry<ulong>(0, 15, r8, null, Offsets.SkillManager.SearchDouble);
            var r17 = round4.AddEntry<ulong>(0, 16, r8, null, Offsets.SkillManager.MagDrillsInventoryCheckAccuracy);
            var r19 = round4.AddEntry<ulong>(0, 18, r8, null, Offsets.SkillManager.MagDrillsInstantCheck);
            var r21 = round4.AddEntry<ulong>(0, 20, r8, null, Offsets.SkillManager.EnduranceBreathElite);
            var r22 = round4.AddEntry<ulong>(0, 21, r8, null, Offsets.SkillManager.EnduranceBuffBreathTimeInc);
            var r23 = round4.AddEntry<ulong>(0, 22, r8, null, Offsets.SkillManager.EnduranceBuffEnduranceInc);
            var r24 = round4.AddEntry<ulong>(0, 23, r8, null, Offsets.SkillManager.EnduranceBuffJumpCostRed);
            var r25 = round4.AddEntry<ulong>(0, 24, r8, null, Offsets.SkillManager.EnduranceBuffRestoration);
            var r26 = round4.AddEntry<ulong>(0, 25, r8, null, Offsets.SkillManager.EnduranceHands);
            var r27 = round4.AddEntry<ulong>(0, 26, r8, null, Offsets.SkillManager.StrengthBuffAimFatigue);
            var r28 = round4.AddEntry<ulong>(0, 27, r8, null, Offsets.SkillManager.StrengthBuffElite);
            var r29 = round4.AddEntry<ulong>(0, 28, r8, null, Offsets.SkillManager.StrengthBuffJumpHeightInc);
            var r30 = round4.AddEntry<ulong>(0, 29, r8, null, Offsets.SkillManager.StrengthBuffLiftWeightInc);
            var r31 = round4.AddEntry<ulong>(0, 30, r8, null, Offsets.SkillManager.StrengthBuffMeleeCrits);
            var r32 = round4.AddEntry<ulong>(0, 31, r8, null, Offsets.SkillManager.StrengthBuffMeleePowerInc);
            var r33 = round4.AddEntry<ulong>(0, 32, r8, null, Offsets.SkillManager.StrengthBuffSprintSpeedInc);
            var r34 = round4.AddEntry<ulong>(0, 33, r8, null, Offsets.SkillManager.StrengthBuffThrowDistanceInc);
            var r35 = round4.AddEntry<ulong>(0, 34, r8, null, Offsets.SkillManager.ThrowingStrengthBuff);
            var r36 = round4.AddEntry<ulong>(0, 35, r8, null, Offsets.SkillManager.ThrowingEliteBuff);
            var r37 = round4.AddEntry<ulong>(0, 36, r8, null, Offsets.SkillManager.VitalityBuffBleedStop);
            var r38 = round4.AddEntry<ulong>(0, 37, r8, null, Offsets.SkillManager.VitalityBuffRegeneration);
            var r39 = round4.AddEntry<ulong>(0, 38, r8, null, Offsets.SkillManager.VitalityBuffSurviobilityInc);
            var r40 = round4.AddEntry<ulong>(0, 39, r8, null, Offsets.SkillManager.SearchBuffSpeed);
            var r41 = round4.AddEntry<ulong>(0, 40, r8, null, Offsets.SkillManager.MetabolismMiscDebuffTime);
            var r42 = round4.AddEntry<ulong>(0, 41, r8, null, Offsets.SkillManager.MetabolismEliteBuffNoDyhydration);
            var r43 = round4.AddEntry<ulong>(0, 42, r8, null, Offsets.SkillManager.AttentionEliteLuckySearch);
            var r44 = round4.AddEntry<ulong>(0, 43, r8, null, Offsets.SkillManager.HealthBreakChanceRed);
            var r45 = round4.AddEntry<ulong>(0, 44, r8, null, Offsets.SkillManager.HealthEliteAbsorbDamage);
            var r46 = round4.AddEntry<ulong>(0, 45, r8, null, Offsets.SkillManager.HealthEnergy);
            var r47 = round4.AddEntry<ulong>(0, 46, r8, null, Offsets.SkillManager.SurgerySpeed);
            var r48 = round4.AddEntry<ulong>(0, 47, r8, null, Offsets.SkillManager.StressBerserk);
            var r49 = round4.AddEntry<ulong>(0, 48, r8, null, Offsets.SkillManager.StressPain);
            var r50 = round4.AddEntry<ulong>(0, 49, r8, null, Offsets.SkillManager.DrawElite);
            var r51 = round4.AddEntry<ulong>(0, 50, r8, null, Offsets.SkillManager.DrawSpeed);
            var r52 = round4.AddEntry<ulong>(0, 51, r8, null, Offsets.SkillManager.DrawSound);
            var r53 = round4.AddEntry<ulong>(0, 52, r8, null, Offsets.SkillManager.CovertMovementSpeed);
            var r54 = round4.AddEntry<ulong>(0, 53, r8, null, Offsets.SkillManager.CovertMovementSoundVolume);
            var r55 = round4.AddEntry<ulong>(0, 54, r8, null, Offsets.SkillManager.CovertMovementLoud);
            var r56 = round4.AddEntry<ulong>(0, 55, r8, null, Offsets.SkillManager.CovertMovementEquipment);
            var r57 = round4.AddEntry<ulong>(0, 56, r8, null, Offsets.SkillManager.CovertMovementElite);
            var r58 = round4.AddEntry<ulong>(0, 57, r8, null, Offsets.SkillManager.PerceptionHearing);
            var r59 = round4.AddEntry<ulong>(0, 58, r8, null, Offsets.SkillManager.PerceptionLootDot);
            var r60 = round5.AddEntry<float>(0, 59, r11, null, Offsets.SkillFloat.Value); //MagDrillsLoad first value
            var r61 = round5.AddEntry<float>(0, 60, r12, null, Offsets.SkillFloat.Value); //MagDrillsUnload
            var r62 = round5.AddEntry<float>(0, 61, r13, null, Offsets.SkillFloat.Value); //JumpStrength
            var r63 = round5.AddEntry<float>(0, 62, r14, null, Offsets.SkillFloat.Value); //WeightStrength
            var r64 = round5.AddEntry<float>(0, 63, r15, null, Offsets.SkillFloat.Value); //ThrowStrength
            var r65 = round5.AddEntry<bool>(0, 64, r16, null, Offsets.SkillBool.Value); //SearchDouble - bool
            var r66 = round5.AddEntry<float>(0, 65, r17, null, Offsets.SkillFloat.Value); //MagDrillsInventoryCheckAccuracy
            var r67 = round5.AddEntry<bool>(0, 66, r19, null, Offsets.SkillBool.Value); //MagDrillsInstantCheck  - bool
            var r68 = round5.AddEntry<float>(0, 68, r21, null, Offsets.SkillFloat.Value); //EnduranceBreathElite
            var r69 = round5.AddEntry<float>(0, 69, r22, null, Offsets.SkillFloat.Value); //EnduranceBuffBreathTimeInc
            var r70 = round5.AddEntry<float>(0, 70, r23, null, Offsets.SkillFloat.Value); //EnduranceBuffEnduranceInc
            var r71 = round5.AddEntry<float>(0, 71, r24, null, Offsets.SkillFloat.Value); //EnduranceBuffJumpCostRed
            var r72 = round5.AddEntry<float>(0, 72, r25, null, Offsets.SkillFloat.Value); //EnduranceBuffRestoration
            var r73 = round5.AddEntry<float>(0, 73, r26, null, Offsets.SkillFloat.Value); //EnduranceHands
            var r74 = round5.AddEntry<float>(0, 74, r27, null, Offsets.SkillFloat.Value); //StrengthBuffAimFatigue
            var r75 = round5.AddEntry<bool>(0, 75, r28, null, Offsets.SkillBool.Value); //StrengthBuffElite - bool
            var r76 = round5.AddEntry<float>(0, 76, r29, null, Offsets.SkillFloat.Value); //StrengthBuffJumpHeightInc
            var r77 = round5.AddEntry<float>(0, 77, r30, null, Offsets.SkillFloat.Value); //StrengthBuffLiftWeightInc
            var r78 = round5.AddEntry<float>(0, 78, r31, null, Offsets.SkillFloat.Value); //StrengthBuffMeleeCrits
            var r79 = round5.AddEntry<float>(0, 79, r32, null, Offsets.SkillFloat.Value); //StrengthBuffMeleePowerInc
            var r80 = round5.AddEntry<float>(0, 80, r33, null, Offsets.SkillFloat.Value); //StrengthBuffSprintSpeedInc
            var r81 = round5.AddEntry<float>(0, 81, r34, null, Offsets.SkillFloat.Value); //StrengthBuffThrowDistanceInc
            var r82 = round5.AddEntry<bool>(0, 82, r35, null, Offsets.SkillBool.Value); //ThrowingStrengthBuff - bool
            var r83 = round5.AddEntry<bool>(0, 83, r36, null, Offsets.SkillBool.Value); //ThrowingEliteBuff - bool
            var r84 = round5.AddEntry<bool>(0, 84, r37, null, Offsets.SkillBool.Value); //VitalityBuffBleedStop - bool
            var r85 = round5.AddEntry<bool>(0, 85, r38, null, Offsets.SkillBool.Value); //VitalityBuffRegeneration - bool
            var r86 = round5.AddEntry<float>(0, 86, r39, null, Offsets.SkillFloat.Value); //VitalityBuffSurviobilityInc
            var r87 = round5.AddEntry<float>(0, 87, r40, null, Offsets.SkillFloat.Value); //SearchBuffSpeed
            var r88 = round5.AddEntry<float>(0, 88, r41, null, Offsets.SkillFloat.Value); //MetabolismMiscDebuffTime
            var r89 = round5.AddEntry<bool>(0, 89, r42, null, Offsets.SkillBool.Value); //MetabolismEliteBuffNoDyhydration - bool
            var r90 = round5.AddEntry<bool>(0, 90, r43, null, Offsets.SkillBool.Value); //AttentionEliteLuckySearch - bool
            var r91 = round5.AddEntry<float>(0, 91, r44, null, Offsets.SkillFloat.Value); //HealthBreakChanceRed
            var r92 = round5.AddEntry<bool>(0, 92, r45, null, Offsets.SkillBool.Value); //HealthEliteAbsorbDamage - bool
            var r93 = round5.AddEntry<float>(0, 93, r46, null, Offsets.SkillFloat.Value); //HealthEnergy
            var r94 = round5.AddEntry<float>(0, 94, r47, null, Offsets.SkillFloat.Value); //SurgerySpeed
            var r95 = round5.AddEntry<bool>(0, 95, r48, null, Offsets.SkillBool.Value); //StressBerserk - bool
            var r96 = round5.AddEntry<float>(0, 96, r49, null, Offsets.SkillFloat.Value); //StressPain
            var r97 = round5.AddEntry<bool>(0, 97, r50, null, Offsets.SkillBool.Value); //DrawElite - bool
            var r98 = round5.AddEntry<float>(0, 98, r51, null, Offsets.SkillFloat.Value); //DrawSpeed
            var r99 = round5.AddEntry<float>(0, 99, r52, null, Offsets.SkillFloat.Value); //DrawSound
            var r100 = round5.AddEntry<float>(0, 100, r53, null, Offsets.SkillFloat.Value); //CovertMovementSpeed
            var r101 = round5.AddEntry<float>(0, 101, r54, null, Offsets.SkillFloat.Value); //CovertMovementSoundVolume
            var r102 = round5.AddEntry<float>(0, 102, r55, null, Offsets.SkillFloat.Value); //CovertMovementLoud
            var r103 = round5.AddEntry<float>(0, 103, r56, null, Offsets.SkillFloat.Value); //CovertMovementEquipment
            var r104 = round5.AddEntry<bool>(0, 104, r57, null, Offsets.SkillBool.Value);   //CovertMovementElite - bool
            var r105 = round5.AddEntry<float>(0, 105, r58, null, Offsets.SkillFloat.Value); //PerceptionHearing
            var r106 = round5.AddEntry<float>(0, 106, r59, null, Offsets.SkillFloat.Value); //PerceptionLootDot


            scatterMap.Execute();

            if (!scatterMap.Results[0][0].TryGetResult<ulong>(out var playerBase))
                return;
            if (!scatterMap.Results[0][1].TryGetResult<ulong>(out var playerProfile))
                return;
            if (!scatterMap.Results[0][2].TryGetResult<ulong>(out var movementContext))
                return;
            if (!scatterMap.Results[0][3].TryGetResult<ulong>(out var baseMovementState))
                return;
            if (!scatterMap.Results[0][4].TryGetResult<ulong>(out var physical))
                return;
            if (!scatterMap.Results[0][5].TryGetResult<ulong>(out var stamina))
                return;
            if (!scatterMap.Results[0][6].TryGetResult<ulong>(out var handsStamina))
                return;
            if (!scatterMap.Results[0][7].TryGetResult<ulong>(out var skillsManager))
                return;
            if (!scatterMap.Results[0][8].TryGetResult<ulong>(out var proceduralWeaponAnimation))
                return;
            if (!scatterMap.Results[0][9].TryGetResult<bool>(out var isADS))
                return;
            
            scatterMap.Results[0][10].TryGetResult<ulong>(out var magDrillsLoad);
            scatterMap.Results[0][11].TryGetResult<ulong>(out var magDrillsUnload);
            scatterMap.Results[0][12].TryGetResult<ulong>(out var jumpStrength);
            scatterMap.Results[0][13].TryGetResult<ulong>(out var weightStrength);
            scatterMap.Results[0][14].TryGetResult<ulong>(out var throwStrength);
            scatterMap.Results[0][15].TryGetResult<ulong>(out var searchDouble);
            scatterMap.Results[0][16].TryGetResult<ulong>(out var magDrillsInventoryCheckAccuracy);
            scatterMap.Results[0][18].TryGetResult<ulong>(out var magDrillsInstantCheck);
            scatterMap.Results[0][20].TryGetResult<ulong>(out var enduranceBreathElite);
            scatterMap.Results[0][21].TryGetResult<ulong>(out var enduranceBuffBreathTimeInc);
            scatterMap.Results[0][22].TryGetResult<ulong>(out var enduranceBuffEnduranceInc);
            scatterMap.Results[0][23].TryGetResult<ulong>(out var enduranceBuffJumpCostRed);
            scatterMap.Results[0][24].TryGetResult<ulong>(out var enduranceBuffRestoration);
            scatterMap.Results[0][25].TryGetResult<ulong>(out var enduranceHands);
            scatterMap.Results[0][26].TryGetResult<ulong>(out var strengthBuffAimFatigue);
            scatterMap.Results[0][27].TryGetResult<ulong>(out var strengthBuffElite);
            scatterMap.Results[0][28].TryGetResult<ulong>(out var strengthBuffJumpHeightInc);
            scatterMap.Results[0][29].TryGetResult<ulong>(out var strengthBuffLiftWeightInc);
            scatterMap.Results[0][30].TryGetResult<ulong>(out var strengthBuffMeleeCrits);
            scatterMap.Results[0][31].TryGetResult<ulong>(out var strengthBuffMeleePowerInc);
            scatterMap.Results[0][32].TryGetResult<ulong>(out var strengthBuffSprintSpeedInc);
            scatterMap.Results[0][33].TryGetResult<ulong>(out var strengthBuffThrowDistanceInc);
            scatterMap.Results[0][34].TryGetResult<ulong>(out var throwingStrengthBuff);
            scatterMap.Results[0][35].TryGetResult<ulong>(out var throwingEliteBuff);
            scatterMap.Results[0][36].TryGetResult<ulong>(out var vitalityBuffBleedStop);
            scatterMap.Results[0][37].TryGetResult<ulong>(out var vitalityBuffRegeneration);
            scatterMap.Results[0][38].TryGetResult<ulong>(out var vitalityBuffSurviobilityInc);
            scatterMap.Results[0][39].TryGetResult<ulong>(out var searchBuffSpeed);
            scatterMap.Results[0][40].TryGetResult<ulong>(out var metabolismMiscDebuffTime);
            scatterMap.Results[0][41].TryGetResult<ulong>(out var metabolismEliteBuffNoDyhydration);
            scatterMap.Results[0][42].TryGetResult<ulong>(out var attentionEliteLuckySearch);
            scatterMap.Results[0][43].TryGetResult<ulong>(out var healthBreakChanceRed);
            scatterMap.Results[0][44].TryGetResult<ulong>(out var healthEliteAbsorbDamage);
            scatterMap.Results[0][45].TryGetResult<ulong>(out var healthEnergy);
            scatterMap.Results[0][46].TryGetResult<ulong>(out var surgerySpeed);
            scatterMap.Results[0][47].TryGetResult<ulong>(out var stressBerserk);
            scatterMap.Results[0][48].TryGetResult<ulong>(out var stressPain);
            scatterMap.Results[0][49].TryGetResult<ulong>(out var drawElite);
            scatterMap.Results[0][50].TryGetResult<ulong>(out var drawSpeed);
            scatterMap.Results[0][51].TryGetResult<ulong>(out var drawSound);
            scatterMap.Results[0][52].TryGetResult<ulong>(out var covertMovementSpeed);
            scatterMap.Results[0][53].TryGetResult<ulong>(out var covertMovementSoundVolume);
            scatterMap.Results[0][54].TryGetResult<ulong>(out var covertMovementLoud);
            scatterMap.Results[0][55].TryGetResult<ulong>(out var covertMovementEquipment);
            scatterMap.Results[0][56].TryGetResult<ulong>(out var covertMovementElite);
            scatterMap.Results[0][57].TryGetResult<ulong>(out var perceptionHearing);
            scatterMap.Results[0][58].TryGetResult<ulong>(out var perceptionLootDot);
            //values
            scatterMap.Results[0][59].TryGetResult<float>(out var magDrillsLoadValue);
            scatterMap.Results[0][60].TryGetResult<float>(out var magDrillsUnloadValue);
            scatterMap.Results[0][61].TryGetResult<float>(out var jumpStrengthValue);
            scatterMap.Results[0][62].TryGetResult<float>(out var weightStrengthValue);
            scatterMap.Results[0][63].TryGetResult<float>(out var throwStrengthValue);
            scatterMap.Results[0][64].TryGetResult<bool>(out var searchDoubleValue);
            scatterMap.Results[0][65].TryGetResult<float>(out var magDrillsInventoryCheckAccuracyValue);
            scatterMap.Results[0][66].TryGetResult<bool>(out var magDrillsInstantCheckValue);
            scatterMap.Results[0][68].TryGetResult<float>(out var enduranceBreathEliteValue);
            scatterMap.Results[0][69].TryGetResult<float>(out var enduranceBuffBreathTimeIncValue);
            scatterMap.Results[0][70].TryGetResult<float>(out var enduranceBuffEnduranceIncValue);
            scatterMap.Results[0][71].TryGetResult<float>(out var enduranceBuffJumpCostRedValue);
            scatterMap.Results[0][72].TryGetResult<float>(out var enduranceBuffRestorationValue);
            scatterMap.Results[0][73].TryGetResult<float>(out var enduranceHandsValue);
            scatterMap.Results[0][74].TryGetResult<float>(out var strengthBuffAimFatigueValue);
            scatterMap.Results[0][75].TryGetResult<bool>(out var strengthBuffEliteValue);
            scatterMap.Results[0][76].TryGetResult<float>(out var strengthBuffJumpHeightIncValue);
            scatterMap.Results[0][77].TryGetResult<float>(out var strengthBuffLiftWeightIncValue);
            scatterMap.Results[0][78].TryGetResult<float>(out var strengthBuffMeleeCritsValue);
            scatterMap.Results[0][79].TryGetResult<float>(out var strengthBuffMeleePowerIncValue);
            scatterMap.Results[0][80].TryGetResult<float>(out var strengthBuffSprintSpeedIncValue);
            scatterMap.Results[0][81].TryGetResult<float>(out var strengthBuffThrowDistanceIncValue);
            scatterMap.Results[0][82].TryGetResult<bool>(out var throwingStrengthBuffValue);
            scatterMap.Results[0][83].TryGetResult<bool>(out var throwingEliteBuffValue);
            scatterMap.Results[0][84].TryGetResult<bool>(out var vitalityBuffBleedStopValue);
            scatterMap.Results[0][85].TryGetResult<bool>(out var vitalityBuffRegenerationValue);
            scatterMap.Results[0][86].TryGetResult<float>(out var vitalityBuffSurviobilityIncValue);
            scatterMap.Results[0][87].TryGetResult<float>(out var searchBuffSpeedValue);
            scatterMap.Results[0][88].TryGetResult<float>(out var metabolismMiscDebuffTimeValue);
            scatterMap.Results[0][89].TryGetResult<bool>(out var metabolismEliteBuffNoDyhydrationValue);
            scatterMap.Results[0][90].TryGetResult<bool>(out var attentionEliteLuckySearchValue);
            scatterMap.Results[0][91].TryGetResult<float>(out var healthBreakChanceRedValue);
            scatterMap.Results[0][92].TryGetResult<bool>(out var healthEliteAbsorbDamageValue);
            scatterMap.Results[0][93].TryGetResult<float>(out var healthEnergyValue);
            scatterMap.Results[0][94].TryGetResult<float>(out var surgerySpeedValue);
            scatterMap.Results[0][95].TryGetResult<bool>(out var stressBerserkValue);
            scatterMap.Results[0][96].TryGetResult<float>(out var stressPainValue);
            scatterMap.Results[0][97].TryGetResult<bool>(out var drawEliteValue);
            scatterMap.Results[0][98].TryGetResult<float>(out var drawSpeedValue);
            scatterMap.Results[0][99].TryGetResult<float>(out var drawSoundValue);
            scatterMap.Results[0][100].TryGetResult<float>(out var covertMovementSpeedValue);
            scatterMap.Results[0][101].TryGetResult<float>(out var covertMovementSoundVolumeValue);
            scatterMap.Results[0][102].TryGetResult<float>(out var covertMovementLoudValue);
            scatterMap.Results[0][103].TryGetResult<float>(out var covertMovementEquipmentValue);
            scatterMap.Results[0][104].TryGetResult<bool>(out var covertMovementEliteValue);
            scatterMap.Results[0][105].TryGetResult<float>(out var perceptionHearingValue);
            scatterMap.Results[0][106].TryGetResult<float>(out var perceptionLootDotValue);

            //Console.WriteLine("MagDrillsLoad: " + magDrillsLoadValue);
            //Console.WriteLine("MagDrillsUnload: " + magDrillsUnloadValue);
            //Console.WriteLine("JumpStrength: " + jumpStrengthValue);
            //Console.WriteLine("WeightStrength: " + weightStrengthValue);
            //Console.WriteLine("ThrowStrength: " + throwStrengthValue);
            //Console.WriteLine("SearchDouble: " + searchDoubleValue);
            //Console.WriteLine("MagDrillsInventoryCheckAccuracy: " + magDrillsInventoryCheckAccuracyValue);
            //Console.WriteLine("MagDrillsInstantCheck: " + magDrillsInstantCheckValue);
            //Console.WriteLine("EnduranceBreathElite: " + enduranceBreathEliteValue);
            //Console.WriteLine("EnduranceBuffBreathTimeInc: " + enduranceBuffBreathTimeIncValue);
            //Console.WriteLine("EnduranceBuffEnduranceInc: " + enduranceBuffEnduranceIncValue);
            //Console.WriteLine("EnduranceBuffJumpCostRed: " + enduranceBuffJumpCostRedValue);
            //Console.WriteLine("EnduranceBuffRestoration: " + enduranceBuffRestorationValue);
            //Console.WriteLine("EnduranceHands: " + enduranceHandsValue);
            //Console.WriteLine("StrengthBuffAimFatigue: " + strengthBuffAimFatigueValue);
            //Console.WriteLine("StrengthBuffElite: " + strengthBuffEliteValue);
            //Console.WriteLine("StrengthBuffJumpHeightInc: " + strengthBuffJumpHeightIncValue);
            //Console.WriteLine("StrengthBuffLiftWeightInc: " + strengthBuffLiftWeightIncValue);
            //Console.WriteLine("StrengthBuffMeleeCrits: " + strengthBuffMeleeCritsValue);
            //Console.WriteLine("StrengthBuffMeleePowerInc: " + strengthBuffMeleePowerIncValue);
            //Console.WriteLine("StrengthBuffSprintSpeedInc: " + strengthBuffSprintSpeedIncValue);
            //Console.WriteLine("StrengthBuffThrowDistanceInc: " + strengthBuffThrowDistanceIncValue);
            //Console.WriteLine("ThrowingStrengthBuff: " + throwingStrengthBuffValue);
            //Console.WriteLine("ThrowingEliteBuff: " + throwingEliteBuffValue);
            //Console.WriteLine("VitalityBuffBleedStop: " + vitalityBuffBleedStopValue);
            //Console.WriteLine("VitalityBuffRegeneration: " + vitalityBuffRegenerationValue);
            //Console.WriteLine("VitalityBuffSurviobilityInc: " + vitalityBuffSurviobilityIncValue);
            //Console.WriteLine("SearchBuffSpeed: " + searchBuffSpeedValue);
            //Console.WriteLine("MetabolismMiscDebuffTime: " + metabolismMiscDebuffTimeValue);
            //Console.WriteLine("MetabolismEliteBuffNoDyhydration: " + metabolismEliteBuffNoDyhydrationValue);
            //Console.WriteLine("AttentionEliteLuckySearch: " + attentionEliteLuckySearchValue);
            //Console.WriteLine("HealthBreakChanceRed: " + healthBreakChanceRedValue);
            //Console.WriteLine("HealthEliteAbsorbDamage: " + healthEliteAbsorbDamageValue);
            //Console.WriteLine("HealthEnergy: " + healthEnergyValue);
            //Console.WriteLine("SurgerySpeed: " + surgerySpeedValue);
            //Console.WriteLine("StressBerserk: " + stressBerserkValue);
            //Console.WriteLine("StressPain: " + stressPainValue);
            //Console.WriteLine("DrawElite: " + drawEliteValue);
            //Console.WriteLine("DrawSpeed: " + drawSpeedValue);
            //Console.WriteLine("DrawSound: " + drawSoundValue);
            //Console.WriteLine("CovertMovementSpeed: " + covertMovementSpeedValue);
            //Console.WriteLine("CovertMovementSoundVolume: " + covertMovementSoundVolumeValue);
            //Console.WriteLine("CovertMovementLoud: " + covertMovementLoudValue);
            //Console.WriteLine("CovertMovementEquipment: " + covertMovementEquipmentValue);
            //Console.WriteLine("CovertMovementElite: " + covertMovementEliteValue);
            //Console.WriteLine("PerceptionHearing: " + perceptionHearingValue);
            //Console.WriteLine("PerceptionLootDot: " + perceptionLootDotValue);

            this.playerBase = playerBase;
            this.playerProfile = playerProfile;
            this.movementContext = movementContext;
            this.baseMovementState = baseMovementState;
            this.physical = physical;
            this.stamina = stamina;
            this.handsStamina = handsStamina;
            this.skillsManager = skillsManager;
            this.proceduralWeaponAnimation = proceduralWeaponAnimation;
            this.isADS = isADS;

            this.magDrillsLoad = magDrillsLoad;
            this.magDrillsUnload = magDrillsUnload;
            this.jumpStrength = jumpStrength;
            this.weightStrength = weightStrength;
            this.throwStrength = throwStrength;
            this.searchDouble = searchDouble;
            this.magDrillsInventoryCheckAccuracy = magDrillsInventoryCheckAccuracy;
            this.magDrillsInventoryCheckSpeed = magDrillsInventoryCheckSpeed;
            this.magDrillsInstantCheck = magDrillsInstantCheck;
            this.magDrillsLoadProgression = magDrillsLoadProgression;
            this.enduranceBreathElite = enduranceBreathElite;
            this.enduranceBuffBreathTimeInc = enduranceBuffBreathTimeInc;
            this.enduranceBuffEnduranceInc = enduranceBuffEnduranceInc;
            this.enduranceBuffJumpCostRed = enduranceBuffJumpCostRed;
            this.enduranceBuffRestoration = enduranceBuffRestoration;
            this.enduranceHands = enduranceHands;
            this.strengthBuffAimFatigue = strengthBuffAimFatigue;
            this.strengthBuffElite = strengthBuffElite;
            this.strengthBuffJumpHeightInc = strengthBuffJumpHeightInc;
            this.strengthBuffLiftWeightInc = strengthBuffLiftWeightInc;
            this.strengthBuffMeleeCrits = strengthBuffMeleeCrits;
            this.strengthBuffMeleePowerInc = strengthBuffMeleePowerInc;
            this.strengthBuffSprintSpeedInc = strengthBuffSprintSpeedInc;
            this.strengthBuffThrowDistanceInc = strengthBuffThrowDistanceInc;
            this.throwingStrengthBuff = throwingStrengthBuff;
            this.throwingEliteBuff = throwingEliteBuff;
            this.vitalityBuffBleedStop = vitalityBuffBleedStop;
            this.vitalityBuffRegeneration = vitalityBuffRegeneration;
            this.vitalityBuffSurviobilityInc = vitalityBuffSurviobilityInc;
            this.searchBuffSpeed = searchBuffSpeed;
            this.metabolismMiscDebuffTime = metabolismMiscDebuffTime;
            this.metabolismEliteBuffNoDyhydration = metabolismEliteBuffNoDyhydration;
            this.attentionEliteLuckySearch = attentionEliteLuckySearch;
            this.healthBreakChanceRed = healthBreakChanceRed;
            this.healthEliteAbsorbDamage = healthEliteAbsorbDamage;
            this.healthEnergy = healthEnergy;
            this.surgerySpeed = surgerySpeed;
            this.stressBerserk = stressBerserk;
            this.stressPain = stressPain;
            this.drawElite = drawElite;
            this.drawSpeed = drawSpeed;
            this.drawSound = drawSound;
            this.covertMovementSpeed = covertMovementSpeed;
            this.covertMovementSoundVolume = covertMovementSoundVolume;
            this.covertMovementLoud = covertMovementLoud;
            this.covertMovementEquipment = covertMovementEquipment;
            this.covertMovementElite = covertMovementElite;
            this.perceptionHearing = perceptionHearing;
            this.perceptionLootDot = perceptionLootDot;
            
            this.OriginalValues = new Dictionary<string, float>()
            {

                ["SearchDouble"] = -1,
                ["Mask"] = 125,
                ["AimingSpeed"] = 1,
                ["AimingSpeedSway"] = 0.2f,
                ["StaminaCapacity"] = -1,
                ["HandStaminaCapacity"] = -1,

                ["BreathEffectorIntensity"] = -1,
                ["WalkEffectorIntensity"] = -1,
                ["MotionEffectorIntensity"] = -1,
                ["ForceEffectorIntensity"] = -1,


                ["MagDrillsLoad"] = magDrillsLoadValue,
                ["MagDrillsUnload"] = magDrillsUnloadValue,
                ["JumpStrength"] = jumpStrengthValue,
                ["WeightStrength"] = weightStrengthValue,
                ["ThrowStrength"] = throwStrengthValue,
                ["MagDrillsInventoryCheckAccuracy"] = magDrillsInventoryCheckAccuracyValue,
                ["EnduranceBreathElite"] = enduranceBreathEliteValue,
                ["EnduranceBuffBreathTimeInc"] = enduranceBuffBreathTimeIncValue,
                ["EnduranceBuffEnduranceInc"] = enduranceBuffEnduranceIncValue,
                ["EnduranceBuffJumpCostRed"] = enduranceBuffJumpCostRedValue,
                ["EnduranceBuffRestoration"] = enduranceBuffRestorationValue,
                ["EnduranceHands"] = enduranceHandsValue,
                ["StrengthBuffAimFatigue"] = strengthBuffAimFatigueValue,
                ["StrengthBuffJumpHeightInc"] = strengthBuffJumpHeightIncValue,
                ["StrengthBuffLiftWeightInc"] = strengthBuffLiftWeightIncValue,
                ["StrengthBuffMeleeCrits"] = strengthBuffMeleeCritsValue,
                ["StrengthBuffMeleePowerInc"] = strengthBuffMeleePowerIncValue,
                ["StrengthBuffSprintSpeedInc"] = strengthBuffSprintSpeedIncValue,
                ["StrengthBuffThrowDistanceInc"] = strengthBuffThrowDistanceIncValue,
                ["VitalityBuffSurviobilityInc"] = vitalityBuffSurviobilityIncValue,
                ["SearchBuffSpeed"] = searchBuffSpeedValue,
                ["MetabolismMiscDebuffTime"] = metabolismMiscDebuffTimeValue,
                ["HealthBreakChanceRed"] = healthBreakChanceRedValue,
                ["HealthEnergy"] = healthEnergyValue,
                ["SurgerySpeed"] = surgerySpeedValue,
                ["StressPain"] = stressPainValue,
                ["DrawSpeed"] = drawSpeedValue,
                ["DrawSound"] = drawSoundValue,
                ["CovertMovementSpeed"] = covertMovementSpeedValue,
                ["CovertMovementSoundVolume"] = covertMovementSoundVolumeValue,
                ["CovertMovementLoud"] = covertMovementLoudValue,
                ["CovertMovementEquipment"] = covertMovementEquipmentValue,
                ["PerceptionHearing"] = perceptionHearingValue,
                ["PerceptionLootDot"] = perceptionLootDotValue
            };

            this.OriginalBools = new Dictionary<string, bool>()
            {
                ["SearchDouble"] = searchDoubleValue,
                ["MagDrillsInstantCheck"] = magDrillsInstantCheckValue,
                ["StrengthBuffElite"] = strengthBuffEliteValue,
                ["ThrowingStrengthBuff"] = throwingStrengthBuffValue,
                ["ThrowingEliteBuff"] = throwingEliteBuffValue,
                ["VitalityBuffBleedStop"] = vitalityBuffBleedStopValue,
                ["VitalityBuffRegeneration"] = vitalityBuffRegenerationValue,
                ["MetabolismEliteBuffNoDyhydration"] = metabolismEliteBuffNoDyhydrationValue,
                ["AttentionEliteLuckySearch"] = attentionEliteLuckySearchValue,
                ["HealthEliteAbsorbDamage"] = healthEliteAbsorbDamageValue,
                ["StressBerserk"] = stressBerserkValue,
                ["DrawElite"] = drawEliteValue,
                ["CovertMovementElite"] = covertMovementEliteValue
            };

            this.MaxedFloats = new Dictionary<string, float>()
            {
                ["MagDrillsLoad"] = 30f,
                ["MagDrillsUnload"] = 30f,
                ["JumpStrength"] = 0.04f,
                ["WeightStrength"] = 0.06f,
                ["ThrowStrength"] = 0.04f,
                ["MagDrillsInventoryCheckAccuracy"] = 2f,
                ["MagDrillsInventoryCheckSpeed"] = 40f,
                ["MagDrillsLoadProgression"] = 30f,
                ["EnduranceBreathElite"] = 1f,
                ["EnduranceBuffBreathTimeInc"] = 1f,
                ["EnduranceBuffEnduranceInc"] = 0.7f,
                ["EnduranceBuffJumpCostRed"] = 0.3f,
                ["EnduranceBuffRestoration"] = 0.75f,
                ["EnduranceHands"] = 0.5f,
                ["StrengthBuffAimFatigue"] = 0.5f,
                ["StrengthBuffJumpHeightInc"] = 0.2f,
                ["StrengthBuffLiftWeightInc"] = 0.3f,
                ["StrengthBuffMeleeCrits"] = 0.5f,
                ["StrengthBuffMeleePowerInc"] = 0.3f,
                ["StrengthBuffSprintSpeedInc"] = 0.2f,
                ["StrengthBuffThrowDistanceInc"] = 0.2f,
                ["VitalityBuffSurviobilityInc"] = 0.2f,
                ["SearchBuffSpeed"] = 0.5f,
                ["MetabolismMiscDebuffTime"] = 0.5f,
                ["HealthBreakChanceRed"] = 0.6f,
                ["HealthEnergy"] = 0.3f,
                ["SurgerySpeed"] = 0.4f,
                ["StressPain"] = 0.5f,
                ["DrawElite"] = 0.5f,
                ["DrawSpeed"] = 0.5f,
                ["DrawSound"] = 0.5f,
                ["CovertMovementSpeed"] = 1.5f,
                ["CovertMovementSoundVolume"] = 0.6f,
                ["CovertMovementLoud"] = 0.6f,
                ["CovertMovementEquipment"] = 0.6f,
                ["PerceptionHearing"] = 0.15f,
                ["PerceptionLootDot"] = 1f 
            };


        }

        public void SetMaxAllSkills(bool on)
        {
            if (on)
            {
                var entries = new List<IScatterWriteEntry>
                {
                    new ScatterWriteDataEntry<float>(this.magDrillsLoad + Offsets.SkillFloat.Value, this.MaxedFloats["MagDrillsLoad"]),
                    new ScatterWriteDataEntry<float>(this.magDrillsUnload + Offsets.SkillFloat.Value, this.MaxedFloats["MagDrillsUnload"]),
                    new ScatterWriteDataEntry<float>(this.jumpStrength + Offsets.SkillFloat.Value, this.MaxedFloats["JumpStrength"]),
                    new ScatterWriteDataEntry<float>(this.weightStrength + Offsets.SkillFloat.Value, this.MaxedFloats["WeightStrength"]),
                    new ScatterWriteDataEntry<float>(this.throwStrength + Offsets.SkillFloat.Value, this.MaxedFloats["ThrowStrength"]),
                    new ScatterWriteDataEntry<float>(this.magDrillsInventoryCheckAccuracy + Offsets.SkillFloat.Value, this.MaxedFloats["MagDrillsInventoryCheckAccuracy"]),
                    //new ScatterWriteDataEntry<float>(this.magDrillsInventoryCheckSpeed + Offsets.SkillFloat.Value, this.MaxedFloats["MagDrillsInventoryCheckSpeed"]),
                    //new ScatterWriteDataEntry<float>(this.magDrillsLoadProgression, this.MaxedFloats["MagDrillsLoadProgression"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBreathElite + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceBreathElite"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffBreathTimeInc + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceBuffBreathTimeInc"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffEnduranceInc + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceBuffEnduranceInc"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffJumpCostRed + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceBuffJumpCostRed"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffRestoration + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceBuffRestoration"]),
                    new ScatterWriteDataEntry<float>(this.enduranceHands + Offsets.SkillFloat.Value, this.MaxedFloats["EnduranceHands"]),
                    //new ScatterWriteDataEntry<float>(this.strengthBuffAimFatigue, this.MaxedFloats["StrengthBuffAimFatigue"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffJumpHeightInc + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffJumpHeightInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffLiftWeightInc + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffLiftWeightInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffMeleeCrits + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffMeleeCrits"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffMeleePowerInc + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffMeleePowerInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffSprintSpeedInc + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffSprintSpeedInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffThrowDistanceInc + Offsets.SkillFloat.Value, this.MaxedFloats["StrengthBuffThrowDistanceInc"]),
                    new ScatterWriteDataEntry<float>(this.vitalityBuffSurviobilityInc + Offsets.SkillFloat.Value, this.MaxedFloats["VitalityBuffSurviobilityInc"]),
                    new ScatterWriteDataEntry<float>(this.searchBuffSpeed + Offsets.SkillFloat.Value, this.MaxedFloats["SearchBuffSpeed"]),
                    new ScatterWriteDataEntry<float>(this.metabolismMiscDebuffTime + Offsets.SkillFloat.Value, this.MaxedFloats["MetabolismMiscDebuffTime"]),
                    new ScatterWriteDataEntry<float>(this.healthBreakChanceRed + Offsets.SkillFloat.Value, this.MaxedFloats["HealthBreakChanceRed"]),
                    new ScatterWriteDataEntry<float>(this.healthEnergy + Offsets.SkillFloat.Value, this.MaxedFloats["HealthEnergy"]),
                    new ScatterWriteDataEntry<float>(this.surgerySpeed + Offsets.SkillFloat.Value, this.MaxedFloats["SurgerySpeed"]),
                    new ScatterWriteDataEntry<float>(this.stressPain + Offsets.SkillFloat.Value, this.MaxedFloats["StressPain"]),
                    new ScatterWriteDataEntry<float>(this.drawSpeed + Offsets.SkillFloat.Value, this.MaxedFloats["DrawSpeed"]),
                    new ScatterWriteDataEntry<float>(this.drawSound + Offsets.SkillFloat.Value, this.MaxedFloats["DrawSound"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementSpeed + Offsets.SkillFloat.Value, this.MaxedFloats["CovertMovementSpeed"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementSoundVolume + Offsets.SkillFloat.Value, this.MaxedFloats["CovertMovementSoundVolume"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementLoud + Offsets.SkillFloat.Value, this.MaxedFloats["CovertMovementLoud"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementEquipment + Offsets.SkillFloat.Value, this.MaxedFloats["CovertMovementEquipment"]),
                    new ScatterWriteDataEntry<float>(this.perceptionHearing + Offsets.SkillFloat.Value, this.MaxedFloats["PerceptionHearing"]),
                    new ScatterWriteDataEntry<float>(this.perceptionLootDot + Offsets.SkillFloat.Value, this.MaxedFloats["PerceptionLootDot"]),
                    new ScatterWriteDataEntry<bool>(this.searchDouble + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.magDrillsInstantCheck + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.strengthBuffElite + Offsets.SkillBool.Value, true),
                    //new ScatterWriteDataEntry<bool>(this.throwingStrengthBuff, true),
                    //new ScatterWriteDataEntry<bool>(this.throwingEliteBuff, true),
                    new ScatterWriteDataEntry<bool>(this.vitalityBuffBleedStop + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.vitalityBuffRegeneration + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.metabolismEliteBuffNoDyhydration + Offsets.SkillBool.Value, true),
                    //new ScatterWriteDataEntry<bool>(this.attentionEliteLuckySearch, true),
                    new ScatterWriteDataEntry<bool>(this.healthEliteAbsorbDamage + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.stressBerserk + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.drawElite + Offsets.SkillBool.Value, true),
                    new ScatterWriteDataEntry<bool>(this.covertMovementElite + Offsets.SkillBool.Value, true)
                };

                Memory.WriteScatter(entries);
            }
            else if (!on)
            {
                var entries = new List<IScatterWriteEntry>
                {
                    new ScatterWriteDataEntry<float>(this.magDrillsLoad + Offsets.SkillFloat.Value, this.OriginalValues["MagDrillsLoad"]),
                    new ScatterWriteDataEntry<float>(this.magDrillsUnload + Offsets.SkillFloat.Value, this.OriginalValues["MagDrillsUnload"]),
                    new ScatterWriteDataEntry<float>(this.jumpStrength + Offsets.SkillFloat.Value, this.OriginalValues["JumpStrength"]),
                    new ScatterWriteDataEntry<float>(this.weightStrength + Offsets.SkillFloat.Value, this.OriginalValues["WeightStrength"]),
                    new ScatterWriteDataEntry<float>(this.throwStrength + Offsets.SkillFloat.Value, this.OriginalValues["ThrowStrength"]),
                    new ScatterWriteDataEntry<float>(this.magDrillsInventoryCheckAccuracy + Offsets.SkillFloat.Value, this.OriginalValues["MagDrillsInventoryCheckAccuracy"]),
                    //new ScatterWriteDataEntry<float>(this.magDrillsInventoryCheckSpeed + Offsets.SkillFloat.Value, this.OriginalValues["MagDrillsInventoryCheckSpeed"]),
                    //new ScatterWriteDataEntry<float>(this.magDrillsLoadProgression + Offsets.SkillFloat.Value, this.OriginalValues["MagDrillsLoadProgression"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBreathElite + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceBreathElite"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffBreathTimeInc + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceBuffBreathTimeInc"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffEnduranceInc + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceBuffEnduranceInc"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffJumpCostRed + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceBuffJumpCostRed"]),
                    new ScatterWriteDataEntry<float>(this.enduranceBuffRestoration + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceBuffRestoration"]),
                    new ScatterWriteDataEntry<float>(this.enduranceHands + Offsets.SkillFloat.Value, this.OriginalValues["EnduranceHands"]),
                    //new ScatterWriteDataEntry<float>(this.strengthBuffAimFatigue, this.OriginalValues["StrengthBuffAimFatigue"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffJumpHeightInc + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffJumpHeightInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffLiftWeightInc + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffLiftWeightInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffMeleeCrits + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffMeleeCrits"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffMeleePowerInc + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffMeleePowerInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffSprintSpeedInc + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffSprintSpeedInc"]),
                    new ScatterWriteDataEntry<float>(this.strengthBuffThrowDistanceInc + Offsets.SkillFloat.Value, this.OriginalValues["StrengthBuffThrowDistanceInc"]),
                    new ScatterWriteDataEntry<float>(this.vitalityBuffSurviobilityInc + Offsets.SkillFloat.Value, this.OriginalValues["VitalityBuffSurviobilityInc"]),
                    new ScatterWriteDataEntry<float>(this.searchBuffSpeed + Offsets.SkillFloat.Value, this.OriginalValues["SearchBuffSpeed"]),
                    new ScatterWriteDataEntry<float>(this.metabolismMiscDebuffTime + Offsets.SkillFloat.Value, this.OriginalValues["MetabolismMiscDebuffTime"]),
                    new ScatterWriteDataEntry<float>(this.healthBreakChanceRed + Offsets.SkillFloat.Value, this.OriginalValues["HealthBreakChanceRed"]),
                    new ScatterWriteDataEntry<float>(this.healthEnergy + Offsets.SkillFloat.Value, this.OriginalValues["HealthEnergy"]),
                    new ScatterWriteDataEntry<float>(this.surgerySpeed + Offsets.SkillFloat.Value, this.OriginalValues["SurgerySpeed"]),
                    new ScatterWriteDataEntry<float>(this.stressPain + Offsets.SkillFloat.Value, this.OriginalValues["StressPain"]),
                    new ScatterWriteDataEntry<float>(this.drawSpeed + Offsets.SkillFloat.Value, this.OriginalValues["DrawSpeed"]),
                    new ScatterWriteDataEntry<float>(this.drawSound + Offsets.SkillFloat.Value, this.OriginalValues["DrawSound"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementSpeed + Offsets.SkillFloat.Value, this.OriginalValues["CovertMovementSpeed"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementSoundVolume + Offsets.SkillFloat.Value, this.OriginalValues["CovertMovementSoundVolume"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementLoud + Offsets.SkillFloat.Value, this.OriginalValues["CovertMovementLoud"]),
                    new ScatterWriteDataEntry<float>(this.covertMovementEquipment + Offsets.SkillFloat.Value, this.OriginalValues["CovertMovementEquipment"]),
                    new ScatterWriteDataEntry<float>(this.perceptionHearing + Offsets.SkillFloat.Value, this.OriginalValues["PerceptionHearing"]),
                    new ScatterWriteDataEntry<float>(this.perceptionLootDot + Offsets.SkillFloat.Value, this.OriginalValues["PerceptionLootDot"]),
                    new ScatterWriteDataEntry<bool>(this.searchDouble + Offsets.SkillBool.Value, this.OriginalBools["SearchDouble"]),
                    new ScatterWriteDataEntry<bool>(this.magDrillsInstantCheck + Offsets.SkillBool.Value, this.OriginalBools["MagDrillsInstantCheck"]),
                    new ScatterWriteDataEntry<bool>(this.strengthBuffElite + Offsets.SkillBool.Value, this.OriginalBools["StrengthBuffElite"]),
                    //new ScatterWriteDataEntry<bool>(this.throwingStrengthBuff, this.OriginalBools["ThrowingStrengthBuff"]),
                    //new ScatterWriteDataEntry<bool>(this.throwingEliteBuff, this.OriginalBools["ThrowingEliteBuff"]),
                    new ScatterWriteDataEntry<bool>(this.vitalityBuffBleedStop + Offsets.SkillBool.Value, this.OriginalBools["VitalityBuffBleedStop"]),
                    new ScatterWriteDataEntry<bool>(this.vitalityBuffRegeneration + Offsets.SkillBool.Value, this.OriginalBools["VitalityBuffRegeneration"]),
                    new ScatterWriteDataEntry<bool>(this.metabolismEliteBuffNoDyhydration + Offsets.SkillBool.Value, this.OriginalBools["MetabolismEliteBuffNoDyhydration"]),
                    //new ScatterWriteDataEntry<bool>(this.attentionEliteLuckySearch, this.OriginalBools["AttentionEliteLuckySearch"]),
                    new ScatterWriteDataEntry<bool>(this.healthEliteAbsorbDamage + Offsets.SkillBool.Value, this.OriginalBools["HealthEliteAbsorbDamage"]),
                    new ScatterWriteDataEntry<bool>(this.stressBerserk + Offsets.SkillBool.Value, this.OriginalBools["StressBerserk"]),
                    new ScatterWriteDataEntry<bool>(this.drawElite + Offsets.SkillBool.Value, this.OriginalBools["DrawElite"]),
                    new ScatterWriteDataEntry<bool>(this.covertMovementElite + Offsets.SkillBool.Value, this.OriginalBools["CovertMovementElite"])
                };

                Memory.WriteScatter(entries);
            }


        }

        /// <summary>
        /// Enables / disables weapon recoil
        /// </summary>
        public void SetNoRecoilSway(bool on)
        {
            var mask = Memory.ReadValue<int>(this.proceduralWeaponAnimation + Offsets.ProceduralWeaponAnimation.Mask);

            if (on && mask != 0)
            {
                Memory.WriteValue(this.proceduralWeaponAnimation + Offsets.ProceduralWeaponAnimation.Mask, 0);
            }
            else if (!on && mask == 0)
            {
                Memory.WriteValue(this.proceduralWeaponAnimation + Offsets.ProceduralWeaponAnimation.Mask, (int)this.OriginalValues["Mask"]);
            }
        }

        /// <summary>
        /// Enables / disables instant ads, changes per weapon
        /// </summary>
        public void SetInstantADS(bool on)
        {
            ulong aimingSpeedAddr = this.proceduralWeaponAnimation + Offsets.ProceduralWeaponAnimation.AimingSpeed;
            ulong aimSwayStrengthAddr = this.proceduralWeaponAnimation + Offsets.ProceduralWeaponAnimation.AimSwayStrength;

            float newAimingSpeed = on ? 7f : (float)this.OriginalValues["AimingSpeed"];
            float newAimSwayStrength = on ? 0f : (float)this.OriginalValues["AimingSpeedSway"];

            //this is just example of how to write multiple values at once
            //should create list of entries for all enabled function and write them all at once
            var entries = new List<IScatterWriteEntry>
            {
                new ScatterWriteDataEntry<float>(aimingSpeedAddr, newAimingSpeed),
                new ScatterWriteDataEntry<float>(aimSwayStrengthAddr, newAimSwayStrength)
            };

            Memory.WriteScatter(entries);
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
                        this.SetSkillValue("MagDrillsLoad", magDrillsLoad + Offsets.SkillFloat.Value, revert ? this.OriginalValues["MagDrillsLoad"] : this._config.MagDrillSpeed * 10);
                        break;
                    case Skills.MagDrillsUnload:
                        this.SetSkillValue("MagDrillsUnload", magDrillsUnload + Offsets.SkillFloat.Value, revert ? this.OriginalValues["MagDrillsUnload"] : this._config.MagDrillSpeed * 10);
                        break;
                    case Skills.JumpStrength:
                        this.SetSkillValue("JumpStrength", jumpStrength + Offsets.SkillFloat.Value, revert ? this.OriginalValues["JumpStrength"] : this._config.JumpPowerStrength / 10);
                        break;
                    case Skills.WeightStrength:
                        this.SetSkillValue("WeightStrength", weightStrength + Offsets.SkillFloat.Value, revert ? this.OriginalValues["WeightStrength"] : 0.5f);
                        break;
                    case Skills.ThrowStrength:
                        this.SetSkillValue("ThrowStrength", throwStrength + Offsets.SkillFloat.Value, revert ? this.OriginalValues["ThrowStrength"] : this._config.ThrowPowerStrength / 10);
                        break;
                    case Skills.SearchDouble:
                        Memory.WriteValue(this.searchDouble + Offsets.SkillFloat.Value, this._config.DoubleSearchEnabled);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ERROR Setting Max Skill: #{skill}");
            }
        }

        private void SetSkillValue(string key, ulong address, float value)
        {
            if (this.OriginalValues[key] != -1)
                this.OriginalValues[key] = Memory.ReadValue<float>(address);

            Memory.WriteValue(address, value);
        }

        /// <summary>
        /// Changes movement state
        /// </summary>
        public void SetMovementState(bool on)
        {
            this.baseMovementState = Memory.ReadPtr(this.movementContext + Offsets.MovementContext.BaseMovementState);
            var animationState = Memory.ReadValue<byte>(this.baseMovementState + Offsets.BaseMovementState.Name);

            if (on && animationState == 5)
            {
                Memory.WriteValue<byte>(this.baseMovementState + Offsets.BaseMovementState.Name, 6);
            }
            else if (!on && animationState == 6)
            {
                Memory.WriteValue<byte>(this.baseMovementState + Offsets.BaseMovementState.Name, 5);
            }
        }

        /// <summary>
        /// Sets maximum stamina / hand stamina
        /// </summary>
        public void SetMaxStamina()
        {
            if (this.OriginalValues["StaminaCapacity"] == -1)
            {
                this.OriginalValues["StaminaCapacity"] = Memory.ReadValue<float>(this.physical + Offsets.Physical.StaminaCapacity);
                this.OriginalValues["HandStaminaCapacity"] = Memory.ReadValue<float>(this.physical + Offsets.Physical.HandsCapacity);
            }

            Memory.WriteValue<float>(this.stamina + Offsets.Stamina.Current, (float)this.OriginalValues["StaminaCapacity"]);
            Memory.WriteValue<float>(this.handsStamina + Offsets.Stamina.Current, (float)this.OriginalValues["HandStaminaCapacity"]);
        }
    }
}