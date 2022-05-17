
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace boldrTome.Items.accessories
{
    public class large_boldr_scale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Large boldr scale");
            Tooltip.SetDefault("A scale of unusual size and magical origion\nincreases armour penatration by 5\nincreases melee speed and damage by 5%");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(20);
            item.accessory = true;
            item.value = Item.sellPrice(silver: 12);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamageMult += 0.05f;
            player.meleeSpeed += 0.5f;
            player.armorPenetration += 5;
        }

        
    }
}