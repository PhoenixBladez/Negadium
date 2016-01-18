using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Negadium.Tiles
{
    public class Incubator : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("Incubator"));
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            for (int index = 0; index < player.inventory.Length; ++index)
            {
                if(player.inventory[index].type == mod.ItemType("DemonFetus"))
                {
                    //if (Negadium.IsInHell(player.position))
                    //{
                        Main.NewText("You inserted the Demon Fetus!", 255, 255, 0, true);
                        player.inventory[index].stack--;

                        NPC.SpawnOnPlayer(Main.myPlayer, mod.NPCType("SerpentHead"));
                        Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                    //}
                    //else
                    //{
                    //    Main.NewText("This action can only be performed in hell!", 255, 255, 0, true);
                    //}
                    return;
                }
            }
            Main.NewText("You do not have the required item(s).", 255, 255, 0, true);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType("Incubator");
        }
    }
}
