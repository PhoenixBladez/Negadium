using Terraria;
using Terraria.ModLoader;

namespace Negadium.Developer.Charon
{
    public class CrimerasBuff : ModBuff
    {
        // Gorateron, for Charon. Used by the baby crimera pet. Put on player by: CrimerasChunk

        public override void SetDefaults()
        {
            Main.buffName[Type] = "Baby Crimera";
            Main.buffTip[Type] = "A baby Crimera is following you";

            Main.buffNoTimeDisplay[Type] = true;
            //Main.buffNoSave[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            int buffNewType = mod.ProjectileType("BabyCrimera");

            // we use a ModPlayer instead of player.eater (bool)
                MharadiumModPlayer modPlayer = (MharadiumModPlayer)player.GetModPlayer(mod, "MharadiumModPlayer");
                modPlayer.babycrimera = true;

            bool flag = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("BabyCrimera")] > 0) // already has a babycrimera
                flag = false;

            if (flag && player.whoAmI == Main.myPlayer)
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0.0f, 0.0f, buffNewType, 0, 0.0f, player.whoAmI, 0.0f, 0.0f);

        }
    }
}
