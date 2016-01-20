public class AcheronYoyo : ModItem
{
	/*
		Gorateron
		for Charon
	*/
	public override void SetDefaults()
	{
		item.name = "Acheron";
		item.useStyle = 5;
		item.width = 30;
		item.height = 26;
		item.noUseGraphic = true;
		item.useSound = 1;
		item.melee = true;
		item.channel = true;
		item.noMelee = true;
		item.shoot = mod.ProjectileType("AcheronYoyoProjectile");
		item.useAnimation = 25;
		item.useTime = 25;
		item.shootSpeed = 16f;
		item.damage = 200;
		item.value = Item.sellPrice(55, 0, 0, 0);
		item.crit = 22;
		item.rare = -12;
		item.toolTip = "Calls upon the solar power to create devastation";
	}
}