using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Listen.Data
{
    public class Listener : IDisposable
    {
        private readonly HttpClient httpClient;

        public string Name { get; set; } = string.Empty;
        public int Counter { get; set; } = 0;
        public int Target { get; set; } = 0;

        public event EventHandler<int> CounterChanged;


        private CancellationTokenSource cancellationTokenSource;
        private ManualResetEvent threadStoppedEvent;
        private bool disposed = false;


        private const int MaxRetries = 5;
        private const double MinDelaySeconds = 1.5;
        private const double MaxDelaySeconds = 60;

        public Listener(string apiBaseUrl)
        {
            var rand = new Random();
            Name = NameGenerator.GenerateRandomMonsterName();
            Target = rand.Next(0, 11);
            httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };


            cancellationTokenSource = new CancellationTokenSource();
            threadStoppedEvent = new ManualResetEvent(false);
        }


        public async Task StartMonitoringAsync()
        {

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await MakeApiCallWithRetryAsync();

                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch (OperationCanceledException)
                {

                }

            }
            threadStoppedEvent.Set();
        }

        public void StopMonitoring()
        {
            cancellationTokenSource.Cancel();

            if (!threadStoppedEvent.WaitOne(TimeSpan.FromSeconds(10)))
            {
                // Handle the case where the thread didn't stop gracefully (e.g., forceful termination)
                // You can log an error or take appropriate action here.

                // Forcefully abort the thread as a last resort
                // Note: Avoid using Thread.Abort() if possible; it's a last-resort option.
                // The better approach is to gracefully handle cancellation.
                // Thread.Abort();
            }
        }

        private async Task MakeApiCallWithRetryAsync()
        {
            bool success = false;

            for (int retryCount = 0; retryCount < MaxRetries && !success; retryCount++)
            {
                try
                {
                    var response = await httpClient.GetAsync(httpClient.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                        if (data?.Data != null)
                        {
                            int number = data.Data.Number;
                            if (Target == number)
                            {
                                Counter++;
                                CounterChanged?.Invoke(this, Counter);

                            }
                            success = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Non-successful response, HTTP Status Code: {(int)response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                if (!success)
                {
                    double delaySeconds = Math.Pow(2, retryCount);
                    delaySeconds = Math.Min(Math.Max(delaySeconds, MinDelaySeconds), MaxDelaySeconds);
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                }
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    cancellationTokenSource.Dispose();
                    threadStoppedEvent.Dispose();
                    httpClient.Dispose();
                }


                disposed = true;
            }
        }

        ~Listener()
        {
            Dispose(true);
        }
    }
}