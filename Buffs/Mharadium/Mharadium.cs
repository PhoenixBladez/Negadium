using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Buffs.Mharadium
{
    public class Mharadium : ModBuff
    {
        //  Gorateron, debuff used when Mharadium Ore is in the inventory

        public override void SetDefaults()
        {
            Main.buffName[Type] = "Mharadium";
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffTip[Type] = "You have a weird feeling, it may be the radioactivity";
            Main.buffNoSave[Type] = true;
            this.canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            MharadiumBuffModPlayer modPlayer = (MharadiumBuffModPlayer)player.GetModPlayer(mod, "MharadiumBuffModPlayer");
            modPlayer.mharadiumbuff = true;

            if (Main.rand.Next(0, 101) <= 12)
            {
                Dust.NewDust(player.position, 20, 20, DustID.Blood);
            }

        }
    }

    public class MharadiumBuffModPlayer : ModPlayer
    {
        public bool mharadiumbuff = false;

        public override void ResetEffects()
        {
            mharadiumbuff = false;
        }

        public override void PostUpdateEquips()
        {
            if (mharadiumbuff)
            {
                player.statLifeMax2 = (int)(player.statLifeMax2 * 0.97);
                player.statManaMax2 = (int)(player.statManaMax2 * 0.97);
                player.statDefense = (int)(player.statDefense * 0.97);
                player.magicDamage = (float)(player.magicDamage * 0.97f);
                player.rangedDamage = (float)(player.rangedDamage * 0.97f);
                player.meleeDamage = (float)(player.meleeDamage * 0.97f);
                player.thrownDamage = (float)(player.thrownDamage * 0.97f);
            }
        }
    }
}
