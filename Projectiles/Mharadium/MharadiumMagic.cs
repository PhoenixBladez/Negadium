using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumMagic : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Magic";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 8;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            projectile.velocity.Y += projectile.ai[0];
            projectile.rotation += 0.1F;

            Lighting.AddLight(projectile.position, 10, 5, 2);
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] += 0.1f;
            target.AddBuff(BuffID.ShadowFlame, 30);
        }
    }
}
