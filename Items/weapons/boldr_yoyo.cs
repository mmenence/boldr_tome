using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace boldrTome.Items.weapons
{
    public class boldr_yoyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("boldr yoyo"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A rock on a string. not quite sure what you were expecting...");
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(24);
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 25);

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.damage = 22;
            item.knockBack = 3.5f;
            item.shoot = mod.ProjectileType("boldr_yoyo_proj");
            item.shootSpeed = 16.5f;
        }

        
    }
}

