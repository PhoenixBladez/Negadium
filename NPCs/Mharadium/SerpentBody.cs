using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.NPCs.Mharadium
{
    public class SerpentBody : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Serpent Body";
            npc.width = 26;
            npc.height = 50;
            npc.aiStyle = 6;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.lifeMax = 40000;
            npc.damage = 40;
            Main.npcFrameCount[npc.type] = 1;
	        npc.soundHit = 8;
	        npc.soundKilled = 10;
            npc.netAlways = true;
            npc.alpha = 30;
            aiType = -1;
            npc.knockBackResist = 0;

            animationType = 89;
        }

        public override void AI()
        {
            npc.TargetClosest(true);

            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
            }
            if (npc.position.X > Main.npc[(int)npc.ai[1]].position.X)
            {
                npc.spriteDirection = 1;
            }
            if (npc.position.X < Main.npc[(int)npc.ai[1]].position.X)
            {
                npc.spriteDirection = -1;
            }
        }
    }
}
