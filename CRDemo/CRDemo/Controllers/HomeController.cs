using CRDemo.Models;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRDemo.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context = new MyContext();
        public ActionResult Index()
        {
            var supplierlist = _context.Suppliers.ToList();
            return View(supplierlist);
        }

        public ActionResult exportSupplier()
        {
            List<Supplier> allSuppliers = new List<Supplier>();
            allSuppliers = _context.Suppliers.ToList();


            ReportDocument repdoc = new ReportDocument();
            repdoc.Load(Path.Combine(Server.MapPath("~/Reports"), "CR_Supplier.rpt"));

            repdoc.SetDataSource(allSuppliers);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = repdoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }

        public ActionResult Reporting()
        {
            ReportDocument repdoc = new ReportDocument();
            repdoc.Load(Path.Combine(Server.MapPath("~/Reports"), "CR_Supplier.rpt"));
            repdoc.SetDataSource(_context.Suppliers.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = repdoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Supplier.pdf");
            }
            catch
            {
                throw;
            }
        }

    }
}