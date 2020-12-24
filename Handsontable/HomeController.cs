using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;
using System.Text;

// Install-Package LumenWorksCsvReader
namespace MVCImportExportCSV.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream, encoding: System.Text.Encoding.UTF8), true))
                        {
                            csvTable.Load(csvReader);
                        }

                        string JsonString = string.Empty;
                        JsonString = JsonConvert.SerializeObject(csvTable);
                        

                        ViewBag.JsonString = JsonString;

                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public  ActionResult UploadConfirm()
        {   

            return View();
        }
    }
}

