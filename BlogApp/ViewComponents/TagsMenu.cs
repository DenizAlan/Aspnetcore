using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private ITagRepository _tagRepository ;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository=tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync ()
        {
            return View( await _tagRepository.Tags.ToListAsync());
        }
    }
}