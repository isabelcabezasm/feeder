﻿using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class FeederController : Controller
    {
        // GET: Feeder
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(int seconds = 1)
        {
            try
            {
                var sent = await IoTHubHelper.SendDirectMethod(seconds);

                if (sent == 200) { ViewBag.Result = "Message sent! "; }
                else { ViewBag.Result = $"Error sending. {sent}"; }
            }
            catch (IotHubException ex)
            {
                ViewBag.Result = $"Exception sending. {ex.Message}";
                Trace.TraceError(ex.Message);
            }
            return View();
        }
    }
}