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
            var messageBuffer = MediatorBus.BufferOf<string>();
            return PartialView(messageBuffer);
        }

        [ChildActionOnly]
        public ActionResult BufferOfChatMessage()
        {
            ViewBag.Message = "Welcome to long polling demo!";
            var messageBuffer = MediatorBus.BufferOf<ChatMessage>();
            return PartialView(messageBuffer);
        }

        [HttpPost]
        public ActionResult NotifyStringMessage(string name, string message)
        {
            MediatorBus.Push<string>(this, message);
            return Json(new { saved = "ok" });
        }

        [HttpPost]
        public ActionResult NotifyChatMessage(string name, ChatMessage message)
        {
            MediatorBus.Push<ChatMessage>(this, message);
            return Json(new { saved = "ok" });
        }
    }
}
