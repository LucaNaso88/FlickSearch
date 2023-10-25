using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FlickSearch.Models
{
    public static class ListUtils
    {
        public static void AddRange(this ObservableCollection<Photo> oc, IEnumerable<Photo> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("empty collection");
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }
        }
    }
}
