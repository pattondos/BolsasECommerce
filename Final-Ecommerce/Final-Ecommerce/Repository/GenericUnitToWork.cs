using Final_Ecommerce.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Ecommerce.Repository
{
    public class GenericUnitToWork: IDisposable
    {
        private Entities DBEntity = new Entities();
        public IRepository<EntityType> GetRepositoryInstance<EntityType>() where EntityType : class
        {
            return new GenericRepository<EntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}