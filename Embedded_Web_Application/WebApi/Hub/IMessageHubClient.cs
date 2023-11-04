using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Hub
{
    public interface IMessageHubClient
    {
        Task SendOffersToUser(List<string> message);
    }
}