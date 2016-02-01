using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.Projectiles.Summoner
{
    public class HydraHeadMinion : ModProjectile
    {
        private Player player
        {
            get { return Main.player[(int)projectile.owner]; }
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.name = "Hydra Head";
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            Main.projFrames[projectile.type] = 4;
        }

        public override bool PreAI()
        {
            if (player.dead)
            {
                player.raven = false;
            }
            if (player.raven)
            {
                projectile.timeLeft = 2;
            }

            Lighting.AddLight(projectile.Center, new Vector3(0.729f, 0.333f, 0.827f));

            float num1 = 0.05F;
            float num2 = 200F;

            Vector2 vector2 = new Vector2(player.Center.X, player.Center.Y);
            float xDir = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float yDir = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;

            Vector2 lookRotation = new Vector2((Main.mouseX + Main.screenPosition.X) - projectile.Center.X,
                (Main.mouseY + Main.screenPosition.Y) - projectile.Center.Y);

            // No target.
            if (projectile.ai[0] < 0)
            {
                for (int i = 0; i < Main.npc.Length; ++i)
                {
                    if (Main.npc[i].CanBeChasedBy((object)this, false))
                    {
                        projectile.ai[0] = i;
                        break;
                    }
                }
            }
            // Target
            if (projectile.ai[0] >= 0)
            {
                NPC target = Main.npc[(int)projectile.ai[0]];
                if (target.active && target.CanBeChasedBy((object)this, true) && !target.dontTakeDamage)
                {
                    lookRotation.X = (target.position.X + (float)(target.width / 2)) - (float)(target.width / 2) - projectile.Center.X;
                    lookRotation.Y = target.position.Y + (float)(target.height / 2) - (float)(target.height / 2) - projectile.Center.Y;

                    if (Main.netMode != 1 && Vector2.Distance(projectile.position, target.position) < 200)
                    {
                        --projectile.localAI[0];
                        if ((double)projectile.localAI[0] <= 40.0)
                        {
                            if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height) && Collision.CanHit(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                            {
                                float num5 = 5f;
                                vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                                float num6 = target.position.X + (float)target.width * 0.5f - vector2.X + (float)Main.rand.Next(-10, 11);
                                float num7 = target.position.Y + (float)target.height * 0.5f - vector2.Y + (float)Main.rand.Next(-10, 11);
                                float num8 = (float)Math.Sqrt((double)num6 * (double)num6 + (double)num7 * (double)num7);
                                float num9 = num5 / num8;
                                xDir = num6 * num9;
                                yDir = num7 * num9;
                                int index = Projectile.NewProjectile(vector2.X, vector2.Y, xDir, yDir, 101, 22, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                                Main.projectile[index].friendly = true;
                                Main.projectile[index].hostile = false;
                                if (projectile.localAI[0] <= 0)
                                    projectile.localAI[0] = 120F;
                            }
                            else
                                projectile.localAI[0] = 60F;
                        }
                    }
                }
                else
                    projectile.ai[0] = -1;
            }
            
            float num4 = (float)Math.Sqrt((double)xDir * (double)xDir + (double)yDir * (double)yDir);
            if ((double)num4 > (double)num2)
            {
                float num5 = num2 / num4;
                xDir *= num5;
                yDir *= num5;
            }
            if ((double)projectile.position.X < (double)player.position.X + (double)xDir)
            {
                projectile.velocity.X = projectile.velocity.X + num1;
                if ((double)projectile.velocity.X < 0.0 && (double)xDir > 0.0)
                    projectile.velocity.X = projectile.velocity.X + num1 * 1.5f;
            }
            else if ((double)projectile.position.X > (double)player.position.X + (double)xDir)
            {
                projectile.velocity.X = projectile.velocity.X - num1;
                if ((double)projectile.velocity.X > 0.0 && (double)xDir < 0.0)
                    projectile.velocity.X = projectile.velocity.X - num1 * 1.5f;
            }
            if ((double)projectile.position.Y < (double)player.position.Y + (double)yDir)
            {
                projectile.velocity.Y = projectile.velocity.Y + num1;
                if ((double)projectile.velocity.Y < 0.0 && (double)yDir > 0.0)
                    projectile.velocity.Y = projectile.velocity.Y + num1 * 1.5f;
            }
            else if ((double)projectile.position.Y > (double)player.position.Y + (double)yDir)
            {
                projectile.velocity.Y = projectile.velocity.Y - num1;
                if ((double)projectile.velocity.Y > 0.0 && (double)yDir < 0.0)
                    projectile.velocity.Y = projectile.velocity.Y - num1 * 1.5f;
            }
            // Speed capping
            if ((double)projectile.velocity.X > 4.0)
                projectile.velocity.X = 4f;
            if ((double)projectile.velocity.X < -4.0)
                projectile.velocity.X = -4f;
            if ((double)projectile.velocity.Y > 4.0)
                projectile.velocity.Y = 4f;
            if ((double)projectile.velocity.Y < -4.0)
                projectile.velocity.Y = -4f;

            projectile.rotation = MathHelper.Lerp(projectile.rotation,
                (float)Math.Atan2((double)lookRotation.Y, (double)lookRotation.X) + 1.57f, 0.1F);

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture1 = (Texture2D)null;
            Color color1 = Color.White;
            Texture2D texture2 = ModLoader.GetTexture("Negadium/Projectiles/Summoner/HydraHeadMinion_Neck");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = new Vector2(player.Center.X, player.Center.Y);
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture2.Width * 0.5f, (float)texture2.Height * 0.5f);
            float num1 = (float)texture2.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture2, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    if (texture1 != null)
                        Main.spriteBatch.Draw(texture1, position - Main.screenPosition, sourceRectangle, color1, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}
