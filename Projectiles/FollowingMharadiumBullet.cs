using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.Projectiles
{
    public class FollowingMharadiumBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Bullet";
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.light = 0.5f;
            projectile.alpha = 255;
            projectile.timeLeft = 300;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            for (int index1 = 0; index1 < 5; ++index1)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * (float)index1;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * (float)index1;
                int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 182, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index2].color = new Color(255, 0, 200);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = x;
                Main.dust[index2].position.Y = y;
                Main.dust[index2].velocity *= 0.0f;
                Main.dust[index2].noGravity = true;
            }
            float direction = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.X);
            float ai = projectile.localAI[0];
            if (ai == 0.0F)
            {
                projectile.localAI[0] = direction;
                ai = direction;
            }

            float X = projectile.position.X;
            float Y = projectile.position.Y;
            float num5 = 300F;
            bool flag2 = false;
            int targetID = 0;
            if (projectile.ai[1] == 0.0F)
            {
                for (int i = 0; i < 200; ++i)
                {
                    if ( Main.npc[i].CanBeChasedBy((object)this, false) && (projectile.ai[1] == 0.0 || projectile.ai[1] == (i + 1)) )
                    {
                        float targetPosX = Main.npc[i].position.X + (Main.npc[i].width / 2);
                        float targetPosY = Main.npc[i].position.Y + (Main.npc[i].height / 2);
                        float newDir = Math.Abs(projectile.position.X + (projectile.width / 2) - targetPosX) +
                            Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - targetPosY);

                        if (newDir < num5 && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2),
                            projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {                            
                            num5 = newDir;
                            X = targetPosX;
                            Y = targetPosY;
                            flag2 = true;
                            targetID = i;
                        }
                    }
                }
                if (flag2)
                    projectile.ai[1] = (float)(targetID + 1);
                flag2 = false;
            }
            if (projectile.ai[1] > 0.0)
            {
                int index = (int)(projectile.ai[1] - 1.0);
                if (Main.npc[index].active && Main.npc[index].CanBeChasedBy((object)this, true) && !Main.npc[index].dontTakeDamage)
                {
                    if ((Math.Abs(projectile.position.X + (projectile.width / 2) - (Main.npc[index].position.X + (Main.npc[index].width / 2))) + 
                        Math.Abs(projectile.position.Y + (projectile.height / 2) - (Main.npc[index].position.Y + (float)(Main.npc[index].height / 2)))) < 1000.0)
                    {
                        flag2 = true;
                        X = Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
                        Y = Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
                    }
                }
                else
                    projectile.ai[1] = 0.0f;
            }
            if (!projectile.friendly)
                flag2 = false;

            if (flag2)
            {
                float newAI = ai;
                Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num8 = X - vector2.X;
                float num9 = Y - vector2.Y;
                float num10 = (float)Math.Sqrt((double)num8 * (double)num8 + (double)num9 * (double)num9);
                float num11 = newAI / num10;
                float num12 = num8 * num11;
                float num13 = num9 * num11;
                int num14 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num14 - 1) + num12) / (float)num14;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num14 - 1) + num13) / (float)num14;
            }
            return false;
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
