using FlickSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickSearch.Models
{
    public class ImageDetail : BaseViewModel
    {

        public string TakenData { get; set; }

        public string Owner { get; set; }

        public string Location { get; set; }
    }
}
