using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

using Microsoft.Xna.Framework;

namespace Negadium.Tiles
{
    public class CustomGlobalTile : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            // Check if the tile in question is actually a 'plant' tile.
            if (type == TileID.Plants || type == TileID.Plants2)
            {
                // Seeds drop with a 50% chance (hence the WorldGen.genRand.Next(2).
                // After that we find the closest player (and probably the player that killes the tile)

                if (WorldGen.genRand.Next(2) == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].HasItem(mod.ItemType("YourItem")))
                {
                    // Spawn a seed!
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Seed);

                    // Return false to stop this tile from dropping anything else.
                    return false;
                }
            }
            return true;
        }
    }
}
