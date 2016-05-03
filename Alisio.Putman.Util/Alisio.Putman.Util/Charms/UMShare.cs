using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Alisio.Putman.UtilMethods.Charms
{
    public class UMShare
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public RandomAccessStreamReference Thumbnail { get; set; }

        public RandomAccessStreamReference Image { get; set; }

        public UMShareFiles Files { get; set; }

        public Uri Link { get; set; }

        public UMShare(string title, string description, string text, RandomAccessStreamReference thumbnail, RandomAccessStreamReference image, Uri link, UMShareFiles files)
        {
            this.Title = title;
            this.Description = description;
            this.Text = text;
            this.Thumbnail = thumbnail;
            this.Image = image;
            this.Link = link;
            this.Files = files;
        }
    }
}
