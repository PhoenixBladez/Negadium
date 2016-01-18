using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.NPCs.Negativity
{
    public class HydraShadow : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Hydra Shadow";
            npc.width = 34;
            npc.height = 44;
            Main.npcFrameCount[npc.type] = 6;
            
            npc.immortal = true;
            npc.lifeMax = 1;
            npc.damage = 20;
            npc.knockBackResist = 0.5F;
            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.npcSlots = 1;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override bool PreAI()
        {
            if (!Main.npc[(int)npc.ai[0]].active)
            {
                npc.active = false;
                return false;
            }

            if (npc.target < 0 || npc.target == (int)byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);

            float num1 = 9F;
            float num2 = 0.06f;
            if (Main.expertMode)
                num2 = 0.08f;

            Vector2 vector2_1 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num5 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num6 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
            float num7 = (float)((int)((double)num5 / 8.0) * 8);
            float num8 = (float)((int)((double)num6 / 8.0) * 8);
            vector2_1.X = (float)((int)((double)vector2_1.X / 8.0) * 8);
            vector2_1.Y = (float)((int)((double)vector2_1.Y / 8.0) * 8);
            float num9 = num7 - vector2_1.X;
            float num10 = num8 - vector2_1.Y;
            float num11 = (float)Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10);
            float num12 = num11;
            float SpeedX1;
            float SpeedY1;

            if ((double)num11 == 0.0)
            {
                SpeedX1 = npc.velocity.X;
                SpeedY1 = npc.velocity.Y;
            }
            else
            {
                float num3 = num1 / num11;
                SpeedX1 = num9 * num3;
                SpeedY1 = num10 * num3;
            }

            if ((double)num12 < 150.0)
            {
                npc.velocity.X = npc.velocity.X + SpeedX1 * 0.03f;
                npc.velocity.Y = npc.velocity.Y + SpeedY1 * 0.03f;
            }
            if (Main.player[npc.target].dead)
            {
                SpeedX1 = (float)((double)npc.direction * (double)num1 / 2.0);
                SpeedY1 = (float)(-(double)num1 / 2.0);
            }
            if ((double)npc.velocity.X < (double)SpeedX1)
            {
                npc.velocity.X = npc.velocity.X + num2;
            }
            else if ((double)npc.velocity.X > (double)SpeedX1)
            {
                npc.velocity.X = npc.velocity.X - num2;
            }
            if ((double)npc.velocity.Y < (double)SpeedY1)
            {
                npc.velocity.Y = npc.velocity.Y + num2;
            }
            else if ((double)npc.velocity.Y > (double)SpeedY1)
            {
                npc.velocity.Y = npc.velocity.Y - num2;
            }

            npc.rotation = (float)Math.Atan2((double)SpeedY1, (double)SpeedX1) - 1.57f;

            float num4 = 0.4f;
            if (npc.collideX)
            {
                npc.netUpdate = true;
                npc.velocity.X = npc.oldVelocity.X * -num4;
                if (npc.direction == -1 && (double)npc.velocity.X > 0.0 && (double)npc.velocity.X < 2.0)
                    npc.velocity.X = 2f;
                if (npc.direction == 1 && (double)npc.velocity.X < 0.0 && (double)npc.velocity.X > -2.0)
                    npc.velocity.X = -2f;
            }
            if (npc.collideY)
            {
                npc.netUpdate = true;
                npc.velocity.Y = npc.oldVelocity.Y * -num4;
                if ((double)npc.velocity.Y > 0.0 && (double)npc.velocity.Y < 1.5)
                    npc.velocity.Y = 2f;
                if ((double)npc.velocity.Y < 0.0 && (double)npc.velocity.Y > -1.5)
                    npc.velocity.Y = -2f;
            }

            if (npc.wet)
            {
                if ((double)npc.velocity.Y > 0.0)
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                npc.velocity.Y = npc.velocity.Y - 0.3f;
                if ((double)npc.velocity.Y < -2.0)
                    npc.velocity.Y = -2f;
            }

            if (((double)npc.velocity.X <= 0.0 || (double)npc.oldVelocity.X >= 0.0) && ((double)npc.velocity.X >= 0.0 || (double)npc.oldVelocity.X <= 0.0) && (((double)npc.velocity.Y <= 0.0 || (double)npc.oldVelocity.Y >= 0.0) && ((double)npc.velocity.Y >= 0.0 || (double)npc.oldVelocity.Y <= 0.0)) || npc.justHit)
                return false;
            npc.netUpdate = true;

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1F;
            npc.frameCounter %= Main.npcFrameCount[npc.type]; 
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;

            npc.spriteDirection = npc.direction;
        }
    }
}
