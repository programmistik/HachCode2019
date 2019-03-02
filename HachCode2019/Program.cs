using HachCode2019;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HachCode2019Slides
{
    class Program
    {

        public static string PathToFile { get; set; }

        static void Main(string[] args)
        {
            string thisAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //string file = System.IO.Path.Combine(thisAppPath, "a_example.txt");
            //string file = System.IO.Path.Combine(thisAppPath, "b_lovely_landscapes.txt");
            //string file = System.IO.Path.Combine(thisAppPath, "c_memorable_moments.txt");
            //string file = System.IO.Path.Combine(thisAppPath, "d_pet_pictures.txt");
            string file = System.IO.Path.Combine(thisAppPath, "e_shiny_selfies.txt");
            PathToFile = file;
            //string file = System.IO.Path.Combine(thisAppPath, "e_shiny_selfies.txt");
            string[] textLines;
            string FirstLine;

            List<Photo> PhotoList = new List<Photo>();

            List<Photo> Out = new List<Photo>(); 

            if (File.Exists(file))
            {
                textLines = File.ReadAllLines(file, Encoding.ASCII);
                FirstLine = textLines[0];

                Regex regex;
                MatchCollection matches;

                Photo photo;
                regex = new Regex(@"[A-Za-z1-9]+");
                for (int i = 1; i < textLines.Length; i++)
                {
                    matches = regex.Matches(textLines[i]);
                    photo = new Photo();
                    photo.Index = i - 1;
                    photo.Orientation = char.Parse(matches[0].ToString());
                    photo.TagCount = int.Parse(matches[1].ToString());

                    for (int t = 2; t < matches.Count; t++)
                    {
                        photo.Tags.Add(matches[t].ToString());
                    }

                    PhotoList.Add(photo);
                }


                List<Photo> HorizontalPhotos = PhotoList.Where(p => p.Orientation == 'H').ToList().OrderBy(p => p.TagCount).ToList();
                List<Photo> VerticalPhotos = PhotoList.Except(HorizontalPhotos).ToList();

                var InitialSlides = new List<Slide>();
                
                foreach(var item in HorizontalPhotos)
                {
                    InitialSlides.Add(new Slide(item));
                }

                InitialSlides.AddRange(GetVerticalSlides(VerticalPhotos));

                //var RealSlideShow = new List<Slide>();

                //RealSlideShow = GetFinalRealSlides(InitialSlides, RealSlideShow);

                var RealSlideShow = GetFinalRealSlidesV2(InitialSlides);

                //Slide currentSlide = InitialSlides[0];
                //RealSlideShow.Add(currentSlide);

                //while(InitialSlides.Count() > 0)
                //{
                //    //Slide currentSlide = InitialSlides[i];
                //    // Slide mostSuitableSlide = InitialSlides.Where(s => s.Tags.Intersect(currentSlide.Tags).Count() > 0 && s.Tags.Except(currentSlide.Tags).Count() > 0).OrderBy(s => s.Tags.Count()).FirstOrDefault();
                //    Slide mostSuitableSlide = InitialSlides.FirstOrDefault(s => s.Tags.Intersect(currentSlide.Tags).Count() > 0 && s.Tags.Except(currentSlide.Tags).Count() > 0);
                //    if (mostSuitableSlide != null)
                //    {
                //        RealSlideShow.Add(mostSuitableSlide);
                //    }
                //    else
                //    {
                //        break;
                //    }

                //    InitialSlides.Remove(currentSlide);
                //    InitialSlides.Remove(mostSuitableSlide);

                //    currentSlide = mostSuitableSlide;

                //}
                //while (HorizontalPhotos.Count > 0)
                //{
                //    //string xtag = slide.Photos[0].Tags[0];
                //    //var xSlide = HorizontalPhotos.Where(x => x.Tags.Where(z => z));
                //    var photosdf = HorizontalPhotos.Where(p => p.Tags.Intersect(slide.Tags).Count() > 0 && p.Tags.Except(slide.Tags).Count() > 0);
                //}

                //SortedList<string, Photo> sortedList = new SortedList<string, Photo>();

                GenerateOutputFile(RealSlideShow);
                //foreach (var item in RealSlideShow)
                //{
                //    string ss = string.Empty;
                //    foreach (var tags in item.Tags)
                //    {
                //        ss += tags + " ";
                //    }

                //    Console.WriteLine(item.Photos[0].Orientation + " " + ss);
                //}
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }


        public static List<Slide> GetFinalRealSlides(List<Slide> InitialSlides, List<Slide> RealSlideShow)
        {
            Slide currentSlide = InitialSlides[0];
            RealSlideShow.Add(currentSlide);

            while (InitialSlides.Count() > 0)
            {
                //Slide currentSlide = InitialSlides[i];
                // Slide mostSuitableSlide = InitialSlides.Where(s => s.Tags.Intersect(currentSlide.Tags).Count() > 0 && s.Tags.Except(currentSlide.Tags).Count() > 0).OrderBy(s => s.Tags.Count()).FirstOrDefault();
                Slide mostSuitableSlide = InitialSlides.FirstOrDefault(s => s.Tags.Intersect(currentSlide.Tags).Count() > 0 && s.Tags.Except(currentSlide.Tags).Count() > 0);
                if (mostSuitableSlide != null)
                {
                    RealSlideShow.Add(mostSuitableSlide);
                }
                else
                {
                    break;
                }

                InitialSlides.Remove(currentSlide);
                InitialSlides.Remove(mostSuitableSlide);

                currentSlide = mostSuitableSlide;

            }

            return RealSlideShow;
        }

        public static List<Slide> GetVerticalSlides(List<Photo> verticalPhotos)
        {
            List<Slide> verticalSlides = new List<Slide>();
            //List<Photo> currentVerticalPhotos = verticalPhotos.OrderBy(p => p.Tags.Count()).ToList();
            //var VerticalPhotosSlides = new List<Slide>();
            //var currentPhoto = verticalPhotos[0];
            //while(currentVerticalPhotos.Count() > 0)
            //{
            //    var mostSuitablePhoto = currentVerticalPhotos.Last(p => p.Tags.Intersect(currentPhoto.Tags).Count() == 0);
            //}

            for(int i = 0; i < verticalPhotos.Count()/2; i++)
            {
                Slide slide = new Slide(new List<Photo> { verticalPhotos[i], verticalPhotos[verticalPhotos.Count() - i - 1] });
                verticalSlides.Add(slide);
            }
            return verticalSlides;
        }


        public static void GenerateOutputFile(List<Slide> slides)
        {
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(PathToFile) + "\\" + Path.GetFileNameWithoutExtension(PathToFile) + ".out"))
            {
                sw.WriteLine(slides.Count);
                foreach (Slide slide in slides)
                {
                    string ss = string.Empty;
                    foreach(var photo in slide.Photos)
                    {
                        ss += photo.Index.ToString() + " ";
                    }
                    sw.WriteLine(ss);
                }
            }
        }

        public static int GetScore(Slide one, Slide second)
        {
           
            var Unic = one.Tags.Intersect(second.Tags).Count();
            var DiffOne = one.Tags.Except(second.Tags).Count();
            var DiffSecond = second.Tags.Except(one.Tags).Count();

            var sum = Math.Min(Unic, Math.Min(DiffOne, DiffSecond));

            return sum;
        }

        public static List<Slide> GetFinalRealSlidesV2(List<Slide> InitialSlides)
        {
            var SlidesList = new LinkedList<Slide>(InitialSlides);
            var RealSlideShow = new List<Slide>(SlidesList.Take(1).ToList());
            SlidesList.RemoveFirst();
            while (SlidesList.Any())
            {
                var prev = RealSlideShow.Last();
                LinkedListNode<Slide> mostSuitableSlide = SlidesList.First;
                LinkedListNode<Slide> SlideNode = SlidesList.First;
                var bestScore = GetScore(prev, mostSuitableSlide.Value);
                int counter = 0;
                while (SlideNode != null)
                {
                    if (counter++ > 200) break;
                    if (SlideNode.Value.Tags.Count / 2 <= bestScore) break;
                    var nextNode = SlideNode.Next;
                    var score = GetScore(prev, SlideNode.Value);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        mostSuitableSlide = SlideNode;
                    }
                    SlideNode = nextNode;
                }
                RealSlideShow.Add(mostSuitableSlide.Value);
                SlidesList.Remove(mostSuitableSlide);
            }
            return RealSlideShow;
        }
    }
}
