using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    public static class ConfigurationManager
    {
        public const int MAX_WAIT_CONNECTION = 10000; //in ms, the duration of the long polling
        public const int MAX_TAKE_EACH_TIME = 1; //the nb of event to wait for before fire
        public const int MAX_ITEMS_IN_QUEUE = 100; //a queue to store the last events
    }
}
