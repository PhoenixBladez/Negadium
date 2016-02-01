using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Negadium.Developer.Charon
{
    public class CrimerasChunk : ModItem
    {
        // Gorateron, for Charon

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.EatersBone);
            item.name = "Crimera's Chunk";
            item.toolTip = "Summons a baby Crimera";
            item.rare = -12;

            item.shoot = mod.ProjectileType("BabyCrimera"); // actual pet as a projectile
            //ProjectileID.BabyEater == 175
            item.width = 32; // changed
            item.height = 28; // changed
            item.buffType = mod.BuffType("CrimerasBuff"); // buff granted
        }

        public override void UseStyle(Player player)
        {
            // actually add the buff thanks for Jopo on updating GH with this
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 2, true);
            }
        }
    }
}