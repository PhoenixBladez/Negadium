using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Mharadium.Armor
{
    public class MinieksHood : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Minieks Hood";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only for the chose one.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 1;
            item.defense = 133;
            item.expert = true;
        }

        // Checks if full set is worn.
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MinieksRobes") && legs.type == mod.ItemType("MinieksLeggings");
        }

        public override void ArmorSetShadows(Player player, ref bool longTrail, ref bool smallPulse, ref bool largePulse, ref bool shortTrail)
        {
            longTrail = true;
            largePulse = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            // Bonusses when set is worn.
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
