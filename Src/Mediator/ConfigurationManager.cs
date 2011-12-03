using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator
{
    public static class ConfigurationManager
    {
        public const int DEFAULT_MAX_WAIT_CONNECTION = 10000; //in ms, the duration of the long polling
        public const int DEFAULT_MAX_TAKE_EACH_TIME = 1; //the nb of event to wait for before fire
        public const int DEFAULT_MAX_ITEMS_IN_QUEUE = 100; //a queue to store the last events

        private static int _max_wait_connection;
        private static int _max_take_each_time;
        private static int _max_items_in_queue;

        public static int MAX_WAIT_CONNECTION
        {
            get
            {
                if (_max_wait_connection == 0)
                {
                    _max_wait_connection = DEFAULT_MAX_WAIT_CONNECTION;
                }
                return DEFAULT_MAX_WAIT_CONNECTION;
            }
        }
        public static int MAX_TAKE_EACH_TIME
        {
            get
            {
                if(_max_take_each_time == 0)
                {
                    _max_take_each_time = DEFAULT_MAX_TAKE_EACH_TIME;
                }
                return DEFAULT_MAX_TAKE_EACH_TIME;
            }
        }
        public static int MAX_ITEMS_IN_QUEUE
        {
            get
            {
                if (_max_items_in_queue == 0)
                {
                    _max_items_in_queue = DEFAULT_MAX_ITEMS_IN_QUEUE;
                }
                return DEFAULT_MAX_ITEMS_IN_QUEUE;
            }
        }
    }
}
