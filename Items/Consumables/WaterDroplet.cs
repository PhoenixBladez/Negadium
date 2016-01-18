using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Consumables
{
    public class WaterDroplet : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Water Droplet";
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.toolTip = "Used to summon the fearsome Void Hydra.";
            item.rare = 9;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.useSound = 44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("HydraBody"));
        }
        public override bool UseItem(Player player)
        {            
            Negadium.SpawnOnPlayer(player.whoAmI, mod.NPCType("HydraBody"), true);
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
