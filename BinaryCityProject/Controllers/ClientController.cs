using BinaryCityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinaryCityProject.Controllers
{
    public class ClientController : Controller
    {
        private BinaryCity_DbContext _db;

        public ClientController(BinaryCity_DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Client> x = await _db.Client.ToListAsync();
            return View(x);
        }

        public IActionResult createClient(Client obj)
        {
            if (ModelState.IsValid)
            {
                _db.Client.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult ContactList(string id)
        {
            var obj = _db.Client.Where(c => c.Client_Key == id).FirstOrDefault()!;

            var ClientContacts = _db.Contact_Client_List.Where(c => c.Client_Key == id);

            int index = ClientContacts!.Count();

            Contact[]? contacts = new Contact[index];

            int i = 0;

            foreach (Contact_Client_List item in ClientContacts)
            {
                if (item.Client_Key == id )
                {
                    var temp = _db.Contact.Where(x => x.Contact_Key == item.Contact_Key).FirstOrDefault();
                    contacts[i] = temp; 
                }

                i++;
            }

            obj.Contacts = contacts;
            return View(obj);
        }

        public IActionResult delinkContact(string Client_Key, string Contact_Key)
        {
            var obj2 = _db.Contact_Client_List.Where(c => c.Client_Key == Client_Key && c.Contact_Key == Contact_Key).FirstOrDefault()!;
            _db.Contact_Client_List.Remove(obj2);
            _db.SaveChanges();

            string id = Client_Key;

            var obj = _db.Client.Where(c => c.Client_Key == id).FirstOrDefault()!;
            var ClientContacts = _db.Contact_Client_List.Where(c => c.Contact_Key == id);

            int index = ClientContacts!.Count();

            Contact[]? contacts = new Contact[index];

            int i = 0;

            foreach (Contact_Client_List item in ClientContacts)
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
   
    }
}