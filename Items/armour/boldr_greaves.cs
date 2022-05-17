
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Terraria.ModLoader.ModContent;


namespace boldrTome.Items.armour
{
	[AutoloadEquip(EquipType.Legs)]	

	public class boldr_greaves : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Boldr greaves");
			Tooltip.SetDefault("As fast as a boulder, twice as noisy\n5% increased movement speed");
		}

		public override void SetDefaults() 
		{
			item.Size = new Vector2(18);
			item.value = Item.sellPrice(silver: 24);
			item.rare = ItemRarityID.Blue;
			item.defense = 6;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("boldr_scale"), 10);
			recipe.AddIngredient(ItemID.StoneBlock, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}