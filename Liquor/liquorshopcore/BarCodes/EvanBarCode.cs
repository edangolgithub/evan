// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Spire.Barcode;
// using System.Drawing;
// using System.Drawing.Imaging;
// //using System.Windows.Forms;
// //using PQScan.BarcodeScanner;
// using System.IO;
// //using System.Windows.Media.Imaging;
// using LiquorShop.Classes;

// namespace EvanDangol.BarCodes
// {
//     public static class EvanBarCode
//     {
//         public static Image GenerateBarCode(this string data, bool save = false, string path = "")
//         {
//             BarcodeSettings setting = new BarcodeSettings();
//             setting.Data = data;
//             setting.Type = Spire.Barcode.BarCodeType.Code93Extended;
//             BarCodeGenerator gen = new BarCodeGenerator(setting);
//             Image barcodeimage = gen.GenerateImage();

            
//             if (save)
//             {
//                 if(!Directory.Exists(path))
//                 {
//                     AdminstrationHelper.MakeDirectory(path);
//                 }
//                 if (File.Exists(path + "\\" + data + ".png"))
//                 {
//                     // File.Delete(path + "\\" + data + ".png");
//                 }
//                 barcodeimage.Save(path + "\\" + data + ".png", ImageFormat.Png);
//             }


//             return barcodeimage;

//         }
//         public static BitmapImage GenerateBarCodeRetBitMap(this string data, bool save = false, string path = "")
//         {
//             BarcodeSettings setting = new BarcodeSettings();
//             setting.Data = data;
//             //setting.Type = Spire.Barcode.BarCodeType.Code93Extended;
//             BarCodeGenerator gen = new BarCodeGenerator(setting);
//             Image barcodeimage = gen.GenerateImage();

//             if (save)
//             {
//                 if (File.Exists(path + "\\" + data + ".png"))
//                 {
//                     // File.Delete(path + "\\" + data + ".png");
//                 }
//                 barcodeimage.Save(path + "\\" + data + ".png", ImageFormat.Png);
//             }


//             MemoryStream ms = new MemoryStream();
//             //variable that holds image
//             barcodeimage.Save(ms, ImageFormat.Bmp);
//             //my buffer byte
//             byte[] buffer = ms.GetBuffer();
//             //Create new MemoryStream that has the contents of buffer
//             MemoryStream bufferPasser = new MemoryStream(buffer);

//             BitmapImage bitmap = new BitmapImage();
//             bitmap.BeginInit();
//             bitmap.StreamSource = bufferPasser;
//             bitmap.EndInit();
//             return bitmap;

//         }
//         //public static string ReadBarCode(this string data)
//         //{

//         //    Image i = Bitmap.FromFile(data);
//         //    Bitmap b = new Bitmap(i);
//         //    BarcodeResult br = BarCodeScanner.Scan(b).FirstOrDefault();
//         //    br.BarType = PQScan.BarcodeScanner.BarCodeType.Code128;
//         //    return br.Data;
//         //    //return "";
//         //}
//         public static string ReadBarCodeSpire(this string data, string filename)
//         {
//             try
//             {
//                 var v = Spire.Barcode.BarcodeScanner.Scan(filename);
//                 if (v.Length < 1)
//                 {
//                     return "";
//                 }
//                 return v[0];
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }

//     }
// }
