using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriUygulaması.Utilities
{
    public class UtilityManager
    {
        static string[] dosyaTipleri = { "excel", "sheet", "pdf", "word", "presentation","powerpoint", "image", "text", "audio", "video", "code", "compressed" };
        static string[] dosyaIconlar = { "fa fa-file-excel-o", "fa fa-file-excel-o", "fa fa-file-pdf-o", "fa fa-file-word-o", "fa fa-file-powerpoint-o", "fa fa-file-powerpoint-o", "fa fa-file-image-o", "fa fa-file-text-o", "fa fa-file-sound-o", "fa fa-file-movie-o", "fa fa-file-code-o", "fa fa-file-archive-o" };
        static string[] dosyaclases = { "bgGreen", "bgGreen", "bgRed", "bgBlue", "bgOrange", "bgOrange", "bgYellow", "bgText", "bgAqua", "bgVideo", "bgCode", "bgPurple" };
        public static byte[] ByteBirlestir(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }

        public static string SetIcon(string contentType)
        {
            for (int i = 0; i < dosyaTipleri.Length; i++)
            {
                if (contentType.Contains(dosyaTipleri[i]))
                {
                    return dosyaIconlar[i];
                }
            }
            return "fa fa-file-o";
        }
        public static string ByteToString(long DosyaBoyutu)
        {
            string[] birimler = { "B", "KB", "MB", "GB", "TB", "PB", "EB"};
            long bytes = Math.Abs(DosyaBoyutu);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(DosyaBoyutu) * num).ToString() + birimler[place];
        }
        
            public static string SetClass(string contentType)
        {
            for (int i = 0; i < dosyaTipleri.Length; i++)
            {
                if (contentType.Contains(dosyaTipleri[i]))
                {
                    return dosyaclases[i];
                    
                }
            }
            return "bgGrey";
        }
    }

}