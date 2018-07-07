/// <summary>
/// Program Author  :   Erkan ESEN (Esen Software And Design)
/// Created Date    :   2016.11.30 
/// Revision Date   :   2016.11.30
/// Description     :   Bu Program *.* Dosyaların Shell Context Menüsüne Eklenir.
///                     Tıkladığında Seçili Olan Tüm Dosyaların Adreslerini Argüman Olarak Alır.
///                     Dosya ile Aynı Dizine Aynı Dosya İsmi ile Text Dökümanı Oluşturur ve
///                     Text Dökümanına Veri Girilmek Üzere Açar.
/// Communication   :   erkanesen2202@gmail.com
///                 :   27.07.2016 Added SourceControl
/// </summary>

using System;
using System.IO;
using System.Windows.Forms;

namespace FormNote
{
    static class Program
    {

        public static string filePath, myDocumentsPath,newTxtFilePath;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] arguments)
        {
            if (arguments.Length > 0)
            {
                // Adres argumanını regtistry den alır
                filePath = arguments[0];
                myDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // My Documents te template in bulunacağı klasör yok ise
                if (!Directory.Exists(myDocumentsPath + "\\FormNote"))
                {
                    Directory.CreateDirectory(myDocumentsPath + "\\FormNote");
                }

                //Klasör içinde template dosyası 
                if (!File.Exists(myDocumentsPath + "\\FormNote\\temp.txt"))
                {
                    MessageBox.Show(myDocumentsPath + "\\FormNote\\temp.txt  : " + "Yolunda template dosyası bulunmamaktadır. Düzenlemeniz için template dosyası açılacaktır.");
                    File.Create(myDocumentsPath + "\\FormNote\\temp.txt");
                    //template txt dosyasını aç
                    System.Diagnostics.Process.Start(myDocumentsPath + "\\FormNote\\temp.txt");
                }
                else
                {

                    if (isDirectory(filePath))
                    {
                        // yok ise temp dosyayı kopyalayarak oluştur.
                        File.Copy(myDocumentsPath + "\\FormNote\\temp.txt", filePath + ".txt");
                        newTxtFilePath = filePath + ".txt";
                    }
                    else
                    {
                        // seçilen dosya ismi ile .txt dosyası varmı
                        if (!File.Exists(filePath.Replace(Path.GetExtension(filePath), "") + ".txt"))
                        {
                            // yok ise temp dosyayı kopyalayarak oluştur.
                            File.Copy(myDocumentsPath + "\\FormNote\\temp.txt", filePath.Replace(Path.GetExtension(filePath), "") + ".txt");
                            newTxtFilePath = filePath.Replace(Path.GetExtension(filePath), "") + ".txt";
                        }

                    }

                    // .txt dosyasını aç
                    System.Diagnostics.Process.Start(newTxtFilePath);
                }

            }
            else
            {
                // adres argumanı gelmedi ise:
                MessageBox.Show("Dosya Seçilmedi || No Selected File");
            }
        }


        private static bool isDirectory(string path)
        {
            FileAttributes attributes = File.GetAttributes(@path);
            // now we will detect whether its a directory or file
            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            else
                return false;

        }
    }
}
