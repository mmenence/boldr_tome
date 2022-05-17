using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace boldrTome.NPCs
{
    public class boldr_summon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("summoned boldr");
        }

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 30;
            //npc.life = 100;
            npc.lifeMax = 10;
            npc.damage = 20;
            npc.defense = 0;
            //npc.HitSound = NPCHit3;//SoundID.Dig;//NPCHit1;
            //npc.DeathSound = NPCDeath3;//SoundID.Dig;//NPCDeath2;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 2;
            //aiType = NPCID.Zombie;
            //animationType = NPCID.Zombie;
            npc.noTileCollide = true;
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
    }
}