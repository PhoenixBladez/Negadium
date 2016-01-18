using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Mharadium.Armor
{
    public class MinieksRobes : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Body);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Minieks Robes";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only for the chose one.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 1;
            item.expert = true;
            item.defense = 266;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
