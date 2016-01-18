using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Armor.Negativity
{
    public class PutrefactionCore : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Body);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Putrefaction Core";
            item.width = 30;
            item.height = 20;
            item.toolTip = "'Matt's body armor'";
            item.toolTip2 = "'Immunity to 'Ichor'\n'+1 max minions'";
            item.value = 10000;
            item.rare = 9;
            item.defense = 40;
        }

        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.Ichor] = true;
            player.maxMinions++;
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