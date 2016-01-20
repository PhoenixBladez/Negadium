using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Accessories.Mharadium
{
    public class MharadiumHealthBand : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Health Band";
            item.width = 28;
            item.height = 20;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "Grants effects from Charm of Myths and Philosopher's Stone and grants insane health regen, " + 
                "hearts are attracted from a larger distance and halves the duration of Mana Sickness.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player)
        {
            player.lifeRegen *= 10; // Extreme health regen.

            player.lifeMagnet = true; // Attracts heart from a longer distance.
            player.pStone = true; // Adds Philosopher's Stone effect.
            player.manaSickReduction += 0.5F; // Halves Mana Sickness.
            player.AddBuff(BuffID.Lifeforce, 2); // Adds the LifeForce buff.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CharmofMyths);
            recipe.AddIngredient(ItemID.RegenerationPotion);
            recipe.AddIngredient(ItemID.SuperHealingPotion, 5);
            recipe.AddIngredient(ItemID.HeartreachPotion);
            recipe.AddIngredient(ItemID.LifeforcePotion);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.LifeFruit, 5);
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
