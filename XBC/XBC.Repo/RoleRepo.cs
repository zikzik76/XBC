using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBD.DataModel;

namespace XBC.Repo
{
    public class RoleRepo
    {
        public static List<RoleViewModel> All(string searchString)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new XBCContext())
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    result = (from r in db.t_role
                              where r.is_delete == false
                              select new RoleViewModel
                              {
                                  id = r.id,
                                  code = r.code,
                                  name = r.name,
                                  description = r.description,
                                  is_delete = r.is_delete

                              }).ToList();
                }
                else
                {
                    var src = from r in db.t_role
                              where r.is_delete == false
                              select new RoleViewModel
                              {
                                  id = r.id,
                                  code = r.code,
                                  name = r.name,
                                  description = r.description,
                                  is_delete = r.is_delete

                              };
                    src = src.Where(s => s.code.Contains(searchString));
                    result = src.ToList();
                }
            }
            return result;
        }
        public static RoleViewModel GetRole(int id)
        {
            RoleViewModel result = new RoleViewModel();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_role
                          where r.id == id
                          select new RoleViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              description = r.description,
                              is_delete = r.is_delete
                          }).FirstOrDefault();
            }
            return result;
        }

        public static ResponResultViewModel Update(RoleViewModel entity,long userid)
        {
            //untuk create & edit
            ResponResultViewModel result = new ResponResultViewModel();
            entity.code = GetNewRole();
            try
            {
                using (var db = new XBCContext())
                {
                    if (entity.id == 0)
                    {
                        t_role role = new t_role();
                        role.code = entity.code;
                        role.name = entity.name;
                        role.description = entity.description;
                        role.created_by = userid;
                        role.created_on = DateTime.Now;
                        role.is_delete = entity.is_delete;

                        db.t_role.Add(role);
                        db.SaveChanges();

                        result.Entity = role;

                    }
                    else
                    {
                        t_role role = db.t_role.Where(x => x.id == entity.id).FirstOrDefault();
                        if (role != null)
                        {
                            role.name = entity.name;
                            role.description = entity.description;
                            role.modified_by = userid;
                            role.modified_on = DateTime.Now;
                            role.is_delete = entity.is_delete;

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return result;
        }
        public static ResponResultViewModel Delete(RoleViewModel entity,long userid)
        {
            //untuk deactive
            ResponResultViewModel result = new ResponResultViewModel();
            try
            {
                using (var db = new XBCContext())
                {
                    t_role role = db.t_role.Where(x => x.id == entity.id).FirstOrDefault();
                    if (role != null)
                    {
                        role.is_delete = true;
                        role.deleted_by = userid;
                        role.deleted_on = DateTime.Now;

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "user not found!";
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return result;
        }
        public static string GetNewRole()
        {
            string newRole = String.Format("RO");

            using (var db = new XBCContext())
            {
                var result = (from r in db.t_role
                              where r.code.Contains(newRole) //contains: check newRev ada atau tidak pada bulan dan tahun itu
                              select new { Code = r.code }).OrderByDescending(x => x.Code).FirstOrDefault();
                if (result != null)
                {
                    string[] lastRef = result.Code.Split('O');
                    newRole += (int.Parse(lastRef[1]) + 1).ToString("D3");
                }
                else
                {
                    newRole += "001";
                }
            }
            return newRole;
        }
    }
}
