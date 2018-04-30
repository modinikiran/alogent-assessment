using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Assessment.Web;
using Assessment.Web.Models;
using Microsoft.Extensions.Configuration;

namespace Assessment
{
  public class Program
  {
    public static void Main(string[] args)
    {
      InitializeData();

      CreateWebHostBuilder(args).Build().Run();
    }

    private static void InitializeData()
    {

      var boards = new List<Board>
            {
                new Board{
                  BoardId = 1, CreatedAt = DateTime.Now.AddDays(-7), Name = "Board #1",
                    PostIts = new List<PostIt> {
                              new PostIt{PostId = 1, PostName = "Post #1", CreatedAt = DateTime.Now.AddDays(-7), PostInfo = "This is Post #1" },
                              new PostIt{PostId = 2, PostName = "Post #2", CreatedAt = DateTime.Now.AddDays(-7), PostInfo = "This is Post #2" },
                              new PostIt{PostId = 3, PostName = "Post #3", CreatedAt = DateTime.Now.AddDays(-7), PostInfo = "This is Post #3" } 
                              }
                         },
                new Board{
                  BoardId = 2, CreatedAt = DateTime.Now.AddDays(-6), Name = "Board #2",
                    PostIts = new List<PostIt> {
                              new PostIt{PostId = 4, PostName = "Post #4", CreatedAt = DateTime.Now.AddDays(-6), PostInfo = "This is Post #4" },
                              new PostIt{PostId = 5, PostName = "Post #5", CreatedAt = DateTime.Now.AddDays(-6), PostInfo = "This is Post #5" },
                              new PostIt{PostId = 6, PostName = "Post #6", CreatedAt = DateTime.Now.AddDays(-6), PostInfo = "This is Post #6" }
                              }
                         },
                new Board{
                  BoardId = 3, CreatedAt = DateTime.Now.AddDays(-5), Name = "Board #3",
                    PostIts = new List<PostIt> {
                              new PostIt{PostId = 7, PostName = "Post #7", CreatedAt = DateTime.Now.AddDays(-5), PostInfo = "This is Post #7" },
                              new PostIt{PostId = 8, PostName = "Post #8", CreatedAt = DateTime.Now.AddDays(-5), PostInfo = "This is Post #8" },
                              new PostIt{PostId = 9, PostName = "Post #9", CreatedAt = DateTime.Now.AddDays(-5), PostInfo = "This is Post #9" }
                              }
                         }
            };
      var filePath = Application.Configuration["BoardsDataFile"];

      if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

      if (File.Exists(filePath)) File.Delete(filePath);

      var boardsJson = JsonConvert.SerializeObject(boards);
      File.WriteAllText(filePath, boardsJson);
    }

  public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureAppConfiguration((context, configBuilder) =>
            {
              configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
              configBuilder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
            });
  }
}
