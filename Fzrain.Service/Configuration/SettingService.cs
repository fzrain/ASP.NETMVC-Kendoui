using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Configuration;

namespace Fzrain.Service.Configuration
{
   public  class SettingService:ISettingService
   {
       private readonly IRepository<Setting> settingRepository;

       public SettingService(IRepository<Setting> settingRepository)
       {
           this.settingRepository = settingRepository;
       }

       public Setting GetById(int id)
       {
          return  settingRepository.GetById(id);
       }

       public IQueryable<Setting> GetAll()
       {
           return settingRepository.Table;
       }

       public string GetValueByName(string name)
       {
        return    settingRepository.Table.Where(s => s.Name == name).FirstOrDefault().Value;
       }

       public void AddSetting(Setting setting)
       {
           settingRepository.Insert(setting);
       }

       public void UpdateSetting(Setting setting)
       {
           settingRepository.Update(setting);
       }

       public void RemoveSetting(Setting setting)
       {
           settingRepository.Delete(setting);
       }
    }
}
