using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Negadium.Items.Accessories.Mharadium
{
    public class MharadiumSuperPowers : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Mharadium Super Powers";
            item.width = 22;
            item.height = 22;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "Grants the effects of all the Mharadium Accessories combined!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.accessory = true;

            item.defense = 20;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 8);
        }

        public override void UpdateAccessory(Player player)
        {
            player.AddBuff(mod.BuffType("MharadiumSuperPowers"), 2); // All handling in here and Handling.cs
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MharadiumBoots");
            recipe.AddIngredient(null, "MharadiumEmblem");
            recipe.AddIngredient(null, "MharadiumShield");
            recipe.AddIngredient(null, "MharadiumHealthBand");
            recipe.AddIngredient(null, "MharadiumMagicBall");
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(null, "MharadiumBar", 10);
            recipe.AddIngredient(null, "DevilmiteShard");
            recipe.AddTile(null, "MharadiumAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
