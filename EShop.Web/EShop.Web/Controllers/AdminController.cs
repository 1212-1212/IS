using EShop.Domain.DTO;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ClosedXML.Excel;
using ExcelDataReader;
using EShop.Domain.Models;
using System.Drawing;
using Stripe.Terminal;
using System.Web.WebPages;

namespace EShop.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICarPartService _carPartService;
        private readonly ICarPartBrandService _carPartBrandService;
        private readonly ICarPartStageService _carPartStageService;
        private readonly ICarPartTypeService _carPartTypeService;
        public AdminController(IUserService userService, ICarPartService carPartService, ICarPartBrandService carPartBrandService, ICarPartStageService carPartStageService, ICarPartTypeService carPartTypeService)
        {
            _userService = userService;
            _carPartService = carPartService;
            _carPartBrandService = carPartBrandService;
            _carPartStageService = carPartStageService;
            _carPartTypeService = carPartTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ImportCarParts(IFormFile file)
        {


            var path = $"E:\\ISFiles\\{file.FileName}";



            using (FileStream fileStream = System.IO.File.Create(path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<CarPart> carParts = readCarPartsFromFile(file.FileName);

            foreach (var carPart in carParts)
            {
                Console.WriteLine(carPart);
                _carPartService.Create(new CarPart
                {
                    Id = new Guid(),
                    CarPartBrandId = carPart.CarPartBrandId,
                    CarPartStageId = carPart.CarPartStageId,
                    CarPartTypeId = carPart.CarPartTypeId,
                    Description = carPart.Description,
                    Price = carPart.Price
                });
            }
            Console.WriteLine(_userService.GetAll().Count());
            return RedirectToAction(nameof(Index));
        }
        private List<CarPart> readCarPartsFromFile(string path)
        {
            List<CarPart> carParts = new List<CarPart>();
            string filePath = $"E:\\ISFiles\\{path}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        var carPartStage = _carPartStageService.GetByStage(reader.GetValue(0).ToString());
                        var carPartType =  _carPartTypeService.GetByType(reader.GetValue(1).ToString());
                        var carPartBrand = _carPartBrandService.getByCountryAndManufacturer(reader.GetValue(2).ToString(), reader.GetValue(3).ToString());
                        if(carPartStage == null)
                        {
                            var Id = new Guid();
                            _carPartStageService.Create(new CarPartStage
                            {
                                Stage = reader.GetValue(0).ToString(),
                                Id = Id,
                            });
                            carPartStage = _carPartStageService.Get(Id);
                        }
                        if (carPartType == null)
                        {
                            var Id = new Guid();
                            _carPartTypeService.Create(new CarPartType
                            {
                                Type = reader.GetValue(1).ToString(),
                             
                            });
                            carPartType = _carPartTypeService.Get(Id);
                        }
                        if (carPartBrand == null)
                        {
                            var Id = new Guid();
                            _carPartBrandService.Create(new CarPartBrand
                            {
                                Country = reader.GetValue(3).ToString(),
                                BrandName = reader.GetValue(2).ToString(),
                
                            });
                            carPartBrand = _carPartBrandService.Get(Id);
                        }
                        carParts.Add(new CarPart
                        {
                            Id = new Guid(),
                            Description = reader.GetValue(4).ToString(),
                            Price = reader.GetValue(5).ToString().AsFloat(),
                            CarPartBrandId = carPartBrand.Id,
                            CarPartStageId = carPartStage.Id,
                            CarPartTypeId = carPartType.Id,

                        });
                    }
                }
            }

            return carParts;
        }

        [HttpPost]
        public IActionResult ImportUsers(IFormFile file)
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var path = $"E:\\ISFiles\\{file.FileName}";

           

            using (FileStream fileStream = System.IO.File.Create(path))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<UserRegistrationDTO> users = readUsersFromFile(file.FileName);

            foreach (var user in users)
            {
                _userService.Create(new Client
                {
                   Name = user.Name,
                   Surname = user.Surname,
                   Email = user.Email,
                   NormalizedEmail = user.Email,
                   EmailConfirmed = true,
                   PhoneNumber = user.PhoneNumber,
                   PhoneNumberConfirmed = true,
                   UserName = user.Email,
                   NormalizedUserName= user.Email,
                   ShoppingCart = new ShoppingCart()
                });
            }
            Console.WriteLine(_userService.GetAll().Count());
            return RedirectToAction(nameof(Index));

        }
        private List<UserRegistrationDTO> readUsersFromFile(string path)
        {
            List<UserRegistrationDTO> users = new List<UserRegistrationDTO>();
            string filePath = $"E:\\ISFiles\\{path}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        
                        users.Add(new UserRegistrationDTO
                        {
                            Name = reader.GetValue(0).ToString(),
                            Surname = reader.GetValue(1).ToString(),
                            Email = reader.GetValue(2).ToString(),
                            Password = reader.GetValue(3).ToString(),
                            ConfirmPassword = reader.GetValue(4).ToString(),
                            PhoneNumber = reader.GetValue(5).ToString(),

                        });
                    }
                }
            }
            Console.WriteLine(_userService.GetAll().Count());
            return users;
        }
        public FileContentResult ExportUsers()
        {
            string fileName = "ExportedUsers.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var wb = new XLWorkbook())
            {
                IXLWorksheet worksheet = wb.Worksheets.Add("ExportedUsers");

                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Surname";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "PhoneNumber";

                var clients = _userService.GetAll().ToList();

                for (int i = 1; i <= clients.Count(); i++)
                {
                    var item = clients[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Name.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Surname.ToString();
                    worksheet.Cell(i + 1, 3).Value = item.Email.ToString();
                    worksheet.Cell(i + 1, 4).Value = item.PhoneNumber.ToString();
              

                }
                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
            }
            }
          

        }
        public FileContentResult ExportCarParts()
        {
            string fileName = "ExportedCarParts.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var wb = new XLWorkbook())
            {
                IXLWorksheet worksheet = wb.Worksheets.Add("ExportedCarParts");

                worksheet.Cell(1, 1).Value = "CarPartId";
                worksheet.Cell(1, 2).Value = "CarPartTypeId";
                worksheet.Cell(1, 3).Value = "CarPartBrandId";
                worksheet.Cell(1, 4).Value = "CarPartStageId";
                worksheet.Cell(1, 5).Value = "CarPartDescription";
                worksheet.Cell(1, 6).Value = "CarPartPrice";

              

                var carParts = _carPartService.GetAll().ToList();

                for (int i = 1; i <= carParts.Count(); i++)
                {
                    var item = carParts[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.CarPartTypeId.ToString();
                    worksheet.Cell(i + 1, 3).Value = item.CarPartBrandId.ToString();
                    worksheet.Cell(i + 1, 4).Value = item.CarPartStageId.ToString();
                    worksheet.Cell(i + 1, 5).Value = item.Description.ToString();
                    worksheet.Cell(i + 1, 6).Value = item.Price.ToString();

                }
                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }


        }
    }


}
