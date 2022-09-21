using BinaryCityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using System.Collections.Generic;

namespace BinaryCityProject.Controllers
{
    public class Client_Contact_ListController : Controller
    {
        private BinaryCity_DbContext _db;

        public Client_Contact_ListController(BinaryCity_DbContext context)
        {
            _db = context;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Client_Contact_List> x = await _db.Client_Contact_List.ToListAsync();
            return View(x);
        }


        [EnableQuery(MaxExpansionDepth = 4)]
        public Client_Contact_List Get(string Client_key)
        {
            Client client = _db.Client.Where(c => c.Client_Key == Client_key).FirstOrDefault()!;

            Contact[] contacts = _db.Contact.ToArray();

            Client_Contact_List result = new Client_Contact_List();
            
            result.Client_Key = Client_key;
            result.Client = client;
            result.contacts = contacts;

            return result;
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Client_Contact_List update)
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

            Client_Contact_List b = _db.Client_Contact_List.FirstOrDefault(x => x.CC_Index == update.CC_Index)!;
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

        public async Task<IActionResult> Post([FromBody] Client_Contact_List insert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Client_Contact_List.Add(insert);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            IQueryable<Client_Contact_List> result = _db.Client_Contact_List.Where(p => p.CC_Index == key);
            _db.Client_Contact_List.Remove(result.FirstOrDefault()!);
            await _db.SaveChangesAsync();
            return Ok();
        }

        bool Exists(int key)
        {
            return _db.Client_Contact_List.Find(key) != null;
        }



        public IActionResult ContactLink(string id)
        { 
           var obj = Get(id);
            return View(obj);
        }
        public IActionResult LinkContact(string Contact_Key, string Client_Key)
        {
            Client_Contact_List insert = new Client_Contact_List();

            insert.Contact_Key  = Contact_Key ;
            insert.Client_Key = Client_Key; 

            var res = _db.Add(insert);
            _db.SaveChanges();

            //return RedirectToAction("Index");

            return RedirectToAction("Index", "Client", new { area = "" });

        }

    }
}