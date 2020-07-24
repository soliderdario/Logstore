using System;
using System.Collections.Generic;
using System.Text;

namespace Logdtore.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
