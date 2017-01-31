﻿using Business.Services;
using Data;
using Data.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class WishlistController : Controller
    {
        WishlistService wishlistService = new WishlistService();
        Repository<Theme> themeRepository = new Repository<Theme>();

        [HttpGet]
        public ActionResult MySets()
        {
            IEnumerable<Theme> themes = themeRepository.Query();
            SearchSetModel model = new SearchSetModel();
            model.AllThemes = themes;
            model.Action = "MySets";
            model.Controller = "Wishlist";

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var sets = wishlistService.GetAllSetsFromWishlistForUser(int.Parse(user.Id));
            ViewBag.sets = sets;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MySets(SearchSetModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Wishlist/MySets");
            }

            IEnumerable<Theme> themes = themeRepository.Query();
            model.AllThemes = themes;

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string searchParameters = SearchHelper.ConstructSearchParameters(model);
            var sets = wishlistService.Search(int.Parse(user.Id), searchParameters);
            ViewBag.sets = sets;

            return View(model);
        }

        public JsonResult AddAjax(string setId)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            WishlistService wishlistService = new WishlistService();
            wishlistService.AddSetToWishlistForUser(int.Parse(user.Id), int.Parse(setId), 1);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveAjax(string setId)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            WishlistService wishlistService = new WishlistService();
            wishlistService.RemoveSetFromWishlistForUser(int.Parse(user.Id), int.Parse(setId), 1);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}