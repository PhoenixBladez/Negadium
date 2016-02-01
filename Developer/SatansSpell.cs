using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Negadium.Developer
{
    public class SatansSpell : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Satan's Spell";
            item.width = 28;
            item.height = 15;
            item.toolTip = "Only some can handle its great powers.";
            item.rare = 10;

            item.value = Item.sellPrice(5, 0, 0, 0);

            item.accessory = true;

            item.defense = 666;
            item.crit += 40;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 8);
        }

        public override void UpdateEquip(Player player)
        {
            if (player.name == "Miniek" || player.name == "miniek" || player.name == "Eldrazi" || player.name == "eldrazi" || 
                player.name == "Charon" || player.name == "charon" || player.name == "GIMJ")
            {
                player.meleeDamage *= 1.3F; // Increase melee damage by 30%
                player.magicDamage *= 1.3F; // Increase magic damage by 30%
                player.rangedDamage *= 1.3F; // Increase ranged damage by 30%

                player.maxMinions += 8; // Increase the max amount of minions by 8.
                player.minionKB *= 1.75F; // Increase the knockback of the minions by 75%
                player.minionDamage *= 1.4F; // Increase the damage of minions by 40%

                player.moveSpeed *= 1.3F; // Increase move speed by 30%
                player.pickSpeed *= 1.5F; // Increase dig speed by 50%
                player.tileSpeed *= 1.5F; // Increase tile placement speed by 50%

                player.lifeRegen *= 10; // Insane health regen.
                player.manaRegen *= 10; // Insane mana regen.

                player.AddBuff(BuffID.WeaponImbueIchor, 2);
            }
            else
            {
                player.AddBuff(BuffID.OnFire, 2);
            }
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
