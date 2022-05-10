﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface ICompanyRepository :IRepository<Company>
    {
        void Update(Company obj);
       // void Save();
        //void Remove(Company obj);
    }
}
