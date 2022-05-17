
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
	[AutoloadEquip(EquipType.Head)]

	public class boldr_helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boldr headpiece");
			Tooltip.SetDefault("'To surpass oneself, you must emulate perfection' - Master Boldr, on growth\n5% increased melee speed");
		}

		public override void SetDefaults()
		{
			item.Size = new Vector2(18);
			item.value = Item.sellPrice(silver: 24);
			item.rare = ItemRarityID.Blue;
			item.defense = 5;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<boldr_chestplate>() && legs.type == ItemType<boldr_greaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "'At last, perfection'";
			player.meleeSpeed += 0.05f;
			player.meleeDamage += 0.4f;
			player.meleeCrit += 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("boldr_scale"), 7);
			recipe.AddIngredient(ItemID.Boulder, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}