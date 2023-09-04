using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Data
{
    /// <summary>
    /// fun little class, in a "normal" project this would be outside of this app class, maybe in it's own service in a shared lib
    /// </summary>
    public static class NameGenerator
    {
        private static readonly string[] normalMonsterNames = {
        "Rathalos", "Rathian", "Diablos", "Barroth", "Tigrex", "Zinogre", "Nargacuga", "Lagiacrus", "Gore Magala", "Kirin", "Plesioth",
        "Barioth", "Lavasioth", "Yian Kut-Ku", "Khezu", "Tetsucabra", "Volvidon", "Great Jaggi", "Ludroth", "Qurupeco", "Gypceros",
        "Basarios", "Nibelsnarf", "Gammoth", "Astalos", "Mizutsune", "Glavenus", "Valstrax", "Zorah Magdaros", "Leshen", "Behemoth"
    };

        private static readonly string[] monsterNamePrefixes = {
        "Subspecies", "Variant", "Rare Species", "Furious", "Raging", "Savage", "Apex", "Zenith", "Gamma", "Molten", "Elder Dragon",
        "Ancient", "Alpha", "Beta", "Crimson", "Azure", "Silver", "Gold", "Emerald", "Shiny", "Rust", "Metal", "Black", "White",
        "Ivory", "Snowy", "Steel", "Crystal", "Diamond", "Vermilion", "Cobalt", "Crimson", "Abyssal", "Radiant", "Heavenly",
        "Hellfire", "Infernal", "Abyss", "Mirage", "Ghostly", "Shadow", "Astral", "Phantom", "Dreadking", "Dreadqueen",
        "Hyperspecies", "Divine", "Aureus", "Primal", "Prime", "Primeval", "Supreme", "Voracious", "Ravenous", "Ecliptic",
        "Eternal"
    };

        private static readonly Random random = new Random();

        public static string GenerateRandomMonsterName()
        {
            string randomMonster = normalMonsterNames[random.Next(normalMonsterNames.Length)];
            string randomPrefix = monsterNamePrefixes[random.Next(monsterNamePrefixes.Length)];
            return randomPrefix + " " + randomMonster;
        }
    }
}
