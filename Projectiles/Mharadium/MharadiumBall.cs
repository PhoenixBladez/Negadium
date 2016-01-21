using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles.Mharadium
{
    public class MharadiumBall : ModProjectile
    {
        private Vector2 value = Vector2.Zero;

        public override void SetDefaults()
        {
            projectile.name = "Mharadium Ball";
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override bool PreAI()
        {
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return false;
            }
            Main.player[projectile.owner].itemAnimation = 10;
            Main.player[projectile.owner].itemTime = 10;
            if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
            Vector2 mountedCenter2 = Main.player[projectile.owner].MountedCenter;
            Vector2 vector18 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num204 = mountedCenter2.X - vector18.X;
            float num205 = mountedCenter2.Y - vector18.Y;
            float num206 = (float)Math.Sqrt((double)(num204 * num204 + num205 * num205));
            if (projectile.ai[0] == 0f)
            {
                float num207 = 160; // Initial flail range
                projectile.tileCollide = true;
                if (num206 > num207)
                {
                    projectile.ai[0] = 1f;
                    projectile.netUpdate = true;
                }
                else if (!Main.player[projectile.owner].channel)
                {
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y * 0.9f;
                    }
                    projectile.velocity.Y = projectile.velocity.Y + 1f;
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                float num208 = 14f / Main.player[projectile.owner].meleeSpeed;
                float num209 = 0.9f / Main.player[projectile.owner].meleeSpeed;
                float num210 = 300F; // While flail is out-range
                Math.Abs(num204);
                Math.Abs(num205);
                if (projectile.ai[1] == 1f)
                {
                    projectile.tileCollide = false;
                }
                if (!Main.player[projectile.owner].channel || num206 > num210 || !projectile.tileCollide)
                {
                    projectile.ai[1] = 1f;
                    if (projectile.tileCollide)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.tileCollide = false;
                    if (num206 < 20f)
                    {
                        projectile.Kill();
                    }
                }
                if (!projectile.tileCollide)
                {
                    num209 *= 2f;
                }
                int num211 = 100; // Retract range
                if (num206 > (float)num211 || !projectile.tileCollide)
                {
                    num206 = num208 / num206;
                    num204 *= num206;
                    num205 *= num206;
                    new Vector2(projectile.velocity.X, projectile.velocity.Y);
                    float num212 = num204 - projectile.velocity.X;
                    float num213 = num205 - projectile.velocity.Y;
                    float num214 = (float)Math.Sqrt((double)(num212 * num212 + num213 * num213));
                    num214 = num209 / num214;
                    num212 *= num214;
                    num213 *= num214;
                    projectile.velocity.X = projectile.velocity.X * 0.98f;
                    projectile.velocity.Y = projectile.velocity.Y * 0.98f;
                    projectile.velocity.X = projectile.velocity.X + num212;
                    projectile.velocity.Y = projectile.velocity.Y + num213;
                }
                else
                {
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                        projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                    }
                    if (Main.player[projectile.owner].velocity.X == 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                    }
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }
            else
            {
                projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }
            
            return false;
        }        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool flag9 = false;
            if (oldVelocity.X != projectile.velocity.X)
            {
                if (Math.Abs(oldVelocity.X) > 4f)
                {
                    flag9 = true;
                }
                projectile.velocity.X = -oldVelocity.X * 0.2f;
            }
            if (oldVelocity.Y != projectile.velocity.Y)
            {
                if (Math.Abs(oldVelocity.Y) > 4f)
                {
                    flag9 = true;
                }
                projectile.velocity.Y = -oldVelocity.Y * 0.2f;
            }
            projectile.ai[0] = 1f;
            if (flag9)
            {
                projectile.netUpdate = true;
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }

            return false;
        }
        
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModLoader.GetTexture("Negadium/Projectiles/Hooks/MharadiumHook_Chain");

            Vector2 position = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            while (flag)
            {
                float f = (float)Math.Sqrt(vector2_4.X * vector2_4.X + vector2_4.Y * vector2_4.Y);
                if ((double) f < 25.0)
                    flag = false;
                else if (float.IsNaN(f))
                {
                    flag = false;
                }
                else
                {
                    float num3 = 12f / f;
                    float num4 = vector2_4.X * num3;
                    float num5 = vector2_4.Y * num3;
                    position.X += num4;
                    position.Y += num5;
                    vector2_4.X = mountedCenter.X - position.X;
                    vector2_4.Y = mountedCenter.Y - position.Y;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
