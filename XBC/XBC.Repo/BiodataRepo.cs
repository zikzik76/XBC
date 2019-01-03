using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    //  HANDLE Circular references in JSONSerializer and StackOverflow exceptions with Create Manual Class
    public class BioAfterUpdate
    {
        public long id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string lastEducation { get; set; }
        public string graduationYear { get; set; }
        public string educationalLevel { get; set; }
        public string majors { get; set; }
        public string gpa { get; set; }
        public long? bootcampTestType { get; set; }
        public int? iq { get; set; }
        public string du { get; set; }
        public int? arithmetic { get; set; }
        public int? nestedLogic { get; set; }
        public int? joinTable { get; set; }
        public string tro { get; set; }
        public string notes { get; set; }
        public string interviewer { get; set; }
        public long createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public long? modified_by { get; set; }
        public DateTime? modified_on { get; set; }
        public long? deleted_by { get; set; }
        public DateTime? deleted_on { get; set; }
        public bool isDelete { get; set; }
    }

    public class BiodataRepo
    {
        public static Object dataBeforeUpdate;  // TEMPORARY BEFORE UPDATING DATA
        //public static Object dataAfterUpdate;   // TEMPORARY AFTER UPDATING DATA
        public static Object dataBeforeDelete;  // TEMPORARY BEFORE DELETING DATA

        //Get All
        public static List<BiodataViewModel> All(/*long userid*/)
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
                              createdBy = 1,
                              createdOn = DateTime.Now,
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
                              id = b.id,
                              name = b.name, //id view Model
                              majors = b.majors,
                              gpa = b.gpa,
                              isDelete = b.is_delete

                          }).ToList();
            }

            return result != null ? result : new List<BiodataViewModel>();
        }

        //Get By Id
        public static BiodataViewModel ById(int id/*, long userid*/)
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
                              modified_by = 1,
                              modified_on = DateTime.Now


                          }).FirstOrDefault();
            }

            return result != null ? result : new BiodataViewModel();
        }

        //Create New
        public static ResponseResult CreateEdit(BiodataViewModel entity/*, long userid*/)
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

                        //  AUDIT LOG => "INSERT"
                        AuditRepo.Insert(bio);
                    }
                    else
                    //update
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


                            db.SaveChanges();
                            result.Entity = entity;

                            //  AUDIT LOG => "MODIFY" UPDATE
                            BioAfterUpdate dau = new BioAfterUpdate();
                            dau.id = bio.id;
                            dau.name = bio.name;
                            dau.gender = bio.gender;
                            dau.lastEducation = bio.last_education;
                            dau.graduationYear = bio.graduation_year;
                            dau.educationalLevel = bio.educational_level;
                            dau.majors = bio.majors;
                            dau.gpa = bio.gpa;
                            dau.bootcampTestType = bio.bootcamp_test_type;
                            dau.iq = bio.iq;
                            dau.du = bio.du;
                            dau.arithmetic = bio.arithmetic;
                            dau.nestedLogic = bio.nested_logic;
                            dau.joinTable = bio.join_table;
                            dau.tro = bio.tro;
                            dau.notes = bio.notes;
                            dau.interviewer = bio.interviewer;

                            dau.createdBy = bio.created_by;
                            dau.createdOn = bio.created_on;

                            dau.modified_by = bio.modified_by;
                            dau.modified_on = bio.modified_on;

                            AuditRepo.Update(dataBeforeUpdate, dau);

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
        public static ResponseResult Delete(BiodataViewModel entity/*, long userid*/)
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

                        bio.deleted_by = 1;
                        bio.deleted_on = DateTime.Now;
                        db.SaveChanges();

                        result.Entity = entity;

                        //  AUDIT LOG => "MODIFY" DELETE
                        BioAfterUpdate dau = new BioAfterUpdate();
                        dau.id = bio.id;
                        dau.name = bio.name;
                        dau.gender = bio.gender;
                        dau.lastEducation = bio.last_education;
                        dau.graduationYear = bio.graduation_year;
                        dau.educationalLevel = bio.educational_level;
                        dau.majors = bio.majors;
                        dau.gpa = bio.gpa;
                        dau.bootcampTestType = bio.bootcamp_test_type;
                        dau.iq = bio.iq;
                        dau.du = bio.du;
                        dau.arithmetic = bio.arithmetic;
                        dau.nestedLogic = bio.nested_logic;
                        dau.joinTable = bio.join_table;
                        dau.tro = bio.tro;
                        dau.notes = bio.notes;
                        dau.interviewer = bio.interviewer;

                        dau.createdBy = bio.created_by;
                        dau.createdOn = bio.created_on;

                        dau.modified_by = bio.modified_by;
                        dau.modified_on = bio.modified_on;

                        dau.deleted_by = bio.deleted_by;
                        dau.deleted_on = bio.deleted_on;

                        dau.isDelete = bio.is_delete;

                        AuditRepo.Update(dataBeforeUpdate, dau);
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
