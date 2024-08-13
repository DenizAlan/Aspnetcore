using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
       private IPostRepository _postRepository;
       private ICommentRepository _commentRepository;
      
        public PostsController(IPostRepository postRepository , ICommentRepository commentRepository)
        {
            _postRepository=postRepository;
            _commentRepository=commentRepository;
            
        }
        public async Task<IActionResult> Index(string tag)
        {
            //tolist eklemediğimiz için veri tabanına gitmedi ders:100 
            var posts = _postRepository.Posts;
            
            if(!string.IsNullOrEmpty(tag))
            {
                posts=posts.Where(x=>x.Tags.Any(t=>t.Url==tag));
            }

            return View(
                new PostsViewModel 
                {
                    //tolist yazınca veri tabanına şimdi gitti ders:100
                    Posts=await posts.ToListAsync(),
                   
                }
            );
        }

        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                                .Posts
                                .Include(x=>x.Tags)
                                .Include(x=>x.Comments)
                                .ThenInclude(x=>x.User)
                                .FirstOrDefaultAsync(p=>p.Url==url) );
        }

        [HttpPost]
        public JsonResult AddComment(int PostId ,string UserName , string Text )
        {
            var entity = new Comment {
                Text=Text,
                PublishedOn=DateTime.Now,
                PostId=PostId,
                User= new User {UserName=UserName , Image="2.png"}
            };
           
            _commentRepository.CreateComment(entity);
            
           return Json(new {
            UserName,
            Text,
            entity.PublishedOn,
            entity.User.Image
           });
           

           
        }
    }
}