using Acr.UserDialogs;
using FlickSearch.Models;
using FlickSearch.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FlickSearch.ViewModels
{
    public class ImageDetailViewModel : BaseViewModel
    {
        private FlickrService _flickrService;

        //image url
        private string _imageUrl;

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        //image title
        private string _imageTitle;
        public string ImageTitle
        {
            get => _imageTitle;
            set => SetProperty(ref _imageTitle, value);
        }

        //image details
        private ImageDetail _imageDetail;

        public ImageDetail ImageDetail
        {
            get => _imageDetail;
            set => SetProperty(ref _imageDetail, value);
        }
        
        public ImageDetailViewModel(string imageID, string imageUrl, string imageTitle)
        {
            _flickrService = new FlickrService();
            LoadImageDetails(imageID, imageUrl, imageTitle);
        }

        private async void LoadImageDetails(string imageID, string imageUrl, string imageTitle)
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                ImageDetail = await _flickrService.LoadImageDetails(imageID);
            }
            catch(Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Errore nel recupero dei dettagli della foto", "OK");
            }
            ImageTitle = imageTitle;
            imageUrl = imageUrl.Replace("_w.jpg", "_b.jpg");
            ImageUrl = imageUrl;
            UserDialogs.Instance.HideLoading();
        }
    }
}
