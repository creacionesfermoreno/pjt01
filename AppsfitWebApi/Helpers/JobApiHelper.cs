using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;

using Quartz.Impl;
using System.IO;
using AppsfitWebApi.Repository;

namespace AppsfitWebApi.Helpers
{
    public class JobApiHelper
    {

        public static async Task SendReceiptJob(string key,int CodigoSede, int CodigoUnidadNegocio,int Venta, string Email, string baseUrl,int type)
        {
            // 1. Create a scheduler Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            // 3. Create a job
            IJobDetail job = JobBuilder.Create<BackgroundJob>()
                    .WithIdentity("sendemail", "sendemailgroup")
                    .UsingJobData("CodigoSede", CodigoSede)
                    .UsingJobData("key", key)
                    .UsingJobData("CodigoUnidadNegocio", CodigoUnidadNegocio)
                    .UsingJobData("Venta", Venta)
                    .UsingJobData("Email", Email)
                    .UsingJobData("baseUrl", baseUrl)
                    .UsingJobData("type", type)
                    .Build();


            // 4. Create a trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("sendemail", "sendemailgroup")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).WithRepeatCount(0))
                .Build();

            // 5. Schedule the job using the job and trigger 
            await scheduler.ScheduleJob(job, trigger);

             //scheduler.Shutdown();
        }
    }


    public class BackgroundJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {

            var jobDataMap = context.MergedJobDataMap;
            var key = jobDataMap.GetString("key");
            var CodigoSede = jobDataMap.GetInt("CodigoSede");
            var CodigoUnidadNegocio = jobDataMap.GetInt("CodigoUnidadNegocio");
            var Venta = jobDataMap.GetInt("Venta");
            var Email = jobDataMap.GetString("Email");
            var baseUrl = jobDataMap.GetString("baseUrl");
            int type = jobDataMap.GetInt("type");

            //Random ram = new Random();
            //var name = ram.Next() + "-"+ CodigoSede + "-" + CodigoUnidadNegocio + "-" + Venta + "-" + Email;
            //var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/Content/assets/pdf/{name}.txt");
            //File.CreateText(path);
            if (type == 2)
            {
                //membresia
                MembresiaApiRepository.sendEmailValiateAccount(CodigoSede, CodigoUnidadNegocio, Venta, Email, baseUrl);
            }
            else
            {
                //comprobante
                PostApiRepository.sendEmailValiateAccountComp(key,CodigoSede, CodigoUnidadNegocio, Venta, Email, baseUrl);
            }
           
        }
    }


}