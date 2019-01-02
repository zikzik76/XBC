using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repo
{
    public class TechTrainerRepo
    {

        public static ResponseResult DeleteTechTrainer(long id)
        {
            //id -> technology Id
            ResponseResult result = new ResponseResult();
            List<TechTrainerViewModel> listId = new List<TechTrainerViewModel>();

            try
            {
                using (var db = new XBCContext())
                {
                    //cari total technology_id di t_technology_trainer
                    var total = db.t_technology_trainer.Count(o => o.technology_id == id);
                    //jika ada maka 
                    if (total > 0)
                    {
                        for (int i = 0; i < total; i++)
                        {
                            // apus lah
                            t_technology_trainer technology_trainer = db.t_technology_trainer.Where(o => o.technology_id == id).FirstOrDefault();
                            db.t_technology_trainer.Remove(technology_trainer);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "technology trainer not found!";
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
