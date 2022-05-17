using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace boldrTome.Items
{
	public class boldr_hammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("boldr hammer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("definitive proof that lunacy does not equal creativity\nHitting a boss empowers the next swing");
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
			if(target.boss == true)
            {
				item.damage = 170;

			}
            else
            {
				item.damage = 34;
            }
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 600);
		}
	}
}