using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotDoge.Entities;
using HotDoge.Business.Interfaces;

namespace HotDoge.Controllers
{
    /// <summary>
    /// This code was scaffolded using the pre-existing entity TestEntity as a Model (tutorial here : http://www.windowsazure.com/en-us/develop/net/tutorials/web-site-with-sql-database/)
    /// It would be better to have a TestEntityModel and perform mappings between the entity and the model as needed
    /// </summary>
    [Authorize(Roles = "canAccessEntityController")]
    public class TestEntityController : Controller
    {
        private readonly ITestEntityService _testEntityService;

        public TestEntityController(ITestEntityService testEntityService)            : base()
        {
            if (testEntityService == null) throw new ArgumentNullException("testEntityService");
            _testEntityService = testEntityService;
        }

        // GET: /TestEntity/
        public ActionResult Index()
        {
            return View(_testEntityService.GetAll());
        }

        // GET: /TestEntity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEntity testentity = _testEntityService.GetById(id);
            if (testentity == null)
            {
                return HttpNotFound();
            }
            return View(testentity);
        }

        // GET: /TestEntity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TestEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,LastName,YetOtherProp")] TestEntity testentity)
        {
            if (ModelState.IsValid)
            {
                _testEntityService.Create(testentity);
                return RedirectToAction("Index");
            }

            return View(testentity);
        }

        // GET: /TestEntity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEntity testentity = _testEntityService.GetById(id);
            if (testentity == null)
            {
                return HttpNotFound();
            }
            return View(testentity);
        }

        // POST: /TestEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,LastName,YetOtherProp")] TestEntity testentity)
        {
            if (ModelState.IsValid)
            {
                _testEntityService.Update(testentity);
                return RedirectToAction("Index");
            }
            return View(testentity);
        }

        // GET: /TestEntity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEntity testentity = _testEntityService.GetById(id);
            if (testentity == null)
            {
                return HttpNotFound();
            }
            return View(testentity);
        }

        // POST: /TestEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestEntity testentity = _testEntityService.GetById(id);
            _testEntityService.Delete(testentity);
            return RedirectToAction("Index");
        }        
    }
}
