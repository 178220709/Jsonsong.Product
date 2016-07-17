using System;
using Jsonsong.Dal.Common.MongoDB;
using Jsonsong.Dal.Mall.Log;
using Microsoft.Extensions.Logging;

namespace Jsonsong.Api.Product.Common
{
    public class MongoLoggerProvider : ILoggerProvider
    {
       

        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MongoLogger : ILogger
    {
        private string CategoryName { get; set; }

        public MongoLogger(string categoryName)
        {
            CategoryName = categoryName;
        }

        protected static Dao<LogModel> dao = new Dao<LogModel>();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
                return;

            var message = formatter == null ? state.ToString() : formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null)
                return;

            LogModel log = new LogModel()
            {
                CategoryName = CategoryName,
                Content = message,
                Title = eventId.Name
            };
            dao.InsertOneAsync(log).Wait();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return dao;
        }
    }
}