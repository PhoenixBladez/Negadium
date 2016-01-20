using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Laser";
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            aiType = 84;
            projectile.hostile = true;
            projectile.penetrate = 3;
            projectile.light = 0.75f;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
            projectile.scale = 1.2f;
            projectile.timeLeft = 600;
            projectile.magic = true;
            projectile.tileCollide = false;            
        }
    }
}
