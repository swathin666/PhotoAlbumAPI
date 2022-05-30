using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PhotoAlbumAPI.Controllers;
using PhotoAlbumAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbumAPITests
{
    [TestFixture]
    public class PhotoAlbumAPITests
    {

        [Test]
        public  async System.Threading.Tasks.Task FetchPhotosAlbumsAsync()
        {
            var controller = new PhotoAlbumController();
            var actionResult = await controller.FetchPhotosAlbumsAsync();
            var objectResult = actionResult as OkObjectResult;
            var data = objectResult.Value as List<AlbumInfo>;
            var albumInfo = new AlbumInfo();
            foreach (AlbumInfo album in data)
            {
                albumInfo.Id = album.Id;
              
            }
            Assert.AreEqual(100, albumInfo.Id);

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);

        }
        [Test]
        public async Task GetAlbumInfo()
        {
            var controller = new PhotoAlbumController();
            var actionResult = await controller.GetAlbumInfo(1);
            var objectResult = actionResult as OkObjectResult;
          
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);

        }
    }
}