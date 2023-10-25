using FlickSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlickSearch.Services
{
    public class FlickrService
    {
        private string _apiKey = "9e7ac66cd8d697b9d3959fb66400e29f";
        private string _format = "json";
        private bool _nojsoncallback = true;
        private int _elementPerPage = 15;
        
        public FlickrService()
        {
        }
        
        //search photos
        public async Task<List<Photo>> SearchPhotos(string searchText, int pageNumber)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri($"https://api.flickr.com/services/rest/?method=flickr.photos.search&format={_format}&api_key={_apiKey}&text={searchText}&nojsoncallback={_nojsoncallback}&per_page={_elementPerPage}&page={pageNumber}");
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(content);
                    JObject photos = obj.Value<JObject>("photos");
                    JArray photo = (JArray)photos["photo"];
                    return photo.ToObject<List<Photo>>();
                }
                else
                    return new List<Photo>();
            }
        }

        //search photo details
        public async Task<ImageDetail> LoadImageDetails(string imageID)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri($"https://api.flickr.com/services/rest/?method=flickr.photos.getInfo&format={_format}&api_key={_apiKey}&photo_id={imageID}&nojsoncallback={_nojsoncallback}");
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(content);
                    JObject photo = obj.Value<JObject>("photo");
                    //get date
                    JObject dates = photo.Value<JObject>("dates");
                    string taken_data = dates.Value<string>("taken");
                    //get username
                    JObject owner = photo.Value<JObject>("owner");
                    string username = owner.Value<string>("username");
                    //get location
                    string location = owner.Value<string>("location");

                    return new ImageDetail() { Owner = username, TakenData = taken_data, Location = location };
                }
                else
                    return new ImageDetail();
            }
        }
    }
}
