using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class RoomRepo
    {
        //GetAll
        public static List<RoomViewModel> All()
        {
            List<RoomViewModel> result = new List<RoomViewModel>();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_room
                          join o in db.t_office
                          on r.office_id equals o.id
                          where r.is_delete == false
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                              office_id = r.office_id,
                          }).ToList();
            }

            return result != null ? result : new List<RoomViewModel>();
        }

        //Get by Id
        public static RoomViewModel ByID(int id)
        {
            RoomViewModel result = new RoomViewModel();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_room
                          join o in db.t_office
                          on r.office_id equals o.id
                          where r.is_delete == false
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                              office_id = r.office_id,
                          }).FirstOrDefault();
            }

            return result != null ? result : new RoomViewModel();
        }

        //Get by Office
        public static List<RoomViewModel> ByOffice(long id)
        {
            List<RoomViewModel> result = new List<RoomViewModel>();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_room
                          join o in db.t_office
                          on r.office_id equals o.id
                          where r.is_delete == false
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                              office_id = r.office_id,
                          }).ToList();
            }
            return result != null ? result : new List<RoomViewModel>();
        }

        //Create new
        public static ResponseResult CreateEdit(RoomViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            result.Success = true;
            try
            {
                using (var db = new XBCContext())
                {

                    //Insert
                    if (entity.id == 0)
                    {
                        t_room Room = new t_room();
                        Room.code = entity.code;
                        Room.name = entity.name;
                        Room.capacity = entity.capacity;
                        Room.any_projector = entity.any_projector;
                        Room.notes = entity.notes;
                        Room.office_id = entity.office_id;

                        Room.created_by = 1;
                        Room.created_on = DateTime.Now;
                        Room.modified_by = 2;
                        Room.modified_on = DateTime.Now;

                        db.t_room.Add(Room);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Room Not Found ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
