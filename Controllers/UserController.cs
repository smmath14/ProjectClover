using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JodhpurPalace.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace JodhpurPalace.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserController : Controller
    {
        
        




        static IDbConnection db = new SqlConnection("Data Source=DESKTOP-MUFI4MA;Initial Catalog=Master;Integrated Security=True;MultipleActiveResultSets=True");


        




        public IActionResult GCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GCreate(GuestCalender Gc)
        {

            ViewBag.Id = HttpContext.Session.GetInt32("ss");
        
            
            var query5 = "Insert into GuestCalander (GuestId,CheckIn,CheckOut,Guest,Rooms) values(@GuestId,@CheckIn,@CheckOut,@Guest,@Rooms)";
            db.Execute(query5,new { GuestId = ViewBag.Id,Gc.CheckIn,Gc.CheckOut,Gc.Guest,Gc.Rooms});
            
            string query6 = "update GuestCalander set No_of_Days = DATEDIFF(day,CheckIn,CheckOut)";
            db.Execute(query6, Gc);
            return RedirectToAction("MyHotels","User");


        }
        //public IActionResult GList(int id)
        //{
        //    var query2 = "insert into GuestCalander2 Select Ut.GuestId ,Gc.CheckIn,Gc.CheckOut,Gc.Guest,Gc.Rooms, No_of_Days = 0 from (Select GuestId from UserTable where GuestId = @GuestId) as Ut, (Select CheckIn, CheckOut, Rooms, Guest from GuestCalander ) as Gc";
        //    db.Execute(query2, new { GuestId =id });
        //    return RedirectToAction("MyHotels", "User");
        //}



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var query = "Select*from UserTable where Username =@Username and Password = @Password ";
                var log = db.Query<User>(query, user).FirstOrDefault();
                if (log != null)
                {
                    HttpContext.Session.SetInt32("ss", log.GuestId); 
                    return RedirectToAction("GCreate", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inavlid Username and Password");
                    return View(user);
                }

            }
            
            return View(user);
        }
        public IActionResult MyHotels()
        {

           




            String query = "select * from BookingSearch";
            var std = db.Query<Hotel>(query, null).ToList();
            return View(std);

            
        }
        public IActionResult NewUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewUser(NewUser nu)
        {
            if (ModelState.IsValid)
            {
                if (nu.Confirm_Password == nu.Password)
                {
                    string query = "Insert into UserTable (Username,Password,Confirm_Password) values(@Username,@Password,@Confirm_Password)";

                    string query2 = "Select* from UserTable where Username =@Username";
                    var log = db.Query<User>(query2,nu).FirstOrDefault();
                    if (log != null && log.Username == nu.Username)
                    {
                        ModelState.AddModelError(string.Empty, "Username Already Used.. Please Try Another ");
                    }
                    else
                    {

                        db.Execute(query, nu);
                    return RedirectToAction("Create", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password Not Matched");
                    return View("NewUser");
                }
                
            }

            return View(nu);
        }










        //else
        //{
        //    ModelState.AddModelError(string.Empty, "Password Not Matched");
        //    return View("NewUser");
        //}




        public IActionResult GuestCart(int id)
        {
            //ViewBag.User = HttpContext.Session.GetString("sk");
            ViewBag.Id =  HttpContext.Session.GetInt32("ss");
            //string query4 = "insert into GuestCart1(GuestId,Rooms,No_of_Days) select GuestId,Rooms,No_of_Days from GuestCalander ";
            //db.Execute(query4, new { GuestId = id });
            string query5 = "insert into GuestCart(RoomId,RoomName,RoomType,Price,GuestId) select RoomId,RoomName,RoomType,Price, @GuestId from BookingSearch where RoomId =@RoomId";
            db.Execute(query5, new {RoomId =id,GuestId = ViewBag.Id });
            //string query6 = "Select *from GuestCart1,GuestCart2";
            //db.Execute(query6, new { GuestId = id });
            //string query = " insert into GuestCart (RoomId,RoomName,RoomType,Price) select RoomId,RoomName,RoomType,Price from BookingSearch where RoomId = @RoomId ";
            //db.Execute(query, new { RoomId = id });
            string query3 = "update GuestCart Set GuestCart.Rooms = GuestCalander.Rooms, GuestCart.No_of_Days = GuestCalander.No_of_days  from GuestCart,GuestCalander where GuestCart.GuestId = GuestCalander.GuestId";
            db.Execute(query3, new { GuestId = id });
            string query2 = "Update GuestCart set Total = Price*Rooms*No_of_Days";
            db.Execute(query2, new { GuestId = id });
            //ViewBag.Total = yourCollection.Sum(x => x.ThePropertyYouWantToSum);
            //string query4 = "select gc.GuestId,gc.RoomId,gc.RoomName,gc.RoomType,gc.Price,gm.Rooms,gm.No_of_Days,gc.Total from GuestCart as gc left join GuestCalander as gm on gc.GuestId = gm.GuestId";
            //db.Execute(query4, new { GuestId = id });
            //string query1 = "insert into GuestCart Select Gc.GuestId,Bs.RoomId,Bs.RoomName,Bs.RoomType,Bs.Price,Gc.Rooms,Gc.No_of_Days,Total = 0 from (Select GuestId,Rooms,No_of_Days from GuestCalander where GuestId = @GuestId) as Gc,(Select RoomId,RoomName,RoomType,Price from BookingSearch Where RoomId = @RoomId) as Bs";
            //db.Execute(query1, new { RoomId = id, GuestId = id });
            //string query2 = "Update GuestCart set Total = Price*Rooms*No_of_Days";
            //db.Execute(query2, new { GuestId = id });





            return RedirectToAction("ShowCart");
            
        }
        
        public IActionResult ShowCart(int id)
        {


            HttpContext.Session.GetInt32("ss");
            string query8 = "Select*from GuestCart";
            var std = db.Query<GuestCart>(query8, new { GuestId = id }).ToList();
            return View(std);

        }
        






        //public IActionResult GuestCart(int id)
        //{
        //    string query3 = "Insert Guestcart (No_of_Days) select No_of_days from GuestCalander where Guestid =@GuestId";
        //    db.Execute(query3, new { GuestId = id });
        //}

        //}
        
        //public IActionResult GuestCart(int id)
        //{
        //    string query = "Select *from BookingSearch where RoomId = @RoomId";
        //    var cc = db.Query<GuestCart>(query, new { RoomId = id }).FirstOrDefault();
        //    return View(cc);
        //}
        

        public IActionResult GuestListEdit(int id)
        {
            string query = "Select *from GuestCart where GuestId =@GuestId";
            var cc = db.Query<GuestCart>(query, new { GuestId = id }).FirstOrDefault();
            return View(cc);
        }
        [HttpPost]
        public IActionResult GuestListEdit(GuestCart gc)
        {
            string query = "update  GuestCart set Rooms = @Rooms ,No_of_Days = @No_of_Days  where GuestId =@GuestId";
            string query2 = "update GuestCart set Total = Price*No_of_Days*Rooms where GuestId = @GuestId";
            db.Execute(query, gc);
            db.Execute(query2, gc);
            return RedirectToAction("ShowCart");


        }

        public IActionResult DeleteList(int id)
        {

            string query = "Delete from GuestCart where GuestId = @GuestId";
            var data = db.Query<GuestCart>(query, new { GuestId = id }).FirstOrDefault();
            return RedirectToAction("GuestCart");

        }

        public IActionResult Summary()
        {
            return View();
        }
        public IActionResult Invoice()
        {
            
            
            return View();
            

        }
        [HttpPost]
        public IActionResult Invoice(Invoice ig)
        {
            string query1 = "insert into Invoice (GuestId,DateOfBooking,RoomName,RoomType,Amount) Select GuestId, GetDate(),RoomName,RoomType,Total from GuestCart";
            db.Execute(query1, ig);


            return RedirectToAction("Invoice");
        }
        
        










    }
}