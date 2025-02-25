using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Api.Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BookStoreServer.Service.Cache
{
    //todo
    public class SessionMemoryCache
    {
        IMemoryCache _memoryCache;
        ILogger<SessionMemoryCache> _logger;
        public SessionMemoryCache(IMemoryCache memoryCache, ILogger<SessionMemoryCache> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public SessionData? Get(string sessionId)
        {
            SessionData sessionData;
            bool isExist = _memoryCache.TryGetValue(sessionId, out sessionData);
            if (!isExist)
            {
                return null;
            }
            return sessionData;
        }
        public void Add(string sessionId, SessionData sessionData) {
            _memoryCache.Set(sessionId, sessionData);
            _logger.LogInformation($"UserIdentity:{sessionData.UserIdentity}, sessionId:{sessionId}");

        }
        public void Refresh(string sessionId, SessionData session)
        {
            Add(sessionId, session);
        }
        public void Delete(string sessionId) {
           _memoryCache.Remove(sessionId);
        }

    }
}
