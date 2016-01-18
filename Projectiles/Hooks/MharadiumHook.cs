using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles.Hooks
{
    public class MharadiumHook : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 16;
            projectile.aiStyle = 7;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 10;
            projectile.magic = true;
            Main.projHook[projectile.type] = true;
        }

        public override bool PreAI()
        {
            if (Main.player[projectile.owner].dead || Main.player[projectile.owner].stoned || Main.player[projectile.owner].webbed || Main.player[projectile.owner].frozen)
            {
                projectile.Kill();
                return false;
            }
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 vector6 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num69 = mountedCenter.X - vector6.X;
            float num70 = mountedCenter.Y - vector6.Y;
            float num71 = (float)Math.Sqrt((double)(num69 * num69 + num70 * num70));
            projectile.rotation = (float)Math.Atan2((double)num70, (double)num69) - 1.57f;

            if (projectile.ai[0] == 0)
            {
                if (num71 > 1000) // Range check.
                {
                    projectile.ai[0] = 1F;
                }

                Vector2 value4 = projectile.Center - new Vector2(5f);
                Vector2 value5 = projectile.Center + new Vector2(5f);
                Point point = (value4 - new Vector2(16f)).ToTileCoordinates();
                Point point2 = (value5 + new Vector2(32f)).ToTileCoordinates();
                int num73 = point.X;
                int num74 = point2.X;
                int num75 = point.Y;
                int num76 = point2.Y;
                if (num73 < 0)
                {
                    num73 = 0;
                }
                if (num74 > Main.maxTilesX)
                {
                    num74 = Main.maxTilesX;
                }
                if (num75 < 0)
                {
                    num75 = 0;
                }
                if (num76 > Main.maxTilesY)
                {
                    num76 = Main.maxTilesY;
                }
                for (int num77 = num73; num77 < num74; ++num77)
                {
                    int num78 = num75;
                    while (num78 < num76)
                    {
                        if (Main.tile[num77, num78] == null)
                        {
                            Main.tile[num77, num78] = new Tile();
                        }
                        Vector2 vector7;
                        vector7.X = (float)(num77 * 16);
                        vector7.Y = (float)(num78 * 16); 
                        if (value4.X + 10f > vector7.X && value4.X < vector7.X + 16f && value4.Y + 10f > vector7.Y && value4.Y < vector7.Y + 16f && Main.tile[num77, num78].nactive() && (Main.tileSolid[(int)Main.tile[num77, num78].type] || Main.tile[num77, num78].type == 314) && (projectile.type != 403 || Main.tile[num77, num78].type == 314))
                        {
                            if (Main.player[projectile.owner].grapCount < 10)
                            {
                                Main.player[projectile.owner].grappling[Main.player[projectile.owner].grapCount] = projectile.whoAmI;
                                Main.player[projectile.owner].grapCount++;
                            }
                            if (Main.myPlayer == projectile.owner)
                            {
                                int num79 = 0;
                                int num80 = -1;
                                int num81 = 100000;
                                int num83 = 4;
                                for (int num84 = 0; num84 < 1000; num84++)
                                {
                                    if (Main.projectile[num84].active && Main.projectile[num84].owner == projectile.owner && Main.projectile[num84].type == projectile.type)
                                    {
                                        if (Main.projectile[num84].timeLeft < num81)
                                        {
                                            num80 = num84;
                                            num81 = Main.projectile[num84].timeLeft;
                                        }
                                        num79++;
                                    }
                                }
                                if (num79 > num83)
                                {
                                    Main.projectile[num80].Kill();
                                }
                            }
                            WorldGen.KillTile(num77, num78, true, true, false);
                            Main.PlaySound(0, num77 * 16, num78 * 16, 1);
                            projectile.velocity.X = 0f;
                            projectile.velocity.Y = 0f;
                            projectile.ai[0] = 2f;
                            projectile.position.X = (float)(num77 * 16 + 8 - projectile.width / 2);
                            projectile.position.Y = (float)(num78 * 16 + 8 - projectile.height / 2);
                            projectile.damage = 0;
                            projectile.netUpdate = true;
                            if (Main.myPlayer == projectile.owner)
                            {
                                NetMessage.SendData(13, -1, -1, "", projectile.owner, 0f, 0f, 0f, 0, 0, 0);
                                break;
                            }
                            break;
                        }
                        else
                        {
                            num78++;
                        }
                    }
                    if (projectile.ai[0] == 2f)
                    {
                        return false;
                    }
                }
                return false;
            }
            if (projectile.ai[0] == 1f)
            {
                float num85 = 24f;

                if (num71 < 24f)
                {
                    projectile.Kill();
                }
                num71 = num85 / num71;
                num69 *= num71;
                num70 *= num71;
                projectile.velocity.X = num69;
                projectile.velocity.Y = num70;
                return false;
            }
            if (projectile.ai[0] == 2f)
            {
                int num86 = (int)(projectile.position.X / 16f) - 1;
                int num87 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num88 = (int)(projectile.position.Y / 16f) - 1;
                int num89 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num86 < 0)
                {
                    num86 = 0;
                }
                if (num87 > Main.maxTilesX)
                {
                    num87 = Main.maxTilesX;
                }
                if (num88 < 0)
                {
                    num88 = 0;
                }
                if (num89 > Main.maxTilesY)
                {
                    num89 = Main.maxTilesY;
                }
                bool flag2 = true;
                for (int num90 = num86; num90 < num87; num90++)
                {
                    for (int num91 = num88; num91 < num89; num91++)
                    {
                        if (Main.tile[num90, num91] == null)
                        {
                            Main.tile[num90, num91] = new Tile();
                        }
                        Vector2 vector8;
                        vector8.X = (float)(num90 * 16);
                        vector8.Y = (float)(num91 * 16);
                        if (projectile.position.X + (float)(projectile.width / 2) > vector8.X && projectile.position.X + (float)(projectile.width / 2) < vector8.X + 16f && projectile.position.Y + (float)(projectile.height / 2) > vector8.Y && projectile.position.Y + (float)(projectile.height / 2) < vector8.Y + 16f && Main.tile[num90, num91].nactive() && (Main.tileSolid[(int)Main.tile[num90, num91].type] || Main.tile[num90, num91].type == 314 || Main.tile[num90, num91].type == 5))
                        {
                            flag2 = false;
                        }
                    }
                }
                if (flag2)
                {
                    projectile.ai[0] = 1f;
                    return false;
                }
                if (Main.player[projectile.owner].grapCount < 10)
                {
                    Main.player[projectile.owner].grappling[Main.player[projectile.owner].grapCount] = projectile.whoAmI;
                    Main.player[projectile.owner].grapCount++;
                    return false;
                }
            }
            return false;
        }

        public override void PostDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModLoader.GetTexture("Negadium/Projectiles/Hooks/MharadiumHook_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
        }
    }
}
