
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using boldrTome.projectiles;

namespace boldrTome.Items.weapons
{
	public class boldr_gun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boldr gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Although it's power has been tamed somewhat, using this is... I'll advised at best.");
		}

		public override void SetDefaults()
		{
			///item.UseSound = 43;
			item.useStyle = 5;
			item.damage = 30;
			item.useAnimation = 40;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.shoot = mod.ProjectileType("boldr_bullet");
			item.shootSpeed = 18f;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.ranged = true;
			item.noMelee = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("boldr_scale"), 5);
			recipe.AddIngredient(mod.ItemType("boldrM"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}