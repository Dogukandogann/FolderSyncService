using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private FileSystemWatcher _watcher;
        private static string sourceDirectory = @"C:\Users\Senem\Desktop\Deneme1";
        private static string targetDirectory = @"C:\Users\Senem\Desktop\Deneme2";
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = sourceDirectory;
            _watcher.Filter = "*.*";
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

            _watcher.Created += OnChanged;
            _watcher.Changed += OnChanged;
            _watcher.Renamed += OnRenamed;
            _watcher.Deleted += OnDeleted;

            _watcher.EnableRaisingEvents = true;
            Log.Information("Dosya izleyici çalışıyor");
            await Task.CompletedTask;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {

            if (IsValidFileExtension(e.FullPath))
            {
                CopyFile(e.FullPath);
                Log.Information($"Dosya eklendi veya güncellendi {e.ChangeType}: {e.FullPath}");
            }
            else
            {
                Log.Error($"Geçersiz dosya formatı:{e.FullPath}");
            }
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {

            if (IsValidFileExtension(e.FullPath))
            {
                CopyFile(e.FullPath);
                Log.Information($"Dosya adı değiştirildi: {e.OldFullPath} --> {e.FullPath}");
            }
            else
            {
                Log.Error($"Geçersiz dosya formatı:{e.FullPath}");
            }

        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {

            if (IsValidFileExtension(e.FullPath))
            {
                DeleteFile(e.FullPath);
                Log.Information($"Dosya silindi: {e.FullPath}");
            }
            else
            {
                Log.Error($"Geçersiz dosya formatı:{e.FullPath}");
            }
        }

        private void CopyFile(string filePath)
        {
            
            try
            {
                string fileName = Path.GetFileName(filePath);
                string destFile =Path.Combine(targetDirectory, fileName);
                File.Copy(filePath, destFile,true);
                Log.Information($"Dosya kopyalandı: {destFile}");
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Hata: Dosya kopyalanamadı.");
            }
        }
        private void DeleteFile(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string destFile = Path.Combine(targetDirectory, fileName);
                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                    _logger.LogInformation($"Dosya silindi: {destFile}");

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Hata: Dosya silinemedi.");
            }
        }

        private bool IsValidFileExtension(string filePath)
        {
            string[] validExtensions = { ".doc", ".docx" };
            string extension = Path.GetExtension(filePath);
            return Array.Exists(validExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase));

        }

    }
}
