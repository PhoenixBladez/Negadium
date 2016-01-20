using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class MharadiumSpear : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Oblivion";
            item.width = 40;
            item.height = 40;
            item.toolTip = "Only some can handle its great powers.";
            item.rare = 10;

            item.value = Item.sellPrice(5, 0, 0, 0);

            item.useStyle = 5;
            item.useAnimation = 22;
            item.useTime = 22;
            item.shootSpeed = 5.6F;
            item.knockBack = 6.4F;
            item.damage = 270;
            item.scale = 1.1f;
            item.useSound = 1;
            item.shoot = mod.ProjectileType("MharadiumSpear");
            item.rare = 10;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float angle = (float)Math.Atan(speedY / speedX);
            Vector2 vector2 = new Vector2(position.X + 40F * (float)Math.Cos(angle), position.Y + 40F * (float)Math.Sin(angle));
            float mouseX = Main.mouseX + Main.screenPosition.X;
            if (mouseX < vector2.X)
            {
                vector2 = new Vector2(position.X - 40F * (float)Math.Cos(angle), position.Y - 40F * (float)Math.Sin(angle));
            }
            Projectile.NewProjectile(vector2.X, vector2.Y, speedX * 2, speedY * 2, mod.ProjectileType("DevilsJavelin"), damage, knockBack, item.owner);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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
