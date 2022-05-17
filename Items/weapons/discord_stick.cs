using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Steamworks;

namespace boldrTome.Items.weapons
{
	public class discord_stick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nice rod"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("ice, is nice");
		}

		public override void SetDefaults()
		{
			
			item.useStyle = 1;
			item.damage = 20;
			item.useAnimation = 20;
			item.useTime = 40;
			item.width = 40;
			item.height = 40;
			item.knockBack = 3.25f;
			item.value = 2000;
			item.magic = true;
			item.noMelee = true;
		}

        public override void RightClick(Player player)
        {
			player.position = Main.MouseWorld;
        }


    }
}