using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JodhpurPalace.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace JodhpurPalace.Controllers
{
    public class HomeController : Controller
    {
       

        static IDbConnection db = new SqlConnection("Data Source=DESKTOP-MUFI4MA;Initial Catalog=Master;Integrated Security=True;MultipleActiveResultSets=True");

        
        
        
        public IActionResult MyList()
        {
            
            

            
            String query = "select * from PalaceRecords";
            var std = db.Query<PalaceRecord>(query, null).ToList();
            //HttpContext.Session.SetString("sk",std.ToString());
            return View(std);
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user1)
        {
            if (ModelState.IsValid)
            {
                var query = "Select*from AdminTable where Username =@Username and Password = @Password ";
                var log = db.Query<User>(query, user1).FirstOrDefault();
                if (log != null)
                {
                    return RedirectToAction("MyList", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inavlid Username and Password");
                    return View(user1);
                }

            }
            return View(user1);
        }


            public IActionResult Edit(int id)
        {
           



            string query = "Select *from PalaceRecords where Id =@Id";
            var cc = db.Query<PalaceRecord>(query, new {Id = id }).FirstOrDefault();
            return View(cc);
            
        }
        [HttpPost]
        public IActionResult Edit(PalaceRecord req)
        {
            




            string query = "update  palaceRecords set FirstName = @FirstName,LastName=@LastName,RoomNo=@RoomNo,PhoneNo=@PhoneNo,CheckIn =@CheckIn,  CheckOut = @CheckOut, Amount = @AMount, No_Of_Rooms = @No_Of_Rooms, Total_Person =@Total_Person ,RoomName = @RoomName where Id =@Id";
            db.Execute(query, req);
            return RedirectToAction("MyList");
            
        }
        public IActionResult RCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RCreate(PalaceRecord pr)
        {
            ViewBag.Id = HttpContext.Session.GetInt32("ss");
            string query = "insert into PalaceRecords(GuestId,FirstName,LastName,RoomNo,PhoneNo,CheckIn, CheckOut,Amount ,No_Of_Rooms ,Total_Person,RoomName) values(@GuestId,@FirstName,@LastName, @RoomNo,@PhoneNO,@CheckIn,@CheckOut,@Amount,@No_Of_Rooms,@Total_Person,@RoomName )";
            //string query3 = "update PalaceRecords Set PalaceRecors.GuestId = GuestCart.GuestId, where PalaceRecord.RoomName = GuestCart.RoomName";
            //db.Execute(query3, new { RoomName = pr });
           
            db.Execute(query, new { GuestId = ViewBag.Id, pr.FirstName, pr.LastName, pr.RoomNo, pr.PhoneNo, pr.CheckIn, pr.CheckOut, pr.Amount, pr.No_Of_Rooms, pr.Total_Person, pr.RoomName });
            return RedirectToAction("MyList");
        }
           
        public IActionResult Delete(int id)
        {

            string query = "Delete from PalaceRecords where Id = @Id";
            var data = db.Query<PalaceRecord>(query, new { Id = id }).FirstOrDefault();
            return RedirectToAction("MyList");

        }

        public IActionResult bookingListCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult bookingListCreate(Hotel qr,IFormFile FormFile)
        {
            string fileName = Path.GetFileName(FormFile.FileName);
            string Uploadpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",fileName);
            var filestream = new FileStream(Uploadpath, FileMode.Create);
            FormFile.CopyToAsync(filestream);
            string uploadedDBpath = "images\\" + fileName;
            qr.Images = uploadedDBpath;
            
            
            string query = "insert into BookingSearch(RoomName,RoomType,Price,Available,Images ) values(@RoomName,@RoomType, @Price,@Available,@Images)";
            db.Execute(query, qr);
            
            return RedirectToAction("MyHotels1");

        }

        public IActionResult GuestDetails()
        {
            String query = "select * from GuestCalander";
            var det = db.Query<GuestCalender>(query, null).ToList();
            return View(det);
            
        }






        public IActionResult MyHotels1()
        {
          



            String query = "select * from BookingSearch";
            var std1 = db.Query<Hotel>(query, null).ToList();
            return View(std1);


        }
        public IActionResult bookingList(int id)
        {
            string query = "Select *from BookingSearch where RoomId =@RoomId";
            var cc1 = db.Query<Hotel>(query, new { RoomId = id }).FirstOrDefault();
            return View(cc1);

        }
        [HttpPost]
        public IActionResult bookingList(Hotel req1, IFormFile FormFile)
        {
            string fileName = Path.GetFileName(FormFile.FileName);
            string Uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
            var filestream = new FileStream(Uploadpath, FileMode.Create);
            FormFile.CopyToAsync(filestream);
            string uploadedDBpath = "images\\" + fileName;

            req1.Images = uploadedDBpath;
            string query = "update  BookingSearch set RoomName = @RoomName,RoomType=@RoomType,Price=@price,Available=@Available,Images =@Images where RoomId =@RoomId";
            db.Execute(query, req1);
            return RedirectToAction("MyHotels1");
            
        }

        public IActionResult DeletebookingList(int id)
        {

            string query = "Delete from BookingSearch where RoomId = @RoomId";
            var data = db.Query<PalaceRecord>(query, new { RoomId = id }).FirstOrDefault();
            return RedirectToAction("MyHotels1");

        }
        







        public IActionResult ACreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ACreate(AdminTable user)
        {
            if (ModelState.IsValid)
            {
                if(user.Confirm_Password == user.Password)
                {
                    string query = "insert into Admintable (Username,Password,Confirm_Password) values(@Username,@Password,@Confirm_Password)";
                    string query2 = "Select*from Admintable where Username =@Username";
                    var log = db.Query<AdminTable>(query2, user).FirstOrDefault();
                    if(log!=null && log.Username == user.Username)
                    {
                        ModelState.AddModelError(string.Empty, "Username Already Used ... Please try Another");
                    }
                    else
                    {
                        db.Execute(query, user);
                        return RedirectToAction("Create", "Home");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password Not Mathched");
                    return RedirectToAction("Create", "Home");
                }

            }
            return View(user);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
