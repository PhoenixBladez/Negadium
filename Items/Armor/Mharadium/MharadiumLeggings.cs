using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Armor.Mharadium
{
    public class MharadiumLeggings : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Legs);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Leggings";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.defense = 75;
        }

        public override void UpdateEquip(Player player)
        {
            // Add bonusses
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBar", 6);
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
