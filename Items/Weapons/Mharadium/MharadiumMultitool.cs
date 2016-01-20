using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Negadium.Items.Weapons.Mharadium
{
    public class MharadiumMultitool : ModItem
    {
        private enum ToolType
        {
            Multi = 0,
            Pickaxe = 1,
            Hammer = 2,
            Axe = 3
        }
        private ToolType currentType;

        public override void SetDefaults()
        {
            item.name = "Mharadium Multitool";
            item.width = 36;
            item.height = 36;
            item.toolTip2 = "Right click to change its functionality!";
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 10;

            item.melee = true;
            item.damage = 90;
            item.useStyle = 1;
            item.useAnimation = 12;
            item.useTime = 4;
            item.knockBack = 5.5f;
            item.useTurn = true;
            item.autoReuse = true;
            item.useSound = 1;

            item.tileBoost += 15; // Adds 15 Range.
            currentType = 0;
            GetTool();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            item.stack = 2;

            currentType++;
            GetTool();
        }

        private void GetTool()
        {
            if ((int)currentType > 3)
                currentType = ToolType.Multi; // Reset the currentType to Multi.
            item.toolTip = "Active function: " + currentType.ToString();
            switch (currentType)
            {
                case ToolType.Multi:
                    item.pick = 300; // 300% pickaxe power.
                    item.hammer = 300; // 300% hammer power.
                    item.axe = 60; // 300% axe power.
                    break;
                case ToolType.Pickaxe:
                    item.pick = 300; // 300% pickaxe power.
                    item.hammer = 0; // 0% hammer power.
                    item.axe = 0; // 0% axe power.
                    break;
                case ToolType.Hammer:
                    item.pick = 0; // 300% pickaxe power.
                    item.hammer = 300; // 0% hammer power.
                    item.axe = 0; // 0% axe power.
                    break;
                case ToolType.Axe:
                    item.pick = 0; // 0% pickaxe power.
                    item.hammer = 0; // 300% hammer power.
                    item.axe = 60; // 300% axe power.
                    break;
            }
        }

        public override void SaveCustomData(System.IO.BinaryWriter writer)
        {
            writer.Write((byte)currentType);
            base.SaveCustomData(writer);
        }
        public override void LoadCustomData(System.IO.BinaryReader reader)
        {
            currentType = (ToolType)reader.ReadByte();
            base.LoadCustomData(reader);
            GetTool();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 30);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
