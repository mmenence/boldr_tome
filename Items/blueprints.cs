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
    public class blueprints : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blueprints of the imperial bank");
            Tooltip.SetDefault("somewhere, somehow... some bastard wants these.");
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
            return !NPC.AnyNPCs(mod.NPCType("chairlie"));
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, player.position);
            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("chairlie"));
            }
            return true;
        }

        
    }
}