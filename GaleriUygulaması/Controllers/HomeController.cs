﻿using GaleriUygulaması.Models;
using GaleriUygulaması.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaleriUygulaması.Controllers
{
    public class HomeController : Controller
    {
        GaleriEntities context = new GaleriEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }

        public ActionResult DeleteFile(int id)
        {
            var entity = context.Dosya.Where(p => p.Id == id).SingleOrDefault();
            if (entity != null)
            {
                context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                //Response.Redirect("/Home");
            }
            return RedirectToAction("Galeri", "Home");
        }
        public ActionResult Upload()
        {
            if (Session["value"] != null)
            {
                Response.Redirect("/");
            }
            return View();
        }
        public FileContentResult fileView(int id)
        {
            var List = context.Dosya.Where(p => p.Id == id).ToList();
            return new FileContentResult(List[0].Deger, List[0].DosyaTipi);
        }

        public ActionResult fileupload()
        {
            HttpPostedFileBase file = HttpContext.Request.Files[0];
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                byte[] value = reader.ReadBytes((Int32)file.ContentLength);
                if (Session["value"] == null)
                {
                    Session["value"] = value;
                }
                else
                {
                    Session["value"] = UtilityManager.ByteBirlestir((byte[])Session["value"], value);
                }
                if (10000 > file.ContentLength)
                {
                    context.Dosya.Add(new Dosya
                    {
                        Deger = (byte[])Session["value"],
                        DosyaAdi = file.FileName,
                        DosyaBoyutu = ((byte[])Session["value"]).Length.ToString(),
                        DosyaTipi = file.ContentType,
                        Ikon = UtilityManager.SetIcon(file.ContentType),
                        BoyutKisaltma = UtilityManager.ByteToString(((byte[])Session["value"]).Length),
                        Renk = UtilityManager.SetClass(file.ContentType),
                        KayitTarihi = DateTime.Now,

                    });
                    context.SaveChanges();
                    Session["value"] = null;
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileDetailById(int id)
        {
            var file = (from d in context.Dosya
                        where d.Id == id
                        select new
                        {
                            d.BoyutKisaltma,
                            d.DosyaAdi,
                            d.DosyaTipi,
                            UrlYolu = "/home/fileview/" + d.Id,
                            d.Baslik,
                            d.Aciklama,
                        }).ToList();
            return Json(file[0], JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateFile(UpFile file)
        {
            try
            {
                var entity = context.Dosya.Where(p => p.Id == file.Id).SingleOrDefault();
                if (entity != null)
                {
                    entity.Baslik = file.Baslik;
                    entity.Aciklama = file.Aciklama;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return Json("E", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("H", JsonRequestBehavior.AllowGet);
            }

        }
    }
}