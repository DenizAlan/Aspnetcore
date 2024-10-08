using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
       private IPostRepository _postRepository;
       private ICommentRepository _commentRepository;

       private ITagRepository _tagRepository;
      
        public PostsController(IPostRepository postRepository , ICommentRepository commentRepository , ITagRepository tagRepository)
        {
            _postRepository=postRepository;
            _commentRepository=commentRepository;
            _tagRepository= tagRepository;
            
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims=User.Claims;
            //tolist eklemediğimiz için veri tabanına gitmedi ders:100 
            var posts = _postRepository.Posts.Where(i=>i.IsActive);
            
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
                                .Include(x=>x.User)
                                .Include(x=>x.Tags)
                                .Include(x=>x.Comments)
                                .ThenInclude(x=>x.User)
                                .FirstOrDefaultAsync(p=>p.Url==url) );
        }

        [HttpPost]
        public JsonResult AddComment(int PostId , string Text )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username= User.FindFirstValue(ClaimTypes.Name);
            var avatar=User.FindFirstValue(ClaimTypes.UserData);
            var entity = new Comment {
                PostId=PostId,
                Text=Text,
                PublishedOn=DateTime.Now,
                UserId=int.Parse(userId ?? "")
            };
           
            _commentRepository.CreateComment(entity);
            
           return Json(new {
            username,
            Text,
            entity.PublishedOn,
            avatar
           });
            
        }

        [Authorize]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
         [Authorize]
        public IActionResult Create (PostCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _postRepository.CreatePost(
                    new Post {
                        Title=model.Title,
                        Content=model.Content,
                        Url=model.Url,
                        UserId=int.Parse(userId ?? ""),
                        PublishedOn=DateTime.Now,
                        Image="2png",
                        IsActive=false
                    }
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List ()
        {
            var userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)?? "");
            var role=User.FindFirstValue(ClaimTypes.Role);

            var posts=_postRepository.Posts;  //veri tabanına gitmedik şuan ders:114

            //kullanıcının bir rolu yoksa
            if(string.IsNullOrEmpty(role))
            {
                //Kullanıcının kendine ait postları listelenir
                posts=posts.Where(i=>i.UserId==userId);
            }

            //kullanıcının bir rolu varsa tüm postlar listelenir
            return View(await posts.ToListAsync()); //Veri tabanına gittik
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var post=_postRepository.Posts.Include(i=>i.Tags).FirstOrDefault(i=>i.PostId==id);
            if(post==null)
            {
                return NotFound();
            }

            ViewBag.Tags=_tagRepository.Tags.ToList();

            return View( new PostEditViewModel {
                PostId=post.PostId,
                Title=post.Title,
                Description=post.Description,
                Content=post.Content,
                Url=post.Url,
                IsActive=post.IsActive,
                Tags=post.Tags
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(PostEditViewModel model , int[] tagIds)
        {
            if(ModelState.IsValid)
            {
                var entityToUpdate= new Post {
                    PostId=model.PostId,
                    Title=model.Title,
                    Description=model.Description,
                    Content=model.Content,
                    Url=model.Url
                };

                if(User.FindFirstValue(ClaimTypes.Role)== "admin")
                {
                    entityToUpdate.IsActive=model.IsActive;
                }

                _postRepository.EditPost(entityToUpdate , tagIds);
                return RedirectToAction("List");
            }
            
            ViewBag.Tags=_tagRepository.Tags.ToList();
            return View(model);
        }
    }
}