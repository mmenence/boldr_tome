
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using boldrTome.projectiles;

namespace boldrTome.Items.weapons
{
	public class society_gun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("society blaster"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("this truly says a lot about society");
		}

		public override void SetDefaults()
		{
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SKIBIDI BOP MM DADA");
			item.useStyle = 5;
			item.damage = 30;
			item.useAnimation = 40;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.Dynamite;
			item.shootSpeed = 12f;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.ranged = true;
			item.noMelee = true;
		}

		
	}
}