using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.MediatorBootsrap), "Start")]

namespace $rootnamespace$.App_Start {
    public static class MediatorBootsrap {
        public static void Start() {
            MediatorBus.RegisterForType<string>();
        }
    }
}