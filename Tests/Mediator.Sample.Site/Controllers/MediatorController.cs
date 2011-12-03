using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mediator.Sample.Site.Models;

namespace Mediator.Sample.Site.Controllers
{
    public class MediatorNotifierController : Controller
    {

        [ChildActionOnly]
        public ActionResult BufferOfString()
        {
            ViewBag.Message = "Welcome to long polling demo!";
            var messageBuffer = MediatorBus.GetMessages<string>();
            return PartialView(messageBuffer);
        }

        [ChildActionOnly]
        public ActionResult BufferOfChatMessage()
        {
            ViewBag.Message = "Welcome to long polling demo!";
            var messageBuffer = MediatorBus.GetMessages<ChatMessage>();
            return PartialView(messageBuffer);
        }

        [HttpPost]
        public ActionResult NotifyStringMessage(string name, string message)
        {
            MediatorBus.Send<string>(this, message);
            return Json(new { saved = "ok" });
        }

        [HttpPost]
        public ActionResult NotifyChatMessage(string name, ChatMessage message)
        {
            MediatorBus.Send<ChatMessage>(this, message);
            return Json(new { saved = "ok" });
        }

        
        [HttpPost]
        public ActionResult NotifyChatMessageJson(string name, [ModelBinder(typeof(JsonMessageBinder))] ChatMessage message)
        {
            MediatorBus.Send<ChatMessage>(this, message);
            return Json(new { saved = "ok" });
        }

    }

    public class JsonMessageBinder : IModelBinder
    {

        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type expectedType = bindingContext.ModelType;
            string modelName = bindingContext.ModelName;
            string value = controllerContext.HttpContext.Request.Form[modelName];
            object result = ServiceStack.Text.JsonSerializer.DeserializeFromString(value, expectedType);
            return result;
        }

        #endregion
    }
}
