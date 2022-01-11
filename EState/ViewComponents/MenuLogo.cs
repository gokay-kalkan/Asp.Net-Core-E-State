using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.ViewComponents
{
    public class MenuLogo:ViewComponent
    {
        GeneralSettingsManager generalSettingsManager = new GeneralSettingsManager(new EfGeneralSettingsDal());
        public IViewComponentResult Invoke()
        {
            var list = generalSettingsManager.List();
            return View(list);
        }
    }
}
