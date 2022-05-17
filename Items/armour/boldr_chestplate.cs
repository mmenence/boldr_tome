
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
	[AutoloadEquip(EquipType.Body)]	

	public class boldr_chestplate : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Boldr chestplate");
			Tooltip.SetDefault("surprisingly light for something comprised entirely of stone\n5% increase to melee damage and critical chance.");
		}

		public override void SetDefaults() 
		{
			item.Size = new Vector2(18);
			item.value = Item.sellPrice(silver: 24);
			item.rare = ItemRarityID.Blue;
			item.defense = 7;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeDamage += 0.5f;
			player.meleeCrit += 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("boldr_scale"), 12);
			recipe.AddIngredient(mod.ItemType("large_boldr_scale"), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}