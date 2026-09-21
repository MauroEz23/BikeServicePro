using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikeServicePro.Helpers
{
    /// <summary>
    /// Helper para operaciones con archivos JSON
    /// Implementa manejo de concurrencia y seguridad en lectura/escritura
    /// </summary>
    public static class JsonFileHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        private static readonly object _lockObject = new();

        /// <summary>
        /// Lee datos de un archivo JSON de forma segura
        /// </summary>
        public static async Task<List<T>> ReadFromJsonFileAsync<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string jsonContent = await File.ReadAllTextAsync(filePath);
                
                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    return new List<T>();
                }

                var result = JsonSerializer.Deserialize<List<T>>(jsonContent, _options);
                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                // Log del error si se implementa logging
                Console.WriteLine($"Error reading from {filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Escribe datos en un archivo JSON de forma segura con locking
        /// </summary>
        public static async Task WriteToJsonFileAsync<T>(string filePath, List<T> data)
        {
            lock (_lockObject)
            {
                // Crear directorio si no existe
                string? directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                try
                {
                    string jsonContent = JsonSerializer.Serialize(data, _options);
                    
                    // Escribir en un archivo temporal y luego renombrar para evitar corrupción
                    string tempFile = filePath + ".tmp";
                    File.WriteAllText(tempFile, jsonContent);
                    
                    // Si existe el archivo original, hacer backup
                    if (File.Exists(filePath))
                    {
                        string backupFile = filePath + ".backup";
                        if (File.Exists(backupFile))
                        {
                            File.Delete(backupFile);
                        }
                        File.Move(filePath, backupFile);
                    }
                    
                    File.Move(tempFile, filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to {filePath}: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Verifica si un archivo existe
        /// </summary>
        public static bool FileExists(string filePath) => File.Exists(filePath);

        /// <summary>
        /// Obtiene la ruta completa para un archivo en DataStorage
        /// </summary>
        public static string GetDataStoragePath(string fileName)
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataStorage");
            
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            return Path.Combine(basePath, fileName);
        }

        /// <summary>
        /// Obtiene la ruta completa para un archivo en DataStorage con respaldo
        /// </summary>
        public static string GetDataStoragePathWithBackup(string fileName)
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataStorage");
            
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // Crear carpeta de backups si no existe
            string backupPath = Path.Combine(basePath, "Backups");
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            return Path.Combine(basePath, fileName);
        }

        /// <summary>
        /// Crea un backup del archivo especificado
        /// </summary>
        public static async Task CreateBackupAsync(string fileName)
        {
            string filePath = GetDataStoragePath(fileName);
            if (!File.Exists(filePath))
                return;

            string backupPath = Path.Combine(
                Path.GetDirectoryName(filePath)!,
                "Backups",
                $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(fileName)}"
            );

            // Crear directorio de backups si no existe
            string? backupDir = Path.GetDirectoryName(backupPath);
            if (!string.IsNullOrEmpty(backupDir) && !Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }

            await Task.Run(() => File.Copy(filePath, backupPath, true));
        }
    }
}