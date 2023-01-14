namespace KaosesTweaks.Patches
{
    /*
        [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
        class BTCaravanPartySizeLimitModelPatch
        {
            static void Postfix(MobileParty party, ref ExplainedNumber __result)
            {
            }

            static bool Prepare() => Factory.Settings is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
        }*/
}
