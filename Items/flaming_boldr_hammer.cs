using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace boldrTome.Items
{
	public class flaming_boldr_hammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Molten boldr hammer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("left too long in the microwave, this has become one of the cults most dangerous weapons\nHitting a boss empowers the next swing");
		}

		public override void SetDefaults()
		{
			item.damage = 34;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 1;
			item.knockBack = 60;
			item.value = 10000;
			item.rare = 2;
			item.hammer = 70;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.boss == true)
			{
				item.damage = 170;

			}
			else
			{
				item.damage = 34;
			}
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 600);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("boldr_hammer"), 1);
			recipe.AddIngredient(ItemID.Hellstone, 6);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}