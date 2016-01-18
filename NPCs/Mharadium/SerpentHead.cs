using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Negadium.NPCs.Mharadium
{
    public class SerpentHead : ModNPC
    {
        private bool firstUpdate = true;

        public override void SetDefaults()
        {
            npc.displayName = "Serpent";
            npc.width = 38;
            npc.height = 56;
            npc.npcSlots = 5f;
            npc.lifeMax = 5000;
            npc.aiStyle = 6;
            npc.knockBackResist = 0;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.netAlways = true;
            npc.boss = true;
            animationType = 87;
        }

        public override void AI()
        {
            if (this.npc.target < 0 || this.npc.target == 255 || Main.player[this.npc.target].dead)
            {
                this.npc.TargetClosest(true);
            }
            if (Main.player[this.npc.target].dead && this.npc.timeLeft > 300)
            {
                this.npc.timeLeft = 300;
            }

            if (firstUpdate && Main.netMode != 1)
            {
                this.npc.ai[2] = (float)npc.whoAmI;
                this.npc.realLife = npc.whoAmI;
                int targetNPC = this.npc.whoAmI;

                if (npc.ai[0] == 0) // Setup Natas 
                {
                    npc.displayName = npc.name = "Natas";
                    npc.damage = 300;
                    npc.life = npc.lifeMax = 300000;
                    npc.defense = 85;

                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, npc.type, 0, 1);
                }
                else if (npc.ai[0] == 1) // Reficul
                {
                    npc.displayName = npc.name = "Reficul";
                    npc.damage = 700;
                    npc.life = npc.lifeMax = 200000;
                    npc.defense = 70;
                }

                for (int part = 0; part < 24; ++part)
                {
                    int partType = mod.NPCType("SerpentBody");

                    if (part == 23)
                    {
                        partType = mod.NPCType("SerpentTail");
                    }

                    int newNPC = NPC.NewNPC((int)(this.npc.position.X + (float)(this.npc.width / 2)), (int)(this.npc.position.Y + (float)this.npc.height), partType, this.npc.whoAmI);
                    Main.npc[newNPC].ai[2] = (float)this.npc.whoAmI;
                    Main.npc[newNPC].realLife = this.npc.whoAmI;
                    Main.npc[newNPC].ai[1] = (float)targetNPC;
                    Main.npc[newNPC].displayName = npc.displayName;
                    Main.npc[targetNPC].ai[0] = (float)newNPC;
                    NetMessage.SendData(23, -1, -1, "", newNPC, 0f, 0f, 0f, 0);
                    targetNPC = newNPC;
                }

                firstUpdate = false;
            }
            
            int leftTilePosX = (int)(this.npc.position.X / 16f) - 1;
            int rightTilePosX = (int)((this.npc.position.X + (float)this.npc.width) / 16f) + 2;
            int topTilePosY = (int)(this.npc.position.Y / 16f) - 1;
            int bottomTilePosY = (int)((this.npc.position.Y + (float)this.npc.height) / 16f) + 2;
            if (leftTilePosX < 0)
            {
                leftTilePosX = 0;
            }
            if (rightTilePosX > Main.maxTilesX)
            {
                rightTilePosX = Main.maxTilesX;
            }
            if (topTilePosY < 0)
            {
                topTilePosY = 0;
            }
            if (bottomTilePosY > Main.maxTilesY)
            {
                bottomTilePosY = Main.maxTilesY;
            }

            npc.spriteDirection = npc.velocity.X < 0 ? 1 : -1;

            float num115 = 16f;
            float num116 = 0.4f;

            Vector2 vector14 = new Vector2(this.npc.position.X + (float)this.npc.width * 0.5f, this.npc.position.Y + (float)this.npc.height * 0.5f);
            float num118 = Main.rand.Next(-500, 500) + Main.player[this.npc.target].position.X + (float)(Main.player[this.npc.target].width / 2);
            float num119 = Main.rand.Next(-500, 500) + Main.player[this.npc.target].position.Y + (float)(Main.player[this.npc.target].height / 2);
            num118 = (float)((int)(num118 / 16f) * 16);
            num119 = (float)((int)(num119 / 16f) * 16);
            vector14.X = (float)((int)(vector14.X / 16f) * 16);
            vector14.Y = (float)((int)(vector14.Y / 16f) * 16);
            num118 -= vector14.X;
            num119 -= vector14.Y;
            float num120 = (float)Math.Sqrt((double)(num118 * num118 + num119 * num119));

            float num123 = Math.Abs(num118);
            float num124 = Math.Abs(num119);
            float num125 = num115 / num120;
            num118 *= num125;
            num119 *= num125;

            bool flag14 = false;
            if (((this.npc.velocity.X > 0f && num118 < 0f) || (this.npc.velocity.X < 0f && num118 > 0f) || (this.npc.velocity.Y > 0f && num119 < 0f) || (this.npc.velocity.Y < 0f && num119 > 0f)) && Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) > num116 / 2f && num120 < 300f)
            {
                flag14 = true;
                if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < num115)
                {
                    this.npc.velocity *= 1.1f;
                }
            }
            if (this.npc.position.Y > Main.player[this.npc.target].position.Y || (double)(Main.player[this.npc.target].position.Y / 16f) > Main.worldSurface || Main.player[this.npc.target].dead)
            {
                flag14 = true;
                if (Math.Abs(this.npc.velocity.X) < num115 / 2f)
                {
                    if (this.npc.velocity.X == 0f)
                    {
                        this.npc.velocity.X = this.npc.velocity.X - (float)this.npc.direction;
                    }
                    this.npc.velocity.X = this.npc.velocity.X * 1.1f;
                }
                else
                {
                    if (this.npc.velocity.Y > -num115)
                    {
                        this.npc.velocity.Y = this.npc.velocity.Y - num116;
                    }
                }
            }
            if (!flag14)
            {
                if ((this.npc.velocity.X > 0f && num118 > 0f) || (this.npc.velocity.X < 0f && num118 < 0f) || (this.npc.velocity.Y > 0f && num119 > 0f) || (this.npc.velocity.Y < 0f && num119 < 0f))
                {
                    if (this.npc.velocity.X < num118)
                    {
                        this.npc.velocity.X = this.npc.velocity.X + num116;
                    }
                    else
                    {
                        if (this.npc.velocity.X > num118)
                        {
                            this.npc.velocity.X = this.npc.velocity.X - num116;
                        }
                    }
                    if (this.npc.velocity.Y < num119)
                    {
                        this.npc.velocity.Y = this.npc.velocity.Y + num116;
                    }
                    else
                    {
                        if (this.npc.velocity.Y > num119)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y - num116;
                        }
                    }
                    if ((double)Math.Abs(num119) < (double)num115 * 0.2 && ((this.npc.velocity.X > 0f && num118 < 0f) || (this.npc.velocity.X < 0f && num118 > 0f)))
                    {
                        if (this.npc.velocity.Y > 0f)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y + num116 * 2f;
                        }
                        else
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y - num116 * 2f;
                        }
                    }
                    if ((double)Math.Abs(num118) < (double)num115 * 0.2 && ((this.npc.velocity.Y > 0f && num119 < 0f) || (this.npc.velocity.Y < 0f && num119 > 0f)))
                    {
                        if (this.npc.velocity.X > 0f)
                        {
                            this.npc.velocity.X = this.npc.velocity.X + num116 * 2f;
                        }
                        else
                        {
                            this.npc.velocity.X = this.npc.velocity.X - num116 * 2f;
                        }
                    }
                }
                else
                {
                    if (num123 > num124)
                    {
                        if (this.npc.velocity.X < num118)
                        {
                            this.npc.velocity.X = this.npc.velocity.X + num116 * 1.1f;
                        }
                        else
                        {
                            if (this.npc.velocity.X > num118)
                            {
                                this.npc.velocity.X = this.npc.velocity.X - num116 * 1.1f;
                            }
                        }
                        if ((double)(Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y)) < (double)num115 * 0.5)
                        {
                            if (this.npc.velocity.Y > 0f)
                            {
                                this.npc.velocity.Y = this.npc.velocity.Y + num116;
                            }
                            else
                            {
                                this.npc.velocity.Y = this.npc.velocity.Y - num116;
                            }
                        }
                    }
                    else
                    {
                        if (this.npc.velocity.Y < num119)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y + num116 * 1.1f;
                        }
                        else
                        {
                            if (this.npc.velocity.Y > num119)
                            {
                                this.npc.velocity.Y = this.npc.velocity.Y - num116 * 1.1f;
                            }
                        }
                        if ((double)(Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y)) < (double)num115 * 0.5)
                        {
                            if (this.npc.velocity.X > 0f)
                            {
                                this.npc.velocity.X = this.npc.velocity.X + num116;
                            }
                            else
                            {
                                this.npc.velocity.X = this.npc.velocity.X - num116;
                            }
                        }
                    }
                }
            }
            this.npc.rotation = (float)Math.Atan2((double)this.npc.velocity.Y, (double)this.npc.velocity.X) + 1.57f;

            npc.netUpdate = true;

            /*if (Main.player[npc.target].dead)
            {
                npc.velocity.Y -= 0.04f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                    return;
                }
                {
                    npc.netUpdate = false;
                    npc.ai[1]++;

                    if (npc.ai[1] >= 10 && Main.netMode != 1)
                    {
                        int ChaosWyrmCloneHeadSpawn = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), "Tremor:ChaosWyrmCloneHead", 0);
                        npc.ai[1] = 20 - Main.rand.Next(360);
                        if (Main.netMode == 2 && ChaosWyrmCloneHeadSpawn < 200)
                        {
                            NetMessage.SendData(23, -1, -1, "", ChaosWyrmCloneHeadSpawn, 0f, 0f, 0f, 0);
                        }
                    }
                }
            }*/
        }
    }
}
