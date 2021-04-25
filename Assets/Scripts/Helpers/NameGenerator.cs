using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helpers
{
    public static class NameGenerator
    {

        private static List<string> prefixes = new List<string>()
    {
        "Born",
        "Gorn",
        "Yorn",
        "Bjorn",
        "Corn",
        "El",
        "Bel",
        "Gel",
        "Hel",
        "Jof",
        "Gof",
        "Jon",
        "Rof",
        "Ron",
        "Bon",
        "Luk",
        "Buk",
        "Puk",
        "Bic",
        "Sic",
        "Cic",
        "Bonk"
    };

        private static List<string> filler = new List<string>()
    {
        "oh",
        "uh",
        "ih",
        "bb",
        "ss",
        "sh",
        " de ",
        " la ",
        " von ",
        "om",
        "bary",
        "bory",
        "alo",
        "op",
        "ap",
        "in",
        "ump",
        "co",
        "ho",
        "ah",
        "no"
    };

        private static List<string> suffixes = new List<string>()
    {
        "gus",
        "jus",
        "hus",
        "ocka",
        "icka",
        "nongo",
        "son",
        "sson",
        "guna",
        "gona",
        "bog",
        "cog",
        "sog",
        "ick",
        "ling",
        "lish",
        "ump",
        "ckid",
        "alus",
        "ina",
        "ona"
    };

        public static string GenerateName()
        {
            var result = GetRandom(prefixes);
            if (UnityEngine.Random.value < .3f)
                result += GetRandom(filler);
            result += GetRandom(suffixes);
            return result;
        }

        private static string GetRandom(List<string> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
