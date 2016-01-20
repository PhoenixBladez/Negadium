using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable.Mharadium
{
    /// <summary>
    /// Dropped by Satan's Servant
    /// </summary>
    public class MharadiumForge : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Forge";
            item.width = 12;
            item.height = 12;
            item.toolTip = "The only forge capable enough to melt Mharadium.";
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 10;

            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MharadiumForge");
        }
    }
}
