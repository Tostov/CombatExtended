﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace CombatExtended
{
    public class StatWorker_MeleeDamageBase : StatWorker_MeleeStats
    {
        #region Constants

        protected const float damageVariationMin = 0.5f;
        protected const float damageVariationMax = 1.5f;
        protected const float damageVariationPerSkillLevel = 0.025f;

        #endregion

        #region Methods

        public static float GetDamageVariationMin(Pawn pawn)
        {
            float unskilledReturnValue = damageVariationMin;
            if (!ShouldUseSkillVariation(pawn, ref unskilledReturnValue))
            {
                return unskilledReturnValue;
            }
            return damageVariationMin + (damageVariationPerSkillLevel * pawn.skills.GetSkill(SkillDefOf.Melee).Level);
        }

        public static float GetDamageVariationMax(Pawn pawn)
        {
            float unskilledReturnValue = damageVariationMax;
            if (!ShouldUseSkillVariation(pawn, ref unskilledReturnValue))
            {
                return unskilledReturnValue;
            }
            return damageVariationMax - (damageVariationPerSkillLevel * (20 - pawn.skills.GetSkill(SkillDefOf.Melee).Level));
        }

        protected static bool ShouldUseSkillVariation(Pawn pawn, ref float unskilledReturnValue)
        {
            if (pawn == null)       //Info windows for when weapon isn't equipped
            {
                return false;
            }
            if ((pawn?.skills?.GetSkill(SkillDefOf.Melee) ?? null) == null)     //Pawns that can equip weapons but don't use skill (mechanoids, custom races) 
            {                                                                   //No damage variation applied (same as animals for unarmed damage)
                unskilledReturnValue = 1.0f;
                return false;
            }
            return true;
        }

        #endregion

    }
}