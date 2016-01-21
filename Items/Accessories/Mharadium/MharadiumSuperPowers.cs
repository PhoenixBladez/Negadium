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
            #region Boots Buffs
            player.noFallDmg = true; // Negate any fall damage.
            player.waterWalk = true; // Walk on liquids.
            player.lavaMax += 600; // 10 seconds of lava immunity.
            player.dash = 1; // Grants the ability to dash once.
            player.jumpBoost = true; // Allows the player to jump higher.
            player.doubleJumpCloud = true; // Allows double jumping.
            player.accRunSpeed = 10f; // Extra run speed max.
            player.moveSpeed += 0.65f; // 65% faster move speed.
            #endregion
            #region Shield Buffs
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
            #endregion
            #region Emblem Buffs
            player.meleeDamage += 0.3F;
            player.magicDamage += 0.3F;
            player.minionDamage += 0.3F;
            player.thrownDamage += 0.3F;
            player.rangedDamage += 0.3F;

            player.AddBuff(BuffID.WeaponImbueFire, 2);
            #endregion
            #region Magic Ball Buffs
            player.manaRegen += 2; // 200% extra mana regen.
            player.manaCost -= 0.2f; // -20% mana cost.

            // Crystal Ball effects.
            player.AddBuff(BuffID.Clairvoyance, 2);
            // Mana Flower effects.
            player.manaCost -= 0.08f; // -8% mana cost.
            player.manaFlower = true;
            // Celestial Cuffs effects.
            player.manaMagnet = true;
            player.magicCuffs = true;
            // Celestial Emblem + Sorcerer Emblem + Magic Power Potion effects.
            player.magicDamage += 0.5f;
            #endregion
            #region Health Band Buffs
            player.lifeRegen += 2; // 200% extra health regen.

            player.lifeMagnet = true; // Attracts heart from a longer distance.
            player.pStone = true; // Adds Philosopher's Stone effect.
            player.manaSickReduction += 0.5F; // Halves Mana Sickness.
            player.AddBuff(BuffID.Lifeforce, 2); // Adds the LifeForce buff.
            #endregion
            
            player.AddBuff(BuffID.WeaponImbueFire, 2);
            player.AddBuff(BuffID.Inferno, 2); // Adds the Inferno buff.
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
