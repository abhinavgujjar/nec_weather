using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controllers
{
    public interface IWeatherProvider
    {
        WeatherCondition GetCurrentWeatherConditions();

        void SaveWeather(WeatherCondition newConditions);
    }
}
