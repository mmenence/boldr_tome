
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using boldrTome.projectiles;

namespace boldrTome.Items.weapons
{
	public class coconut_gun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coconut gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Funky jazz can be heard coming from the barrel.");
		}

		public override void SetDefaults()
		{
			///item.UseSound = 43;
			item.useStyle = 5;
			item.damage = 50;
			item.useAnimation = 40;
			item.useTime = 40;
			item.width = 80;
			item.height = 80;
			item.shoot = mod.ProjectileType("funky_kong");
			item.shootSpeed = 18f;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.magic = true;
			item.noMelee = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}