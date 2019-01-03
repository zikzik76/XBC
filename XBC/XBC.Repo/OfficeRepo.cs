using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBC.DataModel;

namespace XBC.Repo
{
    public class OfficeRepo
    {

        //GetAll

        public static List<OfficeViewModel> get()
        {
            var data = new List<OfficeViewModel>();
            using (var db = new XBCContext())
            {
                data = (from o in db.t_office
                        where o.is_delete == false
                        select new OfficeViewModel
                        {
                            id = o.id,
                            name = o.name,
                            phone = o.phone,
                            email = o.email,
                            address = o.address,
                            notes = o.notes,
                        }).ToList();
            }
            return data;
        }
        public static List<OfficeViewModel> All(string search)
        {
            List<OfficeViewModel> result = new List<OfficeViewModel>();
            using (var db = new XBCContext())
            {
                if (String.IsNullOrEmpty(search))
                {
                    result = (from o in db.t_office
                              join r in db.t_room
                              on o.id equals r.office_id
                              where o.is_delete == false
                              select new OfficeViewModel
                              {
                                  id = o.id,
                                  name = o.name,
                                  phone = o.phone,
                                  email = o.email,
                                  address = o.address,
                                  notes = o.notes,
                              }).ToList();
                }
                else
                {
                    var src = from o in db.t_office
                              join r in db.t_room
                              on o.id equals r.office_id
                              where o.is_delete == false
                              select new OfficeViewModel
                              {
                                  id = o.id,
                                  name = o.name,
                                  phone = o.phone,
                                  email = o.email,
                                  address = o.address,
                                  notes = o.notes,
                              };
                    src = src.Where(s => s.name.Contains(search));
                    result = src.ToList();
                }
            }
            return result;
        }

        //Get by Id
        public static OfficeViewModel ByID(int id)
        {
            OfficeViewModel result = new OfficeViewModel();
            List<RoomViewModel> rest = new List<RoomViewModel>();
            using (var db = new XBCContext())
            {
                result = (from o in db.t_office
                          where o.id == id && o.is_delete == false
                          select new OfficeViewModel
                          {
                              id = o.id,
                              name = o.name,
                              phone = o.phone,
                              email = o.email,
                              address = o.address,
                              notes = o.notes,
                          }).FirstOrDefault();

                //Foreign Key Room ada di office id. result = office
                var src = from r in db.t_room
                          join o in db.t_office
                          on r.office_id equals o.id
                          where o.is_delete == false && r.office_id == result.id
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,

                          };
                //Details = List room yang ada di view model OfficeViewModel
                result.Details = src.ToList();

            }
            return result != null ? result : new OfficeViewModel();
        }

        //Create New
        public static bool Create(OfficeViewModel entity)
        {
            bool result = false;
            using (var db = new XBCContext())
            {
                var Office = new t_office()
                {
                    name = entity.name,
                    phone = entity.phone,
                    email = entity.email,
                    address = entity.address,
                    notes = entity.notes,
                    created_by = 11,
                    created_on = DateTime.Now


                };
                db.t_office.Add(Office);

                try
                {
                    db.SaveChanges();
                    //AuditRepo.Insert(Office);
                    result = true;
                }
                catch (Exception)
                {
                    throw;
                }

                foreach (var data in entity.Details)
                {
                    var Room = new t_room()
                    {
                        code = data.code,
                        name = data.name,
                        capacity = data.capacity,
                        any_projector = data.any_projector,
                        notes = data.notes,
                        office_id = Office.id,
                        created_by =111,
                        created_on = DateTime.Now
                    };
                    db.t_room.Add(Room);
                    try
                    {
                        db.SaveChanges();
                        //AuditRepo.Insert(Room);
                        result = true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        //Cara submit edit berebeda dengan create
        //Edit ada 2 cara yaitu: Dihapus dulu lalu input ulang.Atau ngeget data lama dan ngeinput data baru
        //Lebih gampang cara yang dihapus dulu
        //dihapus dulu karena ketika edit bisa nambah data baru.
        //kalau nambah data baru tidak dihapus maka data lama akan double.
        //Ini Untuk Post Edit

        public static bool CreateEdit(OfficeViewModel entity)
        {
            bool result = false;

            using (var db = new XBCContext())
            {

                //Edit Office
                var data = db.t_office.Find(entity.id);
                data.id = entity.id;
                data.name = entity.name;
                data.phone = entity.phone;
                data.email = entity.email;
                data.address = entity.address;
                data.notes = entity.notes;
                data.is_delete = false;

                //Dicari dari database office_id yang sama dengan Idnya office.
                var hapusroom = db.t_room.Where(x => x.office_id == data.id);
                db.t_room.RemoveRange(hapusroom);

                foreach (var item in entity.Details)
                {
                    var room = new t_room()
                    {
                        office_id = data.id,
                        code = item.code,
                        name = item.name,
                        capacity = item.capacity,
                        any_projector = item.any_projector,
                        notes = item.notes,
                        created_by = 111,
                        created_on = DateTime.Now,
                        is_delete = false
                    };
                    db.t_room.Add(room);
                    try { db.SaveChanges(); result = true; } catch (Exception) { throw; }
                }
            }

            return result;
        }


        //Get by RoomId
        public static RoomViewModel ByRoomId(int id)
        {
            RoomViewModel result = new RoomViewModel();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_room
                          where r.is_delete == false
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                          }).FirstOrDefault();
            }
            return result != null ? result : new RoomViewModel();
        }

        //Delete
        public static ResponseResult Delete(OfficeViewModel entity)
        {
            //id -> 
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_office Office = db.t_office.Where(o => o.id == entity.id).FirstOrDefault();
                    if (Office != null)
                    {

                        Office.is_delete = true;
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Office Not Found";
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

