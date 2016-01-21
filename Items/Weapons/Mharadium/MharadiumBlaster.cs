using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class MharadiumBlaster : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "The Aeon";
            item.width = 46;
            item.height = 26;
            item.toolTip = "Only true heroes can handle its power!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.damage = 360;
            item.crit += 25;

            item.ranged = true;
            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = 5;
            item.autoReuse = false;
            item.noMelee = true;
            item.knockBack = 4;
            item.useSound = 41;
            item.shoot = 14;
            item.shootSpeed = 13F;
            item.scale = 0.85f;
            item.useAmmo = ProjectileID.Bullet;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 direction = new Vector2(speedX, speedY);
            direction.Normalize();
            position += direction * item.width;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(player, target, damage, knockBack, crit);
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
