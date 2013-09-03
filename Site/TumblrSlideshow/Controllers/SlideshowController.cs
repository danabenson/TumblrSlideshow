using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using TumblrSlideshow.Models;

namespace TumblrSlideshow.Controllers
{
    public class SlideshowController : Controller
    {
        [HttpPost]
        public ActionResult Build(string blogName)
        {
            var model = new SlideshowViewModel();
            string consumerKey = ConfigurationManager.AppSettings["Tumblr.ConsumerKey"];
            string consumerSecret = ConfigurationManager.AppSettings["Tumblr.SecretKey"];
            //instantiate the client factory
            var factory = new TumblrClientFactory();
            //create the client; TumblrClient implements IDisposable (to dispose
            //of the internal HttpClient)
            using (var client = factory.Create<TumblrClient>(consumerKey, consumerSecret, null))
            {
                //gets the info for a blog; we can either pass the short blog name or
                //the fully qualified blog name (<blog name>.tumblr.com)
                try
                {
                    Task<BlogInfo> task = Task.Run(() => client.GetBlogInfoAsync(blogName));
                    task.Wait();
                    model.BlogName = task.Result.Name;
                    model.PostsCount = task.Result.PostsCount;
                    GetNextPagePrivate(model, 0, task.Result.Name);
                }
                catch (Exception)
                {
                    TempData.Add("Error", "There was an error retrieving " + blogName + ".");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private void GetNextPagePrivate(SlideshowViewModel model, int offset, string name)
        {
            string consumerKey = ConfigurationManager.AppSettings["Tumblr.ConsumerKey"];
            string consumerSecret = ConfigurationManager.AppSettings["Tumblr.SecretKey"];
            //instantiate the client factory
            var factory = new TumblrClientFactory();
            //create the client; TumblrClient implements IDisposable (to dispose
            //of the internal HttpClient)
            using (var client = factory.Create<TumblrClient>(consumerKey, consumerSecret, null))
            {
                Task<Posts> taskPosts = Task.Run(() => client.GetPostsAsync(name, offset, 20, PostType.Photo));
                taskPosts.Wait();
                var pics = new List<PictureViewModel>();
                foreach (BasePost post in taskPosts.Result.Result)
                {
                    var picture = new PictureViewModel();
                    var pp = post as PhotoPost;
                    picture.Url = pp.Photo.OriginalSize.ImageUrl;
                    picture.ThumbnailUrl =
                        pp.Photo.AlternateSizes.First(x => x.Width == pp.Photo.AlternateSizes.Min(y => y.Width))
                          .ImageUrl;
                    pics.Add(picture);
                }
                model.Pictures = pics;
            }
        }

        [HttpPost]
        public SlideshowViewModel GetNextPage(int offset, string name)
        {
            var model = new SlideshowViewModel();
            GetNextPagePrivate(model, offset, name);
            return model;
        }
    }
}