using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.Projectiles
{
    public class DevilsJavelin : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Devil's Javelin";
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 27;
            projectile.damage = 330;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.light = 1F;
            projectile.alpha = (int)byte.MaxValue;
            projectile.friendly = true;
            projectile.timeLeft = 3 * 60;
        }

        // Gorateron
        // A random chance to convert the javelin into multiple javelins shooting in an even arc
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.localAI[1]++;
            if (projectile.localAI[1] == projectile.timeLeft)
                projectile.Kill();

            if (Main.rand.Next(4) == 0 && projectile.ai[1] < 2f)
            {
                float baseSpeed = (float)Math.Sqrt(Math.Pow((double)projectile.velocity.X, 2) + Math.Pow((double)projectile.velocity.Y, 2));
                double spreadAngle = (Math.PI / 180) * 45;
                double baseAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spreadAngle / 2;
                double deltaAngle = spreadAngle / 8f;
                double offsetAngle;
                float speedX, speedY;
                for (int i = 0; i < 3; i++)
                {
                    offsetAngle = baseAngle + (deltaAngle * i) + spreadAngle / 2 - (spreadAngle * (Math.PI / 180) * projectile.height / 2);
                    speedX = baseSpeed * (float)Math.Sin(offsetAngle);
                    speedY = baseSpeed * (float)Math.Cos(offsetAngle);
                    Projectile.NewProjectile(projectile.position.X, projectile.position.Y, speedX, speedY, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 3f);

                }
                projectile.Kill();
            }

            target.AddBuff(mod.BuffType("MharadiumFire"), Main.rand.Next(1, 5) * 60);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1, 1, 1, 0.5F);
        }
    }
}
