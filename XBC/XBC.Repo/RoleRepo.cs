using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.ViewModel;
using XBC.DataModel;

namespace XBC.Repo
{
    public class dataRoleAfterUpdate
    {
        public long id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string description { get; set; }
        public bool is_delete { get; set; }
        public long? modified_by { get; set; }
        public DateTime? modified_on { get; set; }
        public long? deleted_by { get; set; }
        public DateTime? deleted_on { get; set; }
    }
    public class RoleRepo
    {
        public static Object dataBeforeUpdate;
        public static Object dataBeforeDelete;
        public static List<RoleViewModel> All(string search)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            using (var db = new XBCContext())
            {
                if ((String.IsNullOrEmpty(search)))
                {
                    result = (from r in db.t_role
                              where r.is_delete == false
                              select new RoleViewModel
                              {
                                  id = r.id,
                                  name = r.name,
                                  code = r.code,
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
                                  name = r.name,
                                  code = r.code,
                                  description = r.description,
                                  is_delete = r.is_delete
                              };
                    src = src.Where(o => o.name.StartsWith(search));
                    result = src.ToList();
                }
            }
            return result != null ? result : new List<RoleViewModel>();
        }
        public static RoleViewModel ById(int id)
        {
            RoleViewModel result = new RoleViewModel();
            using (var db = new XBCContext())
            {
                result = (from r in db.t_role
                          where r.id == id
                          select new RoleViewModel
                          {
                              id = r.id,
                              name = r.name,
                              code = r.code,
                              description = r.description,
                              is_delete = r.is_delete
                          }).FirstOrDefault();
            }
            if (result != null)
            {
                dataBeforeUpdate = result;
            }
            return result != null ? result : new RoleViewModel();
        }

        private static bool ByName(string name)
        {
            RoleViewModel result = new RoleViewModel();

            using (var db = new XBCContext())
            {
                //id technology.id
                result = (from r in db.t_role
                          where r.name.ToLower() == name.ToLower() && r.is_delete == false
                          select new RoleViewModel
                          {
                              id = r.id,
                              description = r.description,
                              name = r.name


                          }).FirstOrDefault();

            }
            return result != null ? false : true;
        }

        public static ResponseResult CreateEdit(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                entity.code = GetNewCode();
                using (var db = new XBCContext())
                {
                    bool isNameCreated = ByName(entity.name);

                    if (entity.id == 0)
                    {
                        if (isNameCreated == true)
                        {
                            t_role role = new t_role();

                            role.code = entity.code;
                            role.name = entity.name;
                            role.description = entity.description;
                            role.is_delete = entity.is_delete;

                            role.created_by = 1;
                            role.created_on = DateTime.Now;

                            db.t_role.Add(role);
                            db.SaveChanges();

                            result.Entity = entity;

                            AuditRepo.Insert(role);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Nama yg anda masukan sudah terdaftar!";
                        }
                    }
                    else
                    {
                        t_role rol = db.t_role.Where(o => o.id == entity.id).FirstOrDefault();
                        if (rol != null)
                        {
                            rol.name = entity.name;
                            rol.description = entity.description;
                            rol.is_delete = entity.is_delete;

                            rol.modified_by = 1;
                            rol.modified_on = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;

                            dataRoleAfterUpdate rl = new dataRoleAfterUpdate();
                            rl.id = rol.id;
                            rl.code = rol.code;

                            rl.name = rol.name;
                            rl.description = rol.description;
                            rl.is_delete = rol.is_delete;

                            rl.modified_by = rol.modified_by;
                            rl.modified_on = rol.modified_on;

                            AuditRepo.Update(dataBeforeUpdate, rl);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Role tak tersedia";
                        }
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

        public static ResponseResult Delete(RoleViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_role role = db.t_role.Where(o => o.id == entity.id).FirstOrDefault();
                    if (role != null)
                    {
                        role.is_delete = true;
                        db.SaveChanges();

                        result.Entity = entity;

                        dataRoleAfterUpdate rl = new dataRoleAfterUpdate();
                        rl.id = role.id;
                        rl.code = role.code;

                        rl.name = role.name;
                        rl.description = role.description;
                        rl.is_delete = role.is_delete;

                        rl.deleted_by = role.deleted_by;
                        rl.deleted_on = role.deleted_on;

                        AuditRepo.Update(dataBeforeUpdate, rl);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Role tidak ditemukan";
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

        private static string GetNewCode()
        {
            string newCode = string.Format("RO");

            using (var db = new XBCContext())
            {
                var result = (from r in db.t_role
                              where r.code.Contains(newCode)
                              select new { code = r.code }
                              ).OrderByDescending(o => o.code).FirstOrDefault();

                if (result != null)
                {
                    string[] lastCode = result.code.Split('O');
                    newCode += (int.Parse(lastCode[1]) + 1).ToString("D3");
                }
                else
                {
                    newCode += "001";
                }
            }
            return newCode;
        }
    }
}
