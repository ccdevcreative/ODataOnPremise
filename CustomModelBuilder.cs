using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataOnPremise
{
    public class CustomModelBuilder
    {
        //define model using Microsoft.OData.Edm
        private readonly EdmModel _model = new EdmModel();
        public IEdmModel GetModel()
        {
            return _model;
        }


        //Defines an entity type WeatherForecast within the namespace Sample.NS;
        private EdmEntityType _weatherForecastType;
        public CustomModelBuilder BuildWeatherForecastType()
        {

            _weatherForecastType = new EdmEntityType("ODataOnPremise", "WeatherForecast");
            _weatherForecastType.AddKeys(_weatherForecastType.AddStructuralProperty("Key", EdmPrimitiveTypeKind.Int32, isNullable: false));
            _weatherForecastType.AddStructuralProperty("Date",EdmPrimitiveTypeKind.Date);
            _weatherForecastType.AddStructuralProperty("TemperatureC", EdmPrimitiveTypeKind.Int32);
            _weatherForecastType.AddStructuralProperty("TemperatureF", EdmPrimitiveTypeKind.Int32);
            _weatherForecastType.AddStructuralProperty("Summary", EdmPrimitiveTypeKind.String);
            
            _model.AddElement(_weatherForecastType);

            return this;
        }


        //Defines an entity container DefaultContainer within the namespace Sample.NS;
        //Adds the container to the model
        private EdmEntityContainer _defaultContainer;
        
        public CustomModelBuilder BuildDefaultContainer()
        {
            _defaultContainer = new EdmEntityContainer("ODataOnPremise", "DefaultContainer");
            _model.AddElement(_defaultContainer);
            return this;
        }


        //This code directly adds a new entity set Customers to the default container.
        private EdmEntitySet _customerSet;
        
        public CustomModelBuilder BuildCustomerSet()
        {
            _customerSet = _defaultContainer.AddEntitySet("WeatherForecast", _weatherForecastType);
            return this;
        }
    }
}
