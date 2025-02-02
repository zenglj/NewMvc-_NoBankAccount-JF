﻿using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Filters
{
    public class CustomExceptionFilterAttribute : IExceptionFilter
    {


        //private ILogger<CustomExceptionFilterAttribute> _logger = null;
        //public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        //{
        //    this._logger = logger;
        //}


        public void OnException(ExceptionContext context)
        {
            //string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionName = context.ActionDescriptor.ActionName;

            string controllerName = context.RouteData.Values["controller"].ToString();
            string actionName = context.RouteData.Values["action"].ToString();
            if (context.ExceptionHandled == false)
            {
                context.Result = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet , Data= new ResultInfo
                {
                    Flag = false,
                    ReMsg = context.Exception.Message,
                    DataInfo = null
                }};

                Log4NetHelper.logger.Error($"{controllerName}|{actionName}|{context.Exception.Message}");
                //this._logger.LogError(context.Exception.Message);
            }
            context.ExceptionHandled = true;
        }
    }


}