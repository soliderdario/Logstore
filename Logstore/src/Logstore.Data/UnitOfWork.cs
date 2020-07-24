using Logdtore.Domain;
using Logdtore.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Logstore.Data
{
    public class UnitOfWork :  IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;
        private IDbTransaction _transaction;

        public UnitOfWork(IConfiguration configuration, IDbConnection _connection)
        {
            _configuration = configuration;
            if (_connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }         
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
