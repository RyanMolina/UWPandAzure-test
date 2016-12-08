using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using AzureMobileService.DataObjects;
using AzureMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Drawing;
using System.IO;

namespace AzureMobileService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantDTO>()
                    .ForMember(restaurantDTO => restaurantDTO.Feedback,
                                        map => map.MapFrom(restaurant => restaurant.Feedback))
                    .ForMember(restaurantDTO => restaurantDTO.Cuisine,
                                        map => map.MapFrom(restaurant => restaurant.Cuisine))
                    .ForMember(restaurantDTO => restaurantDTO.StoreTime,
                                        map => map.MapFrom(restaurant => restaurant.StoreTime))
                    .ForMember(restaurantDTO => restaurantDTO.Menu,
                                        map => map.MapFrom(restaurant => restaurant.Menu));

                cfg.CreateMap<RestaurantDTO, Restaurant>()
                        .ForMember(restaurant => restaurant.Feedback,
                                    map => map.MapFrom(restaurantDTO => restaurantDTO.Feedback))
                        .ForMember(restaurant => restaurant.Cuisine,
                                    map => map.MapFrom(restaurantDTO => restaurantDTO.Cuisine))
                        .ForMember(restaurant => restaurant.StoreTime,
                                    map => map.MapFrom(restaurantDTO => restaurantDTO.StoreTime))
                        .ForMember(restaurant => restaurant.Menu,
                                    map => map.MapFrom(restaurantDTO => restaurantDTO.Menu));

                cfg.CreateMap<Admin, AdminDTO>()
                        .ForMember(adminDTO => adminDTO.Restaurant,
                                    map => map.MapFrom(admin => admin.Restaurant));

                cfg.CreateMap<AdminDTO, Admin>()
                        .ForMember(admin => admin.Restaurant,
                                   map => map.MapFrom(adminDTO => adminDTO.Restaurant));

                cfg.CreateMap<Feedback, FeedbackDTO>();
                cfg.CreateMap<FeedbackDTO, Feedback>();
                
                cfg.CreateMap<StoreTime, StoreTimeDTO>();
                cfg.CreateMap<StoreTimeDTO, StoreTime>();

                cfg.CreateMap<Menu, MenuDTO>();
                cfg.CreateMap<MenuDTO, Menu>();
            });

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {


            Admin admin = new Admin { Username = "Leonel", Password = "Paminta", Id = Guid.NewGuid().ToString()};

            context.Set<Admin>().Add(admin);


            base.Seed(context);
        }
    }
}

