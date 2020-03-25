namespace AccounterApplication.Web.Controllers
{
    using AccounterApplication.Common.Enumerations;
    using AccounterApplication.Services.Mapping;
    using AccounterApplication.Web.ViewModels;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class BaseController : Controller
    {
        private IMapper mapper;

        protected IMapper Mapper
        {
            get
            {
                if (this.mapper == null)
                {
                    this.mapper = AutoMapperConfig.MapperInstance;
                }
                
                return this.mapper;
            }
        }

        protected void AddAlertMessageToTempData(AlertMessageTypes alertMessageType, string message)
        {
            switch (alertMessageType)
            {
                case AlertMessageTypes.Success:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-success", "fa-check", "Success!", message));
                    break;
                case AlertMessageTypes.Info:
                    break;
                case AlertMessageTypes.Error:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-danger", "fa-times ", "Error!", message));
                    break;
                default:
                    break;
            }           
        }
    }
}
