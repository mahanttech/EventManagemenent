using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Infrastructure.Models;

namespace Web.Api.Infrastructure.Interface
{
    public interface IEventRepository
    {

        int InsertUpdateEvent(EventMasterModel eventMasterModel); // Method to Insert/Update event
        List<EventMasterModel> GetEventList(DateTime date);//Getting event list
        List<EventMasterModel> GetUserWeekReport(DateTime date);//Getting user week list
        List<EventMasterModel> GetAllEvents();//Getting all events list
        
    }
}
