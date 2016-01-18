using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Negadium.Items.Armor.Negativity
{
    public class PutrefactionBoots : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Legs);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Putrefaction Boots";
            item.width = 22;
            item.height = 18;
            AddTooltip("Matt's armored boots.");
            AddTooltip2("25% increased movement speed");
            item.value = 10000;
            item.rare = 9;
            item.defense = 35;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.25f;
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