using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1, 1, 1, 0.5F);
        }
    }
}
