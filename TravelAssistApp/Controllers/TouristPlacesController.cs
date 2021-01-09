using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelAssistApp.Models;
using TravelAssistApp.Service;

namespace TravelAssistApp.Controllers
{
    public class TouristPlacesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ITouristPlacesService _touristPlacesService;
        public TouristPlacesController(ITouristPlacesService touristPlacesService)
        {
            _touristPlacesService = touristPlacesService;
        }

        public ViewResult Index()
        {
            var result = _touristPlacesService.GetAllTouristPlaces();
            return View(result);
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TouristPlace touristPlace = _context.TouristPlaces.Find(id);
        //    if (touristPlace == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(touristPlace);
        //}

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TouristPlace touristPlace, HttpPostedFileBase imagePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(imagePath != null)
                    {
                        var extensionOfImage = Path.GetFileName(imagePath.FileName).Split('.');
                        var fileNameOriginal = Guid.NewGuid() + "." + extensionOfImage[1];
                        var filePathOriginal = Request.MapPath(Request.ApplicationPath) + @"/Content/Place_Images";
                        string savedOrgFileName = Path.Combine(filePathOriginal, fileNameOriginal);

                        touristPlace.ImagePath = "/Content/Place_Images/" + fileNameOriginal;

                        if (_touristPlacesService.AddTouristPlace(touristPlace))
                        {
                            imagePath.SaveAs(savedOrgFileName);
                        }
                        
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                    ex.Message.ToString();
                }
                
            }

            return View(touristPlace);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TouristPlace touristPlace = _touristPlacesService.GetTouristPlaceDetailsById(id.Value);
            if (touristPlace == null)
            {
                return HttpNotFound();
            }
            return View(touristPlace);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TouristPlace touristPlace, HttpPostedFileBase imagePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (imagePath != null)
                    {
                        var extensionOfImage = Path.GetFileName(imagePath.FileName).Split('.');
                        var fileNameOriginal = Guid.NewGuid().ToString() + "." + extensionOfImage[1];
                        var filePathOriginal = Request.MapPath(Request.ApplicationPath) + @"/Content/Place_Images";
                        string savedOrgFileName = Path.Combine(filePathOriginal, fileNameOriginal);

                        touristPlace.ImagePath = "/Content/Place_Images/" + fileNameOriginal;

                        if (_touristPlacesService.UpdateTouristPlace(touristPlace))
                        {
                            imagePath.SaveAs(savedOrgFileName);
                        }

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                    ex.Message.ToString();
                }

            }
            return View(touristPlace);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TouristPlace touristPlace = _touristPlacesService.GetTouristPlaceDetailsById(id.Value);
            if (touristPlace == null)
            {
                return HttpNotFound();
            }
            return View(touristPlace);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TouristPlace touristPlace = _touristPlacesService.GetTouristPlaceDetailsById(id);
            try
            {
                if (_touristPlacesService.DeleteTouristPlace(touristPlace))
                {
                    ViewBag.Message = "Successfully deleted";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ViewBag.Message = "Operation failed";
            }
            
            return RedirectToAction("Index");
        }
    }
}
