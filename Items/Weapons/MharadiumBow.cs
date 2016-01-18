using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons
{
    public class MharadiumBow : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Nightmare";
            item.width = 12;
            item.height = 28;
            item.toolTip = "Only true heroes can handle its power!";
            item.toolTip2 = "16% chance not to consume ammo.";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 10;
            item.shoot = 1;
            item.useAmmo = 1;
            item.useSound = 5;
            item.damage = 200;
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 relativeCenter = player.RotatedRelativePoint(player.MountedCenter, true);

            float num5 = player.inventory[player.selectedItem].shootSpeed * item.scale;
            Vector2 vector2_3 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - relativeCenter;
            if ((double)player.gravDir == -1.0)
                vector2_3.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - relativeCenter.Y;
            Vector2 vector2_4 = Vector2.Normalize(vector2_3);
            if (float.IsNaN(vector2_4.X) || float.IsNaN(vector2_4.Y))
                vector2_4 = -Vector2.UnitY;
            vector2_4 *= num5;
            item.velocity = vector2_4;

            float rotationOffset = 8;
            Vector2 spinningpoint = Vector2.Normalize(item.velocity) * rotationOffset;
            spinningpoint = Utils.RotatedBy(spinningpoint, Main.rand.NextDouble() * 0.196349546313286 - 0.0981747731566429, new Vector2());
            if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
                spinningpoint = -Vector2.UnitY;

            Projectile.NewProjectile(position.X, position.Y, spinningpoint.X, spinningpoint.Y, type, damage, knockBack, item.owner);

            spinningpoint = Vector2.Normalize(item.velocity) * rotationOffset;
            spinningpoint = Utils.RotatedBy(spinningpoint, Main.rand.NextDouble() * 0.196349546313286 - 0.0981747731566429, new Vector2());
            if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
                spinningpoint = -Vector2.UnitY;
            Projectile.NewProjectile(position.X, position.Y, spinningpoint.X * 1.5F, spinningpoint.Y * 1.5F, type, damage, knockBack, item.owner);
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(0, 6) == 0) // About 16% chance not to consume ammo.
                return false;
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
