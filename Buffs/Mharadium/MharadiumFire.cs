using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace Negadium.Buffs.Mharadium
{
    public class MharadiumFire : ModBuff
    {
        // Gorateron
        // Drains the target for 6 damage per second

        public override void SetDefaults()
        {
            Main.buffName[Type] = "Mharadium Fire!";
            Main.buffTip[Type] = "Quickly losing life";
            Main.buffNoSave[Type] = true;
            this.canBeCleared = false;
            this.longerExpertDebuff = true;
        }

        // The following method follows vanilla principles view MharadiumHandling.cs for extra information

        public override void Update(Player player, ref int buffIndex)
        {
            MharadiumModPlayer modPlayer = (MharadiumModPlayer)player.GetModPlayer(mod, "MharadiumModPlayer");
            modPlayer.onMharadiumFire = true;

            if (player.wet)
                player.buffTime[buffIndex] = 2;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            this.longerExpertDebuff = false;

            MharadiumNPCInfo modNPC = (MharadiumNPCInfo)npc.GetModInfo(mod, "MharadiumNPCInfo");
            modNPC.onMharadiumFire = true;

            if (npc.wet)
                npc.buffTime[buffIndex] = 2;
        }

    }
}
