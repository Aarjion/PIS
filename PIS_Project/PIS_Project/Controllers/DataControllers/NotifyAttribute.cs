﻿using Microsoft.AspNet.Identity;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIS_Project.Controllers.DataControllers
{
    [Serializable]
    public sealed class NotifyAttribute : OnMethodBoundaryAspect
    {
        private object _locker = new object();
        private static NotificationController _notificator = new NotificationController();
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var id_s = "1";
            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.GetUserId()))
                id_s = HttpContext.Current.User.Identity.GetUserId();
            var id_user = int.Parse(id_s);
            var values = (Dictionary<string,object>)args.Arguments.FirstOrDefault();
            var card_id =(new CardsController()).Card.OrderBy(i => i.ID).First().ID;
            _notificator.Log("","",card_id);

        }
        public override void OnException(MethodExecutionArgs args)
        {
           
        }
    }
}