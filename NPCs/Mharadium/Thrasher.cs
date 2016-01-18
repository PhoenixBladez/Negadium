using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.NPCs.Mharadium
{
    public class Thrasher : ModNPC
    {
        private int currentAI = -1;

        public override void SetDefaults()
        {
            npc.name = "Thrasher";
            npc.width = 15;
            npc.height = 38;
            npc.damage = 300;
            npc.defense = 75;
            npc.lifeMax = 125000;
            npc.soundHit = 2;
            npc.soundKilled = 2;
            npc.knockBackResist = 0F;
            npc.value = Item.buyPrice(5, 0, 0, 0);
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Custom, "Sounds/BossMusic/OrionSlime");
        }

        public override void AI()
        {
            if ((double)npc.ai[0] != -1.0 && Main.rand.Next(1000) == 0)
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, Main.rand.Next(88, 92));
            bool expertMode = Main.expertMode;
            bool isHalfLife = npc.life <= npc.lifeMax / 2; // flag1

            int num2 = 18;
            int num3 = 3;
            int damage2 = 30;

            bool flag3 = false;
            bool flag4 = false;
            if (isHalfLife) npc.defense = (int)((double)npc.defense * 0.649999976158142);

            if ((double)npc.ai[0] == 5.0 && (double)npc.ai[1] >= 120 && ((double)npc.ai[1] < 420.0 && npc.justHit))
            {
                npc.ai[0] = 0.0f;
                npc.ai[1] = 0.0f;
                ++npc.ai[3];
                npc.velocity = Vector2.Zero;
                npc.netUpdate = true;

                Main.projectile[(int)npc.ai[2]].ai[1] = -1F;
                Main.projectile[(int)npc.ai[2]].netUpdate = true;
            }
            Player player = Main.player[npc.target]; // Cache the target
            // Check if target is valid. If not, get a new one.
            if (npc.target < 0 || npc.target == (int)byte.MaxValue || (player.dead || !player.active))
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }
            // If the player is dead or the distance between this boss and the player is larger than 350 blocks.
            if (player.dead || (double)Vector2.Distance(player.Center, npc.Center) > 5600.0)
            {
                npc.life = 0;
                npc.HitEffect(0, 10);
                npc.active = false;
                NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1F);
            }

            float num10 = npc.ai[3];
            if ((double)npc.localAI[0] == 0.0)
            {
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 89);
                npc.localAI[0] = 1F;
                npc.alpha = (int)byte.MaxValue;
                npc.rotation = 0.0F;
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1F;
                    npc.netUpdate = true;
                }
            }
            if ((double)npc.ai[0] == -1.0)
            {
                npc.alpha = 0;
                ++npc.ai[1];
                if ((double)npc.ai[1] >= 420.0)
                {
                    npc.ai[0] = 0.0f;
                    npc.ai[1] = 0.0f;
                    npc.netUpdate = true;
                }
                else if ((double)npc.ai[1] > 360.0)
                {
                    NPC npcCopy = npc;
                    Vector2 velocity = npcCopy.velocity * 0.95F;
                    npcCopy.velocity = velocity;
                    if ((double)npc.localAI[2] != 13.0)
                        Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 105);
                    npc.localAI[2] = 13F;
                }
                else if ((double)npc.ai[1] > 300.0)
                {
                    npc.velocity = -Vector2.UnitY;
                    npc.localAI[2] = 10F;
                }
                else
                    npc.localAI[2] = (double)npc.ai[1] <= 120.0 ? 0.0F : 1F;
                flag3 = true;
                flag4 = true;
            }
            if ((double)npc.ai[0] == 0.0)
            {
                if ((double)npc.ai[1] == 0.0)
                    npc.TargetClosest(false);
                npc.localAI[2] = 10F;
                int num9 = Math.Sign(player.Center.X - npc.Center.X); // Get the direction towards the target player.
                if (num9 != 0)
                    npc.direction = npc.spriteDirection = num9; // Set the direction if it's not 0.
                ++npc.ai[1];
                if ((double)npc.ai[1] >= 40.0)
                {
                    int num11 = 0;
                    if (isHalfLife)
                    {
                        switch ((int)npc.ai[3])
                        {
                            case 0:
                                num11 = 0;
                                break;
                            case 1:
                                num11 = 1;
                                break;
                            case 2:
                                num11 = 0;
                                break;
                            case 3:
                                num11 = 5;
                                break;
                            case 4:
                                num11 = 0;
                                break;
                            case 5:
                                num11 = 3;
                                break;
                            case 6:
                                num11 = 0;
                                break;
                            case 7:
                                num11 = 5;
                                break;
                            case 8:
                                num11 = 0;
                                break;
                            case 9:
                                num11 = 2;
                                break;
                            case 10:
                                num11 = 0;
                                break;
                            case 11:
                                num11 = 3;
                                break;
                            case 12:
                                num11 = 0;
                                break;
                            case 13:
                                num11 = 4;
                                npc.ai[3] = -1f;
                                break;
                            default:
                                npc.ai[3] = -1f;
                                break;
                        }
                    }
                    else
                    {
                        switch ((int)npc.ai[3])
                        {
                            case 0:
                                num11 = 0;
                                break;
                            case 1:
                                num11 = 1;
                                break;
                            case 2:
                                num11 = 0;
                                break;
                            case 3:
                                num11 = 2;
                                break;
                            case 4:
                                num11 = 0;
                                break;
                            case 5:
                                num11 = 3;
                                break;
                            case 6:
                                num11 = 0;
                                break;
                            case 7:
                                num11 = 1;
                                break;
                            case 8:
                                num11 = 0;
                                break;
                            case 9:
                                num11 = 2;
                                break;
                            case 10:
                                num11 = 0;
                                break;
                            case 11:
                                num11 = 4;
                                npc.ai[3] = -1f;
                                break;
                            default:
                                npc.ai[3] = -1f;
                                break;
                        }
                    }

                    int maxValue = 6;
                    if (npc.life < npc.lifeMax / 3)
                        maxValue = 4;
                    if (npc.life < npc.lifeMax / 4)
                        maxValue = 3;
                    if (expertMode && isHalfLife && (Main.rand.Next(maxValue) == 0 && num11 != 0) &&
                        (num11 != 4 && num11 != 5 && NPC.CountNPCS(523) < 10))
                    {
                        num11 = 6;
                    }
                    if (num11 == 0)
                    {
                        int yMax = Main.rand.Next(50, 151);
                        int yMin = Main.rand.Next(-150, -49);
                        int xMax = Main.rand.Next(50, 151);
                        int xMin = Main.rand.Next(-150, -49);

                        float num12 = (float)Math.Ceiling((double)(player.Center + 
                            new Vector2(Main.rand.Next(xMin, xMax), Main.rand.Next(yMin, yMax)) - npc.Center).Length() / 50);
                        if ((double)num12 == 0.0)
                            num12 = 1F;

                        NPC npc1 = Main.npc[npc.whoAmI];

                        Vector2 vector2_1 = Utils.RotatedBy(new Vector2(-1, -1f), (double)0, new Vector2()) * 
                            new Vector2(Main.rand.Next(-300, 301), Main.rand.Next(-300, 301));
                        Vector2 vector2_2 = player.Center + vector2_1 - npc.Center;
                        npc1.ai[0] = 1f;
                        npc1.ai[1] = num12 * 2f;
                        npc1.velocity = vector2_2 / num12;
                        if (npc.whoAmI >= npc1.whoAmI)
                        {
                            NPC npc2 = npc1;
                            Vector2 vector2_3 = npc2.position - npc1.velocity;
                            npc2.position = vector2_3;
                        }
                        npc1.netUpdate = true;
                    }
                    if (num11 == 1)
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = 0.0f;
                    }
                    else if (num11 == 2)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0.0f;
                    }
                    else if (num11 == 3)
                    {
                        npc.ai[0] = 4f;
                        npc.ai[1] = 0.0f;
                    }
                    else if (num11 == 4)
                    {
                        npc.ai[0] = 5f;
                        npc.ai[1] = 0.0f;
                    }
                    if (num11 == 5)
                    {
                        npc.ai[0] = 7f;
                        npc.ai[1] = 0.0f;
                    }
                    if (num11 == 6)
                    {
                        npc.ai[0] = 8f;
                        npc.ai[1] = 0.0f;
                    }
                    npc.netUpdate = true;
                }
            }
            else if ((double)npc.ai[0] == 1.0) // Teleporting-ish. 
            {
                flag3 = true;
                npc.localAI[2] = 10F;
                if ((double)(int)npc.ai[1] % 2.0 != 0.0 && (double)npc.ai[1] != 1.0)
                {
                    NPC npcCopy = npc;
                    Vector2 vector2 = npcCopy.position - npc.velocity;
                    npcCopy.position = vector2;
                }
                --npc.ai[1];
                if ((double)npc.ai[1] <= 0.0)
                {
                    npc.ai[0] = 0.0F;
                    npc.ai[1] = 0.0F;
                    ++npc.ai[3];
                    npc.velocity = Vector2.Zero;
                    npc.netUpdate = true;
                }
            } 
            else if ((double)npc.ai[0] == 2.0) // Homing fireballs. 
            {
                npc.localAI[2] = 11F;
                Vector2 vec1 = Vector2.Normalize(player.Center - npc.Center);
                if (Utils.HasNaNs(vec1))
                    vec1 = new Vector2((float)npc.direction, 0.0F);

                if ((double)npc.ai[1] >= 4.0 && (int)((double)npc.ai[1] - 4.0) % num2 == 0)
                {
                    int num11 = Math.Sign(player.Center.X - npc.Center.X);
                    if (num11 != 0)
                        npc.direction = npc.spriteDirection = num11;
                    if (Main.netMode != 1)
                    {
                        Vector2 vec2 = Vector2.Normalize(player.Center - npc.Center + player.velocity * 20f);
                        if (Utils.HasNaNs(vec2))
                            vec2 = new Vector2((float)npc.direction, 0.0f);
                        Vector2 vector2_1 = npc.Center + new Vector2((float)(npc.direction * 30), 12f);
                        for (int index = 0; index < 1; ++index)
                        {
                            Vector2 vector2_2 = Utils.RotatedByRandom(vec2 * (float)(6.0 + Main.rand.NextDouble() * 4.0), 0.523598790168762);
                            Projectile.NewProjectile(vector2_1.X, vector2_1.Y, vector2_2.X, vector2_2.Y, 467, damage2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        }
                    }
                }
                ++npc.ai[1];
                if ((double)npc.ai[1] >= (double)(4 + num2 * num3))
                {
                    npc.ai[0] = 0.0f;
                    npc.ai[1] = 0.0f;
                    ++npc.ai[3];
                    npc.velocity = Vector2.Zero;
                    npc.netUpdate = true;
                }
            }
            else if ((double)npc.ai[0] == 3.0) // Deathray
            {
                int Damage = 100;
                float lifeTime = 180F;
                if(npc.ai[0] == 20)
                {
                    npc.ai[2] = Math.Sign((player.Center - npc.Center).X) == 1 ? 1f : -1f;
                    if (Main.netMode != 1)
                    {
                        Vector2 center2 = npc.Center;
                        Projectile.NewProjectile(center2.X, center2.Y, 0.0f, 0.0f, mod.ProjectileType("MharadiumDeathray"), Damage, 0.0f, Main.myPlayer, (float)(npc.whoAmI + 1), 0.0f);
                    }
                    Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 12);
                    npc.netUpdate = true;
                }
                ++npc.ai[1];
                if ((double)npc.ai[1] >= (double)lifeTime)
                {
                    npc.ai[0] = 0.0F;
                    npc.ai[1] = 0.0F;
                    ++npc.ai[3];
                    npc.velocity = Vector2.Zero;
                    npc.netUpdate = true;
                }
            }
            else
            {
                npc.ai[0] = 1;
            }
        }
        /*
        else if ((double) this.ai[0] == 4.0)
        {
          this.localAI[2] = !flag2 ? 11f : 12f;
          if ((double) this.ai[1] == 20.0 && flag2 && Main.netMode != 1)
          {
            List<int> list = new List<int>();
            for (int index = 0; index < 200; ++index)
            {
              if (Main.npc[index].active && Main.npc[index].type == 440 && (double) Main.npc[index].ai[3] == (double) this.whoAmI)
                list.Add(index);
            }
            foreach (int index1 in list)
            {
              NPC npc = Main.npc[index1];
              Vector2 center2 = npc.Center;
              int num9 = Math.Sign(player.Center.X - center2.X);
              if (num9 != 0)
                npc.direction = npc.spriteDirection = num9;
              if (Main.netMode != 1)
              {
                Vector2 vec = Vector2.Normalize(player.Center - center2 + player.velocity * 20f);
                if (Utils.HasNaNs(vec))
                  vec = new Vector2((float) this.direction, 0.0f);
                Vector2 vector2_1 = center2 + new Vector2((float) (this.direction * 30), 12f);
                for (int index2 = 0; index2 < 1; ++index2)
                {
                  Vector2 vector2_2 = Utils.RotatedByRandom(vec * (float) (6.0 + Main.rand.NextDouble() * 4.0), 0.523598790168762);
                  Projectile.NewProjectile(vector2_1.X, vector2_1.Y, vector2_2.X, vector2_2.Y, 468, 18, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                }
              }
            }
            if ((int) ((double) this.ai[1] - 20.0) % num4 == 0)
              Projectile.NewProjectile(this.Center.X, this.Center.Y - 100f, 0.0f, 0.0f, 465, Damage3, 0.0f, Main.myPlayer, 0.0f, 0.0f);
          }
          ++this.ai[1];
          if ((double) this.ai[1] >= (double) (20 + num4))
          {
            this.ai[0] = 0.0f;
            this.ai[1] = 0.0f;
            ++this.ai[3];
            this.velocity = Vector2.Zero;
            this.netUpdate = true;
          }
        }
        else if ((double) this.ai[0] == 5.0)
        {
          this.localAI[2] = 10f;
          Vector2 vec = Vector2.Normalize(player.Center - center1);
          if (Utils.HasNaNs(vec))
            vec = new Vector2((float) this.direction, 0.0f);
          if ((double) this.ai[1] >= 0.0 && (double) this.ai[1] < 30.0)
          {
            flag3 = true;
            flag4 = true;
            this.alpha = (int) (((double) this.ai[1] - 0.0) / 30.0 * (double) byte.MaxValue);
          }
          else if ((double) this.ai[1] >= 30.0 && (double) this.ai[1] < 90.0)
          {
            if ((double) this.ai[1] == 30.0 && Main.netMode != 1 && flag2)
            {
              ++this.localAI[1];
              Vector2 spinningpoint = new Vector2(180f, 0.0f);
              List<int> list = new List<int>();
              for (int index = 0; index < 200; ++index)
              {
                if (Main.npc[index].active && Main.npc[index].type == 440 && (double) Main.npc[index].ai[3] == (double) this.whoAmI)
                  list.Add(index);
              }
              int num9 = 6 - list.Count;
              if (num9 > 2)
                num9 = 2;
              int length = list.Count + num9 + 1;
              float[] numArray = new float[length];
              for (int index = 0; index < numArray.Length; ++index)
                numArray[index] = Vector2.Distance(this.Center + Utils.RotatedBy(spinningpoint, (double) index * 6.28318548202515 / (double) length - 1.57079637050629, new Vector2()), player.Center);
              int index1 = 0;
              for (int index2 = 1; index2 < numArray.Length; ++index2)
              {
                if ((double) numArray[index1] > (double) numArray[index2])
                  index1 = index2;
              }
              int num11 = index1 >= length / 2 ? index1 - length / 2 : index1 + length / 2;
              int num12 = num9;
              for (int index2 = 0; index2 < numArray.Length; ++index2)
              {
                if (num11 != index2)
                {
                  Vector2 vector2 = this.Center + Utils.RotatedBy(spinningpoint, (double) index2 * 6.28318548202515 / (double) length - 1.57079637050629, new Vector2());
                  if (num12-- > 0)
                  {
                    int index3 = NPC.NewNPC((int) vector2.X, (int) vector2.Y + this.height / 2, 440, this.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                    Main.npc[index3].ai[3] = (float) this.whoAmI;
                    Main.npc[index3].netUpdate = true;
                    Main.npc[index3].localAI[1] = this.localAI[1];
                  }
                  else
                  {
                    int number = list[-num12 - 1];
                    Main.npc[number].Center = vector2;
                    NetMessage.SendData(23, -1, -1, "", number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                  }
                }
              }
              this.ai[2] = (float) Projectile.NewProjectile(this.Center.X, this.Center.Y, 0.0f, 0.0f, 490, 0, 0.0f, Main.myPlayer, 0.0f, (float) this.whoAmI);
              NPC npc = this;
              Vector2 vector2_1 = npc.Center + Utils.RotatedBy(spinningpoint, (double) num11 * 6.28318548202515 / (double) length - 1.57079637050629, new Vector2());
              npc.Center = vector2_1;
              this.netUpdate = true;
              list.Clear();
            }
            flag3 = true;
            flag4 = true;
            this.alpha = (int) byte.MaxValue;
            if (flag2)
            {
              Vector2 vector2 = Main.projectile[(int) this.ai[2]].Center - this.Center;
              if (vector2 == Vector2.Zero)
                vector2 = -Vector2.UnitY;
              vector2.Normalize();
              this.localAI[2] = (double) Math.Abs(vector2.Y) >= 0.769999980926514 ? ((double) vector2.Y >= 0.0 ? 10f : 12f) : 11f;
              int num9 = Math.Sign(vector2.X);
              if (num9 != 0)
                this.direction = this.spriteDirection = num9;
            }
            else
            {
              Vector2 vector2 = Main.projectile[(int) Main.npc[(int) this.ai[3]].ai[2]].Center - this.Center;
              if (vector2 == Vector2.Zero)
                vector2 = -Vector2.UnitY;
              vector2.Normalize();
              this.localAI[2] = (double) Math.Abs(vector2.Y) >= 0.769999980926514 ? ((double) vector2.Y >= 0.0 ? 10f : 12f) : 11f;
              int num9 = Math.Sign(vector2.X);
              if (num9 != 0)
                this.direction = this.spriteDirection = num9;
            }
          }
          else if ((double) this.ai[1] >= 90.0 && (double) this.ai[1] < 120.0)
          {
            flag3 = true;
            flag4 = true;
            this.alpha = (int) byte.MaxValue - (int) (((double) this.ai[1] - 90.0) / 30.0 * (double) byte.MaxValue);
          }
          else if ((double) this.ai[1] >= 120.0 && (double) this.ai[1] < 420.0)
          {
            flag4 = true;
            this.alpha = 0;
            if (flag2)
            {
              Vector2 vector2 = Main.projectile[(int) this.ai[2]].Center - this.Center;
              if (vector2 == Vector2.Zero)
                vector2 = -Vector2.UnitY;
              vector2.Normalize();
              this.localAI[2] = (double) Math.Abs(vector2.Y) >= 0.769999980926514 ? ((double) vector2.Y >= 0.0 ? 10f : 12f) : 11f;
              int num9 = Math.Sign(vector2.X);
              if (num9 != 0)
                this.direction = this.spriteDirection = num9;
            }
            else
            {
              Vector2 vector2 = Main.projectile[(int) Main.npc[(int) this.ai[3]].ai[2]].Center - this.Center;
              if (vector2 == Vector2.Zero)
                vector2 = -Vector2.UnitY;
              vector2.Normalize();
              this.localAI[2] = (double) Math.Abs(vector2.Y) >= 0.769999980926514 ? ((double) vector2.Y >= 0.0 ? 10f : 12f) : 11f;
              int num9 = Math.Sign(vector2.X);
              if (num9 != 0)
                this.direction = this.spriteDirection = num9;
            }
          }
          ++this.ai[1];
          if ((double) this.ai[1] >= 420.0)
          {
            flag4 = true;
            this.ai[0] = 0.0f;
            this.ai[1] = 0.0f;
            ++this.ai[3];
            this.velocity = Vector2.Zero;
            this.netUpdate = true;
          }
        }
        else if ((double) this.ai[0] == 6.0)
        {
          this.localAI[2] = 13f;
          ++this.ai[1];
          if ((double) this.ai[1] >= 120.0)
          {
            this.ai[0] = 0.0f;
            this.ai[1] = 0.0f;
            ++this.ai[3];
            this.velocity = Vector2.Zero;
            this.netUpdate = true;
          }
        }
        else if ((double) this.ai[0] == 7.0)
        {
          this.localAI[2] = 11f;
          Vector2 vec1 = Vector2.Normalize(player.Center - center1);
          if (Utils.HasNaNs(vec1))
            vec1 = new Vector2((float) this.direction, 0.0f);
          if ((double) this.ai[1] >= 4.0 && flag2 && (int) ((double) this.ai[1] - 4.0) % num5 == 0)
          {
            if ((int) ((double) this.ai[1] - 4.0) / num5 == 2)
            {
              List<int> list = new List<int>();
              for (int index = 0; index < 200; ++index)
              {
                if (Main.npc[index].active && Main.npc[index].type == 440 && (double) Main.npc[index].ai[3] == (double) this.whoAmI)
                  list.Add(index);
              }
              foreach (int index1 in list)
              {
                NPC npc = Main.npc[index1];
                Vector2 center2 = npc.Center;
                int num9 = Math.Sign(player.Center.X - center2.X);
                if (num9 != 0)
                  npc.direction = npc.spriteDirection = num9;
                if (Main.netMode != 1)
                {
                  Vector2 vec2 = Vector2.Normalize(player.Center - center2 + player.velocity * 20f);
                  if (Utils.HasNaNs(vec2))
                    vec2 = new Vector2((float) this.direction, 0.0f);
                  Vector2 vector2_1 = center2 + new Vector2((float) (this.direction * 30), 12f);
                  for (int index2 = 0; (double) index2 < 5.0; ++index2)
                  {
                    Vector2 vector2_2 = Utils.RotatedByRandom(vec2 * (float) (6.0 + Main.rand.NextDouble() * 4.0), 1.25663709640503);
                    Projectile.NewProjectile(vector2_1.X, vector2_1.Y, vector2_2.X, vector2_2.Y, 468, 18, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                  }
                }
              }
            }
            int num11 = Math.Sign(player.Center.X - center1.X);
            if (num11 != 0)
              this.direction = this.spriteDirection = num11;
            if (Main.netMode != 1)
            {
              Vector2 vec2 = Vector2.Normalize(player.Center - center1 + player.velocity * 20f);
              if (Utils.HasNaNs(vec2))
                vec2 = new Vector2((float) this.direction, 0.0f);
              Vector2 vector2_1 = this.Center + new Vector2((float) (this.direction * 30), 12f);
              float num9 = 8f;
              float num12 = 0.2513274f;
              for (int index1 = 0; (double) index1 < 5.0; ++index1)
              {
                Vector2 vector2_2 = Utils.RotatedBy(vec2 * num9, (double) num12 * (double) index1 - (1.25663709640503 - (double) num12) / 2.0, new Vector2());
                float ai1 = (float) (((double) Utils.NextFloat(Main.rand) - 0.5) * 0.300000011920929 * 6.28318548202515 / 60.0);
                int index2 = NPC.NewNPC((int) vector2_1.X, (int) vector2_1.Y + 7, 522, 0, 0.0f, ai1, vector2_2.X, vector2_2.Y, (int) byte.MaxValue);
                Main.npc[index2].velocity = vector2_2;
              }
            }
          }
          ++this.ai[1];
          if ((double) this.ai[1] >= (double) (4 + num5 * num6))
          {
            this.ai[0] = 0.0f;
            this.ai[1] = 0.0f;
            ++this.ai[3];
            this.velocity = Vector2.Zero;
            this.netUpdate = true;
          }
        }
        else if ((double) this.ai[0] == 8.0)
        {
          this.localAI[2] = 13f;
          if ((double) this.ai[1] >= 4.0 && flag2 && (int) ((double) this.ai[1] - 4.0) % num7 == 0)
          {
            List<int> list = new List<int>();
            for (int index = 0; index < 200; ++index)
            {
              if (Main.npc[index].active && Main.npc[index].type == 440 && (double) Main.npc[index].ai[3] == (double) this.whoAmI)
                list.Add(index);
            }
            int num9 = list.Count + 1;
            if (num9 > 3)
              num9 = 3;
            int num11 = Math.Sign(player.Center.X - center1.X);
            if (num11 != 0)
              this.direction = this.spriteDirection = num11;
            if (Main.netMode != 1)
            {
              for (int index1 = 0; index1 < num9; ++index1)
              {
                Point point1 = Utils.ToTileCoordinates(this.Center);
                Point point2 = Utils.ToTileCoordinates(Main.player[this.target].Center);
                Vector2 vector2 = Main.player[this.target].Center - this.Center;
                int num12 = 20;
                int num13 = 3;
                int num14 = 7;
                int num15 = 2;
                int num16 = 0;
                bool flag5 = false;
                if ((double) vector2.Length() > 2000.0)
                  flag5 = true;
                while (!flag5 && num16 < 100)
                {
                  ++num16;
                  int index2 = Main.rand.Next(point2.X - num12, point2.X + num12 + 1);
                  int index3 = Main.rand.Next(point2.Y - num12, point2.Y + num12 + 1);
                  if ((index3 < point2.Y - num14 || index3 > point2.Y + num14 || (index2 < point2.X - num14 || index2 > point2.X + num14)) && (index3 < point1.Y - num13 || index3 > point1.Y + num13 || (index2 < point1.X - num13 || index2 > point1.X + num13)) && !Main.tile[index2, index3].nactive())
                  {
                    bool flag6 = true;
                    if (flag6 && Collision.SolidTiles(index2 - num15, index2 + num15, index3 - num15, index3 + num15))
                      flag6 = false;
                    if (flag6)
                    {
                      NPC.NewNPC(index2 * 16 + 8, index3 * 16 + 8, 523, 0, (float) this.whoAmI, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                      break;
                    }
                  }
                }
              }
            }
          }
          ++this.ai[1];
          if ((double) this.ai[1] >= (double) (4 + num7 * num8))
          {
            this.ai[0] = 0.0f;
            this.ai[1] = 0.0f;
            ++this.ai[3];
            this.velocity = Vector2.Zero;
            this.netUpdate = true;
          }
        }
        if (!flag2)
          this.ai[3] = num10;
        this.dontTakeDamage = flag3;
        this.chaseable = !flag4;*/
        public override void NPCLoot()
        {
            int oreAmount = Main.rand.Next(50, 71);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MharadiumAnvil"));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MharadiumOre"), oreAmount);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SuperHealingPotion, 10);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = 0;
            npc.spriteDirection = npc.direction;
        }
    }
}
