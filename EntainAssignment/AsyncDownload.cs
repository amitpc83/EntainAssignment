using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EntainAssignment
{
    public static class AsyncDownload
    {
        public static async Task<string[]> DownloadUrlAsync1(IEnumerable<string> urls, int maxConcurrentLimit)
        {
            using (var client = new HttpClient())
            {
                using (var semaphore = new SemaphoreSlim(maxConcurrentLimit))
                {
                    var tasks = new List<Task<string>>();
                    foreach (var url in urls)
                    {
                        tasks.Add(DownloadUrlHelper(url,semaphore,client));
                    }                  
                    try
                    {
                        await Task.WhenAll(tasks).ConfigureAwait(false);
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var innerException in ex.InnerExceptions)
                        {
                            await Console.Out.WriteLineAsync($"Exception occured:{innerException.Message}");
                        }
                    }
                    return tasks.Where(t => t.IsCompletedSuccessfully).Select(t => t.Result).ToArray();
                }
            }
        }

        private static async Task<string> DownloadUrlHelper(string url, SemaphoreSlim semaphore, HttpClient client)
        {
            await semaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                using (var response = await client.GetAsync(url).ConfigureAwait(false))
                {
                   response.EnsureSuccessStatusCode();
                   return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                await Console.Out.WriteLineAsync($"Http request failed for URL:{url}.Exception:{ex.Message}");
                return string.Empty;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
