using System;
using System.Threading.Tasks;
using Application.Visitors.OnlineVisitors;
using Microsoft.AspNetCore.SignalR;

namespace WebSite.Endpoint.Hubs
{
    public class OnlineVisitorHub : Hub
    {
        private readonly IOnlineVisitorsService _onlineVisitors;

        public OnlineVisitorHub(IOnlineVisitorsService onlineVisitors)
        {
            _onlineVisitors = onlineVisitors;
        }

        public override Task OnConnectedAsync()
        {
            var VisitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitors.OnConnectUser(VisitorId);
            var count = _onlineVisitors.GetCount();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var VisitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitors.OndisConnectUser(VisitorId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}