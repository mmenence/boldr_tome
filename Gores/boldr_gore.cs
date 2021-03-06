using Microsoft.Xna.Framework;
using System;
using System.Drawing.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Serialization;


namespace boldrTome.Gores
{
    public class boldr_gore : ModGore
    {
        public override void OnSpawn(Gore gore)
        {
            gore.velocity = new Vector2(Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() * MathHelper.TwoPi);
            gore.numFrames = 8;
            gore.frame = (byte)Main.rand.Next(8);
            gore.frameCounter = (byte)Main.rand.Next(8);
            updateType = 910;
        }

    }
}
