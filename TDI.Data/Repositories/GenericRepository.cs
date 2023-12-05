﻿//using AutoMapper.Internal;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TDI.Data.Infrastructure;
using System.Threading.Tasks;
using TDI.Utilities.Dtos;
using TDI.Utilities.Constants;

namespace TDI.Data.Repositories
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        //private readonly IConfiguration _config; IConfiguration config
        ConnectionFactory _connection;
        public GenericRepository(ConnectionFactory connection)
        {
            _connection = connection;
        }
        public IDbConnection GetConnection(GConnection gConnection = default)
        {
            return _connection.GetConnection(gConnection);
        }
        public void Dispose()
        {
            GC.Collect();
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = _connection.GetConnection(gConnection))
            {
                return db.Execute(sp, parms, commandType: commandType);
            }
        }

        public T Get(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = GetConnection(gConnection))
            {
                return db.Query<T>(sp, parms, commandType: commandType, commandTimeout: 1800).FirstOrDefault();
            }
        }
        public async Task<T> GetAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = GetConnection(gConnection))
            {
                var models = await db.QueryAsync<T>(sp, parms, commandType: commandType, commandTimeout: 1800); 
                var model = models.FirstOrDefault();
                return model;
            }
        }

        public List<T> GetAll(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = _connection.GetConnection(gConnection))
            {
                return db.Query<T>(sp, parms, commandType: commandType, commandTimeout: 1800).ToList();
            }
        }
        public async Task<List<T>> GetAllAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = GetConnection(gConnection))
            {
                var models = await db.QueryAsync<T>(sp, parms, commandType: commandType, commandTimeout: 1800);
                return models.ToList();
            }
        }

        //public class SingleQuery
        //{
        //    public string query { get; set; }
        //    public DynamicParameters parms { get; set; }

        //    public CommandType commandType { get; set; }
        //}
        //public class MultiQuery
        //{
        //    public List<SingleQuery> queries { get; set; } = new List<SingleQuery>();
        //}
        //public GenericResult InsertMulti(MultiQuery multiQuery)
        //{
        //    T result;
        //    using (IDbConnection db = GetConnection())
        //    {
        //        try
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            using (var tran = db.BeginTransaction())
        //            {
        //                foreach(SingleQuery singleQuery in multiQuery.queries)
        //                {
        //                    try
        //                    {
        //                        result = db.Query<T>(singleQuery.query, singleQuery.parms, commandType: singleQuery.commandType, transaction: tran).FirstOrDefault();
        //                        tran.Commit();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        tran.Rollback();
        //                        throw ex;
        //                    }
        //                }    

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (db.State == ConnectionState.Open)
        //                db.Close();
        //        }
        //        return result;
        //    }
        //}
        public T Insert(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            T result;
            using (IDbConnection db = GetConnection(gConnection))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
                return result;
            }
        }

        public T Update(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            T result;
            using (IDbConnection db = GetConnection(gConnection))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
                return result;
            }
        }

        public string GetScalar(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, GConnection gConnection = default)
        {
            using (IDbConnection db = GetConnection(gConnection))
            {
                var value = db.ExecuteScalar<string>(sp, parms, commandType: commandType, commandTimeout: 1800);

                return value;
            }
        }
    }
}
