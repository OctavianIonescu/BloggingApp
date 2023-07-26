using BloggingApp.Web.Data;
using BloggingApp.Web.Models.Domain;
using BloggingApp.Web.Models.ViewModels;
using BloggingApp.Web.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BloggingApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller
    {
        private readonly ITagRepo tagRepo;
        public AdminTagController(ITagRepo tagRepo)
        {
            this.tagRepo = tagRepo;
        }
        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.name is not null && request.displayName is not null)
            {
                if (request.name == request.displayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest req)
        {
            //mapping addtagreq to tag domain model
            ValidateAddTagRequest(req);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var tag = new Tag
            {
                name = req.name,
                displayName = req.displayName
            };
            await tagRepo.AddAsync(tag);

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepo.GetAllAsync();
            return View(tags);
        }
        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid ID)
        {
            var tag = await tagRepo.GetAsync(ID);
            if (tag != null)
            {
                var req = new EditTagRequest
                {
                    ID = tag.ID,
                    Name = tag.name,
                    DisplayName = tag.displayName
                };
                return View(req);
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest req)
        {
            var tag = new Tag()
            {
                ID = req.ID,
                name = req.Name,
                displayName = req.DisplayName
            };

            var updatedTag = await tagRepo.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //Success
            }
            else
            {
                //fail
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest req)
        {
            var deletedTag = await tagRepo.DeleteAsync(req.ID);
            if (deletedTag != null)
            {
                //success
                return RedirectToAction("List");
            }
            else
            {
                //failure
                return RedirectToAction("Edit", new { id = req.ID });
            }
        }
    }
}
