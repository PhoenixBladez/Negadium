using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.NPCs.Mharadium
{
    public class OrionSlime : ModNPC
    {
        private bool charging = false; // Detemines if the OrionSlime should reflect damage instead of taking it.
        private int chargingTime = 360; // 6 seconds reflect time of which the first second is not used to store energy.
        private int chargedPower = 0;

        private Vector2 eggPosition = Vector2.Zero;

        public override void SetDefaults()
        {
            npc.name = "Orion Slime";
            npc.width = 98;
            npc.height = 92;
            npc.damage = 1; // Insta kill see ModifyHitPlayer
            Main.npcFrameCount[npc.type] = 6;
            npc.defense = 75;
            npc.lifeMax = 150000;
            npc.knockBackResist = 0.0f;
            npc.value = Item.buyPrice(5, 0, 0, 0);
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.alpha = 30;
            npc.value = 10000f;
            npc.scale = 1.25F;
            music = 13;
        }

        //Gorateron, Allows you to modify the damage, etc., that this NPC does to a player.
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = target.statLifeMax2; // <---
            crit = true;
        }

        public override void AI()
        {
            #region Custom Handlers
            if(chargingTime > 0)
                --chargingTime;
            else if (charging)
            {
                charging = false;
                chargingTime = 0;
                Main.player[npc.target].statLife -= chargedPower / 20; // Damage the target player. Needs to be for close by players.
                if (Main.player[npc.target].statLife <= 0)
                    Main.player[npc.target].KillMe(chargedPower / 20, 1); // Kill the player.
                chargedPower = 0;
                Main.NewText("The Orion Slime stopped charging, Attack!", 255, 255, 0);
            }

            eggPosition = new Vector2(npc.Center.X - 11, npc.Center.Y - 12);
            #endregion

            float num1 = 1F;
            bool flag1 = false;
            bool flag2 = false;
            npc.aiAction = 0;
            if ((double)npc.ai[3] == 0.0 && npc.life > 0)
                npc.ai[3] = (float)npc.lifeMax;
            if ((double)npc.localAI[3] == 0.0 && Main.netMode != 1)
            {
                npc.ai[0] = -100F;
                npc.localAI[3] = 1F;
                npc.TargetClosest(true);
                npc.netUpdate = true;
            }
            if (Main.player[npc.target].dead) // If the targeted player is dead.
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead) // And there are no other players left to target.
                {
                    npc.timeLeft = 0; // Deactivate this npc.
                    if ((double)Main.player[npc.target].Center.X < (double)npc.Center.X)
                        npc.direction = 1;
                    else
                        npc.direction = -1;
                }
            }
            if (!Main.player[npc.target].dead && (double)npc.ai[2] >= 300.0 && ((double)npc.ai[1] < 5.0 && (double)npc.velocity.Y == 0.0))
            {
                npc.ai[0] = 0.0F;
                npc.ai[1] = 5F;
                npc.ai[2] = 0.0F;
                if (Main.netMode != 1)
                {
                    npc.TargetClosest(false);
                    Point point1 = Utils.ToTileCoordinates(npc.Center);
                    Point point2 = Utils.ToTileCoordinates(Main.player[npc.target].Center);
                    Vector2 vector2 = Main.player[npc.target].Center - npc.Center;
                    int num2 = 10;
                    int num3 = 0;
                    int num4 = 7;
                    int num5 = 0;
                    bool flag3 = false;
                    if ((double)vector2.Length() > 2000.0)
                    {
                        flag3 = true;
                        num5 = 100;
                    }
                    while (!flag3 && num5 < 100)
                    {
                        ++num5;
                        int index1 = Main.rand.Next(point2.X - num2, point2.X + num2 + 1);
                        int index2 = Main.rand.Next(point2.Y - num2, point2.Y + 1);
                        if ((index2 < point2.Y - num4 || index2 > point2.Y + num4 || (index1 < point2.X - num4 || index1 > point2.X + num4)) && (index2 < point1.Y - num3 || index2 > point1.Y + num3 || (index1 < point1.X - num3 || index1 > point1.X + num3)) && !Main.tile[index1, index2].nactive())
                        {
                            int index3 = index2;
                            int num6 = 0;
                            if (Main.tile[index1, index3].nactive() && Main.tileSolid[(int)Main.tile[index1, index3].type] && !Main.tileSolidTop[(int)Main.tile[index1, index3].type])
                            {
                                num6 = 1;
                            }
                            else
                            {
                                for (; num6 < 150 && index3 + num6 < Main.maxTilesY; ++num6)
                                {
                                    int index4 = index3 + num6;
                                    if (Main.tile[index1, index4].nactive() && Main.tileSolid[(int)Main.tile[index1, index4].type] && !Main.tileSolidTop[(int)Main.tile[index1, index4].type])
                                    {
                                        --num6;
                                        break;
                                    }
                                }
                            }
                            int index5 = index2 + num6;
                            bool flag4 = true;
                            if (flag4 && Main.tile[index1, index5].lava())
                                flag4 = false;
                            if (flag4 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                                flag4 = false;
                            if (flag4)
                            {
                                npc.localAI[1] = (float)(index1 * 16 + 8);
                                npc.localAI[2] = (float)(index5 * 16 + 16);
                                break;
                            }
                        }
                    }
                    if (num5 >= 100)
                    {
                        Vector2 bottom = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
                        npc.localAI[1] = bottom.X;
                        npc.localAI[2] = bottom.Y;
                    }
                }
            }

            if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                ++npc.ai[2];
            if ((double)Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 320.0)
                ++npc.ai[2];

            if ((double)npc.ai[1] == 5.0) // Burrowing?
            {
                flag1 = true;
                npc.aiAction = 1;
                ++npc.ai[0];
                num1 = (float)(0.5 + (double)MathHelper.Clamp((float)((60.0 - (double)npc.ai[0]) / 60.0), 0.0f, 1f) * 0.5);
                if ((double)npc.ai[0] >= 60.0)
                    flag2 = true;
                if ((double)npc.ai[0] >= 60.0 && Main.netMode != 1)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0.0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == 1 && (double)npc.ai[0] >= 120.0)
                {
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0.0f;
                }
                if (!flag2)
                {
                    for (int index1 = 0; index1 < 10; ++index1)
                    {
                        int index2 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, (int)byte.MaxValue, 80), 2f);
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 0.5f;
                    }
                }
            }
            else if ((double)npc.ai[1] == 6.0)
            {
                flag1 = true;
                npc.aiAction = 0;
                ++npc.ai[0];
                num1 = (float)(0.5 + (double)MathHelper.Clamp(npc.ai[0] / 30f, 0.0f, 1f) * 0.5);
                if ((double)npc.ai[0] >= 30.0 && Main.netMode != 1)
                {
                    npc.ai[1] = 0.0f;
                    npc.ai[0] = 0.0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == 1 && (double)npc.ai[0] >= 60.0)
                {
                    npc.ai[1] = 0.0f;
                    npc.ai[0] = 0.0f;
                    npc.TargetClosest(true);
                }
                for (int index1 = 0; index1 < 10; ++index1)
                {
                    int index2 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, 150, new Color(78, 136, (int)byte.MaxValue, 80), 2f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 2f;
                }
            }

            npc.dontTakeDamage = npc.hide = flag2;
            if ((double)npc.velocity.Y == 0.0)
            {
                npc.velocity.X = npc.velocity.X * 0.8F;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    npc.velocity.X = 0.0F;
                if (!flag1)
                {
                    npc.ai[0] += 2f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.8)
                        ++npc.ai[0];
                    if ((double)npc.life < (double)npc.lifeMax * 0.6)
                        ++npc.ai[0];
                    if ((double)npc.life < (double)npc.lifeMax * 0.4)
                        npc.ai[0] += 2f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.2)
                        npc.ai[0] += 3f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.1)
                        npc.ai[0] += 4f;

                    if ((double)npc.ai[0] >= 0.0)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if ((double)npc.ai[1] == 3.0)
                        {
                            npc.velocity.Y = -13f;
                            npc.velocity.X = npc.velocity.X + 3.5f * (float)npc.direction;
                            npc.ai[0] = -200f;
                            npc.ai[1] = 0.0f;
                        }
                        else if ((double)npc.ai[1] == 2.0)
                        {
                            npc.velocity.Y = -6f;
                            npc.velocity.X = npc.velocity.X + 4.5f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            ++npc.ai[1];
                        }
                        else
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X + 4f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            ++npc.ai[1];
                        }
                    }
                    else if ((double)npc.ai[0] >= -30.0)
                        npc.aiAction = 1;
                }
            }
            else if (npc.target < (int)byte.MaxValue && (npc.direction == 1 && (double)npc.velocity.X < 3.0 || npc.direction == -1 && (double)npc.velocity.X > -3.0))
            {
                if (npc.direction == -1 && (double)npc.velocity.X < 0.1 || npc.direction == 1 && (double)npc.velocity.X > -0.1)
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                else
                    npc.velocity.X = npc.velocity.X * 0.93f;
            }

            int index6 = Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, (int)byte.MaxValue, new Color(0, 80, (int)byte.MaxValue, 80), npc.scale * 1.2f);
            Main.dust[index6].noGravity = true;
            Main.dust[index6].velocity *= 0.5f;
            if (npc.life <= 0)
              return;

            float num7 = ((npc.life / npc.lifeMax) * 0.5F + 0.75F) * num1;
            if ((double)num7 != (double)npc.scale)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)npc.height;
                npc.width = (int)(98.0 * (double)npc.scale);
                npc.height = (int)(92.0 * (double)npc.scale);
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)npc.height;
            }
            if (Main.netMode == 1 || npc.life >= npc.ai[3])
                return;

            // Apply on-hit effects.
            if (!charging && Main.rand.Next(200) == 0) // Shield charge start.
            {
                charging = true;                
                chargingTime = 360;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                Main.NewText("The Orion Slime is storing power, stop attacking!", 255, 255, 0, true);
            }
            else if (charging && chargingTime <= 300) // If the Orion Slime is hit while charging and is charging at least one second.
            {
                chargedPower += (int)npc.ai[3] - npc.life; // Store the damage taken.
            }

            npc.ai[3] = (float)npc.life; // Set the new life value.            
        }

        public override void FindFrame(int frameHeight)
        {
            int num1 = 0;
            if (npc.aiAction == 0)
                num1 = (double)npc.velocity.Y >= 0.0 ? ((double)npc.velocity.Y <= 0.0 ? ((double)npc.velocity.X == 0.0 ? 0 : 1) : 3) : 2;
            else if (npc.aiAction == 1)
                num1 = 4;

            if ((double)npc.velocity.Y != 0.0)
            {
                if (npc.frame.Y < frameHeight * 4)
                {
                    npc.frame.Y = frameHeight * 4;
                    npc.frameCounter = 0.0;
                }
                if (++npc.frameCounter >= 4.0)
                    npc.frame.Y = frameHeight * 5;
            }
            else
            {
                if (npc.frame.Y >= frameHeight * 5)
                {
                    npc.frame.Y = frameHeight * 4;
                    npc.frameCounter = 0.0;
                }
                ++npc.frameCounter;
                if (num1 > 0)
                    ++npc.frameCounter;
                if (num1 == 4)
                    ++npc.frameCounter;
                if (npc.frameCounter >= 8.0)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                    if (npc.frame.Y >= frameHeight * 4)
                        npc.frame.Y = 0;
                }
            }
        }

        public override void PostDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D eggTexture = ModLoader.GetTexture("Mharadium/NPCs/Boss/OrionSlime_Egg");

            Color color1 = Lighting.GetColor((int)eggPosition.X / 16, (int)((double)eggPosition.Y / 16.0));
            color1 = npc.GetAlpha(color1);

            Rectangle? sourceRectangle = new Rectangle?();
            Main.spriteBatch.Draw(eggTexture, eggPosition - Main.screenPosition, sourceRectangle, color1);

            base.PostDraw(spriteBatch, drawColor);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            base.BossLoot(ref name, ref potionType);
        }
        public override void NPCLoot()
        {              
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DemonEgg"));            
        }
    }
}
