using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Steamworks;

namespace boldrTome.Items.weapons
{
	public class weaboo_stick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("spacial katana"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("japan");
		}

		public override void SetDefaults()
		{

			item.useStyle = 1;
			item.damage = 20;
			item.useAnimation = 30;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.melee = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			Vector2 perturbedSpeed = new Vector2(0, 0);
			Vector2 thing = player.Center;

			Projectile.NewProjectile(player.position, player.position, (int)perturbedSpeed.X, (int)perturbedSpeed.Y, 3f, 1, 20f, player.whoAmI);
			player.position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Katana, 1);
			recipe.AddIngredient(ItemID.RodofDiscord, 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}