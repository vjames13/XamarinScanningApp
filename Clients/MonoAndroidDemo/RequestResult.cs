using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MonoAndroidDemo
{
    public class RequestResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}