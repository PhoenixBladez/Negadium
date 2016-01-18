using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles
{
    public class PiercingMharadiumBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Bullet";
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.light = 0.5f;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            if (projectile.alpha <= 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.alpha < 170)
            {
                for (int index1 = 0; index1 < 5; ++index1)
                {
                    float x = (projectile.position.X) - projectile.velocity.X / 10f * (float)index1;
                    float y = (projectile.position.Y) - projectile.velocity.Y / 10f * (float)index1;
                    int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 182, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].color = new Color(255, 0, 200);
                    Main.dust[index2].alpha = projectile.alpha;
                    Main.dust[index2].position.X = x;
                    Main.dust[index2].position.Y = y;
                    Main.dust[index2].velocity *= 0.0f;
                    Main.dust[index2].noGravity = true;
                }
            }
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y);
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
    }
}
