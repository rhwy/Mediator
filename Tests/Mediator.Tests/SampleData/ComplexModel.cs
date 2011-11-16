using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator.Tests.SampleData
{
    [Serializable]
    public class ComplexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public long UserId { get; set; }
        public TimeSpan Duration { get; set; }

        public ComplexModel(TimeSpan duration,int id = -1, string title = "", string message = "", long userId = -1)
        {
            Id = id;
            Title = title;
            Message = message;
            UserId = userId;
            Duration = duration;
        }

        public override bool Equals(object obj)
        {
            ComplexModel compared = (ComplexModel)obj;
            if (compared == null)
            {
                return false;
            }
            
            return Id == compared.Id 
                    && Title == compared.Title
                    && Message == compared.Message
                    && UserId == compared.UserId
                    && Duration.Milliseconds == compared.Duration.Milliseconds;
        }
    }
}
