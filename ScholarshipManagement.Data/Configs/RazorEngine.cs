﻿//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Razor;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Routing;
//using ScholarshipManagement.Data.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using ITempDataProvider = System.Web.Mvc.ITempDataProvider;
//using TempDataDictionary = System.Web.Mvc.TempDataDictionary;

//namespace ScholarshipManagement.Data.Configs
//{
//    public class RazorEngine: IRazorEngine
//    {
//        private readonly IRazorViewEngine _viewEngine;
//        private readonly ITempDataProvider _tempDataProvider;
//        private IServiceProvider _serviceProvider;
//        private readonly HttpContext _context;

//        public RazorEngine(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IHttpContextAccessor accessor)
//        {
//            _viewEngine = viewEngine;
//            _tempDataProvider = tempDataProvider;
//            _serviceProvider = serviceProvider;
//            _context = accessor.HttpContext;
//        }
//        public async Task<string> ParseAsync<TModel>(string viewName, TModel model)
//        {
//            var actionContext = GetActionContext();
//            var view = FindView(actionContext, viewName);

//            await using var writer = new StringWriter();
//            var viewContext = new ViewContext(
//                actionContext,
//                view,
//                new ViewDataDictionary<TModel>(
//                    new EmptyModelMetadataProvider(),
//                    new ModelStateDictionary())
//                { Model = model },
//                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
//                writer,
//                new HtmlHelperOptions())
//            { RouteData = _context.GetRouteData() };

//            await view.RenderAsync(viewContext);

//            return writer.ToString();
//        }

//        private IView FindView(ActionContext actionContext, string viewName)
//        {
//            var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
//            if (getViewResult.Success)
//            {
//                return getViewResult.View;
//            }

//            var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
//            if (findViewResult.Success)
//            {
//                return findViewResult.View;
//            }

//            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
//            var errorMessage = string.Join(
//                Environment.NewLine,
//                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

//            throw new InvalidOperationException(errorMessage);
//        }

//        private ActionContext GetActionContext()
//        {
//            //var httpContext = new DefaultHttpContext
//            //{
//            //    RequestServices = _serviceProvider
//            //};
//            return new ActionContext(_context, new RouteData(), new ActionDescriptor());
//        }
//    }
//}
//}
