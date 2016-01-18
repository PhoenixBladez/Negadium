using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.NPCs.Mharadium
{
    public class SatansServant : ModNPC
    {
        private int currentAI = -1;

        private int minionCount = 5;
        private int[] minions;

        public override void SetDefaults()
        {
            npc.name = "Satan's Servant";
            npc.width = 18;
            npc.height = 48;
            npc.damage = 150;
            npc.defense = 100;
            npc.lifeMax = 200000;
            npc.soundHit = 2;
            npc.soundKilled = 2;
            npc.knockBackResist = 0F;
            npc.value = Item.buyPrice(5, 0, 0, 0);
            npc.boss = true;
            music = 25;
        }

        public override void AI()
        {
            npc.TargetClosest(true);

            if (npc.localAI[1] == 0)
            {
                npc.localAI[1] = 1;
                minions = new int[minionCount];
            }
            for (int i = 0; i < minionCount; ++i)
            {
                if (minions[i] == 0 || !Main.npc[minions[i]].active)
                {
                    int newMinion = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-200, 200), (int)npc.position.Y + Main.rand.Next(-200, 200), 62);
                    Main.npc[newMinion].ai[1] = npc.whoAmI;
                    Main.npc[newMinion].lifeMax = 50000;
                    Main.npc[newMinion].life = Main.npc[newMinion].lifeMax;
                    Main.npc[newMinion].damage = 150;
                    Main.npc[newMinion].displayName = "Servants Minion";
                    minions[i] = newMinion;
                }
            }

            if (npc.life >= (npc.lifeMax / 2))
            {
                if (npc.ai[0] == 0.0F)
                    npc.ai[0] = 500F;

                // Teleport effects.
                if ((double)npc.ai[2] != 0.0 && (double)npc.ai[3] != 0.0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 8);
                    for (int i = 0; i < 50; ++i)
                    {
                        int dustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 174, 0.0F, 0.0F, 100, new Color(), 1.5F);
                        Main.dust[dustID].velocity *= 3F;
                        Main.dust[dustID].noGravity = true;
                    }
                    npc.position.X = (float)(npc.ai[2] * 16 - (npc.width / 2) + 8);
                    npc.position.Y = npc.ai[3] * 16 - npc.height;
                    npc.velocity.X = 0.0F;
                    npc.velocity.Y = 0.0F;
                    npc.ai[2] = 0.0F;
                    npc.ai[3] = 0.0F;
                    Main.PlaySound(2, npc.position, 8);
                    for (int i = 0; i < 50; ++i)
                    {
                        int dustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 174, 0.0F, 0.0F, 100, new Color(), 1.5F);
                        Main.dust[dustID].velocity *= 3F;
                        Main.dust[dustID].noGravity = true;
                    }
                }
                ++npc.ai[0];

                if ((double)npc.ai[0] == 50.0 || (double)npc.ai[0] == 100.0 || (double)npc.ai[0] == 150.0 ||
                    (double)npc.ai[0] == 200.0 || (double)npc.ai[0] == 250.0 || (double)npc.ai[0] == 300.0 || (double)npc.ai[0] == 350.0)
                {
                    npc.ai[1] = 30f;
                    npc.netUpdate = true;
                }
                if ((double)npc.ai[0] > 400.0)
                    npc.ai[0] = 650F;

                if (((double)npc.ai[0] >= 650.0 || Vector2.Distance(npc.position, Main.player[npc.target].position) < 200) && Main.netMode != 1)
                {
                    npc.ai[0] = 1F;
                    npc.ai[1] = -1;
                    Teleport();
                }
                if ((double)npc.ai[1] > 0.0) // Shooting
                {
                    --npc.ai[1];
                    if (npc.ai[1] == 25.0F)
                    {
                        if (Main.netMode != 1)
                        {
                            float num1 = 10F;
                            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5F, npc.position.Y); // The center of this npc.
                            float dirX = Main.player[npc.target].position.X + Main.player[npc.target].width * 0.5F - vector2.X; // The direction over the X axis.
                            float dirY = Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5F - vector2.Y; // The direction over the Y axis.

                            float angle = (float)Math.Sqrt((double)dirX * (double)dirX + (double)dirY * (double)dirY); // Get the angle towards the target.
                            float num7 = num1 / angle;
                            float speedX = dirX * num7;
                            float speedY = dirY * num7;
                            int damage = 40;
                            int type = mod.ProjectileType("MharadiumInferno");
                            int projID = Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, type, damage, 0.0F, Main.myPlayer);
                            Main.projectile[projID].timeLeft = 300;
                            npc.localAI[0] = 0.0f;
                        }
                    }
                }
            }
            else
            {
                if (currentAI < 0) // First time in rage mode.
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    currentAI = 0;
                }

                if (currentAI == 0)
                {
                    ++npc.ai[0];
                    if ((double)npc.ai[0] >= 390.0) // 1.5 second cooldown betweens AI version 0 and 1
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        currentAI = 1;
                    }

                    if ((double)npc.ai[0] == 25.0 || (double)npc.ai[0] == 50.0 || (double)npc.ai[0] == 75.0 ||
                        (double)npc.ai[0] == 100.0 || (double)npc.ai[0] == 125.0 || (double)npc.ai[0] == 150.0 ||
                        (double)npc.ai[0] == 175.0 || (double)npc.ai[0] == 200.0 || (double)npc.ai[0] == 225.0 ||
                        (double)npc.ai[0] == 250.0 || (double)npc.ai[0] == 275.0 || (double)npc.ai[0] == 300.0)
                    {
                        npc.ai[1] = 30f;
                        npc.netUpdate = true;
                    }
                    if ((double)npc.ai[1] > 0.0) // Shooting
                    {
                        --npc.ai[1];
                        if (npc.ai[1] == 25.0F)
                        {
                            if (Main.netMode != 1)
                            {
                                float num1 = 10F;
                                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5F, npc.position.Y); // The center of this npc.
                                float dirX = Main.player[npc.target].position.X + Main.player[npc.target].width * 0.5F - vector2.X; // The direction over the X axis.
                                float dirY = Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5F - vector2.Y; // The direction over the Y axis.

                                float angle = (float)Math.Sqrt((double)dirX * (double)dirX + (double)dirY * (double)dirY); // Get the angle towards the target.
                                float num7 = num1 / angle;
                                float speedX = dirX * num7;
                                float speedY = dirY * num7;
                                int damage = 40;
                                int type = mod.ProjectileType("MharadiumInferno");
                                int projID = Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, type, damage, 0.0F, Main.myPlayer, 0.0f, 0.0f);
                                Main.projectile[projID].timeLeft = 300;
                            }
                        }
                    }
                }
                else if (currentAI == 1)
                {
                    npc.ai[0] += 2;
                    if (npc.ai[0] % 10 == 0) // Will shoot a total of 36 lazers.
                    {
                        float num1 = 20F;
                        Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5F, npc.position.Y); // The center of this npc.
                        float angle = (float)(Math.PI / 180) * npc.ai[0];
                        Vector2 dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        float num7 = num1 / angle;
                        float speedX = dir.X * num7;
                        float speedY = dir.Y * num7;
                        int damage = 40;
                        int type = mod.ProjectileType("MharadiumLaser");
                        int projID = Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, type, damage, 0.0F, Main.myPlayer);
                        Main.projectile[projID].timeLeft = 300;
                    }
                    if (npc.ai[0] > 360)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        currentAI = 0;
                    }
                }
            }

            if (Main.rand.Next(2) != 0)
                return;
            int ID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width, npc.height, 174, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, new Color(), 1f);
            Main.dust[ID].noGravity = true;
            Main.dust[ID].velocity *= 0.4f;
            Dust dust = Main.dust[ID];
            dust.velocity.Y = dust.velocity.Y - 0.7f;
        }

        private void Teleport()
        {
            int targetTilePosX = (int)Main.player[npc.target].position.X / 16;
            int targetTilePosY = (int)Main.player[npc.target].position.Y / 16;
            int npcTilePosX = (int)npc.position.X / 16;
            int npcTilePosY = (int)npc.position.Y / 16;
            int num5 = 35; // The random range to which the npc teleports.
            int num6 = 0;
            bool flag1 = false;
            if ((double)Math.Abs(npc.position.X - Main.player[npc.target].position.X) + (double)Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000.0)
            {
                num6 = 100;
                flag1 = true;
            }
            while (!flag1 && num6 < 100)
            {
                ++num6;
                int X = Main.rand.Next(targetTilePosX - num5, targetTilePosX + num5);
                for (int Y = Main.rand.Next(targetTilePosY - num5, targetTilePosY + num5); Y < targetTilePosY + num5; ++Y)
                {
                    if ((Y < targetTilePosY - 4 || Y > targetTilePosY + 4 || (X < targetTilePosX - 4 || X > targetTilePosX + 4)) &&
                        (Y < npcTilePosY - 1 || Y > npcTilePosY + 1 || (X < npcTilePosX - 1 || X > npcTilePosX + 1)) && Main.tile[X, Y].nactive())
                    {
                        if (Main.tileSolid[(int)Main.tile[X, Y].type] && !Collision.SolidTiles(X - 1, X + 1, Y - 4, Y - 1))
                        {
                            npc.ai[1] = 20f;
                            npc.ai[2] = (float)X;
                            npc.ai[3] = (float)Y;
                            flag1 = true;
                            break;
                        }
                    }
                }
            }
            npc.netUpdate = true;
        }

        public override void NPCLoot()
        {
            int oreAmount = Main.rand.Next(50, 71);            
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MharadiumForge"));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MharadiumOre"), oreAmount);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            base.BossLoot(ref name, ref potionType);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = 0;
            npc.spriteDirection = npc.direction;
        }
    }
}
