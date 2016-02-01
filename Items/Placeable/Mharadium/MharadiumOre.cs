using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Negadium.Buffs.Mharadium;

namespace Negadium.Items.Placeable.Mharadium
{
    public class MharadiumOre : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Ore";
            item.width = 12;
            item.height = 12;
            item.toolTip = "A most valuable piece of Mharadium Ore.";
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 10;

            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MharadiumOre");
        }

        public override void UpdateInventory(Player player)
        {
            player.AddBuff(mod.BuffType("Mharadium"), 2);
        }
    }
}
