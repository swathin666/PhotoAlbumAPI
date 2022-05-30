using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotoAlbumAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoAlbumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase
    {
        public static string albumsUrl = "https://jsonplaceholder.typicode.com/albums";
        public static string photosUrl = "https://jsonplaceholder.typicode.com/photos";
       [HttpGet]
        public async Task<ActionResult> FetchPhotosAlbumsAsync()
        {
            var albumInfo= new List<AlbumInfo>();
            var photoInfo = new List<PhotoInfo>();
            var newlstalbumInfo = new List<AlbumInfo>();
           
            using (var client = new HttpClient())
            {
                try
                {
                    var photoresponse = await client.GetAsync(photosUrl);
                    var albumresponse = await client.GetAsync(albumsUrl);
                    photoresponse.EnsureSuccessStatusCode();
                    albumresponse.EnsureSuccessStatusCode();
                    var stringphotoResult = await photoresponse.Content.ReadAsStringAsync();
                    var stringalbumResult = await albumresponse.Content.ReadAsStringAsync();
                    photoInfo = JsonConvert.DeserializeObject<List<PhotoInfo>>(stringphotoResult);
                     albumInfo= JsonConvert.DeserializeObject<List<AlbumInfo>>(stringalbumResult);

                   foreach(AlbumInfo album in albumInfo)
                    {
                        foreach(PhotoInfo photo in  photoInfo)
                        {
                            if(photo.albumId==album.Id)
                            {
                                var newalbumnfo = new AlbumInfo();
                                newalbumnfo = album;
                                newlstalbumInfo.Add(newalbumnfo);
                            }
                        }
                    }

                    return Ok(newlstalbumInfo);
                  
                   

                  
                }
                catch (System.Net.Http.HttpRequestException httpRequestException)
                {
                      return BadRequest($"Error: {httpRequestException.Message}");
                }
           
            }
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<ActionResult> GetAlbumInfo(int userId)
        {
            var newlstalbumInfo = new List<AlbumInfo>();
           
            using (var client = new HttpClient())
            {
                try
                {
                    var albumresponse = await client.GetAsync(albumsUrl);
                    albumresponse.EnsureSuccessStatusCode();
                    var stringalbumResult = await albumresponse.Content.ReadAsStringAsync();
                    var albumInfo = JsonConvert.DeserializeObject<List<AlbumInfo>>(stringalbumResult);

                    foreach (AlbumInfo album in albumInfo)
                    {
                        if (userId == album.userId)
                        {
                            var newalbum = new AlbumInfo();
                            newalbum = album;
                            newlstalbumInfo.Add(newalbum);
                        }
                    }
                    return Ok(newlstalbumInfo);
                }
                catch (System.Net.Http.HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error: {httpRequestException.Message}");
                }

            }
        }
        
    }
}
