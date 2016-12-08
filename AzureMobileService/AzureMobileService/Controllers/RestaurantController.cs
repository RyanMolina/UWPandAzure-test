using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AzureMobileService.DataObjects;
using AzureMobileService.Models;
using AzureMobileService.Utilities;
using System.Diagnostics;
using System.Collections.Generic;
using AutoMapper;

namespace AzureMobileService.Controllers
{
    public class RestaurantController : TableController<RestaurantDTO>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new SimpleMappedEntityDomainManager<RestaurantDTO, Restaurant>
                    (context, Request, Services, restaurant => restaurant.Id);
        }

        // GET tables/Restaurant
        [ExpandableProperty("Feedback", true), ExpandableProperty("Cuisine", true), ExpandableProperty("StoreTime", true), ExpandableProperty("Menu", true)]
        public IQueryable<RestaurantDTO> GetAllRestaurant()
        {
            return Query(); 
        }

        // GET tables/Restaurant/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandableProperty("Feedback", true), ExpandableProperty("Cuisine", true), ExpandableProperty("StoreTime", true), ExpandableProperty("Menu", true)]
        public SingleResult<RestaurantDTO> GetRestaurant(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Restaurant/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<RestaurantDTO> PatchRestaurant(string id, Delta<RestaurantDTO> patch)
        {
            Restaurant currentRestaurant = this.context.Restaurants.Include("Feedback").Include("Cuisine").Include("StoreTime").Include("Menu")
                .First(j => (j.Id == id));

            RestaurantDTO updatedpatchEntity = patch.GetEntity();
            ICollection<FeedbackDTO> feedbackItems;
            ICollection<CuisineDTO> cuisineItems;
            ICollection<StoreTimeDTO> storeTimeItems;
            ICollection<MenuDTO> menuItems;

            bool requestContainsRelatedEntities = patch.GetChangedPropertyNames().Contains("Feedback");
            requestContainsRelatedEntities &= patch.GetChangedPropertyNames().Contains("Cuisine");
            requestContainsRelatedEntities &= patch.GetChangedPropertyNames().Contains("StoreTime");
            requestContainsRelatedEntities &= patch.GetChangedPropertyNames().Contains("Menu");
            if (requestContainsRelatedEntities)
            {
                /*for (int i = 0; i < currentRestaurant.Feedback.Count &&
                    updatedpatchEntity.Feedback != null; i++)
                {
                    FeedbackDTO feedbackDTO = updatedpatchEntity.Feedback.FirstOrDefault(j =>
                                    (j.Id == currentRestaurant.Feedback.ElementAt(i).Id));

                    if (feedbackDTO == null)
                    {
                        this.context.Feedbacks.Remove(currentRestaurant.Feedback.ElementAt(i));
                    }
                } */

                Mapper.Map<RestaurantDTO, Restaurant>(updatedpatchEntity, currentRestaurant);
                feedbackItems = updatedpatchEntity.Feedback;
                cuisineItems = updatedpatchEntity.Cuisine;
                storeTimeItems = updatedpatchEntity.StoreTime;
                menuItems = updatedpatchEntity.Menu;
                

            }
            else
            {
                RestaurantDTO restaurantDTOUpdated = Mapper.Map<Restaurant, RestaurantDTO>(currentRestaurant);

                patch.Patch(restaurantDTOUpdated);
                Mapper.Map<RestaurantDTO, Restaurant>(restaurantDTOUpdated, currentRestaurant);
                feedbackItems = restaurantDTOUpdated.Feedback;
                cuisineItems = restaurantDTOUpdated.Cuisine;
                storeTimeItems = restaurantDTOUpdated.StoreTime;
                menuItems = restaurantDTOUpdated.Menu;
            }

            if (feedbackItems != null)
            {
                currentRestaurant.Feedback = new List<Feedback>();
                foreach (FeedbackDTO currentFeedbackDTO in feedbackItems)
                {
                    Feedback existingFeedback = this.context.Feedbacks.FirstOrDefault(j => (j.Id == currentFeedbackDTO.Id));
                    existingFeedback = Mapper.Map<FeedbackDTO, Feedback>(currentFeedbackDTO, existingFeedback);
                    existingFeedback.Restaurant = currentRestaurant;
                    currentRestaurant.Feedback.Add(existingFeedback);
                }
            }
            if (cuisineItems != null)
            {
                currentRestaurant.Cuisine = new List<Cuisine>();
                foreach (CuisineDTO currentCuisineDTO in cuisineItems)
                {
                    Cuisine existingCuisine = this.context.Cuisines.FirstOrDefault(j => (j.Id == currentCuisineDTO.Id));
                    existingCuisine = Mapper.Map<CuisineDTO, Cuisine>(currentCuisineDTO, existingCuisine);
                    existingCuisine.Restaurant = currentRestaurant;
                    currentRestaurant.Cuisine.Add(existingCuisine);
                }
            }
            if (storeTimeItems != null)
            {
                currentRestaurant.StoreTime = new List<StoreTime>();
                foreach (StoreTimeDTO currentStoreTimeDTO in storeTimeItems)
                {
                    StoreTime existingStoreTime = this.context.StoreTimes.FirstOrDefault(j => (j.Id == currentStoreTimeDTO.Id));
                    existingStoreTime = Mapper.Map<StoreTimeDTO, StoreTime>(currentStoreTimeDTO, existingStoreTime);
                    existingStoreTime.Restaurant = currentRestaurant;
                    currentRestaurant.StoreTime.Add(existingStoreTime);
                }
            }
            if (menuItems != null)
            {
                currentRestaurant.Menu = new List<Menu>();
                foreach (MenuDTO currentMenuDTO in menuItems)
                {
                    Menu existingMenu = this.context.Menus.FirstOrDefault(j => (j.Id == currentMenuDTO.Id));
                    existingMenu = Mapper.Map<MenuDTO, Menu>(currentMenuDTO, existingMenu);
                    existingMenu.Restaurant = currentRestaurant;
                    currentRestaurant.Menu.Add(existingMenu);
                }
            }

            await this.context.SaveChangesAsync();

            var result = Mapper.Map<Restaurant, RestaurantDTO>(currentRestaurant);

            return result;


        }

        // POST tables/Restaurant
        public async Task<IHttpActionResult> PostRestaurant(RestaurantDTO item)
        {
            RestaurantDTO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Restaurant/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRestaurant(string id)
        {
             return DeleteAsync(id);
        }

    }
}