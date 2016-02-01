using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Negadium
{
    public class MharadiumNPCInfo : NPCInfo
    {
        public bool onMharadiumFire = false;
    }

    public class MharadiumModPlayer : ModPlayer
    {
        public bool onMharadiumFire = false;
        public bool hasSuperPowers = false;
        public bool babycrimera = false;
        public bool superPowersLoaded = false;
        public bool drawSuperPowers = false;

        private Texture2D superPowersTexture;

        public override void ResetEffects()
        {
            onMharadiumFire = false;
            hasSuperPowers = false;
            babycrimera = false;
            superPowersLoaded = false;
            drawSuperPowers = false;
        }

        public override void UpdateDead()
        {
            onMharadiumFire = false;
            hasSuperPowers = false;
            babycrimera = false;
            superPowersLoaded = false;
            drawSuperPowers = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (hasSuperPowers)
            {
                bool customDamage = false;

                int Damage1 = (int)damage;
                double num1 = customDamage ? (double)damage : Main.CalculatePlayerDamage(Damage1, player.statDefense);

                if (crit)
                    Damage1 *= 2;
                if (num1 >= 1.0)
                {
                    num1 = (double)(int)((1.0 - (double)player.endurance) * num1);
                    if (num1 < 1.0)
                        num1 = 1.0;

                    int Damage2 = (int)(num1 * 0.25);
                    num1 = (double)(int)(num1 * 0.75);
                    if (Main.player[Main.myPlayer].paladinGive)
                    {
                        int index = Main.myPlayer;
                        if (Main.player[index].team == player.team && player.team != 0)
                        {
                            float num2 = player.position.X - Main.player[index].position.X;
                            float num3 = player.position.Y - Main.player[index].position.Y;
                            if (Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3) < 800.0)
                                Main.player[index].Hurt(Damage2, 0, false, false, "", false, -1);
                        }
                    }
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (hasSuperPowers)
            {
                #region Boots Buffs
                player.noFallDmg = true; // Negate any fall damage.
                player.waterWalk = true; // Walk on liquids.
                player.lavaMax += 600; // 10 seconds of lava immunity.
                player.dash = 1; // Grants the ability to dash once.
                player.jumpBoost = true; // Allows the player to jump higher.
                player.doubleJumpCloud = true; // Allows double jumping.
                player.accRunSpeed = 10f; // Extra run speed max.
                player.moveSpeed += 0.65f; // 65% faster move speed.
                #endregion
                #region Shield Buffs
                player.starCloak = true; // Causes stars to fall when damaged.
                player.longInvince = true; // Extends the invincibility time after being hit.
                player.lavaRose = true; // Reduces damage taken from lava.
                player.fireWalk = true; // Prevents damage from Hellstone and Meteorite blocks.
                player.endurance += 0.2f; // Blocks 20% of incomming damage.
                player.noKnockback = true; // Knockback resist.
                player.buffImmune[46] = true;
                player.buffImmune[33] = true;
                player.buffImmune[36] = true;
                player.buffImmune[30] = true;
                player.buffImmune[20] = true;
                player.buffImmune[32] = true;
                player.buffImmune[31] = true;
                player.buffImmune[35] = true;
                player.buffImmune[23] = true;
                player.buffImmune[22] = true;
                // Paladins Shield in Hurt
                // Ice Barrier in Update
                #endregion
                #region Emblem Buffs
                player.meleeDamage += 0.3F;
                player.magicDamage += 0.3F;
                player.minionDamage += 0.3F;
                player.thrownDamage += 0.3F;
                player.rangedDamage += 0.3F;

                #endregion
                #region Magic Ball Buffs
                player.manaRegen += 2; // 200% extra mana regen.
                player.manaCost -= 0.2f; // -20% mana cost.

                // Crystal Ball effects.
                // Clairvoyance
                player.magicCrit += 2;
                player.magicDamage += 0.05f;
                player.statManaMax2 += 20;
                player.manaCost -= 0.02f;
                // Mana Flower effects.
                player.manaCost -= 0.08f; // -8% mana cost.
                player.manaFlower = true;
                // Celestial Cuffs effects.
                player.manaMagnet = true;
                player.magicCuffs = true;
                // Celestial Emblem + Sorcerer Emblem + Magic Power Potion effects.
                player.magicDamage += 0.5f;
                #endregion
                #region Health Band Buffs
                player.lifeRegen += 2; // 200% extra health regen.

                player.lifeMagnet = true; // Attracts heart from a longer distance.
                player.pStone = true; // Adds Philosopher's Stone effect.
                player.manaSickReduction += 0.5F; // Halves Mana Sickness.
                // Life force buff
                player.lifeForce = true;
                player.statLifeMax2 += player.statLifeMax / 5 / 20 * 20;
                #endregion
                // weapon imbue fire
                //player.AddBuff(BuffID.WeaponImbueFire, 2); // player.meleeEnchant = 3; (ranged from 1 to 8 for buffid 71-79
                player.meleeEnchant = 3;
                #region Buff given immunities
                // see Update()
                #endregion
                // Inferno buff
                player.inferno = false; // this causes the inferno rings
                //DrawInfernoRings(); // custom
                Lighting.AddLight((int)((double)player.Center.X / 16.0), (int)((double)player.Center.Y / 16.0), 0.65f, 0.4f, 0.1f);
                int type = 24;
                float num2 = 200f;
                bool flag = player.infernoCounter % 60 == 0;
                int Damage = 10;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int number = 0; number < 200; ++number)
                    {
                        NPC npc = Main.npc[number];
                        if (npc.active && !npc.friendly && (npc.damage > 0 && !npc.dontTakeDamage) && (!npc.buffImmune[type] && (double)Vector2.Distance(player.Center, npc.Center) <= (double)num2))
                        {
                            if (npc.HasBuff(type) == -1)
                                npc.AddBuff(type, 120, false);
                            if (flag)
                            {
                                npc.StrikeNPC(Damage, 0.0f, 0, false, false, false);
                                if (Main.netMode != 0)
                                    NetMessage.SendData(28, -1, -1, "", number, (float)Damage, 0.0f, 0.0f, 0, 0, 0);
                            }
                        }
                    }
                    if (player.hostile)
                    {
                        for (int number = 0; number < (int)byte.MaxValue; ++number)
                        {
                            Player plr = Main.player[number];
                            if (plr != this.player && plr.active && (!plr.dead && plr.hostile) && (!plr.buffImmune[type] && (plr.team != this.player.team || plr.team == 0)) && (double)Vector2.Distance(this.player.Center, plr.Center) <= (double)num2)
                            {
                                if (plr.HasBuff(type) == -1)
                                    plr.AddBuff(type, 120, true);
                                if (flag)
                                {
                                    plr.Hurt(Damage, 0, true, false, "", false, -1);
                                    if (Main.netMode != 0)
                                        NetMessage.SendData(26, -1, -1, Lang.deathMsg(this.player.whoAmI, -1, -1, -1), number, 0.0f, (float)Damage, 1f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }
        }
    
        private void GetSuperPowersTexture()
        {
            if (!this.superPowersLoaded)
            {
                this.superPowersTexture = mod.GetTexture("Developer/Gorateron/FlameRing");
                this.superPowersLoaded = true;
            }
        }

        public void DrawInfernoRings()
        {
            for (int index1 = 0; index1 < (int)byte.MaxValue; ++index1)
            {
                if (Main.player[index1].active && !Main.player[index1].outOfRange && !Main.player[index1].dead)
                {
                    this.GetSuperPowersTexture();
                    float num1 = 0.1f;
                    float num2 = 0.9f;
                    if (!Main.gamePaused)
                    {
                        Main.player[index1].flameRingScale += 0.004f;
                    }
                    float num3;
                    if ((double)Main.player[index1].flameRingScale < 1.0)
                    {
                        num3 = Main.player[index1].flameRingScale;
                    }
                    else
                    {
                        Main.player[index1].flameRingScale = 0.8f;
                        num3 = Main.player[index1].flameRingScale;
                    }
                    if (!Main.gamePaused)
                        Main.player[index1].flameRingRot += 0.05f;
                    if ((double)Main.player[index1].flameRingRot > 6.28318548202515)
                        Main.player[index1].flameRingRot -= 6.283185f;
                    if ((double)Main.player[index1].flameRingRot < -6.28318548202515)
                        Main.player[index1].flameRingRot += 6.283185f;
                    for (int index2 = 0; index2 < 3; ++index2)
                    {
                        float scale = num3 + num1 * (float)index2;
                        if ((double)scale > 1.0)
                            scale -= num1 * 2f;
                        float num4 = MathHelper.Lerp(0.8f, 0.0f, Math.Abs(scale - num2) * 10f);
                        Main.spriteBatch.Draw(superPowersTexture, Main.player[index1].Center - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 400 * index2, 400, 400)), new Microsoft.Xna.Framework.Color(num4, num4, num4, num4 / 2f), Main.player[index1].flameRingRot + 1.047198f * (float)index2, new Vector2(200f, 200f), scale, SpriteEffects.None, 0.0f);
                    }
                }
            }
        }

        // the following code uses private variables
         /* protected void LoadFlameRing()
        {
            if (Main.flameRingLoaded)
                return;
            this.flameRingTexture = this.Content.Load<Texture2D>("Images/FlameRing");
            Main.flameRingLoaded = true;
        } */


        public override void UpdateLifeRegen()
        {
            if (onMharadiumFire)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;
                player.lifeRegenTime = 0;
                player.lifeRegenTime -= 12;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (onMharadiumFire)
            {
                if (Main.rand.Next(4) == 0 && (double)drawInfo.shadow == 0.0)
                {
                    int index2 = Dust.NewDust(new Vector2(drawInfo.position.X - 2f, drawInfo.position.Y - 2f), drawInfo.drawPlayer.width + 4, drawInfo.drawPlayer.height + 4, 6, drawInfo.drawPlayer.velocity.X * 0.4f, drawInfo.drawPlayer.velocity.Y * 0.4f, 100, new Microsoft.Xna.Framework.Color(), 3f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 1.8f;
                    Dust dust = Main.dust[index2];
                    dust.velocity.Y = dust.velocity.Y - 0.5f;
                    Main.playerDrawDust.Add(index2);
                }
                b *= 0.6f;
                g *= 0.7f;

            }

            if (hasSuperPowers && drawSuperPowers)
            {
                DrawInfernoRings();
            }
        }
    }

    public class MharadiumGlobalNPC : GlobalNPC
    {
        public override void ResetEffects(NPC npc)
        {
            MharadiumNPCInfo modNPC = (MharadiumNPCInfo)npc.GetModInfo(mod, "MharadiumNPCInfo");
            modNPC.onMharadiumFire = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            MharadiumNPCInfo modNPC = (MharadiumNPCInfo)npc.GetModInfo(mod, "MharadiumNPCInfo");
            if (modNPC.onMharadiumFire)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= 12;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            MharadiumNPCInfo modNPC = (MharadiumNPCInfo)npc.GetModInfo(mod, "MharadiumNPCInfo");
            if (modNPC.onMharadiumFire)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int index2 = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 6, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, new Microsoft.Xna.Framework.Color(), 3f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 1.8f;
                    //Dust dust = Main.dust[index2];
                    Main.dust[index2].velocity.Y -= 0.5f;
                }
            }
        }
    }

}
