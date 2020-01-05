using System;
using System.IO;
using Android.Graphics;

namespace MenuPlanerApp.Core.Utility
{
    public static class ImageHelper
    {
        public static string ConvertBitmapToBase64String(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Compress(Bitmap.CompressFormat.Png, 0, stream);
                var bytes = stream.ToArray();
                var base64Str = Convert.ToBase64String(bytes);
                return base64Str;
            }
        }

        public static Bitmap ConvertBase64StringToBitmap(string imageString)
        {
            var imageBytes = Convert.FromBase64String(imageString);
            return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
        }
    }
}