using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium.NPCs.Negativity
{
    public class HydraBody : ModNPC
    {
        private List<NPC> Heads;
        private bool spawnedHeads = false;

        private List<NPC> Creepers;
        private bool spawnedCreepers = false;
        private int creeperCount;

        public override void SetDefaults()
        {
            npc.name = "Void Hydra Vortex";
            npc.width = 636;
            npc.height = 653;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.lifeMax = 70000;
            npc.defense = 70;
            npc.damage = 70;
            npc.knockBackResist = 0.0F;
            npc.aiStyle = -1;

            npc.npcSlots = 1;

            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.boss = true;
            music = 25;
        }

        public override void AI()
        {
            if (!spawnedHeads)
            {
                Heads = new List<NPC>();
                for (int i = 0; i < 3; ++i)
                {
                    int randX = Main.rand.Next((int)npc.Center.X - 800, (int)npc.Center.X + 800);
                    int randY = Main.rand.Next((int)npc.Center.Y - 800, (int)npc.Center.Y + 800);
                    int head = NPC.NewNPC((int)randX, (int)randY, mod.NPCType("HydraHead")); // Spawn a new head.
                    Main.npc[head].ai[0] = npc.whoAmI; // Set the new head.ai[0] to this npc's value for later reference.
                    Main.npc[head].ai[1] = i; // Sets the number of the new head in head.ai[1].
                    Heads.Add(Main.npc[head]);
                }
                creeperCount = 60;
                spawnedHeads = true;
            }

            float speed = 4f; // A variable to cache small speed changes.

            npc.TargetClosest(true); // Target the closest player.

            bool stopMoving = false; // Checks if the Hydra should move (do not move when performing an AI action).

            // The less health the Hydra has, the faster it moves.
            if ((double)npc.life < (double)npc.lifeMax * 0.75)
                speed = 3f;
            if ((double)npc.life < (double)npc.lifeMax * 0.5)
            {
                speed = 4f;
                if (!spawnedCreepers)
                {
                    Creepers = new List<NPC>();
                    for (int i = 0; i < creeperCount; ++i)
                    {
                        int randX = Main.rand.Next((int)npc.Center.X - 800, (int)npc.Center.X + 800);
                        int randY = Main.rand.Next((int)npc.Center.Y - 800, (int)npc.Center.Y + 800);
                        int creep = NPC.NewNPC((int)randX, (int)randY, mod.NPCType("HydraShadow")); // Spawn a new shadow.
                        Main.npc[creep].ai[0] = npc.whoAmI; // Set the new head.ai[0] to this npc's value for later reference.
                        Creepers.Add(Main.npc[creep]);
                    }

                    spawnedCreepers = true;
                }
            }

            if ((double)npc.ai[0] == 0.0) // If no action is occuring (npc.ai[0] == 0.0).
            {
                ++npc.ai[1]; // Increase the npc.ai[1] by one each tick.
                if ((double)npc.life < (double)npc.lifeMax * 0.5)
                    ++npc.ai[1]; // Increase npc.ai[1] faster when the Hydra's health is lower than half its max health.
                if ((double)npc.life < (double)npc.lifeMax * 0.25)
                    ++npc.ai[1]; // Increase npc.ai[1] even more when the Hydra's health is lower than 1/4 of its max health.

                if ((double)npc.ai[1] >= 300.0 && Main.netMode != 1) // When the npc.ai[1] reaches a certain number (300)
                {
                    npc.ai[1] = 0.0f; // Reset the npc.ai[1]
                    npc.ai[0] = 1; // Set the npc.ai[0] to one, to start firing missiles.
                    npc.netUpdate = true; // Make sure this is synchronized over the net.
                }
            }
            else if ((double)npc.ai[0] == 1.0) // When the 1'st action is occuring (npc.ai[0] == 1.0).
            {
                stopMoving = true; // Since an action is occuring, set stopMoving to true.
                ++npc.ai[1]; // Increment the npc.ai[1].

                if ((double)npc.ai[1] % 5.0 == 0.0) // This if statement fires every 15 ticks (4 times each second).
                {
                    // Calculations for projectile direction.
                    Vector2 vector2 = new Vector2(npc.Center.X, npc.Center.Y - 300);
                    float num2 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector2.X;
                    float num3 = Main.player[npc.target].position.Y - vector2.Y;
                    float num4 = 10f / (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
                    float num5 = num2 * num4;
                    float num6 = num3 * num4;

                    int Type = 96;
                    float SpeedX = num5 * (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.00999999977648258);
                    float SpeedY = num6 * (float)(1.0 + (double)Main.rand.Next(-50, 51) * 0.00999999977648258);
                    int damage = 50;
                    int index = Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX * 2, SpeedY * 2, Type, damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index].timeLeft = 300;
                }
                if ((double)npc.ai[1] >= 120.0) // If the npc.ai[1] is equal to or exceeds 120 (after 2 seconds).
                {
                    npc.ai[1] = 0.0f; // Reset the npc.ai[1].
                    npc.ai[0] = 0.0f; // Reset the npc.ai[0].
                }
            }

            // If the magnitude between the center of the Hydra and the center of the targeted player is less than 50, stop moving.
            // Otherwise the Hydra will jitter if it stands under the player, as it's changing directions rapidly.
            if ((double)Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) < 50.0)
                stopMoving = true;

            if (stopMoving) // If the Hydra should stop moving.
            {
                npc.velocity.X = npc.velocity.X * 0.9f; // Start decreasing the X velocity of the Hydra.
                
                // If the X velocity of the Hydra is small enough.
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    npc.velocity.X = 0.0f; // Nullify the X velocity.
            }
            else // If the Hydra should be moving.
            {
                if (npc.direction > 0) // If the Hydra is looking to the right.
                    npc.velocity.X = (float)(((double)npc.velocity.X * 20.0 + (double)speed) / 21.0);
                if (npc.direction < 0) // If the Hydra is looking to the left.
                    npc.velocity.X = (float)(((double)npc.velocity.X * 20.0 - (double)speed) / 21.0);
            }


            Vector2 dir = Main.player[npc.target].Center - npc.Center;
            dir.Normalize();
            npc.velocity = dir * 4;
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(player, item, ref damage, ref knockback, ref crit);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.7f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), Color.White, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
