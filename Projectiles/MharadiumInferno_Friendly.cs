using System;

using Terraria;
using Terraria.ModLoader;

namespace Negadium.Projectiles
{
    public class MharadiumInferno_Friendly : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Inferno";
            projectile.width = 60;
            projectile.height = 60;
            projectile.aiStyle = 50;
            projectile.friendly = true;
            projectile.alpha = (int)byte.MaxValue;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(24, 60 * Main.rand.Next(8, 16), false);
        }
    }
}
