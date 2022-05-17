using Microsoft.Xna.Framework;
using System;
using System.Drawing.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Serialization;
using IL.Terraria.Audio;

namespace boldrTome.NPCs.bosses
{
    [AutoloadBossHead]

    public class bigass_boldr : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A suspiciosly large boulder");
        }

        private int attack_timer;
        private int phase;
        private float dive_speed;
        private int dive_num;

        public override void SetDefaults()
        {
            npc.width = 180;
            npc.height = 180;
            //npc.life = 100;
            npc.lifeMax = 2500;
            npc.damage = 40;
            npc.defense = 5;
            npc.HitSound = SoundID.Item10;
            npc.DeathSound = SoundID.Item107;
            npc.value = 0f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = -1;
            npc.npcSlots = 5;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            music = MusicID.Boss2;
            //music = mod.GetSoundSlot(SoundType.Music, ("Sounds/Custom/plok_boss_music"));
            npc.value = Item.buyPrice(gold: 5);

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale * 0.65f);
            npc.damage = (int)(npc.damage * 1.3f);
        }

        public override void AI()
        {
            //targets closest player
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;

            
            npc.netAlways = true;
            npc.TargetClosest(true);

            //makes sure npc life is not greater than max life
            //if (npc.life >= npc.lifeMax)
            //    npc.life = npc.lifeMax;

            //handles despawning

            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y -= 0.1f;
                if (npc.timeLeft > 20)
                {
                    npc.timeLeft = 20;
                    return;
                }
            }

            if (phase == 0)
            {
                npc.rotation = 0.0f;
                if (attack_timer <= 600)
                {
                    move_towards(npc, target, 3f, 10f);
                    if (attack_timer % 200 == 0)
                    {
                        //NPC.AnyNPCs(mod.NPCType("boldr_summon"));
                        //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("boldr_summon"));
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("boldr_summon"));

                    }
                    npc.noTileCollide = true;
                    dive_speed = 0f;
                }
                else
                {
                    if (attack_timer <= 780)
                    {
                        target.Y -= 300;
                        move_towards(npc, target, 20f, 0f);
                        for (int i = 0; i < 5; i++)
                        {
                            int dustType = 1;
                            int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                            Dust dust = Main.dust[dustIndex];
                            dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                            dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                            dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        }
                    }

                    else
                    {
                        npc.velocity.X = 0f;
                        npc.noTileCollide = false;
                        dive(npc, dive_speed);
                        dive_speed += 0.1f;
                    }
                    if (attack_timer == 780)
                    {
                        Main.PlaySound(SoundID.Roar, player.position, 0);
                    }

                    if (attack_timer == 920)
                    {
                        attack_timer = 0;
                    }

                    if (npc.life <= (0.5f * npc.lifeMax))
                    {
                        npc.defense = 10;
                        Main.PlaySound(SoundID.Roar, player.position, 0);
                        phase++;
                        npc.damage = 50;
                        npc.defense = 10;
                        for (int i = 0; i < 100; i++)
                        {
                            int dustType = 1;
                            int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                            Dust dust = Main.dust[dustIndex];
                            dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                            dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                            dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        }
                        for (int i = 0; i < 100; i++)
                        {
                            int dustType = 6;
                            int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                            Dust dust = Main.dust[dustIndex];
                            dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                            dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                            dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        }
                    }

                }
            }

            else
            {
                npc.rotation += 0.05f;
                npc.knockBackResist = 0f;
                for (int i = 0; i < 13; i++)
                {
                    int dustType = 6;
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                    dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                    dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                }
                if (attack_timer <= 600)
                {
                    move_towards(npc, target, 7f, 30f);
                    if (attack_timer % 150 == 0)
                    {
                        //NPC.AnyNPCs(mod.NPCType("boldr_summon"));
                        //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("boldr_summon"));
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("boldr_summon"));

                    }
                    npc.noTileCollide = true;
                    dive_speed = 0f;
                }
                else
                {
                    if (attack_timer <= 720)
                    {
                        target.Y -= 300;
                        move_towards(npc, target, 20f, 0f);
                        dive_speed = 0f;
                        npc.noTileCollide = true;
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

                    else
                    {
                        npc.rotation = 0.0f;
                        npc.velocity.X = 0f;
                        npc.noTileCollide = false;
                        dive(npc, dive_speed);
                        dive_speed += 0.05f;
                    }
                    if (attack_timer == 720)
                    {
                        Main.PlaySound(SoundID.Roar, player.position, 0);
                    }
                    if (attack_timer >= 780 && dive_num >= 2)
                    {
                        attack_timer = 0;
                        dive_speed = 0f;
                        dive_num = 0;

                    }
                    else if (attack_timer >= 780)
                    {
                        attack_timer = 600;
                        dive_speed = 0f;
                        dive_num += 1;
                    }

                    

                }

                
            }
            attack_timer++;
        }


        //shamelessly stolen move towards function
        private void move_towards(NPC npc, Vector2 playerTarget, float speed, float turnResistance)
        {
            //find velocity
            var move = playerTarget - npc.Center;
            float length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            //turn resistance
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();
            {
                move *= speed / length;
            }
            //change the velocity
            npc.velocity = move;
        }

        private void dive(NPC npc, float speed)
        {
            npc.velocity.Y = -(speed*speed - 15*speed - 4f);


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
            Item.NewItem(npc.position, mod.ItemType("boldr_hammer"));
            Item.NewItem(npc.position, mod.ItemType("large_boldr_scale"));
            Item.NewItem(npc.position, mod.ItemType("boldrM"));
            Item.NewItem(npc.position, mod.ItemType("boldr_summon_staff"));
            for (int i = 0; i < (10 + Main.rand.Next(8)); i++)
            {
                Item.NewItem(npc.position, mod.ItemType("boldr_scale"));
            }
            for (int i = 0; i < (1 + Main.rand.Next(3)); i++)
            {
                Item.NewItem(npc.position, mod.ItemType("lage_boldr_scale"));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }

}