using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Consumables
{
    public class WickedGel : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Wicked Gel";
            item.width = 16;
            item.height = 14;
            item.maxStack = 20;
            item.toolTip = "The underworld would like this.";
            item.rare = 10;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.useSound = 44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("OrionSlime"));
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(Main.myPlayer, mod.NPCType("OrionSlime"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
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
