using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Negadium.Projectiles
{
    public class DevilsSphere : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Devil's Sphere";
            projectile.width = 16;
            projectile.height = 16;
            projectile.extraUpdates = 0;
            projectile.aiStyle = 115;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1.2f;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(projectile.Center, new Vector3(0.5f, 0, 0.25F));
            projectile.velocity *= 0.985f;
            projectile.rotation += projectile.velocity.X * 0.2f;
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += 0.08f;
            }
            else
            {
                projectile.rotation -= 0.08f;
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 30f)
            {
                projectile.alpha += 10;
                if (projectile.alpha >= 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                    return false;
                }
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
