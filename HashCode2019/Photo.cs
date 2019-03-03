using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode2019
{
    public class Photo /*: IComparable
*/    {
        public int Index { get; set; }
        public char Orientation { get; set; }
        public int TagCount { get; set; }
        public List<string> Tags { get; set; }
        public bool IsUse { get; set; }

        public Photo()
        {
            Index = 0; //min
            Orientation = 'N';  //neytral
            TagCount = 0; //min
            Tags = new List<string>();
            IsUse = false;
        }

        //public int CompareTo(object obj)
        //{
        //    if (this.TagCount > ((Photo)obj).TagCount)
        //        return -1;
        //    else if (this.TagCount <((Photo)obj).TagCount)
        //        return 1;
        //    else return 0;
        //}
    }
}
