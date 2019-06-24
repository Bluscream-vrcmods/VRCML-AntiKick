using VRCModLoader;
using Harmony;
using VRCTools;
using VRC.Core;
using System.Collections.Generic;

namespace Mod
{
    [VRCModInfo("AntiKick", "1.0", "Bluscream, marcocorriero#0001")]
    public class Mod : VRCMod
    {
        private void OnApplicationStart() {
            AntiKickPatch();
        }
        private static void AntiKickPatch()
        {
                var instance = HarmonyInstance.Create("AntiKick");
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "KickUserRPC", "KickUserRPC", typeof(Patches));
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "SelfCheckAndEnforceModerations", "SelfCheckAndEnforceModerations", typeof(Patches));
                PatchHelper.PatchMethod(instance, typeof(ModerationManager), "IsKickedFromWorld", "IsKickedFromWorld", typeof(Patches));
                Utils.Log("Patched all required methods!");
        }
        public static void JoinAnotherInstanceOfSameWorld() {
            var currentWorld = RoomManagerBase.currentRoom;
            var currentInstance = RoomManagerBase.currentWorldInstance;
            var newInstance = currentWorld.GetBestInstance(APIUser.CurrentUser.id, new List<string>() { currentInstance.idOnly });
            VRCSDK2.Networking.GoToRoom(currentWorld.id + ":" + newInstance.idWithTags);
        }

    }
    public class Patches
    {
            private static bool KickUserRPC()
            {
                Utils.Log("KickUserRPC() called: Someone in the instance tried to kick you!");
                VRCUiPopupManagerUtils.GetVRCUiPopupManager().ShowStandardPopup("Kicked",
                "Someone tried to kick you from this instance!\n\nDo you want to switch to another instance of this world?",
                    "Yes", () => {
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                        Mod.JoinAnotherInstanceOfSameWorld();
                    },
                    "No", () => {
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                    }
                );
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
                VRCUiPopupManagerUtils.GetVRCUiPopupManager().ShowStandardPopup("Kicked",
                "You got kicked from this instance previously\n\nAre you sure you want to join?",
                    "Yes", () => {
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                    },
                    "No", () => {
                        VRCUiPopupManagerUtils.GetVRCUiPopupManager().HideCurrentPopup();
                        Mod.JoinAnotherInstanceOfSameWorld();
                    }
                );
                __result = false;
                return false;
            }
    }
}
