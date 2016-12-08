using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AzureMobileService.DataObjects;
using AzureMobileService.Models;
using AzureMobileService.Utilities;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace AzureMobileService.Controllers
{
    public class AdminController : TableController<AdminDTO>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new SimpleMappedEntityDomainManager<AdminDTO, Admin>
                    (context, Request, Services, admin => admin.Id);
        }

        // GET tables/Admin
        [ExpandableProperty("Restaurant", true)]
        public IQueryable<AdminDTO> GetAllAdmin()
        {
            return Query(); 
        }

        // GET tables/Admin/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandableProperty("Restaurant", true)]
        public SingleResult<AdminDTO> GetAdmin(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Admin/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<AdminDTO> PatchAdmin(string id, Delta<AdminDTO> patch)
        {
            Admin currentAdmin = this.context.Admins.Include("Restaurant").First(j => (j.Id == id));

            AdminDTO updatedpatchEntity = patch.GetEntity();

            ICollection<RestaurantDTO> restaurantItems;

            bool requestContainsRElatedEntites = patch.GetChangedPropertyNames().Contains("Restaurant");

            if (requestContainsRElatedEntites)
            {
                Mapper.Map<AdminDTO, Admin>(updatedpatchEntity, currentAdmin);

                restaurantItems = updatedpatchEntity.Restaurant;
            }
            else
            {
                AdminDTO adminDTOUpdated = Mapper.Map<Admin, AdminDTO>(currentAdmin);

                patch.Patch(adminDTOUpdated);
                Mapper.Map<AdminDTO, Admin>(adminDTOUpdated, currentAdmin);
                restaurantItems = adminDTOUpdated.Restaurant;
            }

            if (restaurantItems != null)
            {
                currentAdmin.Restaurant = new List<Restaurant>();
                foreach (RestaurantDTO currentRestaurantDTO in restaurantItems)
                {
                    Restaurant existingRestaurant = this.context.Restaurants.FirstOrDefault(j => (j.Id == currentRestaurantDTO.Id));
                    existingRestaurant = Mapper.Map<RestaurantDTO, Restaurant>(currentRestaurantDTO, existingRestaurant);
                    existingRestaurant.Admin = currentAdmin;
                    currentAdmin.Restaurant.Add(existingRestaurant);
                }
            }


            await this.context.SaveChangesAsync();

            var result = Mapper.Map<Admin, AdminDTO>(currentAdmin);
            return result;



        }


        // POST tables/Admin
        public async Task<IHttpActionResult> PostAdmin(AdminDTO item)
        {
            AdminDTO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Admin/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAdmin(string id)
        {
             return DeleteAsync(id);
        }

    }
}