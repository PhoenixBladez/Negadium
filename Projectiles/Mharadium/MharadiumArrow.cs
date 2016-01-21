using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumArrow : ModProjectile
    {
        int bounceTimes = 4;
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Arrow";
            projectile.width = 10;
            projectile.height = 10;

            projectile.arrow = true;
            projectile.aiStyle = 1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounceTimes--;
            if (bounceTimes <= 0)
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
                projectile.velocity *= 0.75f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
