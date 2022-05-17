using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using boldrTome.projectiles;

namespace boldrTome.Items.weapons
{
	public class sans_gun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("sans gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun sans gun");
		}

		public override void SetDefaults()
		{
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/sans_voice");
			item.useStyle = 5;
			item.damage = 180;
			item.useAnimation = 21;
			item.useTime = 5;
			item.width = 40;
			item.height = 40;
			item.shoot = mod.ProjectileType("sans_projectile");
			item.shootSpeed = 18f;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
		}


		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3; // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)); // 30 degree spread.
																												// If you want to randomize the speed to stagger the projectiles
																												// float scale = 1f - (Main.rand.NextFloat() * .3f);
																												// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}

		
	}
}