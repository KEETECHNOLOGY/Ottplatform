using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ottplatform.Models;
using ottplatform.Service;


namespace ottplatform.Controllers
{
    public class OTTwebsiteController : Controller
    {

        public AppDbContext _Context;
        public IWebHostEnvironment _environment;
        public EmailSender _emailSender;


        public OTTwebsiteController(AppDbContext context, IWebHostEnvironment environment,EmailSender emailSender)
        {
            _Context = context;
            _environment = environment;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

      public IActionResult about()
        {
            return View();
        }

        public IActionResult Loginform()
        {
            return View();
        }

        

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Information s, string email)
        {
            var data=_Context.Information.FirstOrDefault(p => p.email == email);
            if(data != null)
            {
                TempData["message"] = "All ready register";
                return RedirectToAction("Register");

            }
            _Context.Information.Add(s);
            _Context.SaveChanges();
            return RedirectToAction("Register");
        }

        [HttpPost]

        public IActionResult Loginform(string email,string password)
        {
            var data=_Context.Information.FirstOrDefault(s => s.email == email && s.password == password);
            if (data != null)
            {
              HttpContext.Session.SetString("user",email);
                return RedirectToAction("Index");
            }

            else
            {
                TempData["msg"] = "Email or password is incorrect";
                return RedirectToAction("Loginform");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Contact (Information2 s)
        {
            _Context.Information2.Add(s);
            _Context.SaveChanges();
            return RedirectToAction("Contact");
        }


        public IActionResult Movies()
        {
            var table_1 = _Context.Admin.ToList();
            var table_2= _Context.SecondAdmin.ToList();
            var viewModel = new AdminSecondAdminViewModel
            {
                Admin = table_1,
                SecondAdmin = table_2
            };
            return View(viewModel);

        }
        public IActionResult Series()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> upload(Admin p,IFormFile photo,IFormFile photo1, IFormFile photo2, IFormFile photo3, IFormFile photo4)
        {
            string uploadfolder = Path.Combine(_environment.WebRootPath, "upload images");
            string filename=photo.FileName;
            string filename1 = photo1.FileName;
            string filename2 = photo2.FileName;
            string filename3 = photo3.FileName;
            string filename4 = photo4.FileName;

            string filepath =Path.Combine(uploadfolder,filename);
            var filestream=new FileStream(filepath, FileMode.Create);
            string filepath1 = Path.Combine(uploadfolder, filename1);
            var filestream1 = new FileStream(filepath1, FileMode.Create);
            string filepath2 = Path.Combine(uploadfolder, filename2);
            var filestream2 = new FileStream(filepath2, FileMode.Create);
            string filepath3 = Path.Combine(uploadfolder, filename3);
            var filestream3 = new FileStream(filepath3, FileMode.Create);
            string filepath4 = Path.Combine(uploadfolder, filename4);
            var filestream4 = new FileStream(filepath4, FileMode.Create);
            await photo.CopyToAsync(filestream);
            await photo1.CopyToAsync(filestream1);
            await photo2.CopyToAsync(filestream2);
            await photo3.CopyToAsync(filestream3);
            await photo4.CopyToAsync(filestream4);

            p.photo=filename;
            p.photo1 = filename1;
            p.photo2 = filename2;
            p.photo3 = filename3;
            p.photo4 = filename4;


            _Context.Admin.Add(p);
            _Context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Show(int id)
        {
            var data=_Context.Admin.Find(id);
            return View(data);
           
        }
        public IActionResult Show2(int id)
        {
            var data = _Context.SecondAdmin.Find(id);
            return View(data);

        }

        public IActionResult Upload2()
        {
            return View();
        }

        [HttpPost]
       public async Task <IActionResult> upload2(SecondAdmin u, IFormFile photo, IFormFile photo1, IFormFile photo2, IFormFile photo3, IFormFile photo4)
        {
            string uploadfolder = Path.Combine(_environment.WebRootPath, "upload images");
            string filename = photo.FileName;
            string filename1 = photo1.FileName;
            string filename2 = photo2.FileName;
            string filename3 = photo3.FileName;
            string filename4 = photo4.FileName;

            string filepath = Path.Combine(uploadfolder, filename);
            var filestream = new FileStream(filepath, FileMode.Create);
            string filepath1 = Path.Combine(uploadfolder, filename1);
            var filestream1 = new FileStream(filepath1, FileMode.Create);
            string filepath2 = Path.Combine(uploadfolder, filename2);
            var filestream2 = new FileStream(filepath2, FileMode.Create);
            string filepath3 = Path.Combine(uploadfolder, filename3);
            var filestream3 = new FileStream(filepath3, FileMode.Create);
            string filepath4 = Path.Combine(uploadfolder, filename4);
            var filestream4 = new FileStream(filepath4, FileMode.Create);
            await photo.CopyToAsync(filestream);
            await photo1.CopyToAsync(filestream1);
            await photo2.CopyToAsync(filestream2);
            await photo3.CopyToAsync(filestream3);
            await photo4.CopyToAsync(filestream4);

            u.photo = filename;
            u.photo1 = filename1;
            u.photo2 = filename2;
            u.photo3 = filename3;
            u.photo4 = filename4;


            _Context.SecondAdmin.Add(u);
            _Context.SaveChanges();
            return RedirectToAction("index");

        }

       
        public IActionResult forgetpassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendotp(string email)
        {
            var data = _Context.Information.FirstOrDefault(x => x.email == email);
            if (data != null)
            {
                Random rnd = new Random();
                int num = rnd.Next(1000, 9999);

                HttpContext.Session.SetString("otp", num.ToString());
                HttpContext.Session.SetString("email", email);
                string sendto=email;
                string subject = "otp for Reset Passward";
                string mail = "Dear " + data.name + ",<br><br>" +
               "We hope this message finds you well.<br><br>" +
               "You recently requested to reset your password for your account with us. To ensure the security of your account, we have generated a One-Time Password (OTP) that you will need to proceed with the reset.<br><br>" +
               "Your OTP is: " + num + "<br><br>" +
               "Please enter this code in the password reset form within the next 10 minutes, as it will expire after that time for security reasons. If you do not complete the process within this timeframe, you will need to request a new OTP.<br><br>" +
               "If you did not initiate this request, please disregard this email. Your account remains secure, and no changes have been made.<br><br>" +
               "For any questions or concerns, or if you require further assistance, please don’t hesitate to reach out to our support team at [support email/contact number]. We are here to help!<br><br>" +
               "Thank you for your attention to this matter.<br><br>" +
               "Best regards,\n[Your Company Name] Support Team<br><br>" +
               "[Your Company Website]";

                await _emailSender.SendEmailAsync(sendto, subject, mail);

                return RedirectToAction("Resetpassword");
            }
            else
            {
                TempData["msg"] = "This email is not registered with us";
                return RedirectToAction("forgetpassword");
            }
        }


        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ResetPassword( string otp,string newpass,string compass)
        {
            string orginalotp = HttpContext.Session.GetString("otp");
            if (otp == orginalotp)
            {
                if (newpass == compass)
                {
                    string email= HttpContext.Session.GetString("email");
                    var data=_Context.Information.FirstOrDefault(x=>x.email == email);
                    data.password= newpass;
                    _Context.Information.Update(data);
                    _Context.SaveChanges();
                   HttpContext.Session.Clear();
                    return RedirectToAction("Loginform");
                }
                else
                {
                    TempData["msg"] = "Confirm Password not Matched";
                    return RedirectToAction("Resetpassword");
                }
            }
            else
            {
                TempData["msg"] = "You Entered Incoorect OTP";
                return RedirectToAction("Resetpassword");
            }


        }

    }
}
