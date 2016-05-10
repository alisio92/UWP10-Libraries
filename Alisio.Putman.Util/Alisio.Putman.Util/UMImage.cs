using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Alisio.Putman.UtilMethods
{
    public class UMImage
    {
        public StorageFile File;
        public BitmapDecoder Decoder;

        public async Task<byte[]> GetBytesFromFileAsync(string imagePath)
        {
            File = await StorageFile.GetFileFromApplicationUriAsync(new Uri(imagePath));
            Byte[] fileBytes = await GetByteArrayFromFileAsync(File);
            Byte[] actualBytes = await GetByteArrayFromFileByteArrayAsync(fileBytes);
            return actualBytes;
        }

        public async Task<byte[]> GetByteArrayFromFileAsync(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                Stream readStream = inputStream.AsStreamForRead();
                byte[] byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }

        public async Task<byte[]> GetByteArrayFromFileByteArrayAsync(byte[] fileBytes)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0));
            writer.WriteBytes(fileBytes);
            await writer.StoreAsync();

            Decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, stream);
            PixelDataProvider provider = await Decoder.GetPixelDataAsync();
            Byte[] bytes = provider.DetachPixelData();
            return bytes;
        }

        #region Static

        public static async Task<string> GetBase64StringFromFileAsync(StorageFile storageFile)
        {
            Stream ms = await storageFile.OpenStreamForReadAsync();
            byte[] bytes = new byte[(int)ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            return Convert.ToBase64String(bytes);
        }

        public static async Task<string> GetBase64StringFromWriteableBitmapAsync(WriteableBitmap writeableBitmap)
        {
            InMemoryRandomAccessStream stream = await CreateStreamFromByteArrayAsync(writeableBitmap.PixelBuffer.ToArray(), writeableBitmap.PixelWidth, writeableBitmap.PixelHeight);
            byte[] bytes = new byte[stream.Size];
            await stream.AsStream().ReadAsync(bytes, 0, bytes.Length);
            return Convert.ToBase64String(bytes);
        }

        //public static string GetBase64StringFromImage(WriteableBitmap writeableBitmap)
        //{
        //    Byte[] bytes = writeableBitmap.PixelBuffer.ToArray();
        //    return Convert.ToBase64String(bytes);
        //}

        //public static string GetBase64StringFromImage(SoftwareBitmap softwareBitmap)
        //{
        //    WriteableBitmap writeableBitmap = new WriteableBitmap(softwareBitmap.PixelWidth, softwareBitmap.PixelHeight);
        //    softwareBitmap.CopyToBuffer(writeableBitmap.PixelBuffer);
        //    return GetBase64StringFromImage(writeableBitmap);
        //}

        public static async Task<Byte[]> GetByteArrayFromStorageFileAsync(StorageFile storageFile)
        {
            Stream ms = await storageFile.OpenStreamForReadAsync();
            byte[] bytes = new byte[(int)ms.Length];
            ms.Read(bytes, 0, (int)ms.Length);
            return bytes;
        }

        //public static byte[] GetByteArrayFromImage(WriteableBitmap writeableBitmap)
        //{
        //    /* Stream stream = bitmap.PixelBuffer.AsStream();
        //     MemoryStream memoryStream = new MemoryStream();
        //     stream.CopyTo(memoryStream);
        //     return memoryStream.ToArray();*/
        //    return writeableBitmap.PixelBuffer.ToArray();
        //}

        //public static byte[] GetByteArrayFromImage(SoftwareBitmap softwareBitmap)
        //{
        //    WriteableBitmap writeableBitmap = new WriteableBitmap(softwareBitmap.PixelWidth, softwareBitmap.PixelHeight);
        //    softwareBitmap.CopyToBuffer(writeableBitmap.PixelBuffer);
        //    return GetByteArrayFromImage(writeableBitmap);
        //}

        //public static async Task<BitmapImage> GetBitmapImageFromByteArrayAsync(byte[] bytes)
        //{
        //    /*using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
        //    {
        //        using (DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0)))
        //        {
        //            writer.WriteBytes(bytes);
        //            await writer.StoreAsync();
        //        }
        //        var image = new BitmapImage();
        //        await image.SetSourceAsync(stream);
        //        return image;
        //    }*/

        //    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
        //    {
        //        BitmapImage image = new BitmapImage();
        //        await stream.WriteAsync(bytes.AsBuffer());
        //        stream.Seek(0);
        //        image.SetSource(stream);
        //        return image;
        //    }
        //}

        public static async Task<BitmapImage> CreateImageFromByteArrayAsync(Byte[] bytes, ImageProperties properties, BitmapDecoder decoder)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, properties.Width, properties.Height, decoder.DpiX, decoder.DpiY, bytes);
            await encoder.FlushAsync();

            BitmapImage image = new BitmapImage();
            await image.SetSourceAsync(stream);
            image.DecodePixelWidth = int.Parse(properties.Width.ToString());
            image.DecodePixelHeight = int.Parse(properties.Height.ToString());
            return image;
        }

        public static async Task<BitmapImage> CreateImageFromByteArrayAsync(Byte[] bytes, int width, int height)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, uint.Parse(width.ToString()), uint.Parse(height.ToString()), 95, 95, bytes);
            await encoder.FlushAsync();

            BitmapImage image = new BitmapImage();
            await image.SetSourceAsync(stream);
            image.DecodePixelWidth = width;
            image.DecodePixelHeight = height;
            return image;
        }

        public static async Task<InMemoryRandomAccessStream> CreateStreamFromByteArrayAsync(Byte[] bytes, int width, int height)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, uint.Parse(width.ToString()), uint.Parse(height.ToString()), 95, 95, bytes);
            await encoder.FlushAsync();
            return stream;
        }

        public static async Task<WriteableBitmap> CreateWriteableBitmapFromByteArrayAsync(Byte[] bytes, int width, int height)
        {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied, uint.Parse(width.ToString()), uint.Parse(height.ToString()), 95, 95, bytes);
            await encoder.FlushAsync();
            WriteableBitmap writeableBitmap = new WriteableBitmap(width, height);
            await writeableBitmap.SetSourceAsync(stream);
            return writeableBitmap;
        }

        public static Color GetPixelColor(int position, Byte[] bytes)
        {
            int start = position * 4;
            byte r = 0;
            byte g = 0;
            byte b = 0;
            byte a = 0;
            r = bytes[start];
            g = bytes[start + 1];
            b = bytes[start + 2];
            a = bytes[start + 3];
            return Color.FromArgb(a, r, g, b);
        }

        public static void SetPixel(int position, ref Byte[] bytes, byte r, byte g, byte b, byte a)
        {
            int start = position * 4;
            bytes[start] = r;
            bytes[start + 1] = g;
            bytes[start + 2] = b;
            bytes[start + 3] = a;
        }

        public static void SetPixel(int position, ref Byte[] bytes, Color pixel)
        {
            int start = position * 4;
            bytes[start] = pixel.R;
            bytes[start + 1] = pixel.G;
            bytes[start + 2] = pixel.B;
            bytes[start + 3] = pixel.A;
        }

        #endregion
    }
}
