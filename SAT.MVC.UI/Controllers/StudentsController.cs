using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.MVC.EF;
using SAT.MVC.UI.Utilities;
namespace SAT.MVC.UI.Controllers
{
    [Authorize(Roles = "Admin,Scheduler")]
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        public object ImageUtility { get; private set; }

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string imageName = "noImage.jpg";

                if (studentImage != null)
                {
                    imageName = studentImage.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".jpg", ".png", ".jpeg", ".gif" };
                    if (goodExts.Contains(ext))
                    {
                        #region Resize Image

                        imageName = Guid.NewGuid() + ext;

                        //params for the Image Utility
                        //What we need:  Filepath, Image File, maximum image size (full size), maximum thumb size (thumbnail)

                        //filepath
                        string savePath = Server.MapPath("~/Content/img/studentPics/");

                        //image file
                        Image convertedImage = Image.FromStream(studentImage.InputStream);

                        //Max image size
                        int maxImageSize = 500; //value in pixels

                        //Max Thumb size
                        int maxThumbSize = 100;

                        //Call the ImageUtility to do work
                        Utilities.ImageUtility.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbSize);

                        #endregion

                    }
                    else
                    {
                        imageName = "noImage.jpg";
                    }
                }

                student.PhotoUrl = imageName;

                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {

                #region File Upload

                string imageName = "";

                if (studentImage != null)
                {
                    imageName = studentImage.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".jpg", ".png", ".jpeg", ".gif" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;

                        //file path
                        string savePath = Server.MapPath("~/Content/img/studentPics/");

                        //image file
                        Image convertedImage = Image.FromStream(studentImage.InputStream);

                        //Max image size
                        int maxImageSize = 500; //value in pixels

                        //Max Thumb size
                        int maxThumbSize = 100;

                        //Call the ImageUtility to do work
                        Utilities.ImageUtility.ResizeImage(savePath, imageName, convertedImage, maxImageSize, maxThumbSize);

                        if (student.PhotoUrl != "noImage.jpg" && student.PhotoUrl != null)
                        {
                            //delete old file
                            string path = Server.MapPath("~/Content/img/studentPics/");

                            //Call Delete Method with path and filename
                            Utilities.ImageUtility.Delete(path, student.PhotoUrl);
                        }
                    }

                    else
                    {
                        imageName = "noImage.jpg";
                    }

                    student.PhotoUrl = imageName;
                }

                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
