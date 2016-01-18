using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Negadium.Projectiles
{
    public class MharadiumRocketII : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Rocket";
            projectile.width = 14;
            projectile.height = 14;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.scale = 0.9f;
            projectile.timeLeft = 60;
        }

        public override bool PreAI()
        {
            // Explosion after reaching max dist.
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                projectile.ai[1] = 0f;
                projectile.alpha = 255;

                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 128;
                projectile.height = 128;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.knockBack = 8f;
            }
            else
            {
                if (Math.Abs(projectile.velocity.X) >= 8f || Math.Abs(projectile.velocity.Y) >= 8f)
                {
                    for (int num246 = 0; num246 < 2; num246++)
                    {
                        float num247 = 0f;
                        float num248 = 0f;
                        if (num246 == 1)
                        {
                            num247 = projectile.velocity.X * 0.5f;
                            num248 = projectile.velocity.Y * 0.5f;
                        }
                        int num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                        Main.dust[num249].velocity *= 0.2f;
                        Main.dust[num249].noGravity = true;
                        num249 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num247, projectile.position.Y + 3f + num248) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                        Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                        Main.dust[num249].velocity *= 0.05f;
                    }
                }
                if (Math.Abs(projectile.velocity.X) < 15f && Math.Abs(projectile.velocity.Y) < 15f)
                {
                    projectile.velocity *= 1.1f;
                }
            }

            projectile.ai[0] += 1f;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity *= 0f;
            projectile.alpha = 255;
            projectile.timeLeft = 3;

            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (!projectile.active)
            {
                return;
            }

            Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
            int num = projectile.timeLeft;
            projectile.timeLeft = 0;
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 22;
            projectile.height = 22;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);

            if (projectile.owner == Main.myPlayer)
            {
                int num700 = 3;
                int num701 = (int)(projectile.position.X / 16f - (float)num700);
                int num702 = (int)(projectile.position.X / 16f + (float)num700);
                int num703 = (int)(projectile.position.Y / 16f - (float)num700);
                int num704 = (int)(projectile.position.Y / 16f + (float)num700);
                if (num701 < 0)
                {
                    num701 = 0;
                }
                if (num702 > Main.maxTilesX)
                {
                    num702 = Main.maxTilesX;
                }
                if (num703 < 0)
                {
                    num703 = 0;
                }
                if (num704 > Main.maxTilesY)
                {
                    num704 = Main.maxTilesY;
                }
                bool flag3 = false;
                for (int num705 = num701; num705 <= num702; num705++)
                {
                    for (int num706 = num703; num706 <= num704; num706++)
                    {
                        float num707 = Math.Abs((float)num705 - projectile.position.X / 16f);
                        float num708 = Math.Abs((float)num706 - projectile.position.Y / 16f);
                        double num709 = Math.Sqrt((double)(num707 * num707 + num708 * num708));
                        if (num709 < (double)num700 && Main.tile[num705, num706] != null && Main.tile[num705, num706].wall == 0)
                        {
                            flag3 = true;
                            break;
                        }
                    }
                }
                AchievementsHelper.CurrentlyMining = true;
                for (int num710 = num701; num710 <= num702; num710++)
                {
                    for (int num711 = num703; num711 <= num704; num711++)
                    {
                        float num712 = Math.Abs((float)num710 - projectile.position.X / 16f);
                        float num713 = Math.Abs((float)num711 - projectile.position.Y / 16f);
                        double num714 = Math.Sqrt((double)(num712 * num712 + num713 * num713));
                        if (num714 < (double)num700)
                        {
                            bool flag4 = true;
                            if (Main.tile[num710, num711] != null && Main.tile[num710, num711].active())
                            {
                                flag4 = true;
                                if (Main.tileDungeon[(int)Main.tile[num710, num711].type] || Main.tile[num710, num711].type == 21 || Main.tile[num710, num711].type == 26 || Main.tile[num710, num711].type == 107 || Main.tile[num710, num711].type == 108 || Main.tile[num710, num711].type == 111 || Main.tile[num710, num711].type == 226 || Main.tile[num710, num711].type == 237 || Main.tile[num710, num711].type == 221 || Main.tile[num710, num711].type == 222 || Main.tile[num710, num711].type == 223 || Main.tile[num710, num711].type == 211 || Main.tile[num710, num711].type == 404)
                                {
                                    flag4 = false;
                                }
                                if (!Main.hardMode && Main.tile[num710, num711].type == 58)
                                {
                                    flag4 = false;
                                }
                                if (flag4)
                                {
                                    WorldGen.KillTile(num710, num711, false, false, false);
                                    if (!Main.tile[num710, num711].active() && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, "", 0, (float)num710, (float)num711, 0f, 0, 0, 0);
                                    }
                                }
                            }
                            if (flag4)
                            {
                                for (int num715 = num710 - 1; num715 <= num710 + 1; num715++)
                                {
                                    for (int num716 = num711 - 1; num716 <= num711 + 1; num716++)
                                    {
                                        if (Main.tile[num715, num716] != null && Main.tile[num715, num716].wall > 0 && flag3)
                                        {
                                            WorldGen.KillWall(num715, num716, false);
                                            if (Main.tile[num715, num716].wall == 0 && Main.netMode != 0)
                                            {
                                                NetMessage.SendData(17, -1, -1, "", 2, (float)num715, (float)num716, 0f, 0, 0, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                AchievementsHelper.CurrentlyMining = false;
            }

            projectile.active = false;
        }
    }
}
