using Microsoft.Xna.Framework;
using System;
using System.Drawing.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace boldrTome.NPCs
{
    public class bastard_fly : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("kawaii bastard fly");
        }

        private int attack_timer;
        private int degeneracy_timer;
        private int degen_num;


        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 50;
            //npc.life = 100;
            npc.lifeMax = 10000;
            npc.damage = 40;
            npc.defense = 10;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 0f;
            npc.knockBackResist = 1f;
            npc.aiStyle = -1;
            npc.npcSlots = 5;
            npc.noGravity = true;


        }

        public override void AI()
        {
            //targets closest player
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;

            //control rotation, in this case preventing it
            npc.rotation = 0.0f;
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
                    Main.NewText("°OwO° is anybawdy there?");
                    return;
                }
            }
            move_towards(npc, target, (float)((int)Vector2.Distance(target, npc.Center) > 300 ? 7f : 3f), 0f);
            attack_timer++;
            degeneracy_timer+= 1;

            if (attack_timer > 250)
            {
                Vector2 shootPos = npc.Center;
                Vector2 shootVel = target - shootPos;
                Projectile.NewProjectile((shootPos.X + (float)(-100 * npc.direction)), shootPos.Y, shootVel.X + 20, shootVel.Y + 20, mod.ProjectileType("degen_proj"), npc.damage, 5f);
                Projectile.NewProjectile((shootPos.X + (float)(-100 * npc.direction)), shootPos.Y, shootVel.X - 20, shootVel.Y - 20, mod.ProjectileType("degen_proj"), npc.damage, 5f);
                Projectile.NewProjectile((shootPos.X + (float)(-100 * npc.direction)), shootPos.Y, shootVel.X, shootVel.Y, mod.ProjectileType("degen_proj"), npc.damage, 5f);
                Main.NewText("take my wuv °OwO°");
                attack_timer = 0;
            }

            if ((int)degeneracy_timer > 60)
            {
                degen_num = Main.rand.Next(1, 10);
                
                switch ((float)degen_num)
                {
                    case (int)1:
                        Main.NewText("my wuv is as inescapable as Ur eventual demise °OwO°");
                        break;
                    case (int)2:
                        Main.NewText("°OwO° I will murder U and raise my young off Ur corpse");
                        break;
                    case (int)3:
                        Main.NewText("I will fill U with my digestive juices and drink U like a smoothie °OwO°");
                        break;
                    case (int)4:
                        Main.NewText("°OwO° Ur intestines look comfy!");
                        break;
                    case (int)5:
                        Main.NewText("°UvU°");
                        break;
                    case (int)6:
                        Main.NewText("No matter how well U dodge, I will always hit U °OwO°");
                        break;
                    case (int)7:
                        Main.NewText("Ur bones look cromchy °OwO°, I bet they taste nice °UvU°");
                        break;
                    case (int)8:
                        Main.NewText("It's more fun to eat U when Ur squirming °OwO°");
                        break;
                    case (int)9:
                        Main.NewText("°OwO°");
                        break;

                }
                degeneracy_timer = 0;

            }

            
            
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);
            
            Main.NewText("°0w0°");
            Main.NewText("twy nawt to gewt huwt");
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
    }

}