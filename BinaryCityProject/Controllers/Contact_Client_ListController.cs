﻿using BinaryCityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

namespace BinaryCityProject.Controllers
{
    public class Contact_Client_ListController : Controller
    {
        private BinaryCity_DbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Contact_Client_ListController(BinaryCity_DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact_Client_List> x = await _db.Contact_Client_List.ToListAsync();
            return View(x);
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public SingleResult<Contact_Client_List> Get([FromODataUri] int key)
        {
            IQueryable<Contact_Client_List> result = _db.Contact_Client_List.Where(s => s.CC_Index == key);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Contact_Client_List update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.CC_Index)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.CC_Index)
            {
                return BadRequest();
            }

            Contact_Client_List b = _db.Contact_Client_List.FirstOrDefault(x => x.CC_Index == update.CC_Index)!;
            try
            {
                b = update;
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(key))
                { return NotFound(); }

                else
                { throw; }
            }

            catch (Exception)
            { throw; }

            return Ok();
        }

        public async Task<IActionResult> Post([FromBody] Contact_Client_List insert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Contact_Client_List.Add(insert);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            IQueryable<Contact_Client_List> result = _db.Contact_Client_List.Where(p => p.CC_Index == key);
            _db.Contact_Client_List.Remove(result.FirstOrDefault()!);
            await _db.SaveChangesAsync();
            return Ok();
        }

        bool Exists(int key)
        {
            return _db.Contact_Client_List.Find(key) != null;
        }
    }
}