using System;

using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumInferno : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Inferno";
            projectile.width = 60;
            projectile.height = 60;
            projectile.aiStyle = 50;
            projectile.hostile = true;
            projectile.alpha = (int)byte.MaxValue;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(24, 60 * Main.rand.Next(8, 16), false);
            base.OnHitPlayer(target, damage, crit);
        }
    }
}
