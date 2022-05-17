using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.Items
{
    public class spicy_gravel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spicy gravel");
            Tooltip.SetDefault("Further refined with forbidden arts, this pinnicle of food is sure to attract something big...");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(copper: 10);
            item.maxStack = 20;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 45;
            item.consumable = true;
            item.noMelee = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("cursed_boldr"));
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, player.position, 0);
            if(Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("cursed_boldr"));
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("seasoned_gravel"), 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddTile(TileID.CookingPots);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}