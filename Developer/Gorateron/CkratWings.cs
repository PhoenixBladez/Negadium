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
using Terraria.DataStructures;

namespace Negadium.Developer.Gorateron
{
    public class CkratWings : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            // Make it a wing type
            equips.Add(EquipType.Wings);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Ckrat Wings";
            item.width = 24;
            item.height = 30;
            item.toolTip = "Ckrat's personal favorite";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = -12;
            item.accessory = true;
            //item.wingSlot = (sbyte) 125;
        }

        public override void UpdateAccessory(Player player)
        {
            player.wingTimeMax = 5 * 60 * 60;
        }

        public override void VerticalWingSpeeds(ref float ascentWhenFalling, ref float ascentWhenRising,
           ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1.1f;
            ascentWhenRising = 0.30f;
            maxCanAscendMultiplier = 1.2f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.175f;
        }

        public override void HorizontalWingSpeeds(ref float speed, ref float acceleration)
        {
            speed = 13f;
            acceleration *= 5f;
        }

        public override void WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                int dust1 = Dust.NewDust(player.position, player.width, player.height, mod.DustType("AcheronDust"));
            }
            base.WingUpdate(player, inUse);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale)
        {
            Player drawPlayer = Main.player[Main.myPlayer];
            Vector2 Position = drawPlayer.position;
            Texture2D drawTexture = mod.GetTexture("Developer/Gorateron/CkratWings_Glow");
            Color color17 = Color.White;
            Main.wingsTexture[drawPlayer.wings] = drawTexture;

            int num17 = 0;
            int num23 = 0;
            int num24 = 0;
            for (int index2 = 0; index2 < 20; ++index2)
            {
                int index3 = index2 % 10;
                if (drawPlayer.dye[index3] != null && drawPlayer.armor[index2].type > 0 && drawPlayer.armor[index2].stack > 0 && (index2 / 10 >= 1 || !drawPlayer.hideVisual[index3] || ((int)drawPlayer.armor[index2].wingSlot > 0 || drawPlayer.armor[index2].type == 934)))
                {
                    if ((int)drawPlayer.armor[index2].wingSlot > 0)
                    {
                        num17 = (int)drawPlayer.dye[index3].dye;
                    }
                }
            }
            DrawData drawData = new DrawData(Main.wingsTexture[drawPlayer.wings], new Vector2((float)((int)((double)Position.X - (double)Main.screenPosition.X + (double)(drawPlayer.width / 2) - (double)(9 * drawPlayer.direction)) + num24 * drawPlayer.direction), (float)(int)((double)Position.Y - (double)Main.screenPosition.Y + (double)(drawPlayer.height / 2) + 2.0 * (double)drawPlayer.gravDir + (double)num23 * (double)drawPlayer.gravDir)), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4)), color17, drawPlayer.bodyRotation, new Vector2((float)(Main.wingsTexture[drawPlayer.wings].Width / 2), (float)(Main.wingsTexture[drawPlayer.wings].Height / 8)), 1f, SpriteEffects.None, 0);
            drawData.shader = num17;
            Main.playerDrawData.Add(drawData);
        }
    }
}
