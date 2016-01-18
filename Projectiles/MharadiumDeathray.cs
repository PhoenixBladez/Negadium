using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles
{
    public class MharadiumDeathray : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Deathray";
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180; // 3 seconds lifetime.
        }

        public override void AI()
        {
            bool dontDestroy = true;
            int targetNPCID = (int)projectile.ai[0] - 1; // num749
            if (projectile.ai[0] == 0F || !Main.npc[targetNPCID].active)
            {
                dontDestroy = false;
            }
            if (!dontDestroy)
            {
                projectile.Kill();
                return;
            }

            NPC targetNPC = Main.npc[targetNPCID]; // nPC7
            float num750 = targetNPC.Center.Y + 46F;
            int targetTileX = (int)targetNPC.Center.X / 16; // Target NPC X axis tile coordinates. - num751
            int targetTileY = (int)num750 / 16; // Target NPC Y axis tile coordinates. - num752
            int num753 = 0;
            bool flag32 = Main.tile[targetTileX, targetTileY].nactive() && Main.tileSolid[(int)Main.tile[targetTileX, targetTileY].type] && !Main.tileSolidTop[(int)Main.tile[targetTileX, targetTileY].type];
            if (flag32)
                num753 = 1;
            else
            {
                while (num753 < 150 && targetTileY + num753 < Main.maxTilesY) // search for an end point over the Y axis.
                {
                    int num754 = targetTileY + num753;
                    bool flag33 = Main.tile[targetTileX, num754].nactive() && Main.tileSolid[(int)Main.tile[targetTileX, num754].type] && !Main.tileSolidTop[(int)Main.tile[targetTileX, num754].type];
                    if (flag33)
                    {
                        num753--;
                        break;
                    }
                    num753++;
                }
            }

            projectile.position.X = targetNPC.Center.X - (float)(projectile.width / 2);
            projectile.position.Y = num750;
            projectile.height = (num753 + 1) * 16; // Set the 'endpoint' of the Deathray.
            int num755 = (int)projectile.position.Y + projectile.height;
            if (Main.tile[targetTileX, num755 / 16].nactive() && Main.tileSolid[(int)Main.tile[targetTileX, num755 / 16].type] && !Main.tileSolidTop[(int)Main.tile[targetTileX, num755 / 16].type])
            {
                int num756 = num755 % 16;
                projectile.height -= num756 - 2;
            }
            for (int num757 = 0; num757 < 2; num757++)
            {
                int num758 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + (float)projectile.height - 16f), projectile.width, 16, 228, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num758].noGravity = true;
                Main.dust[num758].velocity *= 0.5f;
                Dust dust100 = Main.dust[num758];
                dust100.velocity.X = dust100.velocity.X - ((float)num757 - targetNPC.velocity.X * 2f / 3f);
                Main.dust[num758].scale = 2.8f;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num759 = Dust.NewDust(new Vector2(projectile.position.X + (float)(projectile.width / 2) - (float)(projectile.width / 2 * Math.Sign(targetNPC.velocity.X)) - 4f, projectile.position.Y + (float)projectile.height - 16f), 4, 16, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num759].velocity *= 0.5f;
                Dust dust101 = Main.dust[num759];
                dust101.velocity.X = dust101.velocity.X - targetNPC.velocity.X / 2f;
                Main.dust[num759].velocity.Y = -Math.Abs(Main.dust[num759].velocity.Y);
            }
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                    return;
                }
            }
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects1 = SpriteEffects.None;
            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D texture2D = Main.extraTexture[4];
            int num2 = texture.Height / Main.projFrames[projectile.type];
            int num3 = num2 * projectile.frame;
            int height = texture2D.Height / Main.projFrames[projectile.type];
            int y2 = height * projectile.frame;
            Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y2, texture2D.Width, height);
            Vector2 position = projectile.position + new Vector2((float)projectile.width, 0.0f) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Main.spriteBatch.Draw(Main.extraTexture[4], position, new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(color3), projectile.rotation, new Vector2((float)(texture2D.Width / 2), 0.0f), projectile.scale, effects1, 0.0f);
            int num4 = projectile.height - num2 - 14;
            if (num4 < 0)
                num4 = 0;
            if (num4 > 0)
            {
                if (y2 == height * 3)
                    y2 = height * 2;
                Main.spriteBatch.Draw(Main.extraTexture[4], position + Vector2.UnitY * (float)(height - 1), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y2 + height - 1, texture2D.Width, 1)), projectile.GetAlpha(color3), projectile.rotation, new Vector2((float)(texture2D.Width / 2), 0.0f), new Vector2(1f, (float)num4), effects1, 0.0f);
            }
            rectangle.Width = texture.Width;
            rectangle.Y = num3;
            Main.spriteBatch.Draw(texture, position + Vector2.UnitY * (float)(height - 1 + num4), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(color3), projectile.rotation, new Vector2((float)texture.Width / 2f, 0.0f), projectile.scale, effects1, 0.0f);
            return false;
        }
    }
}
