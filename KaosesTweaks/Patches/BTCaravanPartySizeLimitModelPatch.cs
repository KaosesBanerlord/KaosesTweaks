namespace KaosesTweaks.Patches
{
    /*
        [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
        class BTCaravanPartySizeLimitModelPatch
        {
            static void Postfix(MobileParty party, ref ExplainedNumber __result)
            {
            }

            static bool Prepare() => KTSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
        }*/
}
