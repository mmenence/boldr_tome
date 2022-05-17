using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.Items.weapons
{
	public class vulcan_cannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("the vulcan cannon"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'thats a lotta bullets' - unknown scholar");
		}

		public override void SetDefaults()
		{

			item.damage = 20;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); 
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}

		
	}
}