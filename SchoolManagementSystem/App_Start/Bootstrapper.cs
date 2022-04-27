using RecruitmentSystemMgt.Repository;
using SchoolManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace SchoolManagementSystem.App_Start
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            //regitser all your components with the container here
            container.RegisterType<IResumeRepository, ResumeRepository>();

            RegisterTypes(container);
            return container;

        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}