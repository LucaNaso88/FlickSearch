using Acr.UserDialogs;
using FlickSearch.Models;
using FlickSearch.Services;
using FlickSearch.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FlickSearch.ViewModels
{
    public class MainpageViewModel : BaseViewModel
    {

        private bool _isListVisible;

        public bool IsListVisible
        {
            get => _isListVisible;
            set => SetProperty(ref _isListVisible, value);
        }

        private ObservableCollection<Photo> _images;
        
        public ObservableCollection<Photo> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private string _searchtext;

        public string SearchText
        {
            get => _searchtext;
            set => SetProperty(ref _searchtext, value);
        }

        private FlickrService _service;

        private int _pageNumber = 1;
        
        public Command LoadMoreImages { get; set; }

        public ICommand PerformSearch { get; set; }

        public ICommand OpenImageDetailCommand { get; set; }

        private bool isBusy = false;

        public MainpageViewModel()
        {
            _service = new FlickrService();
            LoadMoreImages = new Command(LoadMore);
            PerformSearch = new Command<string>(Search);
            OpenImageDetailCommand = new Command<Photo>(OpenImageDetail);
        }
        
        //search images
        private void Search(string text)
        {
            _pageNumber = 1;
            LoadPhotos(text);
        }

        //search for image details
        private async void OpenImageDetail(Photo img)
        {
            await Shell.Current.GoToAsync($"detailPage?imageId={img.Id}&imageURL={img.ImageUrl}&imageTitle={img.Title}");
        }

        private async void LoadMore()
        {
            if (isBusy)
                return;

            isBusy = true;
            UserDialogs.Instance.ShowLoading();
            try
            {
                _pageNumber++;
                List<Photo> temp = await _service.SearchPhotos(SearchText, _pageNumber);
                Images.AddRange(temp);
            }
            catch(Exception)
            {}
            UserDialogs.Instance.HideLoading();
            isBusy = false;
        }

        private async void LoadPhotos(string searchText)
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                Images = new ObservableCollection<Photo>(await _service.SearchPhotos(searchText, 1));
                if (Images.Count > 0)
                    IsListVisible = true;
                else
                    IsListVisible = false;
            }
            catch(Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Errore nel recupero delle foto", "OK");
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}
