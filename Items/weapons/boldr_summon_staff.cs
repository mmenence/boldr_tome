using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace boldrTome.Items.weapons
{
	public class boldr_summon_staff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("boldr staff"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Summons a small pebble. When it was suggested that the cult adopt a few pet rocks, some of the slower members failed to see the joke");
		}

		public override void SetDefaults()
		{
			item.damage = 17;
			item.summon = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noMelee = true;

			item.shoot = mod.ProjectileType("boldr_minion");
			item.shootSpeed = 1f;

			item.buffType = mod.BuffType("boldr_summon_buff");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, mod.BuffType("boldr_summon_buff"));
			position = Main.MouseWorld;
			return true;

		}
	}
}