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
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "KickUserRPC", "KickUserRPC", typeof(Patches));
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "SelfCheckAndEnforceModerations", "SelfCheckAndEnforceModerations", typeof(Patches));
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "IsKickedFromWorld", "IsKickedFromWorld", typeof(Patches));
                Utils.Log("Patched all required methods!");
        }
    }
    public class Patches
    {
            private static bool KickUserRPC()
            {
                Utils.Log("KickUserRPC() called: Someone in the instance tried to kick you!");
                return false;
            }

            private static bool SelfCheckAndEnforceModerations(bool __result)
            {
                Utils.Log("SelfCheckAndEnforceModerations() called!");
                __result = false;
                return false;
            }

            private static bool IsKickedFromWorld(bool __result)
            {
                Utils.Log("IsKickedFromWorld() called!");
                __result = false;
                return false;
            }
    }
}
