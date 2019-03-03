using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode2019
{
    public class Slide
    {
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public HashSet<string> Tags { get; set; }

        public Slide(Photo photo)
        {
            Photos.Add(photo);
            Tags = new HashSet<string>(photo.Tags);
        }

        public Slide(List<Photo> photos)
        {
            foreach(var photo in photos)
            {
                Photos.Add(photo);
            }

            List<string> _initTags = new List<string>();
            foreach(var photo in photos)
            {
                foreach (var tg in photo.Tags)
                {
                    _initTags.Add(tg);
                }
            }
            Tags = new HashSet<string>(_initTags);

        }
    }
}
