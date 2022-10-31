using MISA.ASP.ClientApp.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ASP.ClientApp.Utils.DesignPattern
{
    public static class CommonPattern
    {
        public static async Task Retry(Func<Task> fun, int RetryTimes = 1, int WaitTime = 500)
        {
            for (int i = 0; i < RetryTimes - 1; i++)
            {
                try
                {
                    await fun();
                    return;
                }
                catch (Exception ex)
                {
                    if (ex is IBreakRetryException)
                    {
                        throw ex;
                    }

                    Console.WriteLine($"Retry {i + 1}: Getting Exception : {ex.Message}");
                    await Task.Delay(WaitTime);
                }
            }
            await fun();
        }

        public static async Task<T> Retry<T>(Func<Task<T>> fun, int RetryTimes = 1, int WaitTime = 500)
        {
            for (int i = 0; i < RetryTimes - 1; i++)
            {
                try
                {
                    return await fun();
                }
                catch (Exception ex)
                {
                    if(ex is IBreakRetryException)
                    {
                        throw ex;
                    }

                    Console.WriteLine($"Retry {i + 1}: Getting Exception : {ex.Message}");
                    await Task.Delay(WaitTime);
                }
            }
            return await fun();
        }
    }
}
