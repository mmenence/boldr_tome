using Microsoft.Xna.Framework;
using System;
using System.Drawing.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Serialization;

namespace boldrTome.NPCs.bosses
{
    [AutoloadBossHead]

    public class cursed_boldr : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A suspiciosly cursed boulder");
        }

        private int attack_timer;
        private int phase;
        private int direction;
        private int portal_attack;

        public override void SetDefaults()
        {
            npc.width = 180;
            npc.height = 180;
            //npc.life = 100;
            npc.lifeMax = 4500;
            npc.damage = 40;
            npc.defense = 15;
            npc.HitSound = SoundID.Item10;
            npc.DeathSound = SoundID.Item107;
            npc.value = 0f;
            npc.knockBackResist = 0f;
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
                //repeated charges
                if (attack_timer < 360)
                {
                    move_towards(npc, target, 7f, (float)((int)Vector2.Distance(target, npc.Center) > 400 ? 3f : 3000f));
                    if (direction != 1 && direction != -1)
                    {
                        if (npc.position.X >= target.X)
                        {
                            direction = -1;
                        }
                        else
                        {
                            direction = 1;
                        }
                        
                    }
                }

                else if (attack_timer < 450)
                {
                    npc.rotation += direction * 0.05f;
                    target.X -= direction * 300;
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
                    npc.velocity.Y = 0f;
                    dive(npc, 7, direction);
                }

                if(attack_timer == 435)
                {
                    Main.PlaySound(SoundID.Roar, player.position, 0);
                }

                if (npc.life <= (0.5f*npc.lifeMax) && attack_timer == 510)
                {
                    phase++;
                    attack_timer = 0;
                    direction *= -1;
                    npc.defense = 25;
                }
                else if (attack_timer == 510)
                {
                    attack_timer = 0;
                    direction *= -1;
                }
            }

            else
            {
                if (attack_timer < 120)
                {
                    target.Y -= 550;

                    for (int i = 0; i < (attack_timer == 0 ? 150 : 10); i++)
                    {
                        int dustType = 163; //75 may be better, but ignores Y velocity
                        int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                        dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                        dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                    }
                    target.X -= 90;
                    npc.position = target;
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;

                    
                }
                else if (attack_timer == 121)
                {
                    /*
                    //the portal for the boldr
                    Projectile.NewProjectile((npc.position.X + 400), npc.position.Y, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                    //the eight portals around the player
                    Projectile.NewProjectile((target.X + 400), target.Y, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X - 400), target.Y, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X), (target.Y + 400f), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X), (target.Y - 400f), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X + 282.8f), target.Y + 282.8f, 0, 0, mod.ProjectileType("cursed_portal_135"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X - 282.8f), target.Y + 282.8f, 0, 0, mod.ProjectileType("cursed_portal_45"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X + 282.8f), target.Y - 282.8f, 0, 0, mod.ProjectileType("cursed_portal_45"), npc.damage, 5f);
                    Projectile.NewProjectile((target.X - 282.8f), target.Y - 282.8f, 0, 0, mod.ProjectileType("cursed_portal_135"), npc.damage, 5f);
                    */
                    Main.PlaySound(SoundID.Roar, player.position, 0);
                    Vector2 portals_centre = player.Center;
                    portal_attack = Main.rand.Next(3);
                    Projectile.NewProjectile((npc.position.X + 400), npc.position.Y + 65, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                    switch (portal_attack)
                    {
                        case (0):
                            Projectile.NewProjectile((portals_centre.X - 282.8f), portals_centre.Y - 232.8f, 0, 0, mod.ProjectileType("cursed_portal_135"), npc.damage, 5f);

                            break;

                        case 1:
                            Projectile.NewProjectile((portals_centre.X+90), (portals_centre.Y - 400f), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                            break;

                        case 2:
                            Projectile.NewProjectile((portals_centre.X - 500), portals_centre.Y -30, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                            break;
                    }

                }
                else if (attack_timer == 161)
                {
                    Vector2 portals_centre = npc.Center;
                    portals_centre.Y += 550;
                    portals_centre.X -= 120;
                    switch (portal_attack)
                    {

                        case (0):
                            Projectile.NewProjectile((portals_centre.X + 262.8f), portals_centre.Y - 282.8f, 0, 0, mod.ProjectileType("cursed_portal_45"), npc.damage, 5f);
                            Projectile.NewProjectile((portals_centre.X + 382.8f), portals_centre.Y + 282.8f, 0, 0, mod.ProjectileType("cursed_portal_135"), npc.damage, 5f);
                            break;

                        case 1:
                            Projectile.NewProjectile((portals_centre.X +190), (portals_centre.Y + 400f - 60), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                            Projectile.NewProjectile((portals_centre.X - 400), portals_centre.Y -100, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                            break;

                        case 2:
                            
                            Projectile.NewProjectile((portals_centre.X + 400), portals_centre.Y - 100, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                            Projectile.NewProjectile((portals_centre.X+45 +120), (portals_centre.Y - 400f - 40), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                            break;
                    }

                }
                else if (attack_timer == 201)
                {
                    Vector2 portals_centre = npc.Center;
                    portals_centre.Y += 550;
                    switch (portal_attack)
                    {
                        case (0):
                            Projectile.NewProjectile((portals_centre.X - 362.8f), portals_centre.Y + 282.8f, 0, 0, mod.ProjectileType("cursed_portal_45"), npc.damage, 5f);
                            //Main.NewText("portal");
                            Projectile.NewProjectile((portals_centre.X + 120), portals_centre.Y - 400, 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                            break;

                        case 1:
                            Projectile.NewProjectile((portals_centre.X + 280), portals_centre.Y-90, 0, 0, mod.ProjectileType("cursed_portal"), npc.damage, 5f);
                            Projectile.NewProjectile((portals_centre.X + 162.8f), portals_centre.Y - 362.8f, 0, 0, mod.ProjectileType("cursed_portal_45"), npc.damage, 5f);
                            break;

                        case 2:
                            Projectile.NewProjectile((portals_centre.X-90 +180), (portals_centre.Y + 400f), 0, 0, mod.ProjectileType("cursed_portal_ninety"), npc.damage, 5f);
                            Projectile.NewProjectile((portals_centre.X + 302.8f), portals_centre.Y + 262.8f, 0, 0, mod.ProjectileType("cursed_portal_135"), npc.damage, 5f);
                            break;
                    }
                }
                if(attack_timer>=120 && attack_timer<=210)
                {
                    if (attack_timer % 12 == 0)
                    {
                        Projectile.NewProjectile((npc.Center.X + 270), npc.Center.Y + 300, -3, -3, mod.ProjectileType("circle_boldr"), 40, 5f);
                        Projectile.NewProjectile((npc.Center.X + 270), npc.Center.Y + 300, 3, 3, mod.ProjectileType("circle_boldr"), 40, 5f);
                        
                        Projectile.NewProjectile((npc.Center.X - 270), npc.Center.Y + 820, -3, -3, mod.ProjectileType("reverse_circle_boldr"), 40, 5f);
                        Projectile.NewProjectile((npc.Center.X - 270), npc.Center.Y + 820, 3, 3, mod.ProjectileType("reverse_circle_boldr"), 40, 5f);
                        
                        Projectile.NewProjectile((npc.Center.X + 270), npc.Center.Y + 820, -3, 3, mod.ProjectileType("circle2_boldr"), 40, 5f);
                        Projectile.NewProjectile((npc.Center.X + 270), npc.Center.Y + 820, 3, -3, mod.ProjectileType("circle2_boldr"), 40, 5f);
                        
                        Projectile.NewProjectile((npc.Center.X - 270), npc.Center.Y + 300, -3, 3, mod.ProjectileType("reverse2_circle_boldr"), 40, 5f);
                        Projectile.NewProjectile((npc.Center.X - 270), npc.Center.Y + 300, 3, -3, mod.ProjectileType("reverse2_circle_boldr"), 40, 5f);

                    }
                }

                switch (attack_timer)
                {
                    case 260:
                        npc.velocity.X = 7f;
                        npc.velocity.Y = 0f;
                        Main.PlaySound(SoundID.Roar, player.position, 0);
                        break;

                    case 295:
                        switch (portal_attack)
                        {
                            case 0:
                                npc.position.X -= (400 + 282.8f - 90 - 45);
                                npc.position.Y += (550f - 282.8f - 90 + 65);
                                npc.velocity.X = 10f;
                                npc.velocity.Y = 10f;
                                break;

                            case 1:
                                npc.position.X -= (400 - 90 - 45);
                                npc.position.Y += (150);
                                npc.velocity.X = 0;
                                npc.velocity.Y = 14;
                                break;

                            case 2:
                                npc.position.X -= (800 - 90 - 15);
                                npc.position.Y += (550 - 90 - 15);
                                npc.velocity.X = 15;
                                npc.velocity.Y = 0;
                                break;
                        }
                        break;

                    case 345:
                        switch (portal_attack)
                        {
                            case 0:
                                npc.position.X -= (0);
                                npc.position.Y -= (400);
                                npc.velocity.X = -9.9f;
                                npc.velocity.Y = 9.9f;
                                break;

                            case 1:
                                npc.position.X -= (400);
                                npc.position.Y -= (400);
                                npc.velocity.X = 14;
                                npc.velocity.Y = 0.1f;
                                break;

                            case 2:
                                npc.position.X -= (370);
                                npc.position.Y += (400);
                                npc.velocity.X = 0;
                                npc.velocity.Y = -14;
                                break;
                        }
                        break;

                    case 395:
                        switch (portal_attack)
                        {
                            case 0:
                                //Main.NewText("0");
                                npc.position.X += (322.8f);
                                npc.position.Y -= (682.8f);
                                npc.velocity.X = 0f;
                                npc.velocity.Y = 9.9f;
                                break;

                            case 1:
                                //Main.NewText("1");
                                npc.position.X += (0);
                                npc.position.Y += (-300);
                                npc.velocity.X = -9.9f;
                                npc.velocity.Y = 9.9f;
                                break;

                            case 2:
                                //Main.NewText("2");
                                npc.position.X += (282.8f);
                                npc.position.Y += (120 + 600);
                                npc.velocity.X = -9.9f;
                                npc.velocity.Y = -9.9f;
                                break;
                        }
                        break;



                }
                if (attack_timer <= 560)
                {
                    npc.rotation += 0.05f;
                }

                else if (attack_timer >= 560)
                {
                    move_towards(npc, target, (float)((int)Vector2.Distance(target, npc.Center) > 500 ? 50f : 14f), (float)((int)Vector2.Distance(target, npc.Center) > 500 ? 0f : 3000f));
                }


                
                if(attack_timer == 850)
                {
                    attack_timer = 0;
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

        private void dive(NPC npc, float speed, float direction)
        {
            npc.velocity.X = direction*(18);


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

        

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.65f;
            return null;
        }

    }

}