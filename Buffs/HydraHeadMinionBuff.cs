using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Buffs
{
    public class HydraHeadMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Hydra Head Summon";
            Main.buffTip[this.Type] = "A Hydra Head that fights for you";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("HydraHeadMinion")] > 0)
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
