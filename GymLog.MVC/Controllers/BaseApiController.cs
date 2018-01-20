using GymLog.MVC.Exceptions;
using GymLog.MVC.Models;
using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    public class BaseApiController : Controller
    {
        protected void HandleBadRequest(ApiException apiException)
        {
            if (apiException.StatusCode == HttpStatusCode.BadRequest)
            {
                var badRequestData = JsonConvert.DeserializeObject<JsonBadRequest>(apiException.JsonData);

                if (badRequestData.ModelState != null)
                {
                    foreach (var modelStateItem in badRequestData.ModelState)
                    {
                        foreach (var message in modelStateItem.Value)
                        {
                            ModelState.AddModelError(modelStateItem.Key, message);
                        }
                    }
                }

                if (string.Equals(badRequestData.Error, "invalid_grant"))
                {
                    ModelState.AddModelError("", badRequestData.ErrorDescription);
                }
            }
        }
    }
}