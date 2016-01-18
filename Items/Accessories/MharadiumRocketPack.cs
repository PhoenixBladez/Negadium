using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Accessories
{
    public class MharadiumRocketPack : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Body);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Rocket Pack";
            item.width = 22;
            item.height = 26;
            item.toolTip = "Provides immunity to 'On Fire!'";
            item.toolTip2 = "16% increased rocket damage and 25% decreased ammo usage";
            item.value = 10000000;
            item.rare = 10;
            item.accessory = true;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.rangedDamage += 0.16f; // Increases ranged damage by 16%
            player.rocketDamage += 0.16f; // Increases rocket damage by 16%
            player.ammoCost75 = true;
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