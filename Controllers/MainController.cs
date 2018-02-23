using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;

namespace BeltExam.Controllers
{
    public class MainController : Controller
    {
        private Context _context;

        public MainController(Context context)
        {
            _context = context;
        }

        private RegisterUser ActiveUser
        {
            get{ return _context.User.Where( u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();}
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Index()
        {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            RegisterUser thisUser = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            ViewBag.UserInfo = thisUser;

            List<Models.Activity> Activity = _context.Activity.Include(a => a.UserActivity).ToList();
            return View(Activity);
        }

        [HttpGet]
        [Route("newactivity")]
        public IActionResult newActivity() {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult createActivity(ActivityViewModel model)
        {
            if(ModelState.IsValid)
            {
                Models.Activity newActivity = new Models.Activity
                {
                    Title = model.Title,
                    Time = model.Time,
                    Date = model.Date,
                    Duration = model.Duration,
                    Description = model.Description,
                    CreatedBy = ActiveUser.UserId,
                };
                _context.Activity.Add(newActivity);
                _context.SaveChanges();
                Models.Activity activity = _context.Activity.Where(a => a.Title == model.Title).SingleOrDefault();
                return RedirectToAction("ShowActivity", new {ActivityId = activity.ActivityId});
            }
            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult showActivity(int ActivityId) {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");
            
            ViewBag.activity = _context.Activity.Where(a => a.ActivityId == ActivityId)
                                                    .Include(a => a.UserActivity)
                                                        .ThenInclude(u => u.User)
                                                            .SingleOrDefault();
            return View("ShowActivity");
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int ActivityId)
        {
            Models.Activity toDelete = _context.Activity.SingleOrDefault(d => d.ActivityId == ActivityId);
            _context.Activity.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } 

        [HttpPost]
        [Route("join")]
        public IActionResult Join(int ActivityId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            UserActivity newJoin = new UserActivity
            {
                UserId = ActiveUser.UserId,
                ActivityId = ActivityId
            };
            _context.UserActivity.Add(newJoin);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            List<Models.Activity> Activity = _context.Activity.ToList();
            return RedirectToAction("Index", Activity);
        }

        [HttpPost]
        [Route("leave")]
        public IActionResult Leave(int ActivityId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            UserActivity toDelete = _context.UserActivity.SingleOrDefault(r => r.ActivityId == ActivityId && r.UserId == ActiveUser.UserId);
            _context.UserActivity.Remove(toDelete);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            List<Models.Activity> Activity = _context.Activity.ToList();
            return RedirectToAction("Index", Activity);
        } 


        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }


    }
}