using Harmony;
using System;
using System.Reflection;

namespace Mod
{
    public class PatchHelper
    {
        public static void PatchMethod(HarmonyInstance harmonyInstance, Type type, string methodName, string resultMethod)
        {
            var methodString = type.Name + "." + methodName;
            try {
                Utils.Log("Patching", methodString, "with", resultMethod);
                harmonyInstance.Patch(type.GetType().GetMethod(methodName, ((BindingFlags)62)), GetPatch(resultMethod, typeof(PatchesInternal)), null, null);
            } catch (Exception ex) {
                Utils.Log("[ERROR] Unable to patch", methodString, "with", resultMethod, ex.Message.Enclose());
            }
        }
        internal static HarmonyMethod GetPatch(string name, Type type) => new HarmonyMethod(type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        internal static class PatchesInternal {
            private static bool ReturnFalse()
            {
                return false;
            }

            private static bool ReturnTrue()
            {
                return true;
            }

            private static void Void()
            {
                return;
            }

            private static bool EnterWorldWithStringsPatch(string __0, string __1)
            {
                __0 = "";
                __1 = "";
                return false;
            }

            private static void JustContinue()
            {
            }

            private static bool RefFalseDontContinue(bool __result)
            {
                __result = false;
                return false;
            }

            private static bool RefTrueContinue(bool __result)
            {
                __result = true;
                return true;
            }
        }
    }
}