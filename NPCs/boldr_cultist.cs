using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.NPCs
{
    public class Boldr_cultist : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("boldr cultist");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 27;
            npc.height = 60;
            //npc.life = 100;
            npc.lifeMax = 100;
            npc.damage = 40;
            npc.defense = 3;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 0f;
            npc.knockBackResist = 1f;
            npc.aiStyle = 3;
            aiType = NPCID.Zombie;
            animationType = NPCID.Zombie;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustType = 1;
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }


        public override void NPCLoot()
        {
            Item.NewItem(npc.position, mod.ItemType("boldrM")); 
        }
    }
}