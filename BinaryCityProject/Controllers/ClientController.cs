using BinaryCityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;

namespace BinaryCityProject.Controllers
{
    public class ClientController : Controller
    {
        private static BinaryCity_DbContext _db;
        public string keyCode;

        public ClientController(BinaryCity_DbContext context)
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
                obj.Client_Key = generateUserCode(obj);
                getLastIndex();
                obj.Client_Key += keyCode;

                _db.Client.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult ContactList(string id)
        {
            var obj = _db.Client.Where(c => c.Client_Key == id).FirstOrDefault()!;

            var ClientContacts = _db.Client_Contact_List.Where(c => c.Client_Key == id);

            int index = ClientContacts!.Count();

            Contact[]? contacts = new Contact[index];

            int i = 0;

            foreach (Client_Contact_List item in ClientContacts)
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

        //Check 
        public string generateUserCode(Client client)
        {
            string code = "Error";
            int CodeSize = getWordCount(client.Client_Name!);
            string Cname = client.Client_Name!;

            switch (CodeSize)
            {
                case 0:
                    //No Name insterted, return error massage..
                    return code.ToUpper();

                case 1:
                    //Step one, Remove any white Space
                    Cname = Cname.Replace(" ", string.Empty);

                    //Get First three Characters
                    var alphaNum1 = Cname.Substring(0, 1);
                    var alphaNum2 = getRandomChar();
                    var alphaNum3 = getRandomChar();

                    //Add characters plus 3 Digit Code
                    var alphaNum = alphaNum1 + alphaNum2 + alphaNum3;

                    code = alphaNum;

                    return code.ToUpper();

                case 2:

                    //Split Names into Arr
                    string[] Names = Cname.Split(' ');

                    var code2 = "";

                    foreach (var name in Names)
                    {
                        var alphaNum11 = name.Substring(0, 1);
                        code2 += alphaNum11;
                    }
                    code2 += getRandomChar();

                    return code2.ToUpper();

                //Three or more Names
                default:

                    var code3 = "";

                    if (CodeSize >= 3)
                    {
                        string[] Names2 = Cname.Split(' ');

                        foreach (var name in Names2)
                        {
                            var alphaNum11 = name.Substring(0, 1);
                            code3 += alphaNum11;
                        }
                    }

                    return code3.ToUpper();
            }
        }

        //Get Word Count
        public int getWordCount(string text)
        {
            int wordCount = 0, index = 0;
            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;
                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;

            }
            return wordCount;
        }
        public char getRandomChar()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return chars[new Random().Next(0, 25)];
        }

        public  void getLastIndex()
        {
            var context =  _db.Client.Select(c => c).OrderBy(c => c.Client_Index).LastOrDefault();
            //var context =  _db.Client.FromSqlRaw("");

            //var con = context.LastOrDefault();

            int? x = context!.Client_Index + 1;
            //int? x = 2;

            switch (x)
            {
                case < 10: keyCode = "00" + x;
                    break;

                case < 99: keyCode = "0" + x;
                    break;

                case > 99: keyCode = "" +x;
                    break;

                default: keyCode = "";
                    break;
            } 
        }
    }
}