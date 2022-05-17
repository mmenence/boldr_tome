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
    public class boldr_scale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boldr scale");
            Tooltip.SetDefault("A scale of and ancient (and thankfully dead) creature.");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(copper: 10);
            item.maxStack = 999;
        }
    }
}