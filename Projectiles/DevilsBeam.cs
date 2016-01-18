using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles
{
    /// <summary>
    /// Not yet finished!
    /// </summary>
    public class DevilsBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Devils Beam";
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 27;
            projectile.damage = 1000;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.light = 1f;
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
