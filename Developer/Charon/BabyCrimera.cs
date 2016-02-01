using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Negadium.Developer.Charon
{
    class BabyCrimera : ModProjectile
    {
        // Gorateron, for Charon

        public override void SetDefaults()
        {
            projectile.name = "Baby Crimera";
            projectile.width = 32; // Changed
            projectile.height = 54; // Changed
            //projectile.aiStyle = 26; // from baby eater
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;

            Main.projPet[projectile.type] = true; // is a pet
        }

        public override bool PreAI()
        {
            // use this for AI relics

            return true; // run ai
        }

        public override void AI()
        {
            // kill pet if plr is dead
            Player player = Main.player[projectile.owner];
            MharadiumModPlayer modPlayer = (MharadiumModPlayer)player.GetModPlayer(mod, "MharadiumModPlayer");
            if (player.dead)
                modPlayer.babycrimera = false;
            if (modPlayer.babycrimera)
                projectile.timeLeft = 2;


            // calculate movement in ai
            float num2 = 0.1f;
            projectile.tileCollide = false;
            int num3 = 300;
            Vector2 vector2_1 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num4 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector2_1.X;
            float num5 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector2_1.Y;
            if (projectile.type == (int)sbyte.MaxValue)
                num5 = Main.player[projectile.owner].position.Y - vector2_1.Y;
            float num6 = (float)Math.Sqrt((double)num4 * (double)num4 + (double)num5 * (double)num5);
            float num7 = 7f;
            if ((double)num6 < (double)num3 && (double)Main.player[projectile.owner].velocity.Y == 0.0 && ((double)projectile.position.Y + (double)projectile.height <= (double)Main.player[projectile.owner].position.Y + (double)Main.player[projectile.owner].height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height)))
            {
                projectile.ai[0] = 0.0f;
                if ((double)projectile.velocity.Y < -6.0)
                    projectile.velocity.Y = -6f;
            }
            if ((double)num6 < 150.0)
            {
                if ((double)Math.Abs(projectile.velocity.X) > 2.0 || (double)Math.Abs(projectile.velocity.Y) > 2.0)
                {
                    Projectile proj = projectile;
                    Vector2 vector2_2 = proj.velocity * 0.99f;
                    proj.velocity = vector2_2;
                }
                num2 = 0.01f;
                if ((double)num4 < -2.0)
                    num4 = -2f;
                if ((double)num4 > 2.0)
                    num4 = 2f;
                if ((double)num5 < -2.0)
                    num5 = -2f;
                if ((double)num5 > 2.0)
                    num5 = 2f;
            }
            else
            {
                if ((double)num6 > 300.0)
                    num2 = 0.2f;
                float num8 = num7 / num6;
                num4 *= num8;
                num5 *= num8;
            }
            if ((double)Math.Abs(num4) > (double)Math.Abs(num5) || (double)num2 == 0.0500000007450581)
            {
                if ((double)projectile.velocity.X < (double)num4)
                {
                    projectile.velocity.X = projectile.velocity.X + num2;
                    if ((double)num2 > 0.0500000007450581 && (double)projectile.velocity.X < 0.0)
                        projectile.velocity.X = projectile.velocity.X + num2;
                }
                if ((double)projectile.velocity.X > (double)num4)
                {
                    projectile.velocity.X = projectile.velocity.X - num2;
                    if ((double)num2 > 0.0500000007450581 && (double)projectile.velocity.X > 0.0)
                        projectile.velocity.X = projectile.velocity.X - num2;
                }
            }
            if ((double)Math.Abs(num4) <= (double)Math.Abs(num5) || (double)num2 == 0.0500000007450581)
            {
                if ((double)projectile.velocity.Y < (double)num5)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num2;
                    if ((double)num2 > 0.0500000007450581 && (double)projectile.velocity.Y < 0.0)
                        projectile.velocity.Y = projectile.velocity.Y + num2;
                }
                if ((double)projectile.velocity.Y > (double)num5)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num2;
                    if ((double)num2 > 0.0500000007450581 && (double)projectile.velocity.Y > 0.0)
                        projectile.velocity.Y = projectile.velocity.Y - num2;
                }
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - 1.57f;
            ++projectile.frameCounter;
            if (projectile.frameCounter > 6)
            {
                ++projectile.frame;
                projectile.frameCounter = 0;
            }
            if (projectile.frame <= 1)
                return;
            projectile.frame = 0;
        }
    }
}
