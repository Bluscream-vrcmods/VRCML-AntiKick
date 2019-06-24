using System;
using System.IO;
using System.Linq;
using VRCModLoader;

namespace Mod
{
    public class Utils
    {
        public static void Log(params object[] msgs) {
            var msg = "[AntiKick]:";
            foreach (var _msg in msgs) {
                try {
                    msg += $" {_msg}";
                } catch {
                    msg += $" {_msg.ToString()}";
                }
            }
            VRCModLogger.Log(msg);
        }
        public static string CombinePaths(string source, params string[] paths)  {
            if (source == null) throw new ArgumentNullException("source");
            if (paths == null) throw new ArgumentNullException("paths");
            return paths.Aggregate(source, (acc, p) => Path.Combine(acc, p));
        }
    }
}
