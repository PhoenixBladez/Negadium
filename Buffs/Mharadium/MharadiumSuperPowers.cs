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
    public class MharadiumSuperPowers : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[Type] = "Super Powers";
            Main.buffTip[Type] = "Grants Mharadium Super Powers";
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
            this.canBeCleared = false;
        }

        // The following method follows vanilla principles view MharadiumHandling.cs for extra information

        public override void Update(Player player, ref int buffIndex)
        {
            MharadiumModPlayer modPlayer = (MharadiumModPlayer)player.GetModPlayer(mod, "MharadiumModPlayer");
            modPlayer.hasSuperPowers = true;
            modPlayer.drawSuperPowers = true;
            player.paladinGive = true; // needed for paladin buff

            // Ice Barrier
            if ((double)player.statLife <= (double)player.statLifeMax2 * 0.5)
            {
                Lighting.AddLight((int)((double)player.Center.X / 16.0), (int)((double)player.Center.Y / 16.0), 0.1f, 0.2f, 0.45f);
                player.iceBarrier = true;
                player.endurance += 0.25f;
                ++player.iceBarrierFrameCounter;
                if ((int)player.iceBarrierFrameCounter > 2)
                {
                    player.iceBarrierFrameCounter = (byte)0;
                    ++player.iceBarrierFrame;
                    if ((int)player.iceBarrierFrame >= 12)
                        player.iceBarrierFrame = (byte)0;
                }
            }
            // this is used by ice barrier, but we aren't adding the ice barrier buff!
            /*else
            {
                player.DelBuff(buffIndex);
                --buffIndex;
            }*/

            player.buffImmune[BuffID.Lifeforce] = true;
            for (int i = 71; i < 79; i++)
            {
                player.buffImmune[i] = true;
            }
            player.buffImmune[BuffID.Inferno] = true;
            player.buffImmune[BuffID.MagicPower] = true;
            player.buffImmune[BuffID.Clairvoyance] = true;
            player.buffImmune[BuffID.PaladinsShield] = true;
            player.buffImmune[BuffID.IceBarrier] = true;
        }
    }
}
