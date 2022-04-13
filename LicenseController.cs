using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LicenseApiWrapper
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseController : ControllerBase
    {
        [HttpGet(Name = "GetLicense")]
        public string? Get(string key, int expire_year, int expire_month, int expire_day, int check_spoof)
        {
            string fileName = "LicenseGen.exe";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            string args = key + " " + expire_year.ToString() + " " + expire_month.ToString() + " " + 
                          expire_day.ToString() + " " + check_spoof.ToString();

            ProcessStartInfo processStartInfo = new()
            {
                FileName = filePath,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };

            using Process process = new()
            {
                StartInfo = processStartInfo
            };

            process.Start();

            string? processOutput = "";

            while (!process.StandardOutput.EndOfStream)
            {
                processOutput = process.StandardOutput.ReadLine();
            }

            return processOutput;
        }
    }
}
