using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Client2.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Client2.Utils;
using Microsoft.Extensions.Configuration;

namespace Client2.Services
{

    public static class PackageService
    {
        public static IEnumerable<Package> GetPackages(int? page, int? size, PackageContext context, ILogger logger, MyAppData appState, IConfiguration configuration)
        {

            /* SYNC PACKAGES FROM KTAPI EVERY 24 hours */
            var hours = appState.PackageRetrievedAt != null ?
                        ((TimeSpan)(DateTime.Now - appState.PackageRetrievedAt)).TotalHours :
                        0;
            if (!appState.PackageRetrieved || hours > 24)
            {
                appState.PackageRetrieved = true;
                appState.PackageRetrievedAt = DateTime.Now;

                var packagesJArray = KtapiUtils.GetPackagesFromKTAPI(configuration["KTAPI:Key"], configuration["KTAPI:Secret"]).Result;
                foreach (Newtonsoft.Json.Linq.JObject pObject in packagesJArray)
                {
                    var package = context.Package.FirstOrDefault(t => t.SfdcId == (String)pObject["id"]);
                    if (package == null)
                    {
                        package = new Package();
                        package.SfdcId = (String)pObject["id"];
                        context.Package.Add(package);
                    }
                    package.Name = (String)pObject["name"];
                    package.Data = pObject.ToString();
                }

                context.SaveChanges();
            }
            /* END OF SYNC LOGIC */

            if (size != null && page != null)
            {

                Console.WriteLine("DD: "+size+" : "+page);

                return context.Package
                                    .OrderBy(x => x.Id)
                                    .Skip((int)page * (int)size)
                                    .Take((int)size)
                                    .Select(item => new Package
                                        {
                                            Id = item.Id,
                                            SfdcId = item.SfdcId,
                                            Name = item.Name,
                                            Data = item.Data,
                                        })
                                    .ToList();
            }
            else
            {
                return context.Package.ToList();
            }
        }

        public static Package GetPackageWithDetails(string packageId, PackageContext context, IConfiguration configuration)
        {

            Console.WriteLine("PackageId: "+packageId);

            var package = context.Package.FirstOrDefault(t => t.SfdcId == packageId);

            if (package == null)
            {
                return null;
            }

            var lastUpdatedInHours = package.UpdatedAt != null ?
                        ((TimeSpan)(DateTime.Now - package.UpdatedAt)).TotalHours :
                        0;

            if (package.Details == null || package.UpdatedAt == null || lastUpdatedInHours > 24)
            {
                var packageJObject = KtapiUtils.GetPackageDetailsFromKTAPI(
                                                                        packageId,
                                                                        configuration["KTAPI:Key"],
                                                                        configuration["KTAPI:Secret"]).Result;

                Console.WriteLine(packageJObject);
                package.Details = packageJObject.ToString();
                package.UpdatedAt = DateTime.Now;

                context.SaveChanges();

            }

            return package;
        }

    }
}

