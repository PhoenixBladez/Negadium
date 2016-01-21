using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Accessories.Mharadium
{
    public class MharadiumShield : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            //equips.Add(EquipType.Shield);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Mharadium Shield";
            item.width = 28;
            item.height = 28;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "Grants effects from the Ankh Shield, Obsidian Rose, Paladin's Shield, Star Veil, Frozen Turtle Shell " + 
                "and Shackle.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;
            item.accessory = true;

            item.defense = 20;
        }

        public override void UpdateAccessory(Player player)
        {
            player.starCloak = true; // Causes stars to fall when damaged.
            player.longInvince = true; // Extends the invincibility time after being hit.
            player.lavaRose = true; // Reduces damage taken from lava.
            player.fireWalk = true; // Prevents damage from Hellstone and Meteorite blocks.
            player.endurance += 0.2f; // Blocks 20% of incomming damage.
            player.noKnockback = true; // Knockback resist.
            player.buffImmune[46] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            player.AddBuff(BuffID.PaladinsShield, 2); // Adds the Paladins Shield buff.
            player.AddBuff(BuffID.IceBarrier, 2);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddIngredient(ItemID.ObsidianRose);
            recipe.AddIngredient(ItemID.IronskinPotion);
            recipe.AddIngredient(ItemID.SharkToothNecklace);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddIngredient(ItemID.FrozenTurtleShell);
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddIngredient(ItemID.ObsidianSkinPotion);
            recipe.AddIngredient(null, "MharadiumBar", 5);
            recipe.AddTile(null, "MharadiumAnvil");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
