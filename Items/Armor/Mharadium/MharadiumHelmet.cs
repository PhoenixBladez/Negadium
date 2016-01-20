using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace Negadium.Items.Armor.Mharadium
{
    public class MharadiumHelmet : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Helmet";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.defense = 40;
        }

        // Checks if full set is worn.
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MharadiumBreastplate") && legs.type == mod.ItemType("MharadiumLeggings");
        }

        public override void ArmorSetShadows(Player player, ref bool longTrail, ref bool smallPulse, ref bool largePulse, ref bool shortTrail)
        {
            shortTrail = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            // Bonusses when set is worn.
            player.setBonus = "Deal 40% more damage.";
            player.meleeDamage += 0.4F;
            player.magicDamage += 0.4F;
            player.minionDamage += 0.4F;
            player.thrownDamage += 0.4F;
            player.rangedDamage += 0.4F;
            Lighting.AddLight(player.position, 0.7F, 0.5F, 0.3F); // Add light.
            player.waterWalk = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBar", 10);
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
