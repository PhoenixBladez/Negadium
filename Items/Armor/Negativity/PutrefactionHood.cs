using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Armor.Negativity
{
    public class PutrefactionHood : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Putrefaction Hood";
            item.width = 28;
            item.height = 22;
            item.toolTip = "Of demon's blood";
            item.value = 10000;
            item.rare = 9;
            item.defense = 30;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return legs.type == mod.ItemType("PutrefactionBoots") && body.type == mod.ItemType("PutrefactionCore");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "'Applies Ichor to melee attacks'";
            player.AddBuff(BuffID.Archery, 2);
            player.AddBuff(BuffID.AmmoReservation, 2);
            player.crimsonRegen = true;
            // Increase all damage by 30%.
            player.meleeDamage += 0.3f;
            player.thrownDamage += 0.3f;
            player.rangedDamage += 0.3f;
            player.magicDamage += 0.3f;
            player.minionDamage += 0.3f;
            player.noKnockback = true;
        }

        public override void ArmorSetShadows(Player player, ref bool longTrail, ref bool smallPulse, ref bool largePulse, ref bool shortTrail)
        {
            shortTrail = true;
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