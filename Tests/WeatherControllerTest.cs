using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Controllers;

namespace Tests
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void Test_Correct_Conditions_MadeAvaliable()
        {
            //arrange
            var mock = new Mock<IWeatherProvider>();
            mock.Setup(f => f.GetCurrentWeatherConditions()).Returns(
                new WeatherCondition()
                {
                    Condition = "Sunny",
                    High = 55,
                    Low = 20
                });

            //Act
            var controller = new ModifyWeatherController(mock.Object);

            //Assert
            Assert.AreEqual("55", controller.HighTextControl.Text);
            Assert.AreEqual(20, controller.Low);
            Assert.AreEqual("Sunny", controller.Condition);

            
        }

        [TestMethod]
        public void Test_Validation_Fails_Does_Not_Update()
        {
            //arrange
            var newConditions = new WeatherCondition()
            {
                High = 105
            };

            var mock = new Mock<IWeatherProvider>();
            mock.Setup(f => f.GetCurrentWeatherConditions()).Returns(
               new WeatherCondition()
               );
            var controller = new ModifyWeatherController(mock.Object);
            controller.ErrorControl = (new Mock<TextControl>()).Object;

            //Act


            controller.UpdateConditions(newConditions);

            mock.Verify(f => f.SaveWeather(It.IsAny<WeatherCondition>()), Times.Never());


        }

        [TestMethod]
        public void Test_Validation_Fails_shows_Error()
        {
            //arrange
            var newConditions = new WeatherCondition()
            {
                High = 105
            };

            var mock = new Mock<IWeatherProvider>();
            mock.Setup(f => f.GetCurrentWeatherConditions()).Returns(
               new WeatherCondition()
               );
            var controller = new ModifyWeatherController(mock.Object);
            controller.ErrorControl = (new Mock<TextControl>()).Object;

            //Act


            controller.UpdateConditions(newConditions);

            Assert.AreEqual(true, controller.ErrorControl.IsVisible);


        }

        [TestMethod]
        public void Test_Validation_Does_NotAllow_MoreThanHigh()
        {
            //arrange
            var newConditions = new WeatherCondition()
            {
                High = 105
            };

            var mock = new Mock<IWeatherProvider>();
            mock.Setup(f => f.GetCurrentWeatherConditions()).Returns(
               new WeatherCondition()
               {
                   High = 120
               }
               );
            var controller = new ModifyWeatherController(mock.Object);
            controller.ErrorControl = (new Mock<TextControl>()).Object;

            //Act


            controller.UpdateConditions(newConditions);
            Assert.AreEqual(false, controller.ErrorControl.IsVisible);


        }

        [TestMethod]
        public void Test_Validation_Does_NotAllow_MoreThanHigh2()
        {
            //arrange
            var newConditions = new WeatherCondition()
            {
                High = 55
            };

            var mock = new Mock<IWeatherProvider>();
            mock.Setup(f => f.GetCurrentWeatherConditions()).Returns(
               new WeatherCondition()
               {
                   High = 40
               }
               );
            var controller = new ModifyWeatherController(mock.Object);
            controller.ErrorControl = (new Mock<TextControl>()).Object;

            //Act


            controller.UpdateConditions(newConditions);
            Assert.AreEqual(true, controller.ErrorControl.IsVisible);


        }
    }
}
