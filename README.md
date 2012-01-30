#Welcome to Mediator demo!
 
Mediator is a simple to use and to learn framwork to do real time html applications over traditional http.
 
This is an help over a sample to learn some kind of long pooling with asp.net platform
 
# Mediator Library Usage

## 1. Configuration

1. Import the Mediator.dll in your project/web site

2. by default you should have the run all managed modules enabled: 

    <modules runAllManagedModulesForAllRequests="true"/>
 
 If it is the case, you don't need to add any configuration in your web.config, our handler will be called automaticaly. If it is set to false, you should add manually the route handler.
 
3. You should define the route used by the bus event messages. There is no default route, we let it at your conveniance. Just add the route to our custom routeHandler just like any other routes:
 
 
         //Messages bus
        routes.Add(
            "mediator",
            new Route(
                "mediator/{type}/{name}",
                new RouteValueDictionary(new { type = "string", name = "default" }),
                new EventBusRouteHandler()));


 this route is used to listen to future messages. This mecanism doesn't handle the notification of the messages, we want it to be availble from anywhere you need in your code and we want to allow it as it should be cleaner for your app.
 
## 2. Server code

### 1. Push messages
 
As in our exemple, you should add a special route to push the messages : 

    //Message push
    routes.MapRoute(
        "Push", // default route
        "MediatorNotifier/{action}",
        new { controller = "MediatorNotifier", action = "Index" } // Parameter defaults
    ); 

This is the easiest way to centralize all your notifications. This is then, how a notification action should look like: 

    [HttpPost]
    public ActionResult NotifyChatMessage(string name, ChatMessage message)
    {
        MediatorBus.Push<ChatMessage>(this, message);
        return Json(new { saved = "ok" });
    }

 There is also another interest in using an asp.net mvc action, is to automaticaly use the mvc Default Model Binder (as in our exemple, we use directly our ChatMessage model)
 

### 2. From server side, you should want to print current messages from the buffer when loading a page.

In order to do this you should get the current buffer directly from the mediator buffer helper, use it and return it to view as any other data you use to manipulate. In our exemple, we choose to pass it through a partial action in order to centralize all these calls and decouple them from the view. In our controller:

    [ChildActionOnly]
    public ActionResult BufferOfChatMessage()
    {
        ViewBag.Message = "Welcome to long polling demo!";
        var messageBuffer = MediatorBus.BufferOf<ChatMessage>();
        return PartialView(messageBuffer);
    }

 If you centralize it in a Mediator controller, you should call it from your original view like this: 

    <h2>Simple message exchange</h2>
    <p>
     @Html.Action("BufferOfChatMessage","MediatorNotifier")
    </p>
     The buffer list is an untyped simple IEnumerable. you should print your messages in the partial view like this: @model IEnumerable
    @{
        Layout = null;
    }
    @{
        var messages = Model.Cast<MessageOf<ChatMessage>>();
    }
    <ul class="unstyled" id="messages">
        @foreach (var item in messages)
        {
            <li>@item.MessageItem.User : @item.MessageItem.Message</li>   
        }
    </ul>
 

## 3. Client code

From client side, the simplest way it to push ajax messages with the help of jQuery
 
After page load, just define a recursive ajax call like this in order to subscribe to messages:

    function getMessages() {
        $.post("/mediator/string", null, function (data, s) {
            if (data.MessageItem != "" && data.MessageItem != undefined) {
                var $msg = $('<li/>');
                $msg.html(data.MessageItem); 
                $msg.prependTo('#messages');
            } else {
                console.log(data.TimeStamp);
            }
            setTimeout(function () {
                getMessages();
            }, 10000);
        });
    }

 For the send messages part, it's nothing more than traditional asp.net mvc if you want to choose the same paradigm as me ;-).
 
## 4. Get MORE!
 
This is a simple and functional framework. It's initial target is mainly to help learning async handlers and long polling other asp.net.It's not at this time a production grade framework but it is relatively safe to use. By the way, it is yours now and it depends only on you if you want to help and enhance it!
 
So fork it, push your updates and help us make it better!

If you want more mature frameworks with quite similar functionalities, have a great look on SignalR!
 
If you don't need for this information push to be on asp.net, the better way is certainly today to build a signaling system with Node.Js in compbination with the Socket.io framework

    Learn, share, enjoy!