using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Placeable.Mharadium
{
    /// <summary>
    /// Dropped by Thrasher.
    /// </summary>
    public class MharadiumAnvil : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Anvil";
            item.width = 30;
            item.height = 18;
            item.toolTip = "An anvil made out of pure Mharadium.";
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 10;

            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MharadiumAnvil");
        }
    }
}
