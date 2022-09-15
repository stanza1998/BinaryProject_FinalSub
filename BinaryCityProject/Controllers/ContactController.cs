using BinaryCityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

namespace BinaryCityProject.Controllers
{
    public class ContactController : Controller
    {
        private BinaryCity_DbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactController(BinaryCity_DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> x = await _db.Contact.ToListAsync();
            return View(x);
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public SingleResult<Contact> Get([FromODataUri] int key)
        {
            IQueryable<Contact> result = _db.Contact.Where(s => s.Contact_Index == key);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Contact update)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.Contact_Index)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.Contact_Index)
            {
                return BadRequest();
            }

            Contact b = _db.Contact.FirstOrDefault(x => x.Contact_Index == update.Contact_Index)!;
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

        public IActionResult createContact()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createContact(Contact obj)
        {
            if (ModelState.IsValid)
            {
                _db.Contact.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }



        public IActionResult ClientList(string id)
        {
            var obj = _db.Contact.Where(c => c.Contact_Key == id).FirstOrDefault()!;

            var ContactClientsts = _db.Client_Contact_List.Where(c => c.Contact_Key == id);

            int index = ContactClientsts!.Count();

            Client[]? clients = new Client[index];

            int i = 0;

            foreach (Client_Contact_List item in ContactClientsts)
            {
                if (item.Contact_Key == id)
                {
                    var temp = _db.Client.Where(x => x.Client_Key == item.Client_Key).FirstOrDefault();
                    clients[i] = temp;
                }

                i++;
            }

            obj.Clients = clients;
            return View(obj);
        }

        public IActionResult delinkClient(string Client_Key, string Contact_Key)
        {
            var obj2 = _db.Client_Contact_List.Where(c => c.Client_Key == Client_Key && c.Contact_Key == Contact_Key).FirstOrDefault()!;
            _db.Client_Contact_List.Remove(obj2);
            _db.SaveChanges();

            string id = Client_Key;

            var obj = _db.Client.Where(c => c.Client_Key == id).FirstOrDefault()!;
            var ClientContacts = _db.Client_Contact_List.Where(c => c.Contact_Key == id);

            int index = ClientContacts!.Count();

            Contact[]? contacts = new Contact[index];

            int i = 0;

            foreach (Client_Contact_List item in ClientContacts)
            {
                if (item.Contact_Key == id)
                {
                    var temp = _db.Contact.Where(x => x.Contact_Key == item.Client_Key).FirstOrDefault();
                    contacts[i] = temp;
                }

                i++;
            }

            obj.Contacts = contacts;
            return RedirectToAction("Index");

        }

         

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            IQueryable<Contact> result = _db.Contact.Where(p => p.Contact_Index == key);
            _db.Contact.Remove(result.FirstOrDefault()!);
            await _db.SaveChangesAsync();
            return Ok();
        }
        bool Exists(int key)
        {
            return _db.Contact.Find(key) != null;
        }


    }
}