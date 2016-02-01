using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumInferno_Friendly : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Inferno";
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 50;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 12;
            projectile.timeLeft = 5 * 60;
            projectile.scale = 5f;
            projectile.alpha = 255; // invis
        }

        public override void AI()
        {
            projectile.localAI[1]++; // add to counter)
            if (projectile.localAI[1] == 6f) // 10 times a second (divide each second into 10 frames)
            {
                projectile.localAI[1] = 0f; // reset counter
                projectile.velocity.Y += 0.8f; // gives it some 'weight'
                if (projectile.ai[1] != 1f) // split projectile
                {
                    projectile.scale -= 0.2f; // reduce scale
                    projectile.velocity.Y += 0.5f; // gives more weight
                }
                else
                {
                    // split projectile
                    projectile.scale -= 0.6f; // reduce scale
                    projectile.timeLeft -= (int)(0.5 * 30); // reduce timeLeft
                }
            }

        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                if (projectile.oldPos[k] == Vector2.Zero)
                {
                    return null;
                }
                projHitbox.X = (int)projectile.oldPos[k].X;
                projHitbox.Y = (int)projectile.oldPos[k].Y;
                projHitbox.X *= (int)projectile.scale;
                projHitbox.Y *= (int)projectile.scale;
                if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return null;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // oldVelocity == velocity before collision

            projectile.penetrate--;
            if (projectile.ai[1] == 1f) projectile.penetrate--;

            // kill if no more pen
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }

            // 10% chance to split initial projectile
            bool split = false;
            if (Main.rand.Next(0, 100) <= 10 && projectile.ai[1] != 1f)
            {
                split = true;
            }

            // swap velocity
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            // slowdown projectile by 14%
            projectile.velocity *= 0.86f;

            // split projectile into counterparts
            if (split)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, projectile.type, (int)(projectile.damage * 0.4), projectile.knockBack, projectile.owner, 0f, 1f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X + (float)Main.rand.Next(-3, 3), projectile.velocity.Y + (float)Main.rand.Next(-3, 3), projectile.type, (int)(projectile.damage * 0.4), projectile.knockBack, projectile.owner, 0f, 1f);
                projectile.Kill();
            }

            return false;
        }

        public override void TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("MharadiumFire"), 60 * Main.rand.Next(8, 16));
        }
    }
}
