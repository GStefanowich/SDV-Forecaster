using System;
using System.Linq;

namespace ForecasterText.Objects {
    public static class Config {
        public static string[] Values<T>() where T : struct, Enum
            => Enum.GetValues<T>().Select(e => e.ToString()).ToArray();
    }
}
