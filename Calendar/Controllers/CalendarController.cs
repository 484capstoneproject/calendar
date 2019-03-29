using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Calendar.Controllers
{
    [RoutePrefix("api/Calendar")]
    public class CalendarController : ApiController
    {
        /// <summary>  
        /// Gets all events from Events table  
        /// </summary>  
        /// <returns></returns>  
        ///  
        //[HttpGet]  
        [Route("GetEvents")]
        public IHttpActionResult GetEvents()
        {
            using (CalendarDB context = new CalendarDB())
            {
                var eventsList = context.Events.ToList();
                return Ok(eventsList);
            }
        }

        /// <summary>  
        /// Save or update event.  
        /// </summary>  
        /// <param name="eventObject"></param>  
        /// <returns></returns>  
        ///  
        //[HttpPost]  
        [Route("PostSaveOrUpdate")]
        public IHttpActionResult PostSaveOrUpdate(Event NewEvent)
        {
            using (CalendarDB context = new CalendarDB())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var eventObj = context.Events.FirstOrDefault(e => e.EventID == NewEvent.EventID);
                if (eventObj != null)
                {
                    eventObj.EventTitle = NewEvent.EventTitle;
                    eventObj.EventDescription = NewEvent.EventDescription;
                    eventObj.StartDate = NewEvent.StartDate;
                    eventObj.EndDate = NewEvent.EndDate;
                }
                else
                {
                    context.Events.Add(NewEvent);
                }

                context.SaveChanges();

                return Ok();
            }
        }

        /// <summary>  
        /// Delete an event based on the given id.  
        /// </summary>  
        /// <param name="eventId"></param>  
        /// <returns></returns>  
        ///  
        //[HttpDelete]  
        [Route("DeleteEvent/{eventId:int}")]
        public IHttpActionResult DeleteEvent(int EventID)
        {
            using (CalendarDB context = new CalendarDB())
            {
                Event eventObj = context.Events.FirstOrDefault(e => e.EventID == EventID);

                if (eventObj == null)
                {
                    return NotFound();
                }

                context.Events.Remove(eventObj);
                context.SaveChanges();

                return Ok(eventObj);
            }
        }

    }
}  

