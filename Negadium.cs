using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium
{
    public class Negadium : Mod
    {
        public override void SetModInfo(out string name, ref ModProperties properties)
        {
            name = "Negadium";
            properties.Autoload = true;
            properties.AutoloadGores = true;
            properties.AutoloadSounds = true;
        }

        public static bool IsInHell(Vector2 position)
        {
            return (position.Y / 16) > (Main.maxTilesY - 200);
        }

        public static void SpawnOnPlayer(int plr, int Type, bool lateDraw)
        {
            bool flag = false;
            int num1 = 0;
            int num2 = 0;
            int minValue1 = (int)((double)Main.player[plr].position.X / 16.0) - 100 * 2;
            int maxValue1 = (int)((double)Main.player[plr].position.X / 16.0) + 100 * 2;
            int minValue2 = (int)((double)Main.player[plr].position.Y / 16.0) - 100 * 2;
            int maxValue2 = (int)((double)Main.player[plr].position.Y / 16.0) + 100 * 2;
            int num3 = (int)((double)Main.player[plr].position.X / 16.0) - NPC.safeRangeX;
            int num4 = (int)((double)Main.player[plr].position.X / 16.0) + NPC.safeRangeX;
            int num5 = (int)((double)Main.player[plr].position.Y / 16.0) - NPC.safeRangeY;
            int num6 = (int)((double)Main.player[plr].position.Y / 16.0) + NPC.safeRangeY;
            if (minValue1 < 0)
                minValue1 = 0;
            if (maxValue1 > Main.maxTilesX)
                maxValue1 = Main.maxTilesX;
            if (minValue2 < 0)
                minValue2 = 0;
            if (maxValue2 > Main.maxTilesY)
                maxValue2 = Main.maxTilesY;
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                for (int index2 = 0; index2 < 100; ++index2)
                {
                    int index3 = Main.rand.Next(minValue1, maxValue1);
                    int index4 = Main.rand.Next(minValue2, maxValue2);
                    if (!Main.tile[index3, index4].nactive() || !Main.tileSolid[(int)Main.tile[index3, index4].type])
                    {
                        if ((!Main.wallHouse[(int)Main.tile[index3, index4].wall] || index1 >= 999) && (Type != 50 || index1 >= 500 || (int)Main.tile[index4, index4].wall <= 0))
                        {
                            for (int index5 = index4; index5 < Main.maxTilesY; ++index5)
                            {
                                if (Main.tile[index3, index5].nactive() && Main.tileSolid[(int)Main.tile[index3, index5].type])
                                {
                                    if (index3 < num3 || index3 > num4 || (index5 < num5 || index5 > num6) || index1 == 999)
                                    {
                                        int num7 = (int)Main.tile[index3, index5].type;
                                        num1 = index3;
                                        num2 = index5;
                                        flag = true;
                                        break;
                                    }
                                    else
                                        break;
                                }
                            }
                            if (flag && Type == 50 && index1 < 900)
                            {
                                int num7 = 20;
                                if (!Collision.CanHit(new Vector2((float)num1, (float)(num2 - 1)) * 16f, 16, 16, new Vector2((float)num1, (float)(num2 - 1 - num7)) * 16f, 16, 16) || !Collision.CanHit(new Vector2((float)num1, (float)(num2 - 1 - num7)) * 16f, 16, 16, Main.player[plr].Center, 0, 0))
                                {
                                    num1 = 0;
                                    num2 = 0;
                                    flag = false;
                                }
                            }
                            if (flag && index1 < 999)
                            {
                                int num7 = num1 - 10 / 2;
                                int num8 = num1 + 10 / 2;
                                int num9 = num2 - 10;
                                int num10 = num2;
                                if (num7 < 0)
                                    flag = false;
                                if (num8 > Main.maxTilesX)
                                    flag = false;
                                if (num9 < 0)
                                    flag = false;
                                if (num10 > Main.maxTilesY)
                                    flag = false;
                                if (flag)
                                {
                                    for (int index5 = num7; index5 < num8; ++index5)
                                    {
                                        for (int index6 = num9; index6 < num10; ++index6)
                                        {
                                            if (Main.tile[index5, index6].nactive() && Main.tileSolid[(int)Main.tile[index5, index6].type])
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            continue;
                    }
                    if (flag || flag)
                        break;
                }
                if (flag && index1 < 999)
                {
                    Rectangle rectangle1 = new Rectangle(num1 * 16, num2 * 16, 16, 16);
                    for (int index2 = 0; index2 < (int)byte.MaxValue; ++index2)
                    {
                        if (Main.player[index2].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)((double)Main.player[index2].position.X + (double)(Main.player[index2].width / 2) - (double)(NPC.sWidth / 2) - (double)NPC.safeRangeX), (int)((double)Main.player[index2].position.Y + (double)(Main.player[index2].height / 2) - (double)(NPC.sHeight / 2) - (double)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                            if (rectangle1.Intersects(rectangle2))
                                flag = false;
                        }
                    }
                }
                if (flag)
                    break;
            }
            if (!flag)
                return;
            int index = -1;
            for (int index2 = 199; index2 >= 0; --index2)
            {
                if (!Main.npc[index2].active)
                {
                    index = index2;
                    break;
                }
            }
            if (index < 0)
                return;
            int number = NPC.NewNPC(num1 * 16 + 8, num2 * 16, Type, index, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
            if (number == 200)
                return;
            Main.npc[number].target = plr;
            Main.npc[number].timeLeft *= 20;
            string str = Main.npc[number].name;
            if (Main.npc[number].displayName != "")
                str = Main.npc[number].displayName;
            if (Main.netMode == 2 && number < 200)
                NetMessage.SendData(23, -1, -1, "", number, 0.0f, 0.0f, 0.0f, 0, 0, 0);

            if (Type == 125)
            {
                if (Main.netMode == 0)
                {
                    Main.NewText(Lang.misc[48], (byte)175, (byte)75, byte.MaxValue, false);
                }
                else
                {
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendData(25, -1, -1, Lang.misc[48], (int)byte.MaxValue, 175f, 75f, (float)byte.MaxValue, 0, 0, 0);
                }
            }
            else
            {
                if (Type == 82 || Type == 126 || (Type == 50 || Type == 398))
                    return;
                if (Main.netMode == 0)
                {
                    Main.NewText(str + " " + Lang.misc[16], (byte)175, (byte)75, byte.MaxValue, false);
                }
                else
                {
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendData(25, -1, -1, str + " " + Lang.misc[16], (int)byte.MaxValue, 175f, 75f, (float)byte.MaxValue, 0, 0, 0);
                }
            }
        }
    }
}
