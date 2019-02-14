//using System;
//using System.Collections.Generic;
//using BLL.IRepositories;
//using BLL.Models;
//using BLL.Services;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using VacationTrackingSoftware.Controllers;
//using Xunit;

//namespace XUnitTestProject1
//{
//    public class UnitTest1
//    {
//        //chek data from Repo 
//        [Fact]
//        public void Test1()
//        {
//            var mockCompanyRepo = new Mock<ICompanyHolidayRepository>();
//            var mockCompanyHolidayService = new Mock<ICompanyHolidayService>();
//            HolidayController controller = new HolidayController(mockCompanyRepo.Object, mockCompanyHolidayService.Object);
//            mockCompanyRepo.Setup(repo => repo.GetAllHolidaysForCurrentYear()).Returns(GetCurrentHoliday());

//            var result = controller.GetForCurrentYear();

//            var viewResult = Assert.IsType<List<CompanyHoliday>>(result);
//            var model = Assert.IsAssignableFrom<List<CompanyHoliday>>(viewResult);
//            Assert.Equal(4, model.Count);
//        }
//        private List<CompanyHoliday> GetCurrentHoliday()
//        {
//            var holidays = new List<CompanyHoliday>
//            {
//                new CompanyHoliday{Id=1,Date=new DateTime(2019,12,12,00,00,00),Description="some"},
//                new CompanyHoliday{Id=2,Date=new DateTime(2019,01,22,00,00,00),Description="finish of holidays" },
//                new CompanyHoliday{Id=6,Date=new DateTime(2019,01,28,00,00,00),Description="test" },
//                new CompanyHoliday{Id=3005,Date=new DateTime(2019,01,23,00,00,00),Description="uio" },
//            };
//            return holidays;
//        }

//        [Fact]
//        public void AddNewHoliday() {
//            var mockCompanyRepo = new Mock<ICompanyHolidayRepository>();
//            var mockCompanyHolidayService = new Mock<ICompanyHolidayService>();
//            HolidayController controller = new HolidayController(mockCompanyRepo.Object, mockCompanyHolidayService.Object);

//            //act
//            var newHoliday = new CompanyHoliday() { };
//            var result = controller.AddHoliday(newHoliday);

//            //Assert
//            var actioResult = Assert.IsAssignableFrom<ActionResult>(result);
//            Assert.NotNull(actioResult);
            
//        }


//    }
//}
