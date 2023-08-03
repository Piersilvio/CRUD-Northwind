using DBLayer.DAO.DAOImpl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.ExceptionHandler
{
    public delegate Task<T> ExceptionHandlerDelegate<T>();

    public class ExceptionHandlerClass
    {
        public ExceptionHandlerClass() { }

        public static async Task<T> HandleExceptions<T>(ExceptionHandlerDelegate<T> handler)
        {
            try
            {
                // Esegui l'azione passata come delegate
                return await handler();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
