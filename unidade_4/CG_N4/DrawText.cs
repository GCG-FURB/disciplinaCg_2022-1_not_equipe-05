using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace CG_N4
{
    internal class DrawText
    {
        private int text_id;
        public void AddTexture(Bitmap texture, bool mipmaped)
        {
            this.text_id = LoadTexture(texture, mipmaped);
        }
        public void AddText(string text, Color color, float x, float y, float scale)
        {
            const int side = 256;
            Bitmap texture = new Bitmap(side, side);
            Graphics g = Graphics.FromImage(texture);
            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, texture.Size));
            }
            using (Font font = new Font(SystemFonts.DialogFont.FontFamily, 12f))
            {
                SizeF sz = g.MeasureString(text, font);
                float f = 256 / Math.Max(sz.Width, sz.Height) * scale;
                g.TranslateTransform(256 / 2 + f * sz.Width / 2, 256 / 2 - f * sz.Height / 2);
                g.ScaleTransform(-f, f);
                using (Brush brush = new SolidBrush(color))
                {
                    g.DrawString(text, font, brush, 0, 0);
                }
            }
            AddTexture(texture, true);
        }

        public static int LoadTexture(Bitmap texture, bool mipmaped)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            int wt = texture.Width;
            int ht = texture.Height;

            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            System.Drawing.Imaging.BitmapData data = texture.LockBits(
                new Rectangle(0, 0, wt, ht),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, wt, ht, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            texture.UnlockBits(data);

            if (mipmaped)
            {

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.TexParameter(TextureTarget.Texture2D,
                    TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D,
                    TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            }
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            return id;
        }
    }
}
