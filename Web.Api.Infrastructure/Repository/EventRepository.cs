using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Entity;
using Web.Api.Infrastructure.Interface;
using Web.Api.Infrastructure.Models;
using System.Linq;
using Web.Api.Infrastructure.Helper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Web.Api.Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Web.Api.Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private NodeTestContext _context;
        private readonly IMapper _mapper;
        private readonly IErrorLog _errorLog;

        public EventRepository(NodeTestContext context, IMapper mapper, IErrorLog errorLog)
        {
            _context = context;
            _mapper = mapper;
            _errorLog = errorLog;
        }

        public List<EventMasterModel> GetEventList(DateTime dateStr)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dateStr);
                return _context.EventMaster.Where(x => x.startDateTime.Date == (DateTime)date.Date && x.endDateTime.Date == ((DateTime)(date.Date))).Select(x => new EventMasterModel()
                {
                    id = x.id,
                    endDateTime = x.endDateTime.ToString("dd MM yyyy HH:MM"),
                    startDateTime = x.startDateTime.ToString("dd MM yyyy HH:MM"),
                    event_Type = x.event_Type,
                    name = x.name,
                    user_Id = x.user_Id
                }).ToList();
            }
            catch (Exception ex)
            {
                _errorLog.BindErrorLogModel("GetEventList", ex.Message, "error");
                return null;
            }
        }


        public List<EventMasterModel> GetAllEvents()
        {
            try
            {
                return _context.EventMaster.Select(x => new EventMasterModel()
                {
                    id = x.id,
                    endDateTime = x.endDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.000Z"),
                    startDateTime = x.startDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.000Z"),
                    event_Type = x.event_Type,
                    name = x.name,
                    user_Id = x.user_Id
                }).ToList();
            }
            catch (Exception ex)
            {
                _errorLog.BindErrorLogModel("GetEventList", ex.Message, "error");
                return null;
            }
        }


        public List<EventMasterModel> GetUserWeekReport(DateTime dateStr)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dateStr);
                var today = date;
                var yesterday = date.AddDays(-1);
                var thisWeekStart = date.AddDays(-(int)date.DayOfWeek);
                var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

                return _context.EventMaster.Where(x => x.startDateTime.Date >= (DateTime)thisWeekStart.Date && x.endDateTime.Date <= ((DateTime)(thisWeekEnd.Date))).Select(x => new EventMasterModel()
                {
                    id = x.id,
                    endDateTime = x.endDateTime.ToString("dd MM yyyy HH:MM"),
                    startDateTime = x.startDateTime.ToString("dd MM yyyy HH:MM"),
                    event_Type = x.event_Type,
                    name = x.name,
                    user_Id = x.user_Id
                }).ToList();
            }
            catch (Exception ex)
            {
                _errorLog.BindErrorLogModel("GetEventList", ex.Message, "error");
                return null;
            }
        }


        public int InsertUpdateEvent(EventMasterModel eventMasterModel)
        {
            try
            {
                EventMaster _eventMaster = new EventMaster();
                _eventMaster = _context.EventMaster.Where(x => x.id == eventMasterModel.id).FirstOrDefault();
                _eventMaster = _mapper.Map<EventMasterModel, EventMaster>(eventMasterModel, _eventMaster);
                if (string.IsNullOrEmpty(_eventMaster.id))
                {
                    DateTime startDate =Convert.ToDateTime(eventMasterModel.startDateTime);
                    DateTime endDate =Convert.ToDateTime(eventMasterModel.endDateTime);
                    //Checking for same day entry for adding new event
                    var dataList = _context.EventMaster.Where(x => x.user_Id == eventMasterModel.user_Id && x.startDateTime.Date == startDate.Date && x.endDateTime.Date == endDate.Date).ToList();
                    for (int i = 0; i < dataList.Count; i++)
                    {
                       if (dataList[i].endDateTime.Hour >startDate.Hour && dataList[i].startDateTime.Hour < endDate.Hour)
                        {
                            return -11;//For having duplicate record...
                        }
                     
                    }
                        _eventMaster.id = Guid.NewGuid().ToString();
                        _context.EventMaster.Add(_eventMaster);
                }
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.BindErrorLogModel("InsertUpdateTest", ex.Message, "error");
                return -1;
            }
        }

    }
}
