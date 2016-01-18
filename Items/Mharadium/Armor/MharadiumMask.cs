using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Negadium.Items.Mharadium.Armor
{
    public class MharadiumMask : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Mask";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.defense = 65;
        }

        // Checks if full set is worn.
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MharadiumBreastplate") && legs.type == mod.ItemType("MharadiumLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            // Bonusses when set is worn.
            player.setBonus = "Take 35% less damage from any source.";
            player.endurance += 0.35F; // Take 35% less dmg
            Lighting.AddLight(player.position, 0.7F, 0.5F, 0.3F); // Add light.
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
