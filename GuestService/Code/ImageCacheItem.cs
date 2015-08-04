namespace GuestService.Code
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class ImageCacheItem
    {
        public Stream CraeteStream()
        {
            return ((this.Data != null) ? new MemoryStream(this.Data) : null);
        }

        public static ImageCacheItem Create(Stream stream, string mediaType)
        {
            ImageCacheItem item = new ImageCacheItem();
            if (stream != null)
            {
                item.Data = new byte[stream.Length];
                stream.Read(item.Data, 0, item.Data.Length);
                stream.Position = 0L;
            }
            item.MediaType = mediaType;
            return item;
        }

        public byte[] Data { get; set; }

        public string MediaType { get; set; }
    }
}

