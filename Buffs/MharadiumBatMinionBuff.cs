using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Buffs
{
    public class MharadiumBatMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Mharadium Bat Summon";
            Main.buffTip[this.Type] = "A Mharadium Bat that fights for you";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("MharadiumBatMinion")] > 0)
            {
                player.raven = true;
            }
            if (!player.raven)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
