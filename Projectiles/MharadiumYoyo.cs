using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Projectiles
{
    public class MharadiumYoyo : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Mharadium Yoyo";
            projectile.width = 16;
            projectile.height = 16;

            projectile.extraUpdates = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 99;
            aiType = 603;
            projectile.melee = true;
            projectile.scale = 1;
        }

        public override void AI()
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] >= 3f) // Spawn new projectile
            {
                float num2 = 400f;
                Vector2 vector = projectile.velocity;
                Vector2 vector2 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                vector2.Normalize();
                vector2 *= (float)Main.rand.Next(10, 41) * 0.1f;
                if (Main.rand.Next(3) == 0)
                {
                    vector2 *= 2f;
                }
                vector *= 0.25f;
                vector += vector2;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].CanBeChasedBy(this, false))
                    {
                        float num3 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
                        float num4 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
                        float num5 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num3) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num4);
                        if (num5 < num2 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                        {
                            num2 = num5;
                            vector.X = num3;
                            vector.Y = num4;
                            vector -= projectile.Center;
                            vector.Normalize();
                            vector *= 8f;
                        }
                    }
                }
                vector *= 0.8f;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector.X, vector.Y, mod.ProjectileType("DevilsSphere"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.localAI[1] = 0f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
