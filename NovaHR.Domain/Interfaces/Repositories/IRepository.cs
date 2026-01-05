using NovaHR.Domain.Entities;
using NovaHR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaHR.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IReadOnlyList<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }



}

