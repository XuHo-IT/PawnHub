using BussinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShopInformationRepository
    {           
            public ShopInformation GetInformation() => ShopInformationDAO.Instance.GetInformation();    

    }
}
