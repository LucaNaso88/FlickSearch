using FlickSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlickSearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageDetail : ContentPage, IQueryAttributable
    {
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string imageID = HttpUtility.UrlDecode(query["imageId"]);
            string imageURL = HttpUtility.UrlDecode(query["imageURL"]);
            string imageTitle = HttpUtility.UrlDecode(query["imageTitle"]);
            LoadDetail(imageID, imageURL, imageTitle);
        }

        public ImageDetail()
        {
            InitializeComponent();
        }

        private void LoadDetail(string imageId, string imageUrl, string imageTitle)
        {
            BindingContext = new ImageDetailViewModel(imageId, imageUrl, imageTitle);
        }
    }
}