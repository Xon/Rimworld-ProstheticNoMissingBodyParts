using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ProstheticNoMissingBodyParts;

[Harmony]
[HarmonyPatch(typeof(ApparelUtility), nameof(ApparelUtility.HasPartsToWear))]
internal static class ApparelUtility_HasPartsToWear
{
    public static void Postfix(ref bool __result, Pawn p, ThingDef apparel)
    {
        if (__result)
        {
            return;
        }

        // *****
        // ProstheticNoMissingBodyParts patch code start
        // *****

        // this code run only if original Rimworld check return false (body part is missed)
        // and we try to fix this, if pawn has a bionic part in right place (Bionic Arm, Bionic Leg, etc...)


        // prepare bool set for apparel body part groups
        var isLeftHand = false;
        var isRightHand = false;
        var isHands = false;
        var isFeet = false;

        foreach (var groupDefName in apparel.apparel.bodyPartGroups.Select(group => group.defName))
        {
            if (groupDefName == ModDefOf.LeftHand)
            {
                isLeftHand = true;
                break;
            }

            if (groupDefName == ModDefOf.RightHand)
            {
                isRightHand = true;
                break;
            }

            if (groupDefName == ModDefOf.Hands)
            {
                isHands = true;
                break;
            }

            if (groupDefName == ModDefOf.Feet)
            {
                isFeet = true;
                break;
            }

            var body = p.def.race.body.AllParts;

            if (BodyPartUtils.ExistsByGroupAndParent(body, ModDefOf.Shoulder, groupDefName))
            {
                isHands = true;
                break;
            }

            if (!BodyPartUtils.ExistsByGroupAndParent(body, ModDefOf.Leg, groupDefName))
            {
                continue;
            }

            isFeet = true;
            break;
        }

        var hediffs = p.health.hediffSet.hediffs;
        // check if apparel needed left hand, especially for jewelry mod, pawns try to wear bracer... on fingers... omg...
        if (isLeftHand)
        {
            __result = hediffs.Exists(h =>
            {
                if (h.Part?.customLabel == null || h.def == null)
                {
                    return false;
                }

                // true if left arm replaced with whitelisted bionic part
                if (ModDefOf.ShoulderDefs.Contains(h.Part.def))
                {
                    return ModDefOf.ArmsAllowHediffDefs.Contains(h.def) &&
                           BodyPartUtils.ExistsDeep(h.Part, ModDefOf.LeftHand);
                }

                // true if left hand replaced with whitelisted bionic part
                if (ModDefOf.HandDefs.Contains(h.Part.def))
                {
                    return ModDefOf.HandsAllowHediffDefs.Contains(h.def) &&
                           BodyPartUtils.ExistsDeep(h.Part, ModDefOf.LeftHand);
                }

                return false;
            });
            return;
        }

        // check if apparel needed right hand, especially for jewelry, wearing rings?
        if (isRightHand)
        {
            __result = hediffs.Exists(h =>
            {
                if (h.Part?.customLabel == null || h.def == null)
                {
                    return false;
                }

                // true if right arm replaced with whitelisted bionic part
                if (ModDefOf.ShoulderDefs.Contains(h.Part.def))
                {
                    return ModDefOf.ArmsAllowHediffDefs.Contains(h.def) &&
                           BodyPartUtils.ExistsDeep(h.Part, ModDefOf.RightHand);
                }

                // true if right hand replaced with whitelisted bionic part
                if (ModDefOf.HandDefs.Contains(h.Part.def))
                {
                    return ModDefOf.HandsAllowHediffDefs.Contains(h.def) &&
                           BodyPartUtils.ExistsDeep(h.Part, ModDefOf.RightHand);
                }

                return false;
            });
            return;
        }

        // check if apparel needed hands, useful for gloves
        if (isHands)
        {
            __result = hediffs.Exists(h =>
            {
                if (h.Part?.def == null || h.def == null)
                {
                    return false;
                }

                // true if arm replaced with whitelisted bionic part
                if (ModDefOf.ShoulderDefs.Contains(h.Part.def))
                {
                    return ModDefOf.ArmsAllowHediffDefs.Contains(h.def);
                }

                // true if hand replaced with whitelisted bionic part
                return ModDefOf.HandDefs.Contains(h.Part.def) &&
                       ModDefOf.HandsAllowHediffDefs.Contains(h.def);
            });
            return;
        }

        // check if apparel needed feet, useful for boots
        if (isFeet)
        {
            __result = hediffs.Exists(h =>
            {
                if (h.Part?.def == null || h.def == null)
                {
                    return false;
                }

                // true if leg replaced with whitelisted bionic part
                if (ModDefOf.LegDefs.Contains(h.Part.def))
                {
                    return ModDefOf.LegsAllowHediffDefs.Contains(h.def);
                }

                // true if foot replaced with whitelisted bionic part
                return ModDefOf.FootDefs.Contains(h.Part.def) &&
                       ModDefOf.FeetAllowHediffDefs.Contains(h.def);
            });
            return;
        }

        // *****
        // ProstheticNoMissingBodyParts patch code end
        // *****

        // body part still missed

        __result = false;
    }
}
