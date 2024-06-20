using EventPlanner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Business.UseCases.CreateEvent
{
    public class CreateEventResponse
    {
        public Event Event { get; set; }
    }
}
