
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.Items.weapons
{
	public class nice_rod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nice rod"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("ice, is nice");
		}

		public override void SetDefaults()
		{
			item.mana = 10;
			item.UseSound = SoundID.DD2_DarkMageCastHeal;
			item.useStyle = 1;
			item.damage = 20;
			item.useAnimation = 20;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.shoot = mod.ProjectileType("nice_bullet");
			item.shootSpeed = 6f;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.magic = true;
			item.noMelee = true;
		}

		
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ItemID.SnowBlock, 50);
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 80);
			}
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			// 60 frames = 1 second
			target.AddBuff(BuffID.Frostburn, 600);
			target.AddBuff(BuffID.Frozen, 600);
		}
	}
}