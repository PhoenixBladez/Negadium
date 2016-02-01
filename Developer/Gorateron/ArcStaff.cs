using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Developer.Gorateron
{
    /*
        This weapon currently is only for demonstration purposes only!
        By Gorateron
    */

    public class ArcStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Arc Staff";
            item.damage = 125;
            item.magic = true;
            item.mana = 45;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 10000;
            item.rare = -12;
            item.useSound = 20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("NegativeArcOrb");
            item.shootSpeed = 1f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(GetMouse(player).X, GetMouse(player).Y, speedX, speedY, item.shoot, damage, 0f, Main.myPlayer, 0f, 0f);
            return false;
        }

        private Vector2 GetMouse(Player player)
        {
            Vector2 position = Main.screenPosition;
            position.X += Main.mouseX;
            position.Y += player.gravDir == 1 ? Main.mouseY : Main.screenHeight - Main.mouseY;
            return position;
        }
    }

    public class NegativeArcOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CultistBossLightningOrb);
            projectile.name = "Negative Arc Orb";
            projectile.hostile = false;
            projectile.friendly = true;
            Main.projFrames[projectile.type] = 4;
            projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }

        public override bool PreAI()
        {
            return false; // stop vanilla AI
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void PostAI()
        {
            // Prepare yourself for some ugly calculations!

            Player player = Main.player[Main.myPlayer];

            projectile.ai[1] = projectile.whoAmI; // unused, for now

            /* PROJALPHA */
            if (projectile.localAI[1] == 0f)
            {
                //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 121);
                projectile.localAI[1] = 1f;
            }
            if (projectile.ai[0] < 180f)
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            else
            {
                projectile.alpha += 2;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                    return;
                }
            }
            projectile.ai[0] += 1f;

            //projectile.position.X = Main.player[projectile.owner].position.X - Main.player[projectile.owner].width * 2 + (float)Main.projectileTexture[projectile.type].Width / 16;
            //projectile.position.Y = Main.player[projectile.owner].Center.Y - 150f;

            if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 180f && Main.netMode != 1)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num851 = 0;
                float num852 = 2000f;
                /* START: EDITABLE PART */
                for (int num853 = 0; num853 < Main.npc.Length; num853++)
                {
                    if (Main.npc[num853].active && !Main.npc[num853].friendly && !Main.npc[num853].townNPC && Main.npc[num853].CanBeChasedBy((object)this, false) && !Main.npc[num853].dontTakeDamage)
                    {
                        // I added !townNPC, CanBeChasedBy and !dontTakeDamage.. otherwise it's a spam fest!
                        // Don't change below here
                        Vector2 center9 = Main.npc[num853].Center;
                        float num854 = Vector2.Distance(center9, base.projectile.Center);
                        if (num854 < num852 && Collision.CanHit(base.projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num851] = num853;
                            array5[num851] = center9;
                            if (++num851 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                /* END: EDITABLE PART */
                /* LIGHTNING STRIKES */
                for (int num855 = 0; num855 < num851; num855++)
                {
                    Vector2 vector82 = array5[num855] - base.projectile.Center;
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 7f;
                    int f12 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector83.X, vector83.Y, mod.ProjectileType("NegativeArc"), projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                    //Main.projectile[f12].friendly = true;
                    //Main.projectile[f12].hostile = false;
                }
            }
            /* LIGHTING */
            Lighting.AddLight(base.projectile.Center, 0.4f, 0.85f, 0.9f);
            /* PROJFRAMES */
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            /* DUSTS */
            if (projectile.alpha < 150 && projectile.ai[0] < 180f)
            {
                for (int num856 = 0; num856 < 1; num856++)
                {
                    float num857 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num857 < -0.5f)
                    {
                        num857 = -0.5f;
                    }
                    if (num857 > 0.5f)
                    {
                        num857 = 0.5f;
                    }
                    /* DUST INNER */
                    Vector2 value48 = new Vector2((float)(-(float)projectile.width) * 0.2f * projectile.scale, 0f).RotatedBy((double)(num857 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num858 = Dust.NewDust(base.projectile.Center - Vector2.One * 5f, 10, 10, mod.DustType("ArcDust"), -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num858].position = base.projectile.Center + value48;
                    Main.dust[num858].velocity = Vector2.Normalize(Main.dust[num858].position - base.projectile.Center) * 2f;
                    Main.dust[num858].noGravity = true;
                }
                for (int num859 = 0; num859 < 1; num859++)
                {
                    float num860 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num860 < -0.5f)
                    {
                        num860 = -0.5f;
                    }
                    if (num860 > 0.5f)
                    {
                        num860 = 0.5f;
                    }
                    /* DUST OUTER */
                    Vector2 value49 = new Vector2((float)(-(float)projectile.width) * 0.6f * projectile.scale, 0f).RotatedBy((double)(num860 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num861 = Dust.NewDust(base.projectile.Center - Vector2.One * 5f, 10, 10, mod.DustType("ArcDust"), -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num861].velocity = Vector2.Zero;
                    Main.dust[num861].position = base.projectile.Center + value49;
                    Main.dust[num861].noGravity = true;
                }
                return;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            /* PROJDRAW */
            // Please don't change below here
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
            {
                color25 = Lighting.GetColor((int)projectile.Center.X / 16, (int)(projectile.Center.Y / 16f));
            }
            Vector2 vector35 = projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D26 = Main.projectileTexture[projectile.type];
            Rectangle rectangle10 = texture2D26.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Color alpha5 = projectile.GetAlpha(color25);
            Vector2 origin6 = rectangle10.Size() / 2f;
            Main.spriteBatch.Draw(texture2D26, vector35, new Microsoft.Xna.Framework.Rectangle?(rectangle10), alpha5, projectile.rotation, origin6, projectile.scale, SpriteEffects.None, 0f);

        }
        //465 466

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false; // stop drawing
        }

        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return base.PreDrawExtras(spriteBatch);
        }
    }

    public class NegativeArc : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.CloneDefaults(ProjectileID.CultistBossLightningOrbArc); // is below
            projectile.name = "Negative Arc";
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.width = 14;
            projectile.height = 14;
            //projectile.aiStyle = 88;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 120 * (projectile.extraUpdates + 1);
            ProjectileID.Sets.TrailingMode[projectile.type] = ProjectileID.Sets.TrailingMode[ProjectileID.CultistBossLightningOrbArc]; // REQUIRED
            ProjectileID.Sets.TrailCacheLength[projectile.type] = ProjectileID.Sets.TrailCacheLength[ProjectileID.CultistBossLightningOrbArc]; // REQUIRED
        }

        public override bool PreAI()
        {
            return false; // stop AI
        }

        public override void PostAI()
        {
            projectile.frameCounter++;
            /* LIGHTING */
            Lighting.AddLight(base.projectile.Center, 0.3f, 0.45f, 0.5f);
            if (projectile.velocity == Vector2.Zero)
            {
                /* PROJ LIFE */
                if (projectile.frameCounter >= projectile.extraUpdates * 2)
                {
                    projectile.frameCounter = 0;
                    bool flag34 = true;
                    for (int num862 = 1; num862 < projectile.oldPos.Length; num862++)
                    {
                        if (projectile.oldPos[num862] != projectile.oldPos[0])
                        {
                            flag34 = false;
                        }
                    }
                    if (flag34)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                /* DUSTS */
                if (Main.rand.Next(projectile.extraUpdates) == 0)
                {
                    for (int num863 = 0; num863 < 2; num863++)
                    {
                        float num864 = projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num865 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector84 = new Vector2((float)Math.Cos((double)num864) * num865, (float)Math.Sin((double)num864) * num865);
                        int num866 = Dust.NewDust(base.projectile.Center, 0, 0, 226, vector84.X, vector84.Y, 0, Color.Red, 1f);
                        Main.dust[num866].noGravity = true;
                        Main.dust[num866].scale = 1.2f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 value50 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                        int num867 = Dust.NewDust(base.projectile.Center + value50 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, Color.Red, 1.5f);
                        Main.dust[num867].velocity *= 0.5f;
                        Main.dust[num867].velocity.Y = -Math.Abs(Main.dust[num867].velocity.Y);
                        return;
                    }
                }
            }
            /* DONT LOOK BELOW ... UGLY */
            else if (projectile.frameCounter >= projectile.extraUpdates * 2)
            {
                projectile.frameCounter = 0;
                float num868 = projectile.velocity.Length();
                Random random = new Random((int)projectile.ai[1]);
                int num869 = 0;
                Vector2 spinningpoint2 = -Vector2.UnitY;
                Vector2 vector85;
                do
                {
                    int num870 = random.Next();
                    projectile.ai[1] = (float)num870;
                    num870 %= 100;
                    float f = (float)num870 / 100f * 6.28318548f;
                    vector85 = f.ToRotationVector2();
                    if (vector85.Y > 0f)
                    {
                        vector85.Y *= -1f;
                    }
                    bool flag35 = false;
                    if (vector85.Y > -0.02f)
                    {
                        flag35 = true;
                    }
                    if (vector85.X * (float)(projectile.extraUpdates + 1) * 2f * num868 + projectile.localAI[0] > 40f)
                    {
                        flag35 = true;
                    }
                    if (vector85.X * (float)(projectile.extraUpdates + 1) * 2f * num868 + projectile.localAI[0] < -40f)
                    {
                        flag35 = true;
                    }
                    if (!flag35)
                    {
                        goto IL_23620;
                    }
                }
                while (num869++ < 100);
                projectile.velocity = Vector2.Zero;
                projectile.localAI[1] = 1f;
                goto IL_23628;
                IL_23620:
                spinningpoint2 = vector85;
                IL_23628:
                if (projectile.velocity != Vector2.Zero)
                {
                    projectile.localAI[0] += spinningpoint2.X * (float)(projectile.extraUpdates + 1) * 2f * num868;
                    projectile.velocity = spinningpoint2.RotatedBy((double)(projectile.ai[0] + 1.57079637f), default(Vector2)) * num868;
                    projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            /* PROJDRAW */
            // Please don't change anything below.. precious code
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            // Thank the gods of the nine divines for the above calculation
            Vector2 end = projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D tex3 = Main.projectileTexture[projectile.type];
            // normally tex3 uses Main.extraTexture[33]
            projectile.GetAlpha(color25);
            Vector2 scale10 = new Vector2(projectile.scale) / 2f;
            for (int num225 = 0; num225 < 3; num225++)
            {
                if (num225 == 0)
                {
                    scale10 = new Vector2(projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                }
                else if (num225 == 1)
                {
                    scale10 = new Vector2(projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                }
                else
                {
                    scale10 = new Vector2(projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                // Below code requires the Projectile cache set! DO NOT change!
                for (int num226 = projectile.oldPos.Length - 1; num226 > 0; num226--)
                {
                    //Main.NewText(projectile.oldPos[num226].ToString());
                    if (!(projectile.oldPos[num226] == Vector2.Zero))
                    {
                        //Main.NewText("first pos");
                        Vector2 start = projectile.oldPos[num226] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        //Main.NewText(start.ToString());
                        Vector2 end2 = projectile.oldPos[num226 - 1] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        //Main.NewText(end2.ToString());
                        Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale10, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
               // Main.NewText(projectile.oldPos[0].ToString());
                if (projectile.oldPos[0] != Vector2.Zero)
                {
                    //Main.NewText("second pos");
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                    //Main.NewText(start2.ToString());
                    Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale10, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false; // stop drawing
        }

        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return base.PreDrawExtras(spriteBatch);
        }
    }
}
