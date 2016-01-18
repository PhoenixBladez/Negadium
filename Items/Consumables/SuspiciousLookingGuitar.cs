using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Consumables
{
    public class SuspiciousLookingGuitar : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Suspicious Looking Guitar";
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.toolTip = "The underworld would like this.";
            item.rare = 9;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.useSound = 44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Thrasher"));
        }

        public override bool UseItem(Player player)
        {
            //if (Negadium.IsInHell(player.position))
            //{
                NPC.SpawnOnPlayer(Main.myPlayer, mod.NPCType("Thrasher"));
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            //}
            //else
            //{
            //    Main.NewText("This action can only be performed in hell!", 255, 255, 0, true);
            //    item.stack++;
            //}
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
