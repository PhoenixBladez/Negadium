using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.Developer.Charon
{
	public class AcheronYoyoProjectile : ModProjectile
	{
		/*
			Gorateron
			The main projectile which is the yoyo itself
			This yoyo cannot deal damage by itself, which is one of the downsides of the weapon.
			It spawns orbs circling around the yoyo, which deal major damage! (on of the upsides!)
		*/
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodYoyo);
			projectile.damage = 0;
			projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4; // Length of ghosting
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void PostAI()
		{
			//((AcheronYoyoInfo)projectile.GetModInfo(mod, "AcheronYoyoInfo")).isProjActive = false;
			// do NOT uncomment the above!
		}

		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}

		// Ghosting effect
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		// This code spawns the orbs
		public override void AI()
		{
			++projectile.localAI[1];
			int minRadius = 1;
			int minSpeed = 1;

			if ((double)projectile.localAI[1] <= 1.0)
			{
				int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0,0, mod.ProjectileType("AcheronYoyoCircleProjectile"), projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f);
				Main.projectile[proj].localAI[0] = projectile.whoAmI;
			}
			else //if ((double)projectile.localAI[1] >= 1.0)
			{
				// Eventually I want to optimize this using a for loop!
				switch ((int)projectile.localAI[1])
				{
					case 150:
						minRadius += 1;
						minSpeed -= 1;
						break;
					case 250:
						minRadius += 2;
						minSpeed -= 1;
						break;
					case 350:
						minRadius += 3;
						minSpeed -= 2;
						break;
					case 450:
						minRadius += 4;
						minSpeed -= 2;
						break;
					case 575:
						minRadius += 5;
						minSpeed -= 3;
						break;
					case 625:
						minRadius += 1;
						minSpeed -= 3;
						break;
					case 675:
						minRadius += 2;
						minSpeed -= 3;
						break;
					case 725:
						minRadius += 3;
						minSpeed -= 4;
						break;
					case 775:
						minRadius += 4;
						minSpeed -= 4;
						break;
					case 825:
						minRadius += 5;
						minSpeed -= 4;
						break;
				}

				if (new[] {150, 250, 350, 450, 575, 625, 675, 725, 775, 825 }.Contains((int)projectile.localAI[1]))
				{
					int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, minRadius, minSpeed, mod.ProjectileType("AcheronYoyoCircleProjectile"), projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f);
					Main.projectile[proj].localAI[0] = projectile.whoAmI;
				}

			}

		}

		// Not used... yet
		private static int GetAvg(int min, int max)
		{
			List<int> collection = new List<int>();
			for (int i = min; i < max; i++)
			{
				collection.Add(i);
			}

			int count = 0;

			foreach (var item in collection)
			{
				count += (int)item;
			}

			return (count / collection.ToArray().Length);
		}

	}

	// The orb which circles the yoyo
	public class AcheronYoyoCircleProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.damage = mod.GetItem("AcheronYoyo").item.damage;
			projectile.friendly = true;
			//projectile.extraUpdates = 0;
			projectile.name = "AcheronYoyo";
			projectile.width = 12; // set to texture width
			projectile.height = 12; // set to texture height
			//projectile.aiStyle = 115;
			// do not uncomment the above
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.melee = true;
			projectile.scale = 1;
			Main.projFrames[projectile.type] = 6;
		}

		public override void AI()
		{
			if (!Main.projectile[(int)projectile.localAI[0]].active)
			{
				projectile.Kill();
			}

			int minRadius = (int)projectile.velocity.X  * 16;
			int minSpeed = (int)projectile.velocity.Y;

			projectile.frameCounter++;
			if (projectile.frameCounter > 6)
			{
				projectile.frameCounter = 0;
				if (projectile.frame == 5)
				{
					projectile.frame = 0;
				}
				else projectile.frame++;
			}

			Lighting.AddLight(projectile.Center, 0.65f*Main.essScale, 0.2f*Main.essScale, 0.85f*Main.essScale);
			Dust.NewDust(projectile.Center, 5, 5, mod.DustType("AcheronDust"));

			int radius = 8*16 - minRadius; // pixels*tilesize - minRadius
			int orbitSpeed = 12 - minSpeed; // pixelspeed - minSpeed
			int sizeTexture = 12; // in pixels (size of texture)
	​
			projectile.localAI[1] += 2f * (float)Math.PI / 600f * orbitSpeed;
			projectile.localAI[1] %= 2f * (float)Math.PI;

			Vector2 dir = Main.projectile[(int)projectile.localAI[0]].Center; // get yoyo's center
			dir.X -= sizeTexture - 2; // remove texture width - 2 pixels
			projectile.rotation = (float)Math.Atan2((double)dir.Y, (double)dir.X) - 2f;
			projectile.Center = dir + radius * new Vector2((float)Math.Cos(projectile.localAI[1]), (float)Math.Sin(projectile.localAI[1]));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			float time1 = 3.0f;
			float time2 = (time1 * 10f + 120f) / 1.5f;

			// Possibly needs a nerf
			if (!target.friendly)
			{
				if (Main.rand.Next() >= 2)
				{
					int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, GetVelocity(projectile).X, GetVelocity(projectile).Y, ProjectileID.NebulaArcanumExplosionShot, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f);
					Projectile proj1 = Main.projectile[proj];
					proj1.penetrate = -1;
					proj1.maxPenetrate = 12; // nerf here
					proj1.damage = mod.GetItem("AcheronYoyo").item.damage - 42;
					proj1.melee = false;
					proj1.magic = true;
				}
			}

			// custom debuff here soon
		}

		private static Vector2 GetVelocity(Projectile projectile)
		{
			float num1 = 400f;
			Vector2 vector2_1 = projectile.velocity;
			Vector2 vector2_2 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			vector2_2.Normalize();
			Vector2 vector2_3 = vector2_2 * ((float)Main.rand.Next(10, 41) * 0.1f);
			if (Main.rand.Next(3) == 0)
				vector2_3 *= 2f;
			Vector2 vector2_4 = vector2_1 * 0.25f + vector2_3;
			for (int index = 0; index < 200; ++index)
			{
				if (Main.npc[index].CanBeChasedBy((object)projectile, false))
				{
					float num2 = Main.npc[index].position.X + (float)(Main.npc[index].width / 2);
					float num3 = Main.npc[index].position.Y + (float)(Main.npc[index].height / 2);
					float num4 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num2) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num3);
					if ((double)num4 < (double)num1 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
					{
						num1 = num4;
						vector2_4.X = num2;
						vector2_4.Y = num3;
						Vector2 vector2_5 = vector2_4 - projectile.Center;
						vector2_5.Normalize();
						vector2_4 = vector2_5 * 8f;
					}
				}
			}
			Vector2 vector2_6 = vector2_4 * 0.8f;
			return vector2_6;
		}
	}
}