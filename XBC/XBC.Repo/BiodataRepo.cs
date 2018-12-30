using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class BiodataRepo
    {
        //Get All
        public static List<BiodataViewModel> All()
        {
            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_biodata
                          where b.is_delete == false
                          select new BiodataViewModel
                          {
                              id = b.id,
                              name = b.name,
                              majors = b.majors,
                              gpa = b.gpa,
                              isDelete = b.is_delete
                          }).ToList();
            }
            return result != null ? result : new List<BiodataViewModel>();
        }

        //get by param
        public static List<BiodataViewModel> search(string searching)
        {
            List<BiodataViewModel> result = new List<BiodataViewModel>();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_biodata
                          where b.is_delete == false && b.name.StartsWith(searching) || b.is_delete == false && b.majors.StartsWith(searching) || b.is_delete == false && searching == null
                          select new BiodataViewModel
                          {
                              name = b.name, //id view Model
                              majors = b.majors,
                              gpa = b.gpa,
                              isDelete = b.is_delete

                          }).ToList();
            }

            return result != null ? result : new List<BiodataViewModel>();
        }

        //Get By Id
        public static BiodataViewModel ById(int id)
        {
            BiodataViewModel result = new BiodataViewModel();
            using (var db = new XBCContext())
            {
                result = (from b in db.t_biodata
                          where b.id == id
                          select new BiodataViewModel
                          {
                              name = b.name, //id view Model
                              gender = b.gender,
                              lastEducation = b.last_education,
                              graduationYear = b.graduation_year,
                              educationalLevel = b.educational_level,
                              majors = b.majors,
                              gpa = b.gpa,
                              bootcampTestType = b.bootcamp_test_type,
                              iq = b.iq,
                              du = b.du,
                              arithmetic = b.arithmetic,
                              nestedLogic = b.nested_logic,
                              joinTable = b.join_table,
                              tro = b.tro,
                              notes = b.notes,
                              interviewer = b.interviewer,


                          }).FirstOrDefault();
            }

            return result != null ? result : new BiodataViewModel();
        }

        //Create New
        public static ResponseResult CreateEdit(BiodataViewModel entity)
        {
            ResponseResult result = new ResponseResult();

            try
            {
                using (var db = new XBCContext())
                {
                    if (entity.id == 0)
                    {
                        t_biodata bio = new t_biodata();

                        bio.name = entity.name;
                        bio.last_education = entity.lastEducation;
                        bio.educational_level = entity.educationalLevel;
                        bio.graduation_year = entity.graduationYear;
                        bio.majors = entity.majors;
                        bio.gpa = entity.gpa;

                        bio.created_by = 1;
                        bio.created_on = DateTime.Now;

                        db.t_biodata.Add(bio);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        t_biodata bio = db.t_biodata.Where(o => o.id == entity.id).FirstOrDefault();

                        if (bio != null)
                        {
                            bio.name = entity.name;
                            bio.gender = entity.gender;
                            bio.last_education = entity.lastEducation;
                            bio.graduation_year = entity.graduationYear;
                            bio.educational_level = entity.educationalLevel;
                            bio.majors = entity.majors;
                            bio.gpa = entity.gpa;
                            bio.bootcamp_test_type = entity.bootcampTestType;
                            bio.iq = entity.iq;
                            bio.du = entity.du;
                            bio.arithmetic = entity.arithmetic;
                            bio.nested_logic = entity.nestedLogic;
                            bio.join_table = entity.joinTable;
                            bio.tro = entity.tro;
                            bio.notes = entity.notes;
                            bio.interviewer = entity.interviewer;


                            bio.modified_by = 1;
                            bio.modified_on = DateTime.Now; ;
                            bio.deleted_by = 1;
                            bio.deleted_on = DateTime.Now;

                            db.SaveChanges();
                            result.Entity = entity;

                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Biodata Not Found";
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

        //Delete
        public static ResponseResult Delete(BiodataViewModel entity)
        {
            //id -> Variant Id
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBCContext())
                {
                    t_biodata bio = db.t_biodata.Where(o => o.id == entity.id).FirstOrDefault();
                    if (bio != null)
                    {
                        //db.t_biodata.Remove(bio);
                        bio.is_delete = true;
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Biodata not found!";
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

        public static List<BiodataViewModel> GetBiodataIdle()
        {
            List<MonitoringViewModel> listIdMonitoring = MonitoringRepo.GetIdleDuringPlacement();

            int totalIdleAvailable = listIdMonitoring.Count;
            long[] array1D = new long[totalIdleAvailable];
            for (int i = 0; i < array1D.Length; i++)
            {
                array1D[i] = Convert.ToInt32(listIdMonitoring[i].biodata_id);
            }

            List<long> listId = new List<long>(array1D.Length) { };

            for (int i = 0; i < array1D.Length; i++)
            {
                listId.Add(array1D[i]);
            }

            List<BiodataViewModel> result = new List<BiodataViewModel>();

            using (var db = new XBCContext())
            {
                result = (from b in db.t_biodata
                          where b.is_delete == false && !listId.Contains(b.id)
                          select new BiodataViewModel
                          {
                              id = b.id,
                              name = b.name,
                              majors = b.majors,
                              gpa = b.gpa,
                              isDelete = b.is_delete
                          }).ToList();
            }
            return result != null ? result : new List<BiodataViewModel>();
        }

    }
}
