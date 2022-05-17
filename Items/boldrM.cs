
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.Items
{
	public class boldrM : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Boldr tome"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Only a master of the ancient arts can use this without accident. You are not a master.");
		}

		public override void SetDefaults() 
		{
			item.mana = 10;
			///item.UseSound = 43;
			item.useStyle = 5;
			item.damage = 1000;
			item.useAnimation = 40;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.shoot = 99;
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