using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.NPCs.Negativity
{
    public class HydraHead : ModNPC
    {
        private int body
        {
            get
            {
                return (int)npc.ai[0];
            }
        }
        private int headID
        {
            get
            {
                return (int)npc.ai[1];
            }
        }

        private Vector2 rootPos
        {
            get { return Main.npc[body].Center; }
        }

        public override void SetDefaults()
        {
            npc.name = "Void Hydra Head";
            npc.width = 52;
            npc.height = 36;
            Main.npcFrameCount[npc.type] = 4;

            npc.noGravity = true; // Do not use gravity.
            npc.noTileCollide = true; // Do not collide with any tile.

            npc.lifeMax = 35000;
            npc.damage = 20;
            npc.knockBackResist = 0.0F;
            npc.aiStyle = -1;

            npc.npcSlots = 1;

            npc.soundHit = 1;
            npc.soundKilled = 1;

            npc.scale = 1.5F;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1F;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;

            npc.spriteDirection = npc.direction;
        }

        public override void AI()
        {
            if (!Main.npc[body].active)
            {
                npc.active = false;
                return;
            }

            npc.TargetClosest(true);
            float num1 = 0.05F;
            float num2 = 1000F;

            ++npc.ai[2];
            if ((double)npc.ai[2] > 300.0)
            {
                num2 = (float)(int)((double)num2 * 1.3);
                if ((double)npc.ai[2] > 450.0)
                    npc.ai[2] = 0.0f;
            }

            Vector2 vector2 = new Vector2(rootPos.X, rootPos.Y - 300);
            float xDir = (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2)) - (float)(npc.width / 2) - vector2.X;
            float yDir = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - (float)(npc.height / 2) - vector2.Y;
            float num4 = (float)Math.Sqrt((double)xDir * (double)xDir + (double)yDir * (double)yDir);
            if ((double)num4 > (double)num2)
            {
                float num5 = num2 / num4;
                xDir *= num5;
                yDir *= num5;
            }
            if ((double)npc.position.X < (double)rootPos.X + (double)xDir)
            {
                npc.velocity.X = npc.velocity.X + num1;
                if ((double)npc.velocity.X < 0.0 && (double)xDir > 0.0)
                    npc.velocity.X = npc.velocity.X + num1 * 1.5f;
            }
            else if ((double)npc.position.X > (double)rootPos.X + (double)xDir)
            {
                npc.velocity.X = npc.velocity.X - num1;
                if ((double)npc.velocity.X > 0.0 && (double)xDir < 0.0)
                    npc.velocity.X = npc.velocity.X - num1 * 1.5f;
            }
            if ((double)npc.position.Y < (double)rootPos.Y + (double)yDir)
            {
                npc.velocity.Y = npc.velocity.Y + num1;
                if ((double)npc.velocity.Y < 0.0 && (double)yDir > 0.0)
                    npc.velocity.Y = npc.velocity.Y + num1 * 1.5f;
            }
            else if ((double)npc.position.Y > (double)rootPos.Y + (double)yDir)
            {
                npc.velocity.Y = npc.velocity.Y - num1;
                if ((double)npc.velocity.Y > 0.0 && (double)yDir < 0.0)
                    npc.velocity.Y = npc.velocity.Y - num1 * 1.5f;
            }
            // Speed capping
            if ((double)npc.velocity.X > 4.0)
                npc.velocity.X = 4f;
            if ((double)npc.velocity.X < -4.0)
                npc.velocity.X = -4f;
            if ((double)npc.velocity.Y > 4.0)
                npc.velocity.Y = 4f;
            if ((double)npc.velocity.Y < -4.0)
                npc.velocity.Y = -4f;

            npc.rotation = MathHelper.Lerp(npc.rotation,
                (float)Math.Atan2(Main.player[npc.target].Center.X - npc.Center.X,
                Main.player[npc.target].Center.Y - npc.Center.Y) + 1.57f, 0.1F);

            if (npc.collideX)
            {
                npc.netUpdate = true;
                npc.velocity.X = npc.oldVelocity.X * -0.7f;
                if ((double)npc.velocity.X > 0.0 && (double)npc.velocity.X < 2.0)
                    npc.velocity.X = 2f;
                if ((double)npc.velocity.X < 0.0 && (double)npc.velocity.X > -2.0)
                    npc.velocity.X = -2f;
            }
            if (npc.collideY)
            {
                npc.netUpdate = true;
                npc.velocity.Y = npc.oldVelocity.Y * -0.7f;
                if ((double)npc.velocity.Y > 0.0 && (double)npc.velocity.Y < 2.0)
                    npc.velocity.Y = 2f;
                if ((double)npc.velocity.Y < 0.0 && (double)npc.velocity.Y > -2.0)
                    npc.velocity.Y = -2f;
            }
            if (Main.netMode == 1)
                return;

            if (!Main.player[npc.target].dead)
            {
                if (npc.justHit)
                    npc.localAI[0] = 120f;
                --npc.localAI[0];
                if ((double)npc.localAI[0] <= 40.0)
                {
                    if (!Collision.SolidCollision(npc.position, npc.width, npc.height) && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        float num5 = 10f;
                        vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num6 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector2.X + (float)Main.rand.Next(-10, 11);
                        float num7 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector2.Y + (float)Main.rand.Next(-10, 11);
                        float num8 = (float)Math.Sqrt((double)num6 * (double)num6 + (double)num7 * (double)num7);
                        float num9 = num5 / num8;
                        xDir = num6 * num9;
                        yDir = num7 * num9;
                        int Damage = 22;
                        if (Main.expertMode)
                            Damage = (int)((double)Damage * 0.8);
                        int Type = 101;
                        int index = Projectile.NewProjectile(vector2.X, vector2.Y, xDir, yDir, Type, Damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        //npc.localAI[0] = 0.0f;
                        if (npc.localAI[0] <= 0)
                            npc.localAI[0] = 120F;
                    }
                    else
                        npc.localAI[0] = 60F;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture1 = (Texture2D)null;
            Color color1 = Color.White;
            Texture2D texture2 = ModLoader.GetTexture("Negadium/NPCs/Negativity/HydraHead_Neck");

            Vector2 position = npc.Center;
            Vector2 mountedCenter = new Vector2(rootPos.X, rootPos.Y - 300);
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
                    color2 = npc.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture2, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    if (texture1 != null)
                        Main.spriteBatch.Draw(texture1, position - Main.screenPosition, sourceRectangle, color1, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}
