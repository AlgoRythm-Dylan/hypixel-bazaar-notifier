using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace BazaarNotifier.Lib
{
    public class AppData
    {
        public static async Task<T> Load<T>(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(fileName);
                return JsonSerializer.Deserialize<T>(await FileIO.ReadTextAsync(file));
            }
            catch
            {
                return default;

            }
        }
        public static async Task Save<T>(string fileName, T data)
        {
            var folder = ApplicationData.Current.LocalFolder;
            StorageFile file;
            try
            {
                file = await folder.CreateFileAsync(fileName);
            }
            catch
            {
                file = await folder.GetFileAsync(fileName);
            }
            await FileIO.WriteTextAsync(file, JsonSerializer.Serialize(data));
        }
    }
}
