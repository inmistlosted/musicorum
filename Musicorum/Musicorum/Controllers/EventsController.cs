using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Services.Models;
using Musicorum.Web.Extensions;
using Musicorum.Web.Infrastructure.Filters;
using Musicorum.Web.Models.Events;

namespace Musicorum.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService eventService;

        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult AddEvent()
        {
            return PartialView("_CreateEvent");
        }

        [HttpPost]
        public IActionResult AddEvent(EventFormModel model)
        {
            this.eventService.Create(
                model.ImageUrl,
                model.Title,
                model.Location,
                model.Description,
                model.DateStarts,
                model.DateEnds,
                model.Photos,
                model.Videos);

            return RedirectToAction("Index", "Admin", new { categoryId = 4 });
        }

        [HttpGet]
        public IActionResult EditEvent(int eventId)
        {
            EventModel ev = this.eventService.EventById(eventId);

            EventFormModel model = new EventFormModel
            {
                EventId = ev.Id,
                ImageUrl = ev.ImageUrl,
                Title = ev.Title,
                Location = ev.Location,
                Description = ev.Description,
                DateEnds = ev.DateEnds,
                DateStarts = ev.DateStarts,
            };

            return PartialView("_EditEvent", model);
        }

        [HttpPost]
        public IActionResult EditEvent(EventFormModel model)
        {
            this.eventService.Edit(
                model.EventId,
                model.ImageUrl,
                model.Title,
                model.Location,
                model.Description,
                model.DateStarts,
                model.DateEnds,
                model.Photos,
                model.Videos);

            return RedirectToAction("Index", "Admin", new { categoryId = 4 });
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int eventId)
        {
            this.eventService.Delete(eventId);

            return Json(new { message = "Успішно видалено" });
        }

        [HttpGet]
        public IActionResult GetEvent(int eventId)
        {
            EventModel ev = this.eventService.EventById(eventId);

            return View("EventPage", ev);
        }

        public IActionResult Details(int id)
        {
            if (!this.eventService.Exists(id))
            {
                return NotFound();
            }

            return Ok();
        }

        public IActionResult JointEvent(int id)
        {
            if (!this.eventService.Exists(id))
            {
                return NotFound();
            }


            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}