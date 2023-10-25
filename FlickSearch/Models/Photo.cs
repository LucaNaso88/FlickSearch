using FlickSearch.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickSearch.Models
{
    public class Photo : BaseViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("farm")]
        public int Farm { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("ispublic")]
        public bool Ispublic { get; set; }

        [JsonProperty("isfriend")]
        public bool Isfriend { get; set; }

        [JsonProperty("isfamily")]
        public bool Isfamily { get; set; }

        public string ImageUrl
        {
            get => $"https://live.staticflickr.com/{Server}/{Id}_{Secret}_w.jpg";
        }

    }
}
