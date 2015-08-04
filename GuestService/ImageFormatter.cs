namespace GuestService
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class ImageFormatter
    {
        private Image image;

        public ImageFormatter(Image image) : this(image, null)
        {
        }

        public ImageFormatter(Image image, Image defaultImage)
        {
            this.Format = ImageFormat.Png;
            this.Mode = AjustMode.fillRegion;
            this.Background = Color.Transparent;
            this.image = (image != null) ? image : defaultImage;
            if (this.image != null)
            {
                this.Width = this.image.Width;
                this.Height = this.image.Height;
            }
            else
            {
                this.Width = this.Height = 0;
            }
        }

        public Stream CreateStream()
        {
            if (this.image != null)
            {
                MemoryStream stream = new MemoryStream();
                if ((this.Mode == AjustMode.original) || ((this.image.Width == this.Width) && (this.image.Height == this.Height)))
                {
                    this.image.Save(stream, this.Format);
                }
                else
                {
                    using (Bitmap bitmap = new Bitmap(this.Width, this.Height))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            float num = 1f;
                            if (this.Mode == AjustMode.fillRegion)
                            {
                                num = Math.Max((float) (((float) this.Width) / ((float) this.image.Width)), (float) (((float) this.Height) / ((float) this.image.Height)));
                            }
                            else if (this.Mode == AjustMode.fitRegion)
                            {
                                graphics.FillRectangle(new SolidBrush(this.Background), new Rectangle(new Point(0, 0), bitmap.Size));
                                num = Math.Min((float) (((float) this.Width) / ((float) this.image.Width)), (float) (((float) this.Height) / ((float) this.image.Height)));
                            }
                            int num2 = Convert.ToInt32((float) (this.image.Width * num));
                            int num3 = Convert.ToInt32((float) (this.image.Height * num));
                            graphics.DrawImage(this.image, new Rectangle(((this.Width - num2) / 2) - 1, ((this.Height - num3) / 2) - 1, num2 + 2, num3 + 2), -1, -1, this.image.Width + 2, this.image.Height + 2, GraphicsUnit.Pixel);
                        }
                        bitmap.Save(stream, this.Format);
                    }
                }
                stream.Position = 0L;
                return stream;
            }
            return null;
        }

        public Color Background { get; set; }

        public ImageFormat Format { get; set; }

        public int Height { get; set; }

        public string MediaType
        {
            get
            {
                if (this.Format == ImageFormat.Png)
                {
                    return "image/png";
                }
                if (this.Format != ImageFormat.Jpeg)
                {
                    throw new Exception(string.Format("unsupported image format: {0}", this.Format.ToString()));
                }
                return "image/jpeg";
            }
        }

        public AjustMode Mode { get; set; }

        public int Width { get; set; }

        public enum AjustMode
        {
            fillRegion,
            fitRegion,
            original
        }
    }
}

