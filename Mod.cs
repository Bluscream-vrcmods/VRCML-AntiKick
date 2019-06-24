using VRCModLoader;
using Harmony;

namespace Mod
{
    [VRCModInfo("AntiKick", "1.0", "Bluscream, marcocorriero#0001")]
    public class Mod : VRCMod
    {
        private void OnApplicationStart() {
            AntiKickPatch();
        }
        public static void AntiKickPatch()
        {
                var instance = HarmonyInstance.Create("AntiKick");
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "KickUserRPC", "ReturnFalse");
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "SelfCheckAndEnforceModerations", "RefFalseDontContinue");
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "IsKickedFromWorld", "RefFalseDontContinue");
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "IsPublicOnlyBannedFromWorld", "RefFalseDontContinue");
                Utils.Log("Patched all methods!");
        }
    }
}
