using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ModifyWeatherController
    {
        public string Condition { get; set; }
        public TextControl HighTextControl { get; set; }
        public int Low { get; set; }

        public string MyProperty { get; set; }

        public TextControl ErrorControl { get; set; }
        
        private IWeatherProvider _provider;
        private WeatherCondition _currentConditions;

        public ModifyWeatherController(IWeatherProvider provider) 
        {
            _provider = provider;
            HighTextControl = new TextControl();

            var conditions = provider.GetCurrentWeatherConditions();
            _currentConditions = conditions;
            Condition = conditions.Condition;
            HighTextControl.Text = conditions.High.ToString();
            Low = conditions.Low;
        }

        private bool _isValid(WeatherCondition condition)
        {
            if (condition.High > _currentConditions.High)
                return false;

            return true;
        }

        public void UpdateConditions(WeatherCondition newConditions)
        {
            //invoke validator
            bool isValid = _isValid(newConditions);

            //if validation succeeds
            if (isValid)
            {
                _provider.SaveWeather(newConditions);
            }
            else
            {
                ErrorControl.IsVisible = true;
                ErrorControl.Text = "Dude - I'll die in that weather";
            }
        }
    }
}
