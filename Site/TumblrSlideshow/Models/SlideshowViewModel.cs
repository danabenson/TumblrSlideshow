using System.Collections.Generic;

namespace TumblrSlideshow.Models
{
    public class SlideshowViewModel
    {
        public string BlogName { get; set; }

        public long PostsCount { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }
    }
}